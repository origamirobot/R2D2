using Iot.Device.FtCommon;
using Iot.Device.Pwm;
using Iot.Device.ServoMotor;
using R2D2;
using System.Device.Gpio;
using System.Device.I2c;
using System.Device.Pwm;
using System.Runtime.InteropServices;

var isRaspberryPi = RuntimeInformation.RuntimeIdentifier.StartsWith("raspbian");

var hostBuilder = Host.CreateDefaultBuilder(args)
	.ConfigureAppConfiguration(builder =>
		builder
			.AddJsonFile(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json"), optional: false, reloadOnChange: true)
			.AddJsonFile(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "r2d2.json"), optional: true, reloadOnChange: true))
	.ConfigureServices((ctx, services) =>
	{
		//services.AddSingleton(x =>
		//{
		//	//if (!isRaspberryPi)
		//	//	return new FakeGpioController();
		//	//Pi.Init<BootstrapWiringPi>();
		//	//return Pi.Gpio;
		//});
		services.AddSingleton(new CancellationTokenSource());
	})
	.ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());


if(isRaspberryPi)
{
	hostBuilder = hostBuilder
		.ConfigureWebHost(host => host.UseKestrel(options => options.Listen(
			System.Net.IPAddress.Any,
			5001,
			listenOptions => { if (System.IO.File.Exists("/raspberrypi.pfx")) listenOptions.UseHttps("/raspberrypi.pfx"); })));
}

using var host = hostBuilder.Build();
Banner.Show();
Console.WriteLine("------------------------------------------------");


var settings = new I2cConnectionSettings(1, 64);
var device = I2cDevice.Create(settings);
using (var pca9685 = new Pca9685(device, pwmFrequency: 50))
{
	pca9685.SetDutyCycleAllChannels(1);
	PwmChannel firstChannel = pca9685.CreatePwmChannel(0); // channel 0

	var motor = new ServoMotor(firstChannel, 180, minimumPulseWidthMicroseconds: 500, maximumPulseWidthMicroseconds: 2500);
	motor.Start();
	motor.WriteAngle(0);
	motor.WritePulseWidth(90);
	motor.WritePulseWidth(180);
	motor.Stop();
}
 



Console.WriteLine("Starting Web Server");
await host.RunAsync(host.Services.GetRequiredService<CancellationTokenSource>().Token);
Console.WriteLine("Exiting R2D2 Control System");



