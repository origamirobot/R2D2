using R2D2.Core.IO;

namespace R2D2.Core.Control
{

	/// <summary>
	/// Generic Linux game controller based off a /dev/input/{joystick} device file.
	/// </summary>
	public class GamepadController : IDisposable
	{

		#region PROTECTED PROPERTIES


		/// <summary>
		/// Gets the cancellation source.
		/// </summary>
		protected CancellationTokenSource CancellationSource { get; private set; }

		/// <summary>
		/// Gets the file stream.
		/// </summary>
		protected IFileStream FileStream { get; private set; }


		#endregion PROTECTED PROPERTIES

		#region PUBLIC ACCESSORS


		/// <summary>
		/// Gets all of the buttons on this gamepad along with their current states.
		/// </summary>
		public Dictionary<Byte, Boolean> Buttons { get; private set; } = new Dictionary<Byte, Boolean>();

		/// <summary>
		/// Gets all of the axis and their current values.
		/// </summary>
		public Dictionary<Byte, Int16> Axis { get; private set; } = new Dictionary<Byte, Int16>();


		#endregion PUBLIC ACCESSORS

		#region PUBLIC EVENTS


		/// <summary>
		/// Occurs when an axis is changed on this controller.
		/// </summary>
		public event EventHandler<AxisEventArgs>? AxisChanged;

		/// <summary>
		/// Occurs when a button state is changed on this controller.
		/// </summary>
		public event EventHandler<ButtonEventArgs>? ButtonChanged;


		#endregion PUBLIC EVENTS

		#region CONSTRUCTORS


		/// <summary>
		/// Initializes a new instance of the <see cref="GamepadController"/> class.
		/// </summary>
		/// <param name="deviceFile">The device file.</param>
		public GamepadController(String deviceFile = "/dev/input/js0")
			: this(new FileStreamWrapper(deviceFile, FileMode.Open)) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="GamepadController"/> class.
		/// </summary>
		/// <param name="fileStream">The device file file-stream.</param>
		public GamepadController(IFileStream fileStream)
		{
			CancellationSource = new CancellationTokenSource();
			FileStream = fileStream;
			Task.Factory.StartNew(() => ProcessMessages(CancellationSource.Token));
		}


		#endregion CONSTRUCTORS

		#region EVENT HANDLERS


		/// <summary>
		/// Called when an axis value is changed.
		/// </summary>
		/// <param name="axis">The axis that is changed.</param>
		/// <param name="value">The new value of this axis.</param>
		protected virtual void OnAxisChanged(Byte axis, Int16 value)
			=> AxisChanged?.Invoke(this, new AxisEventArgs(axis, value));

		/// <summary>
		/// Called when a button's pressed state changes.
		/// </summary>
		/// <param name="button">The button.</param>
		/// <param name="isPressed">if set to <c>true</c> [is pressed].</param>
		protected virtual void OnButtonChanged(Byte button, Boolean isPressed)
			=> ButtonChanged?.Invoke(this, new ButtonEventArgs(button, isPressed));


		#endregion EVENT HANDLERS

		#region PROTECTED METHODS


		/// <summary>
		/// Processes the specified message from the device file.
		/// </summary>
		/// <param name="cancellationToken">The cancellation token.</param>
		protected virtual void ProcessMessages(CancellationToken cancellationToken)
		{
			var message = new Byte[8];
			while(!cancellationToken.IsCancellationRequested)
			{
				// READ 8 BYTES AT A TIME FROM THE DEVICE FILE TO CHECK FOR STATE CHANGES IN THE CONTROLLER
				FileStream.Read(message, 0, 8);
				if (message.HasConfiguration())
					ProcessConfiguration(message);
				ProcessValues(message);
			}
		}

		/// <summary>
		/// Processes a configuration message.
		/// </summary>
		/// <param name="message">The message to process.</param>
		protected virtual void ProcessConfiguration(Byte[] message)
		{
			var key = message.GetAddress();
			if (message.IsButton())
			{
				if(!Buttons.ContainsKey(key))
				{
					Buttons.Add(key, false);
					return;
				}
			}
			else if (message.IsAxis())
			{
				if(!Axis.ContainsKey(key))
				{
					Axis.Add(key, 0);
					return;
				}
			}
		}

		/// <summary>
		/// Processes the values read from the device file.
		/// </summary>
		/// <param name="message">The message to process.</param>
		protected virtual void ProcessValues(Byte[] message)
		{
			if(message.IsButton())
			{
				var oldValue = Buttons[message.GetAddress()];
				var newValue = message.IsButtonPressed();
				if(newValue != oldValue)
				{
					Buttons[message.GetAddress()] = newValue;
					OnButtonChanged(message.GetAddress(), newValue);
				}
			}
			else if(message.IsAxis())
			{
				var oldValue = Axis[message.GetAddress()];
				var newValue = message.GetAxisValue();
				if(oldValue != newValue)
				{
					Axis[message.GetAddress()] = message.GetAxisValue();
					OnAxisChanged(message.GetAddress(), newValue);
				}
			}
		}


		#endregion PROTECTED METHODS

		#region PUBLIC METHODS


		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			FileStream.Dispose();
		}


		#endregion PUBLIC METHODS

	}

}
