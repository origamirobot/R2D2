using Microsoft.Extensions.Logging;
using System.Device.Gpio;

namespace R2D2.Core.GPIO
{

	/// <summary>
	/// Wrapper around the <see cref="GpioController"/> class.
	/// </summary>
	/// <seealso cref="R2D2.Core.GPIO.IGpioController" />
	public class DefaultGpioController : IGpioController
	{

		#region PROTECTED PROPERTIES


		protected GpioController Controller { get; private set; }
		protected ILogger<DefaultGpioController> Logger { get; private set; }


		#endregion PROTECTED PROPERTIES

		#region PUBLIC ACCESSORS


		/// <summary>
		/// Gets the numbering scheme.
		/// </summary>
		public PinNumberingScheme NumberingScheme => Controller.NumberingScheme;

		/// <summary>
		/// Gets the number of pins this controller can control.
		/// </summary>
		public Int32 PinCount => Controller.PinCount;


		#endregion PUBLIC ACCESSORS

		#region CONSTRUCTORS


		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultGpioController"/> class.
		/// </summary>
		/// <param name="numberingScheme">The numbering scheme.</param>
		public DefaultGpioController(PinNumberingScheme numberingScheme, ILogger<DefaultGpioController> logger)
			: this(new GpioController(numberingScheme), logger) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultGpioController"/> class.
		/// </summary>
		/// <param name="controller">The controller.</param>
		public DefaultGpioController(GpioController controller, ILogger<DefaultGpioController> logger)
		{
			Controller = controller;
			Logger = logger;
		}


		#endregion CONSTRUCTORS

		#region PUBLIC METHODS


		/// <summary>
		/// Closes the specified pin.
		/// </summary>
		/// <param name="pinNumber">The pin number to close.</param>
		public void ClosePin(Int32 pinNumber) 
			=> Controller.ClosePin(pinNumber);

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
			=> Controller.Dispose();

		/// <summary>
		/// Gets the mode for an already open pin.
		/// </summary>
		/// <param name="pinNumber">The number of the pin to get the mode for.</param>
		/// <returns></returns>
		public PinMode GetPinMode(Int32 pinNumber)
			=> Controller.GetPinMode(pinNumber);

		/// <summary>
		/// Determines whether the pin mode is supported on the specified pin.
		/// </summary>
		/// <param name="pinNumber">The pin number to check.</param>
		/// <param name="mode">The mode to check.</param>
		/// <returns></returns>
		public bool IsPinModeSupported(Int32 pinNumber, PinMode mode)
			=> Controller.IsPinModeSupported(pinNumber, mode);

		/// <summary>
		/// Determines whether the specified pin is already open.
		/// </summary>
		/// <param name="pinNumber">The number of the pin to check.</param>
		/// <returns></returns>
		public bool IsPinOpen(Int32 pinNumber)
			=> Controller.IsPinOpen(pinNumber);

		/// <summary>
		/// Opens the specified pin for use.
		/// </summary>
		/// <param name="pinNumber">The pin number.</param>
		public void OpenPin(Int32 pinNumber)
			=> Controller.OpenPin(pinNumber);

		/// <summary>
		/// Opens the specified pin using the provided <see cref="PinMode" />
		/// </summary>
		/// <param name="pinNumber">The pin number to set.</param>
		/// <param name="mode">The mode to set for this pin after it is opened.</param>
		public void OpenPin(Int32 pinNumber, PinMode mode)
			=> Controller.OpenPin(pinNumber, mode);

		/// <summary>
		/// Reads the value from the specified pin.
		/// </summary>
		/// <param name="pinNumber">The pin number to read from.</param>
		/// <returns></returns>
		public PinValue Read(Int32 pinNumber)
			=> Controller.Read(pinNumber);

		/// <summary>
		/// Reads all of the values from the specified pins
		/// </summary>
		/// <param name="valuePairs">The value pairs.</param>
		public void Read(Span<PinValuePair> valuePairs)
			=> Controller.Read(valuePairs);

