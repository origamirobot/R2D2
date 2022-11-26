using Microsoft.Extensions.Logging;
using R2D2.Core.Control;
using R2D2.Core.Serial;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using UnitsNet;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace R2D2.Core.Sound
{

	/// <summary>
	/// 
	/// </summary>
	/// <seealso cref="R2D2.Core.Sound.ISoundTrigger" />
	public class SparkFunMp3Trigger : ISoundTrigger
	{

		#region PROTECTED PROPERTIES


		/// <summary>
		/// Gets the serial port.
		/// </summary>
		protected ISerialPort SerialPort { get; private set; }

		/// <summary>
		/// Gets the logger.
		/// </summary>
		protected ILogger Logger { get; private set; }

		/// <summary>
		/// The cancellation source
		/// </summary>
		protected CancellationTokenSource CancellationSource = new CancellationTokenSource();


		#endregion PROTECTED PROPERTIES

		#region PUBLIC ACCESSORS


		/// <summary>
		/// Gets the index of the currently selected file.
		/// </summary>
		public Int32 SelectedFileIndex { get; private set; }

		/// <summary>
		/// Gets the total number of tracks saved on this sound trigger device.
		/// </summary>
		public Int32 TotalTracks { get; private set; }

		/// <summary>
		/// Gets the name of the sound trigger device.
		/// </summary>
		public String DeviceName { get; private set; } = "Sparkfun MP3 Trigger (Uninitialized)";

		/// <summary>
		/// Gets a value indicating whether this instance is initialized.
		/// </summary>
		public Boolean IsInitialized{ get; private set; }


		#endregion PUBLIC ACCESSORS

		#region CONSTANTS


		private const String StartStopCommand = "O";
		private const String ForwardCommand = "F";
		private const String ReverseCommand = "R";
		private const String PlayCommand = "p";
		private const String SetVolumeCommand = "v";
		private const String RequestVersionCommand = "S0";
		private const String RequestStatusCommand = "S1";
		private const String QuietModeOnCommand = "Q1";
		private const String QuietModeOffCommand = "Q0";



		#endregion CONSTANTS

		#region CONSTRUCTORS


		/// <summary>
		/// Initializes a new instance of the <see cref="SparkFunMp3Trigger" /> class.
		/// </summary>
		/// <param name="serialPort">The serial port.</param>
		/// <param name="logger">The logger.</param>
		public SparkFunMp3Trigger(
			ISerialPort serialPort, 
			ILogger<SparkFunMp3Trigger> logger)
		{
			SerialPort = serialPort;
			Logger = logger;
		}


		#endregion CONSTRUCTORS

		#region EVENT HANDLERS


		private void SerialPort_ErrorReceived(Object sender, SerialErrorReceivedEventArgs e)
		{
			Console.WriteLine("Error: " + e.EventType);
		}

		private void SerialPort_DataReceived(Object sender, SerialDataReceivedEventArgs e)
		{
			Console.WriteLine("Received: " + e.EventType);
		}


		#endregion EVENT HANDLERS

		#region PROTECTED METHODS


		/// <summary>
		/// Processes the specified message from the device file.
		/// </summary>
		/// <param name="cancellationToken">The cancellation token.</param>
		protected virtual void ProcessMessages(CancellationToken cancellationToken)
		{
			var message = new Byte[2];
			while (!cancellationToken.IsCancellationRequested)
			{
				SerialPort.Read(message, 0, 2);
				var text = Encoding.ASCII.GetString(message);
				Console.WriteLine();
				Console.Write(text);
			}
		}

		/// <summary>
		/// Toggles quiet mode.
		/// </summary>
		/// <param name="on">if set to <c>true</c> [on].</param>
		protected void ToggleQuietMode(Boolean on)
		{
			var payload = Encoding.ASCII.GetBytes(on
				? QuietModeOnCommand
				: QuietModeOnCommand);

			SerialPort.Write(payload, 0, payload.Length);
		}


		#endregion PROTECTED METHODS

		#region PUBLIC METHODS


		/// <summary>
		/// Initializes this sound trigger.
		/// </summary>
		public void Initialize()
		{
			Console.WriteLine($"Opening serial port {SerialPort.PortName} for Sparkfun MP3 Trigger");
			SerialPort.Open();
			SerialPort.ErrorReceived += SerialPort_ErrorReceived;
			SerialPort.DataReceived += SerialPort_DataReceived;
			Task.Factory.StartNew(() => ProcessMessages(CancellationSource.Token));


			// GET VERSION INFORMATION FROM THE SPARKFUN MP3 TRIGGER
			//Console.WriteLine($"Retrieving Sparkfun MP3 Trigger version from serial port {SerialPort.PortName}");
			//var versionRequestPayload = Encoding.ASCII.GetBytes(RequestVersionCommand);
			//SerialPort.Write(versionRequestPayload, 0, versionRequestPayload.Length);
			//var versionResponseBuffer = new Byte[18];
			//SerialPort.Read(versionResponseBuffer, 0, versionResponseBuffer.Length);
			//DeviceName = Encoding.ASCII.GetString(versionResponseBuffer);
			//Console.WriteLine($"Received version: {DeviceName} from serial port {SerialPort.PortName}");

			// GET THE TOTAL NUMBER OF FILES STORED ON THE MP3 TRIGGER DEVICE
			//Console.WriteLine($"Retrieving sound trigger file count from serial port {SerialPort.PortName}");
			//var fileCountRequestPayload = Encoding.ASCII.GetBytes(RequestStatusCommand);
			//SerialPort.Write(fileCountRequestPayload, 0, fileCountRequestPayload.Length);
			//var fileCountResponseBuffer = new Byte[2];
			//SerialPort.Read(fileCountResponseBuffer, 0, fileCountResponseBuffer.Length);
			//var fileCount = Encoding.ASCII.GetString(fileCountResponseBuffer);
			//Logger.LogDebug($"Received file count: {FileCount} from serial port {SerialPort.PortName}");

		}

		/// <summary>
		/// Starts or stops the currently selected file.
		/// </summary>
		public void Toggle()
		{
			var payload = Encoding.ASCII.GetBytes(StartStopCommand);
			SerialPort.Write(payload, 0, payload.Length);
		}

		/// <summary>
		/// This command performs the same function as pushing the on-board nav switch right position. The next
		/// MP3 track in the directory will be started.
		/// </summary>
		public void Forward()
		{
			var payload = Encoding.ASCII.GetBytes(ForwardCommand);
			SerialPort.Write(payload, 0, payload.Length);
		}

		/// <summary>
		/// This command performs the same function as pushing the on-board nav switch left position. The
		/// previous MP3 track in the directory will be started.
		/// </summary>
		public void Reverse()
		{
			var payload = Encoding.ASCII.GetBytes(ReverseCommand);
			SerialPort.Write(payload, 0, payload.Length);
		}

		/// <summary>
		///  If it exists, the nth track in the directory will be played. The total number of available tracks in the
		/// directory can be retrieved using Status Request command below.
		/// </summary>
		public void Play(Int32 trackIndex)
		{
			var command = Encoding.ASCII.GetBytes(PlayCommand);
			var payload = command.Concat(new[] { (Byte)trackIndex }).ToArray();
			SerialPort.Write(payload, 0, 2);
			SelectedFileIndex = trackIndex;
		}

		/// <summary>
		/// The VS1053 volume will be set to the value n. Per the VS1053 datasheet, maximum volume is 0x00,
		/// and values much above 0x40 are too low to be audible.
		/// </summary>
		public void SetVolume(Int32 volume)
		{
			if (volume < 0 || volume > 255)
				throw new ArgumentOutOfRangeException(nameof(volume), "Must me a positive number between 0 and 255");



		}


		#endregion PUBLIC METHODS

	}

}
