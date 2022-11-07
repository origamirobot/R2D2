using System.Net;
using System.Net.NetworkInformation;

namespace R2D2.Core.Network
{


	/// <summary>
	/// Used to get information about network interfaces.
	/// </summary>
	public interface INetworkInterfaces
	{

		/// <summary>
		/// Lists all the network interfaces on this machine.
		/// </summary>
		/// <returns></returns>
		List<NetworkInterface> ListAll();

		/// <summary>
		/// Queries the network interfaces available on this machine.
		/// </summary>
		/// <returns></returns>
		List<NetworkInterface> Query(Func<NetworkInterface, Boolean> criteria);

		/// <summary>
		/// Returns a list of all the local IP addresses on this machine.
		/// </summary>
		/// <returns></returns>
		List<String> GetAllLocalIpAddresses();

		/// <summary>
		/// Gets the mac addresses associated with this machine.
		/// </summary>
		/// <returns></returns>
		List<String> GetMacAddresses();

	}

	/// <summary>
	/// Acts a wrapper around the <see cref="NetworkInterface"/> class for testibility.
	/// </summary>
	/// <seealso cref="PracticeRCCP.Core.Network.INetworkInterfaces" />
	public class NetworkInterfaces : INetworkInterfaces
	{

		/// <summary>
		/// Lists all the network interfaces on this machine.
		/// </summary>
		/// <returns></returns>
		public List<NetworkInterface> ListAll()
		{
			return NetworkInterface.GetAllNetworkInterfaces().ToList();
		}

		/// <summary>
		/// Queries the network interfaces available on this machine.
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns></returns>
		public List<NetworkInterface> Query(Func<NetworkInterface, bool> criteria)
		{
			return NetworkInterface.GetAllNetworkInterfaces().Where(criteria).ToList();
		}

		/// <summary>
		/// Returns a list of all the local IP addresses on this machine.
		/// </summary>
		/// <returns></returns>
		public List<String> GetAllLocalIpAddresses()
		{
			var result = new List<String>();
			var hostEntry = Dns.GetHostEntry(Dns.GetHostName());
			var totalHosts = hostEntry.AddressList.GetUpperBound(0);

			for (var i = 0; i <= totalHosts; i++)
			{
				var addr = hostEntry.AddressList[i].ToString();
				if (addr.Contains("."))
					result.Add(addr);
			}
			return result;
		}

		/// <summary>
		/// Gets the MAC addresses associated with this machine.
		/// </summary>
		/// <returns></returns>
		public List<String> GetMacAddresses()
		{
			var list = NetworkInterface
				.GetAllNetworkInterfaces()
				.Where(nic => nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
				.Select(nic => nic.GetPhysicalAddress().ToString()).ToList();
			return list;
		}

	}

}