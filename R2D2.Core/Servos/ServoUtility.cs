using Iot.Device.Pwm;
using Iot.Device.ServoMotor;
using R2D2.Core.PWM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace R2D2.Core.Servos
{
	public class ServoUtility
	{


		#region PROTECTED PROPERTIES


		protected IServoFactory ServoFactory { get; private set; }

		protected IPwmControllerFactory PwmControllerFactory { get; private set; }


		#endregion PROTECTED PROPERTIES

		#region CONSTRUCTORS


		/// <summary>
		/// Initializes a new instance of the <see cref="ServoUtility"/> class.
		/// </summary>
		/// <param name="servoFactory">The servo factory.</param>
		/// <param name="pwmControllerFactory">The PWM controller factory.</param>
		public ServoUtility(
			IServoFactory servoFactory,
			IPwmControllerFactory pwmControllerFactory)
		{
			ServoFactory = servoFactory;
			PwmControllerFactory = pwmControllerFactory;
		}


		#endregion CONSTRUCTORS

		#region PUBLIC METHODS


		public void Run()
		{
			Console.WriteLine("Running Servo Utility");
			var controller = PwmControllerFactory.CreateController("Default Controller", 1, 50, 64);
			var channel = controller.CreatePwmChannel(0);

			var servo = ServoFactory.Create(channel, new Configuration.ServoSettings() { MaxPulseWidth = 2500, MinPulseWidth = 500, MaxRotation = 180 });
			servo.Start();

			while (true)
			{
				var command = Console.ReadLine()?.ToLower()?.Split(' ');
				if (command?[0] is not { Length: > 0 })
					return;
				switch (command[0][0])
				{
					case 'q':
						controller.SetDutyCycleAllChannels(0.0);
						return;
					case 'f':
						var frequency = Double.Parse(command[1]);
						controller.PwmFrequency = frequency;
						Console.WriteLine($"PWM Frequency has been set to {frequency}Hz");
						break;
					case 'd':
						switch (command.Length)
						{
							case 2:
								var value = Double.Parse(command[1]);
								controller.SetDutyCycleAllChannels(value);
								Console.WriteLine($"PWM duty cycle has been set to {value}");
								break;
							case 3:
								var selectedChannel = Int32.Parse(command[1]);
								var channelValue = Double.Parse(command[2]);
								controller.SetDutyCycle(selectedChannel, channelValue);
								Console.WriteLine($"PWM duty cycle for channel {selectedChannel} has been set to {channelValue}");
								break;
						}
						break;
					case 'h':
						PrintHelp();
						break;
					case 't':
						var demoChannel = Int32.Parse(command[1]);
						ServoDemo(controller, demoChannel);
						PrintHelp();
						break;
				}

			}

		}


		#endregion PUBLIC METHODS

		#region PRIVATE METHODS


		private void ServoDemo(Pca9685 controller, Int32 channelIndex)
		{
			var channel = controller.CreatePwmChannel(channelIndex);
			var servo = ServoFactory.Create(channel, new Configuration.ServoSettings() { MaxRotation = 180, MaxPulseWidth = 2500, MinPulseWidth = 500 });
			PrintServoDemoHelp();

			while(true)
			{
				string? command = Console.ReadLine()?.ToLower();
				if (String.IsNullOrEmpty(command))
					return;

				switch(command[0])
				{
					case 'q':
						return;
					case 'c':
						CalibrateServo(servo);
						PrintServoDemoHelp();
						break;
					default:
						var value = double.Parse(command);
						servo.WriteAngle(value);
						Console.WriteLine($"Angle is set to {value}. PWM duty cycle = {controller.GetDutyCycle(channelIndex)}");
						break;
				}
			}
		}

		private void CalibrateServo(ServoMotor servo)
		{
			int maximumAngle = 180;
			int minimumPulseWidthMicroseconds = 520;
			int maximumPulseWidthMicroseconds = 2590;

			Console.WriteLine("Searching for minimum pulse width");
			CalibratePulseWidth(servo, ref minimumPulseWidthMicroseconds);
			Console.WriteLine();

			Console.WriteLine("Searching for maximum pulse width");
			CalibratePulseWidth(servo, ref maximumPulseWidthMicroseconds);

			Console.WriteLine("Searching for angle range");
			Console.WriteLine(
				"What is the angle range? (type integer with your angle range or enter to move to MIN/MAX)");

			while (true)
			{
				servo.WritePulseWidth(maximumPulseWidthMicroseconds);
				Console.WriteLine("Servo is now at MAX");
				if (int.TryParse(Console.ReadLine(), out maximumAngle))
				{
					break;
				}

				servo.WritePulseWidth(minimumPulseWidthMicroseconds);
				Console.WriteLine("Servo is now at MIN");

				if (int.TryParse(Console.ReadLine(), out maximumAngle))
				{
					break;
				}
			}

			servo.Calibrate(maximumAngle, minimumPulseWidthMicroseconds, maximumPulseWidthMicroseconds);
			Console.WriteLine($"Angle range: {maximumAngle}");
			Console.WriteLine($"Min PW [uS]: {minimumPulseWidthMicroseconds}");
			Console.WriteLine($"Max PW [uS]: {maximumPulseWidthMicroseconds}");
		}

		private void CalibratePulseWidth(ServoMotor servo, ref int pulseWidthMicroSeconds)
		{
			void SetPulseWidth(ref int pulseWidth)
			{
				pulseWidth = Math.Max(pulseWidth, 0);
				servo.WritePulseWidth(pulseWidth);
			}

			Console.WriteLine("Use A/Z (1x); S/X (10x); D/C (100x)");
			Console.WriteLine("Press enter to accept value");

			while (true)
			{
				SetPulseWidth(ref pulseWidthMicroSeconds);
				Console.WriteLine($"Current value: {pulseWidthMicroSeconds}");

				switch (Console.ReadKey().Key)
				{
					case ConsoleKey.A:
						pulseWidthMicroSeconds++;
						break;
					case ConsoleKey.Z:
						pulseWidthMicroSeconds--;
						break;
					case ConsoleKey.S:
						pulseWidthMicroSeconds += 10;
						break;
					case ConsoleKey.X:
						pulseWidthMicroSeconds -= 10;
						break;
					case ConsoleKey.D:
						pulseWidthMicroSeconds += 100;
						break;
					case ConsoleKey.C:
						pulseWidthMicroSeconds -= 100;
						break;
					case ConsoleKey.Enter:
						return;
				}
			}
		}


		private void PrintHelp()
		{
			Console.WriteLine("Command:");
			Console.WriteLine("    F {freq_hz}          set PWM frequency (Hz)");
			Console.WriteLine("    D {value}            set duty cycle (0.0 .. 1.0) for all channels");
			Console.WriteLine("    S {channel} {value}  set duty cycle (0.0 .. 1.0) for specific channel");
			Console.WriteLine("    T {channel}          servo test (defaults to SG90 params)");
			Console.WriteLine("    H                    prints help");
			Console.WriteLine("    Q                    quit");
			Console.WriteLine();
		}

		private void PrintServoDemoHelp()
		{
			Console.WriteLine("Q                   return to previous menu");
			Console.WriteLine("C                   calibrate");
			Console.WriteLine("{angle}             set angle (0 - 180)");
			Console.WriteLine();
		}


		#endregion PRIVATE METHODS

	}

}