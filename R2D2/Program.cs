using R2D2;
using R2D2.Core.Configuration;
using R2D2.Core.Control;
using R2D2.Core.Network;
using R2D2.Core.PWM;
using R2D2.Core.Servos;
using R2D2.Core.Utilities;
using System.Runtime.InteropServices;

var isRaspberryPi = RuntimeInformation.RuntimeIdentifier.StartsWith("raspbian");


// CREATE PRE-BUILT CONFIGURATION TO USE CONFIG VALUES IN BOOTSTRAP PROCESS
var config = new ConfigurationBuilder()
	.AddJsonFile(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings", "webServer.json"), optional: false, reloadOnChange: true)
	.Build();
var webServerSettings = config.Get<WebServerSettings>();


var hostBuilder = Host.CreateDefaultBuilder(args)
	.ConfigureAppConfiguration(builder =>
	{
		builder.Sources.Clear();
		builder
			.AddJsonFile(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings", "r2d2.json"), optional: false, reloadOnChange: true)
			.AddJsonFile(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings", "webServer.json"), optional: false, reloadOnChange: true)
			.AddJsonFile(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings", "controllers.json"), optional: false, reloadOnChange: true);
	})
	.ConfigureServices((ctx, services) =>
	{
		services.AddSingleton(new CancellationTokenSource());
		services.Configure<R2D2Settings>(ctx.Configuration.GetSection("r2d2"));
		services.Configure<WebServerSettings>(ctx.Configuration.GetSection("webServer"));
		services.Configure<ControllerSettings>(ctx.Configuration.GetSection("controllers"));
		services.AddSingleton<IServoFactory, ServoFactory>();
		services.AddSingleton<IPwmControllerFactory, PwmControllerFactory>();
		services.AddSingleton<INetworkInterfaces, NetworkInterfaces>();
		services.AddSingleton<ISystemUtility, SystemUtility>();

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


var controller = new GamepadController("/dev/input/js0");
controller.AxisChanged += (Object? sender, AxisEventArgs e) => Console.WriteLine($"Axis: {e.Axis}, Value: {e.Value}");
controller.ButtonChanged += (Object? sender, ButtonEventArgs e) => Console.WriteLine($"Button: {e.Button}, Pressed: {e.Pressed}");




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



