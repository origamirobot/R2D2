using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace R2D2.Core.Utilities
{

	/// <summary>
	/// Handles system level things like shutdowns and restarts.
	/// </summary>
	public interface ISystemUtility
	{

		/// <summary>
		/// Gets a value indicating whether this application is running on a Raspberry Pi.
		/// </summary>
		Boolean IsRaspberryPi { get; }

		/// <summary>
		/// Closes this application.
		/// </summary>
		/// <param name="exitCode">The exit code.</param>
		void CloseApplication(Int32 exitCode = 0);

		/// <summary>
		/// Shuts down the machine this application is running on.
		/// </summary>
		void ShutdownMachine();

		/// <summary>
		/// Restarts the machine this application is running on.
		/// </summary>
		void RestartMachine();

		/// <summary>
		/// Asynchronous task to close this application.
		/// </summary>
		Task CloseApplicationAsync(Int32 exitCode = 0);

		/// <summary>
		/// Asynchronous task to shutdown this machine
		/// </summary>
		Task ShutdownMachineAsync();

		/// <summary>
		/// Asynchronous task to restart this machine
		/// </summary>
		Task RestartMachineAsync();

		/// <summary>
		/// Gets the name of the machine this application is currently running on.
		/// </summary>
		/// <returns></returns>
		String GetMachineName();

		/// <summary>
		/// Gets the current directory.
		/// </summary>
		/// <returns></returns>
		String GetCurrentDirectory();

		/// <summary>
		/// Gets an environment variable from the environment settings.
		/// </summary>
		/// <param name="variableName">Name of the variable.</param>
		/// <returns></returns>
		String? GetEnvironmentVariable(String variableName);

	}

	/// <summary>
	/// Handles system level things like shutdowns and restarts.
	/// </summary>
	/// <seealso cref="ISystemUtility" />
	public class SystemUtility : ISystemUtility
	{

		#region PROTECTED PROPERTIES


		/// <summary>
		/// Gets the logger.
		/// </summary>
		protected ILogger Logger { get; private set; }


		#endregion PROTECTED PROPERTIES

		#region PUBLIC ACCESSORS


		/// <summary>
		/// Gets a value indicating whether this application is running on a Raspberry Pi.
		/// </summary>
		public Boolean IsRaspberryPi 
			=> RuntimeInformation.RuntimeIdentifier.StartsWith("raspbian");


		#endregion PUBLIC ACCESSORS

		#region CONSTRUCTORS


		/// <summary>
		/// Initializes a new instance of the <see cref="SystemUtility"/> class.
		/// </summary>
		/// <param name="logger">The logger.</param>
		public SystemUtility(ILogger<SystemUtility> logger)
		{
			Logger = logger;
		}


		#endregion CONSTRUCTORS

		#region PUBLIC METHODS


		/// <summary>
		/// Gets an environment variable from the environment settings.
		/// </summary>
		/// <param name="variableName">Name of the variable.</param>
		/// <returns></returns>
		public String? GetEnvironmentVariable(String variableName) 
			=> Environment.GetEnvironmentVariable(variableName);

		/// <summary>
		/// Closes this application.
		/// </summary>
		/// <param name="exitCode">The exit code.</param>
		public void CloseApplication(Int32 exitCode = 0)
		{
			Logger.LogDebug("Application exiting");
			Environment.Exit(0);
		}

		/// <summary>
		/// Shuts down this machine.
		/// </summary>
		public void ShutdownMachine()
		{
			Logger.LogDebug("System shutting down");
			if(IsRaspberryPi)
				System.Diagnostics.Process.Start("sudo shutdown", "now");
			else
				System.Diagnostics.Process.Start("shutdown.exe", "-s -t 0");
		}

		/// <summary>
		/// Restarts this machine.
		/// </summary>
		public void RestartMachine()
		{
			Logger.LogDebug("System restarting");
			if (IsRaspberryPi)
				System.Diagnostics.Process.Start("sudo shutdown", "-r now");
			else
				System.Diagnostics.Process.Start("shutdown.exe", "-r -t 0");
		}

		/// <summary>
		/// Closes this application asynchronously.
		/// </summary>
		/// <param name="exitCode">The exit code.</param>
		/// <returns></returns>
		public async Task CloseApplicationAsync(Int32 exitCode = 0)
		{
			await Task.Run(() => CloseApplication(exitCode));
		}

		/// <summary>
		/// Shutdowns this machine asynchronously.
		/// </summary>
		/// <returns></returns>
		public async Task ShutdownMachineAsync()
		{
			await Task.Run(() => ShutdownMachine());
		}

		/// <summary>
		/// Restarts this machine asynchronously.
		/// </summary>
		/// <returns></returns>
		public async Task RestartMachineAsync()
		{
			await Task.Run(() => RestartMachine());
		}

		/// <summary>
		/// Gets the name of the machine this application is currently running on.
		/// </summary>
		/// <returns></returns>
		public String GetMachineName() => Environment.MachineName;

		/// <summary>
		/// Gets the current directory.
		/// </summary>
		/// <returns></returns>
		public String GetCurrentDirectory() => Environment.CurrentDirectory;


		#endregion PUBLIC METHODS

	}

}
