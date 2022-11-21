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

var isRaspberryPi = RuntimeInformation.RuntimeIdentifier.StartsWith("raspbian");


// CREATE PRE-BUILT CONFIGURATION TO USE CONFIG VALUES IN BOOTSTRAP PROCESS
var config = new ConfigurationBuilder()
	.AddJsonFile(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings", "sound.json"), optional: false, reloadOnChange: true)
	.AddJsonFile(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings", "webServer.json"), optional: false, reloadOnChange: true)
	.Build();
var webServerSettings = config.Get<WebServerSettings>();


var hostBuilder = Host.CreateDefaultBuilder(args)
	.ConfigureAppConfiguration(builder =>
	{
		builder.Sources.Clear();
		builder
			.AddJsonFile(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings", "r2d2.json"), optional: false, reloadOnChange: true)
			.AddJsonFile(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings", "sound.json"), optional: false, reloadOnChange: true)
			.AddJsonFile(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings", "webServer.json"), optional: false, reloadOnChange: true)
			.AddJsonFile(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings", "controllers.json"), optional: false, reloadOnChange: true);
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
			return new SparkFunMp3Trigger(serialPort);
		});

	})
	.ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());


if(isRaspberryPi)
{
	// USE KESTREL WEBSERVER IF RUNNING ON RASPBERRY PI
	hostBuilder = hostBuilder
		.ConfigureWebHost(host => host.UseKestrel(options => options.Listen(
			System.Net.IPAddress.Any,
			webServerSettings.Port,
			listenOptions =>
			{
				if (webServerSettings.useHttps && !String.IsNullOrEmpty(webServerSettings.SslCertificate))
					listenOptions.UseHttps(webServerSettings.SslCertificate);
			})));
}

using var host = hostBuilder.Build();
Banner.Show();
Console.WriteLine("------------------------------------------------");




Console.WriteLine("Sound Trigger Initializing");
var soundTrigger = host.Services.GetService<ISoundTrigger>();




Console.WriteLine("Playing Track 1");
soundTrigger.Play(1);

Thread.Sleep(1000);
Console.WriteLine("Forward");
soundTrigger.Forward();
Console.WriteLine("Play/Pause");
soundTrigger.PlayPause();
Thread.Sleep(1000);
Console.WriteLine("Forward");
soundTrigger.Forward();
Console.WriteLine("Play/Pause");
soundTrigger.PlayPause();
Thread.Sleep(1000);
Console.WriteLine("Forward");
soundTrigger.Forward();
Console.WriteLine("Play/Pause");
soundTrigger.PlayPause();
Thread.Sleep(1000);
Console.WriteLine("Playing Track 2");
soundTrigger.Play(2);
Thread.Sleep(1000);
Console.WriteLine("Triggering Track 1");
soundTrigger.Trigger(1);





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





if (webServerSettings.Enabled)
{
	Console.WriteLine("Starting Web Server");
	await host.RunAsync(host.Services.GetRequiredService<CancellationTokenSource>().Token);
}
else
{
	Console.WriteLine("Web Server disabled in settings");
}

Console.ReadLine();
Console.WriteLine("Exiting R2D2 Control System");



