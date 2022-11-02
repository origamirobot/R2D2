using System.Device.Gpio;

namespace R2D2.Core.GPIO
{

	/// <summary>
	/// Defines a contract that all GPIO controllers must implement. Handles the manipulation of General 
	/// Purpose Input/Output pins on a micro controller board.
	/// </summary>
	public interface IGpioController : IDisposable
	{

		#region PROPERTIES


		/// <summary>
		/// Gets the numbering scheme.
		/// </summary>
		public PinNumberingScheme NumberingScheme { get; }

		/// <summary>
		/// Gets the number of pins this controller can control.
		/// </summary>
		public Int32 PinCount { get; }


		#endregion PROPERTIES

		#region METHODS


		/// <summary>
		/// Opens the specified pin for use.
		/// </summary>
		/// <param name="pinNumber">The pin number.</param>
		void OpenPin(Int32 pinNumber);

		/// <summary>
		/// Opens the specified pin using the provided <see cref="PinMode"/>
		/// </summary>
		/// <param name="pinNumber">The pin number to set.</param>
		/// <param name="mode">The mode to set for this pin after it is opened.</param>
		void OpenPin(Int32 pinNumber, PinMode mode);

		/// <summary>
		/// Closes the specified pin.
		/// </summary>
		/// <param name="pinNumber">The pin number to close.</param>
		void ClosePin(Int32 pinNumber);

		/// <summary>
		/// Takes an already open pin and sets it's mode.
		/// </summary>
		/// <param name="pinNumber">The number of the pin to set.</param>
		/// <param name="mode">The mode to set for this open pin.</param>
		void SetPinMode(Int32 pinNumber, PinMode mode);

		/// <summary>
		/// Gets the mode for an already open pin.
		/// </summary>
		/// <param name="pinNumber">The number of the pin to get the mode for.</param>
		/// <returns></returns>
		PinMode GetPinMode(Int32 pinNumber);

		/// <summary>
		/// Determines whether the specified pin is already open.
		/// </summary>
		/// <param name="pinNumber">The number of the pin to check.</param>
		Boolean IsPinOpen(Int32 pinNumber);

		/// <summary>
		/// Determines whether the pin mode is supported on the specified pin.
		/// </summary>
		/// <param name="pinNumber">The pin number to check.</param>
		/// <param name="mode">The mode to check.</param>
		Boolean IsPinModeSupported(Int32 pinNumber, PinMode mode);

		/// <summary>
		/// Reads the value from the specified pin.
		/// </summary>
		/// <param name="pinNumber">The pin number to read from.</param>
		/// <returns></returns>
		PinValue Read(Int32 pinNumber);

		/// <summary>
		/// Writes the value to the specified pin.
		/// </summary>
		/// <param name="pinNumber">The pin to write to.</param>
		/// <param name="value">The value to write.</param>
		void Write(Int32 pinNumber, PinValue value);

		/// <summary>
		/// Writes the values to the specified pins.
		/// </summary>
		/// <param name="pinValues">The pins.</param>
		void Write(ReadOnlySpan<PinValuePair> pinValues);

		/// <summary>
		/// Reads all of the values from the specified pins
		/// </summary>
		/// <param name="valuePairs">The value pairs.</param>
		void Read(Span<PinValuePair> valuePairs);


		#endregion METHODS

		#region EVENTS


		/// <summary>
		/// Waits for the specified event to occur.
		/// </summary>
		/// <param name="pinNumber">The pin to watch.</param>
		/// <param name="eventTypes">The event types to watch for.</param>
		/// <param name="timeout">The amount of time to wait before timing out.</param>
		/// <returns></returns>
		WaitForEventResult WaitForEvent(Int32 pinNumber, PinEventTypes eventTypes, TimeSpan timeout);

		/// <summary>
		/// Waits for the specified event to occur.
		/// </summary>
		/// <param name="pinNumber">The pin to watch.</param>
		/// <param name="eventTypes">The event types to watch for.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns></returns>
		WaitForEventResult WaitForEvent(Int32 pinNumber, PinEventTypes eventTypes, CancellationToken cancellationToken);

		/// <summary>
		/// Waits for the specified event to occur.
		/// </summary>
		/// <param name="pinNumber">The pin to watch.</param>
		/// <param name="eventTypes">The event types to watch for.</param>
		/// <param name="timeout">The amount of time to wait before timing out.</param>
		/// <returns></returns>
		ValueTask<WaitForEventResult> WaitForEventAsync(Int32 pinNumber, PinEventTypes eventTypes, TimeSpan timeout);

		/// <summary>
		/// Waits for the specified event to occur.
		/// </summary>
		/// <param name="pinNumber">The pin to watch.</param>
		/// <param name="eventTypes">The event types to watch for.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns></returns>
		ValueTask<WaitForEventResult> WaitForEventAsync(Int32 pinNumber, PinEventTypes eventTypes, CancellationToken cancellationToken);

		/// <summary>
		/// Registers the callback for pin value changed event.
		/// </summary>
		/// <param name="pinNumber">The pin number.</param>
		/// <param name="eventTypes">The event types.</param>
		/// <param name="callback">The callback.</param>
		void RegisterCallbackForPinValueChangedEvent(Int32 pinNumber, PinEventTypes eventTypes, PinChangeEventHandler callback);

		/// <summary>
		/// Unregisters the callback for pin value changed event.
		/// </summary>
		/// <param name="pinNumber">The pin number.</param>
		/// <param name="callback">The <see cref="PinValueChangedEventArgs"/> instance containing the event data.</param>
		void UnregisterCallbackForPinValueChangedEvent(Int32 pinNumber, PinChangeEventHandler callback);


		#endregion EVENTS

	}

}
