using System.Device.Gpio;

namespace R2D2.Core.GPIO
{

	/// <summary>
	/// Represents a <see cref="GpioController"/> that doesn't control anything. 
	/// </summary>
	/// <seealso cref="R2D2.Core.GPIO.IGpioController" />
	public class NullGpioController : IGpioController
	{

		#region PUBLIC ACCESSORS


		/// <summary>
		/// Gets the numbering scheme.
		/// </summary>
		public PinNumberingScheme NumberingScheme => PinNumberingScheme.Board;

		/// <summary>
		/// Gets the number of pins this controller can control.
		/// </summary>
		public Int32 PinCount => 0;


		#endregion PUBLIC ACCESSORS

		#region PUBLIC METHODS


		/// <summary>
		/// Closes the specified pin.
		/// </summary>
		/// <param name="pinNumber">The pin number to close.</param>
		public void ClosePin(Int32 pinNumber) { }

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose() { }

		/// <summary>
		/// Gets the mode for an already open pin.
		/// </summary>
		/// <param name="pinNumber">The number of the pin to get the mode for.</param>
		/// <returns></returns>
		public PinMode GetPinMode(Int32 pinNumber) 
			=> PinMode.Input;

		public Boolean IsPinModeSupported(Int32 pinNumber, PinMode mode)
			=> false;

		/// <summary>
		/// Determines whether the specified pin is already open.
		/// </summary>
		/// <param name="pinNumber">The number of the pin to check.</param>
		/// <returns></returns>
		public Boolean IsPinOpen(Int32 pinNumber)
			=> false;

		/// <summary>
		/// Opens the specified pin for use.
		/// </summary>
		/// <param name="pinNumber">The pin number.</param>
		public void OpenPin(Int32 pinNumber) { }

		/// <summary>
		/// Opens the specified pin using the provided <see cref="PinMode" />
		/// </summary>
		/// <param name="pinNumber">The pin number to set.</param>
		/// <param name="mode">The mode to set for this pin after it is opened.</param>
		public void OpenPin(Int32 pinNumber, PinMode mode) { }

		/// <summary>
		/// Reads the value from the specified pin.
		/// </summary>
		/// <param name="pinNumber">The pin number to read from.</param>
		/// <returns></returns>
		public PinValue Read(Int32 pinNumber) 
			=> PinValue.Low;

		/// <summary>
		/// Reads all of the values from the specified pins
		/// </summary>
		/// <param name="valuePairs">The value pairs.</param>
		public void Read(Span<PinValuePair> valuePairs) { }


		/// <summary>
		/// Takes an already open pin and sets it's mode.
		/// </summary>
		/// <param name="pinNumber">The number of the pin to set.</param>
		/// <param name="mode">The mode to set for this open pin.</param>
		public void SetPinMode(Int32 pinNumber, PinMode mode) { }


		/// <summary>
		/// Writes the value to the specified pin.
		/// </summary>
		/// <param name="pinNumber">The pin to write to.</param>
		/// <param name="value">The value to write.</param>
		public void Write(Int32 pinNumber, PinValue value) { }

		/// <summary>
		/// Writes the values to the specified pins.
		/// </summary>
		/// <param name="pinValues">The pins.</param>
		/// <exception cref="System.NotImplementedException"></exception>
		public void Write(ReadOnlySpan<PinValuePair> pinValues) { }


		#endregion PUBLIC METHODS

		#region EVENT METHODS


		/// <summary>
		/// Registers the callback for pin value changed event.
		/// </summary>
		/// <param name="pinNumber">The pin number.</param>
		/// <param name="eventTypes">The event types.</param>
		/// <param name="callback">The callback.</param>
		public void RegisterCallbackForPinValueChangedEvent(Int32 pinNumber, PinEventTypes eventTypes, PinChangeEventHandler callback) { }

		/// <summary>
		/// Unregisters the callback for pin value changed event.
		/// </summary>
		/// <param name="pinNumber">The pin number.</param>
		/// <param name="callback">The <see cref="PinValueChangedEventArgs" /> instance containing the event data.</param>
		public void UnregisterCallbackForPinValueChangedEvent(Int32 pinNumber, PinChangeEventHandler callback) { }

		/// <summary>
		/// Waits for the specified event to occur.
		/// </summary>
		/// <param name="pinNumber">The pin to watch.</param>
		/// <param name="eventTypes">The event types to watch for.</param>
		/// <param name="timeout">The amount of time to wait before timing out.</param>
		/// <returns></returns>
		public WaitForEventResult WaitForEvent(Int32 pinNumber, PinEventTypes eventTypes, TimeSpan timeout) 
			=> new WaitForEventResult();

		/// <summary>
		/// Waits for the specified event to occur.
		/// </summary>
		/// <param name="pinNumber">The pin to watch.</param>
		/// <param name="eventTypes">The event types to watch for.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns></returns>
		public WaitForEventResult WaitForEvent(Int32 pinNumber, PinEventTypes eventTypes, CancellationToken cancellationToken)
			=> new WaitForEventResult();

		/// <summary>
		/// Waits for the specified event to occur.
		/// </summary>
		/// <param name="pinNumber">The pin to watch.</param>
		/// <param name="eventTypes">The event types to watch for.</param>
		/// <param name="timeout">The amount of time to wait before timing out.</param>
		/// <returns></returns>
		public async ValueTask<WaitForEventResult> WaitForEventAsync(Int32 pinNumber, PinEventTypes eventTypes, TimeSpan timeout)
		{
			await Task.Yield();
			return new WaitForEventResult();
		}

		/// <summary>
		/// Waits for the specified event to occur.
		/// </summary>
		/// <param name="pinNumber">The pin to watch.</param>
		/// <param name="eventTypes">The event types to watch for.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public async ValueTask<WaitForEventResult> WaitForEventAsync(Int32 pinNumber, PinEventTypes eventTypes, CancellationToken cancellationToken)
		{
			await Task.Yield();
			return new WaitForEventResult();
		}


		#endregion EVENT METHODS

	}

}
