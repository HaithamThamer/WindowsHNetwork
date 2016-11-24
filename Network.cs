using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;

namespace HNetwork
{
    public class Network
    {
        public string hostName = Dns.GetHostName();
        public Network()
        {

        }
        public IPAddress getCurrentIP()
        {
            foreach (IPAddress ip in Dns.GetHostEntry(hostName).AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }
            return new IPAddress(new byte[] { 0, 0, 0, 0 }); ;
        }
        public NetworkInterface[] GetAllNetworkInterfaces()
        {
            return NetworkInterface.GetAllNetworkInterfaces();
        }
        public bool changeIP(NetworkInterface Adapter, IPAddress IP, IPAddress subnetMask, IPAddress defaultGateway,params IPAddress[] dnsServers)
        {
            Process tempProcess = new Process();
            tempProcess.StartInfo = new ProcessStartInfo("netsh", string.Format("interface ip set address name=\"{0}\" static {1} {2} {3}", Adapter.Name, IP, subnetMask, defaultGateway));
            tempProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            tempProcess.StartInfo.UseShellExecute = true;
            tempProcess.StartInfo.CreateNoWindow = true;
            tempProcess.Start();
            tempProcess.StartInfo = new ProcessStartInfo("netsh", string.Format("interface ip set dns \"{0}\" static 8.8.8.8", Adapter.Name));
            tempProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            tempProcess.StartInfo.UseShellExecute = true;
            tempProcess.StartInfo.CreateNoWindow = true;
            tempProcess.Start();
            return true;
        }
        
    }
}
