namespace R2D2.Core.Sound
{

	/// <summary>
	/// Represents a device that can trigger sounds.
	/// </summary>
	public interface ISoundTrigger
	{

		/// <summary>
		/// Gets the index of the currently selected file.
		/// </summary>
		Int32 SelectedFileIndex { get; }

		/// <summary>
		/// Gets the total number of tracks saved on this sound trigger device.
		/// </summary>
		Int32 TotalTracks { get; }

		/// <summary>
		/// Gets the name of the sound trigger device.
		/// </summary>
		String DeviceName { get; }

		/// <summary>
		/// Gets a value indicating whether this instance is initialized.
		/// </summary>
		Boolean IsInitialized { get; }

		/// <summary>
		/// Initializes this sound trigger.
		/// </summary>
		void Initialize();

		/// <summary>
		///  If it exists, the nth track in the directory will be played. The total number of available tracks in the
		/// directory can be retrieved using Status Request command.
		/// </summary>
		void Play(Int32 trackIndex);

		/// <summary>
		/// Starts or stops the currently selected file.
		/// </summary>
		void Toggle();

		/// <summary>
		/// Starts the next MP3 track in the directory.
		/// </summary>
		void Forward();

		/// <summary>
		/// Starts the previous MP3 track in the directory.
		/// </summary>
		void Reverse();

	}

}
