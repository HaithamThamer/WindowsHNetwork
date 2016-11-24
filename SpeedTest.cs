using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HNetwork
{
    public class SpeedTest
    {
        public SpeedTest()
        {

        }
        public int ping(IPAddress IP,int loop = 1,int delay = 1000)
        {
            Ping p = new Ping();
            PingReply pr;
            long sum = 0;
            for (int i = 0; i < loop; i++)
            {
                pr = p.Send(IP);
                if (pr.Status == IPStatus.Success)
                {
                    sum += pr.RoundtripTime;
                }
                Thread.Sleep(delay);
            }
            return (int)(sum/loop);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Speed in KiloBytes</returns>
        public double download(Uri file)
        {
            WebClient wc = new WebClient();
            double begin = Environment.TickCount;
            wc.DownloadFile(file, @"d:\t" + begin);
            double end = Environment.TickCount;
            double fileSizeInByte = new FileInfo(@"d:\t" + begin).Length;
            File.Delete(@"d:\t" + begin);
            return (fileSizeInByte / ((end - begin) / 1000) / 1024);
        }
    }
}
