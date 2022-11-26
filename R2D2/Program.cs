using Iot.Device.Pwm;
using Iot.Device.ServoMotor;
using R2D2;
using R2D2.Core.Configuration;
using R2D2.Core.Control;
using R2D2.Core.Network;
using R2D2.Core.PWM;
using R2D2.Core.Servos;
using R2D2.Core.Utilities;
using R2D2.Core.Extensions;
using System.Device.I2c;
using System.Device.Pwm;
using System.Runtime.InteropServices;
using R2D2.Core.Serial;
using R2D2.Core.Sound;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using R2D2.Core.Exceptions;
using R2D2.Core.IO;
using Serilog;

var isRaspberryPi = RuntimeInformation.RuntimeIdentifier.StartsWith("raspbian");


// CREATE LOGGERS
var logger = LoggerFactory.Create(config =>
{
	config.AddConsole();
}).CreateLogger("Program");










// CREATE PRE-BUILT CONFIGURATION TO USE CONFIG VALUES IN BOOTSTRAP PROCESS
IConfigurationRoot? config = null;
WebServerSettings? webServerSettings = null;

var hostBuilder = Host.CreateDefaultBuilder(args)
	.ConfigureAppConfiguration(builder =>
	{
		builder.Sources.Clear();
		builder
			.AddJsonFile(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings", "r2d2.json"), optional: false, reloadOnChange: true)
			.AddJsonFile(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings", "sound.json"), optional: false, reloadOnChange: true)
			.AddJsonFile(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings", "webServer.json"), optional: false, reloadOnChange: true)
			.AddJsonFile(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings", "controllers.json"), optional: false, reloadOnChange: true);
		config = builder.Build();
	})
	.ConfigureServices((ctx, services) =>
	{
		services.AddSingleton(new CancellationTokenSource());
		services.Configure<R2D2Settings>(ctx.Configuration.GetSection("r2d2"));
		services.Configure<WebServerSettings>(ctx.Configuration.GetSection("webServer"));
		services.Configure<SoundSettings>(ctx.Configuration.GetSection("sound"));
		services.Configure<ControllerSettings>(ctx.Configuration.GetSection("controllers"));
		services.AddSingleton<IServoFactory, ServoFactory>();
		services.AddSingleton<IPwmControllerFactory, PwmControllerFactory>();
		services.AddSingleton<INetworkInterfaces, NetworkInterfaces>();
		services.AddSingleton<ISystemUtility, SystemUtility>();
		services.AddSingleton<IFileUtility, FileUtility>();
		services.AddTransient<ISerialPort, SerialPortWrapper>();
		services.AddSingleton<ISoundTrigger>((serviceProvider) =>
		{
			var soundSettings = serviceProvider.GetService<IOptions<SoundSettings>>();
			if (soundSettings == null)
				throw new ConfigurationException();
			var serialPort = new SerialPortWrapper(
				logger: serviceProvider.GetRequiredService<ILogger<SerialPortWrapper>>(),
				comPortName: soundSettings.Value.DeviceFile,
				baudRate: soundSettings.Value.BaudRate,
				bitCount: soundSettings.Value.BitCount,
				parity: soundSettings.Value.Parity,
				stopBits: soundSettings.Value.StopBits) {
				ReadTimeout = soundSettings.Value.ReadTimeout,
				WriteTimeout =soundSettings.Value.WriteTimeout
			};
			return new SparkFunMp3Trigger(serialPort, serviceProvider.GetService<ILogger<SparkFunMp3Trigger>>());
		});

	})
	.ConfigureLogging(builder =>
	{
		builder.AddFilter("Microsoft", LogLevel.Warning).AddFilter("System", LogLevel.Warning);
		Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();
 		//builder.AddSerilog();

		builder.AddConsole();
	})
	.ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());


//if(isRaspberryPi)
//{
//	// USE KESTREL WEBSERVER IF RUNNING ON RASPBERRY PI
//	webServerSettings = config.Get<WebServerSettings>();
//	hostBuilder = hostBuilder
//		.ConfigureWebHost(host => host.UseKestrel(options => options.Listen(
//			System.Net.IPAddress.Any,
//			webServerSettings.Port,
//			listenOptions =>
//			{
//				if (webServerSettings.useHttps && !String.IsNullOrEmpty(webServerSettings.SslCertificate))
//					listenOptions.UseHttps(webServerSettings.SslCertificate);
//			})));
//}

using var host = hostBuilder.Build();
Banner.Show();
Console.WriteLine("------------------------------------------------");




var soundTrigger = host.Services.GetService<ISoundTrigger>();
soundTrigger.Initialize();

var controller = new GamepadController("/dev/input/js0");
controller.ButtonChanged += (Object? sender, ButtonEventArgs e) => 
{
	if (!e.Pressed) return;
	soundTrigger.Play(e.Button);
};



//Console.WriteLine("Playing Track 1");
//soundTrigger.Play(1);

//Thread.Sleep(1000);
//Console.WriteLine("Forward");
//soundTrigger.Forward();
//Console.WriteLine("Play/Pause");
//soundTrigger.PlayPause();
//Thread.Sleep(1000);
//Console.WriteLine("Forward");
//soundTrigger.Forward();
//Console.WriteLine("Play/Pause");
//soundTrigger.PlayPause();
//Thread.Sleep(1000);
//Console.WriteLine("Forward");
//soundTrigger.Forward();
//Console.WriteLine("Play/Pause");
//soundTrigger.PlayPause();
//Thread.Sleep(1000);
//Console.WriteLine("Playing Track 2");
//soundTrigger.Play(2);
//Thread.Sleep(1000);
//Console.WriteLine("Triggering Track 1");
//soundTrigger.Trigger(1);





//var settings = new I2cConnectionSettings(1, 64);
//var device = I2cDevice.Create(settings);
//ServoMotor motor = null;
//using (var pca9685 = new Pca9685(device, pwmFrequency: 50))
//{
//	pca9685.SetDutyCycleAllChannels(1);
//	PwmChannel firstChannel = pca9685.CreatePwmChannel(0); // channel 0

//	motor = new ServoMotor(firstChannel, 180, minimumPulseWidthMicroseconds: 500, maximumPulseWidthMicroseconds: 2500);
//	motor.Start();
//	motor.WriteAngle(0);
//	motor.WritePulseWidth(90);
//	motor.WritePulseWidth(180);


//	var controller = new GamepadController("/dev/input/js0");
//	controller.AxisChanged += (Object? sender, AxisEventArgs e) =>
//	{
//		Console.WriteLine($"Axis: {e.Axis} Value: {e.Value}");
//		var val = e.Value.Normalize(32767, -32767, 180, 0);
//		motor.WriteAngle(val);
//	};
//	controller.ButtonChanged += (Object? sender, ButtonEventArgs e) => Console.WriteLine($"Button: {e.Button}, Pressed: {e.Pressed}");

//	Console.ReadLine();
//	motor.Stop();
//	Console.WriteLine("Exiting R2D2 Control System");

//}

Console.ReadLine();





//if (webServerSettings != null && webServerSettings.Enabled)
//{
//	Console.WriteLine("Starting Web Server");
//	await host.RunAsync(host.Services.GetRequiredService<CancellationTokenSource>().Token);
//}
//else
//{
//	Console.WriteLine("Web Server disabled in settings");
//}

Console.ReadLine();
Console.WriteLine("Exiting R2D2 Control System");



