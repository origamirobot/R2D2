namespace R2D2.Core.IO
{

	/// <summary>
	/// Defines a contract for file streams. Esstentially acts as a wrapper around <see cref="System.IO.FileStream"/> for testability purposes.
	/// </summary>
	public interface IFileStream : IDisposable
	{

		/// <summary>
		/// Reads a block of bytes from the stream and writes the data in a given buffer.
		/// </summary>
		/// <param name="buffer">The buffer.</param>
		/// <param name="offset">The byte offset in array at which the read bytes will be placed.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <returns></returns>
		Int32 Read(Byte[] buffer, Int32 offset, Int32 count);

	}

	/// <summary>
	/// Acts as a wrapper around <see cref="System.IO.FileStream"/> for testability purposes.
	/// </summary>
	/// <seealso cref="R2D2.Core.IO.IFileStream" />
	public class FileStreamWrapper : IFileStream
	{

		#region PROTECTED PROPERTIES


		/// <summary>
		/// Gets the stream.
		/// </summary>
		protected FileStream Stream { get; private set; }


		#endregion PROTECTED PROPERTIES

		#region CONSTRUCTORS


		/// <summary>
		/// Initializes a new instance of the <see cref="FileStreamWrapper"/> class.
		/// </summary>
		public FileStreamWrapper(String file, FileMode mode)
		{
			Stream = new FileStream(file, mode);
		}


		#endregion CONSTRUCTORS

		#region PUBLIC METHODS


		/// <summary>
		/// Reads a block of bytes from the stream and writes the data in a given buffer.
		/// </summary>
		/// <param name="buffer">The buffer.</param>
		/// <param name="offset">The byte offset in array at which the read bytes will be placed.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <returns></returns>
		public Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
			=> Stream.Read(buffer, offset, count);

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Stream.Dispose();
		}


		#endregion PUBLIC METHODS

	}

}
