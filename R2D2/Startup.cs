namespace R2D2
{

	/// <summary>
	/// Contains code that executes at startup.
	/// </summary>
	public class Startup
	{

		/// <summary>
		/// Configures the services for this application.
		/// </summary>
		/// <param name="services">The services.</param>
		public void ConfigureServices(IServiceCollection services)
		{
			
		}

		/// <summary>
		/// Configures the specified application.
		/// </summary>
		/// <param name="app">The application.</param>
		/// <param name="env">The env.</param>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			app.UseRouting();
			app.UseEndpoints(x =>
			{
				// TODO: Figure out gRPC.

				x.MapGet("/", async context => {
					var banner = File.ReadAllText("Banner.txt");
					await context.Response.WriteAsync(banner);
				});
			});
		} 

	}

}
