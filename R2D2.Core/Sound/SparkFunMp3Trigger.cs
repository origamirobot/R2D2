using R2D2.Core.Control;
using R2D2.Core.Serial;
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
	public class SparkFunMp3Trigger : ISoundTrigger
	{

		#region PROTECTED PROPERTIES


		/// <summary>
		/// Gets the serial port.
		/// </summary>
		protected ISerialPort SerialPort { get; private set; }

		/// <summary>
		/// The cancellation source
		/// </summary>
		protected CancellationTokenSource CancellationSource = new CancellationTokenSource();


		#endregion PROTECTED PROPERTIES

		#region CONSTANTS


		private const String StartStopCommand = "O";
		private const String ForwardCommand = "F";
		private const String ReverseCommand = "R";
		private const String TriggerAsciiCommand = "T";
		private const String TriggerBinaryCommand = "t";
		private const String PlayCommand = "p";
		private const String SetVolumeCommand = "v";
		private const String RequestStatusCommand = "S";


		#endregion CONSTANTS

		#region CONSTRUCTORS


		/// <summary>
		/// Initializes a new instance of the <see cref="SparkFunMp3Trigger"/> class.
		/// </summary>
		/// <param name="serialPort">The serial port.</param>
		public SparkFunMp3Trigger(ISerialPort serialPort)
		{
			SerialPort = serialPort;
			SerialPort.Open();
			SerialPort.ErrorReceived += SerialPort_ErrorReceived;
			SerialPort.DataReceived += SerialPort_DataReceived;
			Task.Factory.StartNew(() => ProcessMessages(CancellationSource.Token));
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
			var message = new Byte[1];
			while (!cancellationToken.IsCancellationRequested)
			{
				SerialPort.Read(message, 0, 1);
				var text = Encoding.ASCII.GetString(message);
				Console.WriteLine("Read: " + message);
			}
		}


		#endregion PROTECTED METHODS

		#region PUBLIC METHODS


		/// <summary>
		/// This command performs the same function as pushing the on-board nav switch center position. If the
		/// current track is playing, it stops.If the current track is stopped, it will restart from the beginning
		/// </summary>
		public void PlayPause()
		{
			var payload = Encoding.ASCII.GetBytes(PlayCommand);
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

		/// <summary>
		/// If it exists, the track with the filename “TRACKNNN.MP3” will be started, where NNN is the ASCII
		/// equivalent of the data byte n.
		/// </summary>
		public void Trigger(Int32 trackIndex)
		{
			if (trackIndex < 0 || trackIndex > 255)
				throw new ArgumentOutOfRangeException(nameof(trackIndex), "Must me a positive number between 0 and 255");

			var command = Encoding.ASCII.GetBytes(TriggerBinaryCommand);
			var payload = command.Concat(new[] { (Byte)trackIndex }).ToArray();
			SerialPort.Write(payload, 0, 2);
		}


		#endregion PUBLIC METHODS

	}

}
