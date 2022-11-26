using Iot.Device.Pwm;
using Iot.Device.ServoMotor;
using Microsoft.Extensions.Logging;
using R2D2.Core.Configuration;
using R2D2.Core.PWM;
using R2D2.Core.Servos;
using R2D2.Core.Sound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R2D2.Core
{

	/// <summary>
	/// Main controller for the R2D2 unit.
	/// </summary>
	public class R2D2Unit
	{

		#region PRIVATE PROPERTIES


		private readonly List<Pca9685> _pwmControllers = new List<Pca9685>();
		private readonly List<ServoMotor> _servoMotors = new List<ServoMotor>();
		private Boolean _isInitialized = false;


		#endregion PRIVATE PROPERTIES

		#region PROTECTED PROPERTIES


		protected ILogger<R2D2Unit> Logger { get; private set; }
		protected R2D2Settings Settings { get; private set; }
		protected IServoFactory ServoFactory { get; private set; }
		protected IPwmControllerFactory PwmControllerFactory { get; private set; }
		protected ISoundTrigger SoundTrigger { get; private set; }


		#endregion PROTECTED PROPERTIES

		#region CONSTRUCTORS


		/// <summary>
		/// Initializes a new instance of the <see cref="R2D2Unit"/> class.
		/// </summary>
		/// <param name="settings">The settings.</param>
		/// <param name="logger">The logger.</param>
		/// <param name="servoFactory">The servo factory.</param>
		/// <param name="pwmControllerFactory">The PWM controller factory.</param>
		/// <param name="soundTrigger">The sound trigger.</param>
		public R2D2Unit(
			R2D2Settings settings,
			ILogger<R2D2Unit> logger, 
			IServoFactory servoFactory, 
			IPwmControllerFactory pwmControllerFactory,
			ISoundTrigger soundTrigger)
		{
			Settings = settings;
			Logger = logger;
			ServoFactory = servoFactory;
			PwmControllerFactory = pwmControllerFactory;
			SoundTrigger = soundTrigger;
		}


		#endregion CONSTRUCTORS

		#region PUBLIC METHODS


		/// <summary>
		/// Initializes this R2D2 Unit.
		/// </summary>
		public void Initialize()
		{
			Logger.LogDebug("Initializing R2D2");
			if (_isInitialized)
			{
				Logger.LogDebug("R2D2 Unit already initialized");
				return;
			}

			// INITIALIZE ALL OF THE PWM CONTROLLERS
			Logger.LogDebug("Initializing PWM Controllers");
			foreach (var settings in Settings.PwmControllers)
			{
				var pwmController = PwmControllerFactory.CreateController(settings);
				_pwmControllers.Add(pwmController);

				Logger.LogDebug($"Initializing Servo Motors for Controller {settings.Name}");
				foreach (var servoSettings in Settings.Servos.Where(x => x.ControllerAddress == settings.Address))
				{

					var channel = pwmController.CreatePwmChannel(servoSettings.Channel);
					var servo = ServoFactory.Create(channel, servoSettings);
					_servoMotors.Add(servo);
				}

			}
			Logger.LogDebug($"{_pwmControllers.Count} PWM Controllers initialized");

			// INITIALIZE ALL OF THE SERVO MOTORS
			// TODO: Initialize servo motors
			Logger.LogDebug($"{_servoMotors.Count} Servo Motors initialized");

			// INITIALIZE SOUND TRIGGER
			SoundTrigger.Initialize();

		}


		#endregion PUBLIC METHODS


	}

}
