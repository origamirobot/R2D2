using R2D2;
using System.Runtime.InteropServices;
using Unosquare.RaspberryIO;
using Unosquare.WiringPi;

var isRaspberryPi = RuntimeInformation.RuntimeIdentifier.StartsWith("raspbian");

var hostBuilder = Host.CreateDefaultBuilder(args)
	.ConfigureAppConfiguration(builder =>
		builder
			.AddJsonFile(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json"), optional: false, reloadOnChange: true)
			.AddJsonFile(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "r2d2.json"), optional: true, reloadOnChange: true))
	.ConfigureServices((ctx, services) =>
	{
		services.AddSingleton(x =>
		{
			//if (!isRaspberryPi)
			//	return new FakeGpioController();
			Pi.Init<BootstrapWiringPi>();
			return Pi.Gpio;
		});
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
await host.RunAsync(host.Services.GetRequiredService<CancellationTokenSource>().Token);
Console.WriteLine("Exiting R2D2 Control System");

