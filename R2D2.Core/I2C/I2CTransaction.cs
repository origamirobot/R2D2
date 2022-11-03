namespace R2D2.Core.I2C
{

	/// <summary>
	/// Represents a list of actions that can be performed against an I2C device.
	/// </summary>
	public class I2CTransaction
	{

		#region PUBLIC ACCESSORS


		/// <summary>
		/// Gets the actions this transaction will perform.
		/// </summary>
		public I2CAction[] Actions { get; private set; }


		#endregion PUBLIC ACCESSORS

		#region CONSTRUCTORS


		/// <summary>
		/// Initializes a new instance of the <see cref="I2CTransaction"/> class.
		/// </summary>
		/// <param name="actions">The actions.</param>
		/// <exception cref="System.ArgumentNullException">actions</exception>
		public I2CTransaction(params I2CAction[] actions)
		{
			if (actions == null)
				throw new ArgumentNullException(nameof(actions));
			Actions = actions;
		}


		#endregion CONSTRUCTORS

	}

}