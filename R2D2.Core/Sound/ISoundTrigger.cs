using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R2D2.Core.Sound
{

	/// <summary>
	/// Represents a device that can trigger sounds.
	/// </summary>
	public interface ISoundTrigger
	{

		void Play(Int32 trackIndex);
		void PlayPause();
		void Forward();
		void Reverse();
		void Trigger(Int32 trackIndex);
	}

}
