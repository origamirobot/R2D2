namespace R2D2.Core.Configuration
{

	/// <summary>
	/// Represents a configuration used for remote controllers.
	/// </summary>
	public class ControllerSettings
	{

		public String Name { get; set; } = "Default Controller";

		public Dictionary<String, ButtonMapping> Mappings { get; set; } = new Dictionary<String, ButtonMapping>();

	}

}