		/// <summary>
		/// Writes the value to the specified pin.
		/// </summary>
		/// <param name="pinNumber">The pin to write to.</param>
		/// <param name="value">The value to write.</param>
		public void Write(Int32 pinNumber, PinValue value)
			=> Controller.Write(pinNumber, value);

		/// <summary>
		/// Writes the values to the specified pins.
		/// </summary>
		/// <param name="pinValues">The pins.</param>
		public void Write(ReadOnlySpan<PinValuePair> pinValues)
			=> Controller.Write(pinValues);

		/// <summary>
		/// Takes an already open pin and sets it's mode.
		/// </summary>
		/// <param name="pinNumber">The number of the pin to set.</param>
		/// <param name="mode">The mode to set for this open pin.</param>
		public void SetPinMode(Int32 pinNumber, PinMode mode)
			=> Controller.SetPinMode(pinNumber, mode);


		#endregion PUBLIC METHODS

		#region EVENT METHODS


		/// <summary>
		/// Registers the callback for pin value changed event.
		/// </summary>
		/// <param name="pinNumber">The pin number.</param>
		/// <param name="eventTypes">The event types.</param>
		/// <param name="callback">The callback.</param>
		public void RegisterCallbackForPinValueChangedEvent(int pinNumber, PinEventTypes eventTypes, PinChangeEventHandler callback)
			=> Controller.RegisterCallbackForPinValueChangedEvent(pinNumber, eventTypes, callback);

		/// <summary>
		/// Unregisters the callback for pin value changed event.
		/// </summary>
		/// <param name="pinNumber">The pin number.</param>
		/// <param name="callback">The callback.</param>
		public void UnregisterCallbackForPinValueChangedEvent(int pinNumber, PinChangeEventHandler callback)
			=> Controller.UnregisterCallbackForPinValueChangedEvent(pinNumber, callback);

		/// <summary>
		/// Waits for the specified event to occur.
		/// </summary>
		/// <param name="pinNumber">The pin to watch.</param>
		/// <param name="eventTypes">The event types to watch for.</param>
		/// <param name="timeout">The amount of time to wait before timing out.</param>
		/// <returns></returns>
		public WaitForEventResult WaitForEvent(int pinNumber, PinEventTypes eventTypes, TimeSpan timeout)
			=> Controller.WaitForEvent(pinNumber, eventTypes, timeout);

		/// <summary>
		/// Waits for the specified event to occur.
		/// </summary>
		/// <param name="pinNumber">The pin to watch.</param>
		/// <param name="eventTypes">The event types to watch for.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns></returns>
		public WaitForEventResult WaitForEvent(int pinNumber, PinEventTypes eventTypes, CancellationToken cancellationToken)
			=> Controller.WaitForEvent(pinNumber, eventTypes, cancellationToken);

		/// <summary>
		/// Waits for the specified event to occur.
		/// </summary>
		/// <param name="pinNumber">The pin to watch.</param>
		/// <param name="eventTypes">The event types to watch for.</param>
		/// <param name="timeout">The amount of time to wait before timing out.</param>
		/// <returns></returns>
		public async ValueTask<WaitForEventResult> WaitForEventAsync(int pinNumber, PinEventTypes eventTypes, TimeSpan timeout)
			=> await Controller.WaitForEventAsync(pinNumber, eventTypes, timeout);

		/// <summary>
		/// Waits for the specified event to occur.
		/// </summary>
		/// <param name="pinNumber">The pin to watch.</param>
		/// <param name="eventTypes">The event types to watch for.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns></returns>
		public async ValueTask<WaitForEventResult> WaitForEventAsync(int pinNumber, PinEventTypes eventTypes, CancellationToken cancellationToken)
			=> await Controller.WaitForEventAsync(pinNumber, eventTypes, cancellationToken);


		#endregion EVENT METHODS

	}

}
