using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using R2D2.Core.Control;
using R2D2.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R2D2.Core.Tests.Control
{

	[TestClass]
	public class GamepadController
	{

		#region PROTECTED PROPERTIES


		protected Mock<IFileStream> MockFileStream;


		#endregion PROTECTED PROPERTIES

		#region TEST METHODS


		[TestInitialize]
		public void Setup()
		{
			MockFileStream = new Mock<IFileStream>();
		}


		[TestMethod]
		public void Should_use_the_provided_file_stream_when_reading_gamepad_controller_values()
		{
			// ARRANGE
			using (var gamepad = new R2D2.Core.Control.GamepadController(MockFileStream.Object))
			{
				// ASSERT
				MockFileStream.Verify(x => x.Read(It.IsAny<Byte[]>(), It.IsAny<Int32>(), It.IsAny<Int32>()));
			}
		}

		[TestMethod]
		public void Should_read_exactly_8_bytes_from_the_device_file_at_a_time()
		{
			// ARRANGE
			using (var gamepad = new R2D2.Core.Control.GamepadController(MockFileStream.Object))
			{
				// ASSERT
				MockFileStream.Verify(x => x.Read(It.Is<Byte[]>(y => y.Length == 8), It.IsAny<Int32>(), It.Is<Int32>(y => y == 8)));
			}
		}

		[TestMethod]
		public void Should_build_the_axis_collection_from_configuration_packets_received_while_reading_the_file_stream()
		{
			// ARRANGE
			MockFileStream.Setup(x => x.Read(It.IsAny<Byte[]>(), It.IsAny<Int32>(), It.IsAny<Int32>()));

			using (var gamepad = new R2D2.Core.Control.GamepadController(MockFileStream.Object))
			{
				// ASSERT
				throw new NotImplementedException();
			}
		}

		[TestMethod]
		public void Should_build_the_button_collection_from_configuration_packets_received_while_reading_the_file_stream()
		{
			// ARRANGE
			using (var gamepad = new R2D2.Core.Control.GamepadController(MockFileStream.Object))
			{
				// ASSERT
				throw new NotImplementedException();
			}
		}

		[TestMethod]
		public void Should_update_the_axis_values_stored_in_the_collection_when_a_new_value_is_read_from_the_file_stream()
		{
			// ARRANGE
			using (var gamepad = new R2D2.Core.Control.GamepadController(MockFileStream.Object))
			{
				// ASSERT
				throw new NotImplementedException();
			}
		}

		[TestMethod]
		public void Should_update_the_button_values_stored_in_the_collection_when_a_new_value_is_read_from_the_file_stream()
		{
			// ARRANGE
			using (var gamepad = new R2D2.Core.Control.GamepadController(MockFileStream.Object))
			{
				// ASSERT
				throw new NotImplementedException();
			}
		}

		[TestMethod]
		public void Should_dispose_of_the_injected_file_stream_when_it_is_disposed_as_well()
		{
			// ARRANGE
			using (var gamepad = new R2D2.Core.Control.GamepadController(MockFileStream.Object)) { }
			MockFileStream.Verify(x => x.Dispose());
		}




		#endregion TEST METHODS

	}

}
