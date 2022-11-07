namespace R2D2.Core.Configuration
{

	/// <summary>
	/// Represents settings needed for running a web server.
	/// </summary>
	public class WebServerSettings
	{

		/// <summary>
		/// Gets or sets a value indicating whether to enable R2D2's web server.
		/// </summary>
		public Boolean Enabled { get; set; }

		/// <summary>
		/// Gets or sets the port this web server should run on.
		/// </summary>
		public Int32 Port { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to use HTTPS for web traffic.
		/// </summary>
		public Boolean useHttps { get; set; }

		/// <summary>
		/// Gets or sets the location of the SSL certificate to use (if any).
		/// </summary>
		public String? SslCertificate { get; set; }

	}

}
