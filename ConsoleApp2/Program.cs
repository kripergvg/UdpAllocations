using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var port = 53333;

            Console.WriteLine(args[0]);
            var server = args[0] == "server";
            
            if (server)
            {
                var buffer = new byte[1024 * 4];
                var endpoint = new IPEndPoint(IPAddress.Any, port);
                var socket = new Socket(endpoint.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
                socket.Bind(endpoint);

                EndPoint endPoint1 = endpoint;
                while (true)
                {
                    var size= socket.ReceiveFrom(buffer, SocketFlags.None, ref endPoint1);
                    Thread.Sleep(1);
                }
            }
            else
            {
                var endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
                var testData = Encoding.UTF8.GetBytes("TestData");
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                while (true)
                {
                    socket.SendTo(testData, 0, testData.Length, SocketFlags.None, endpoint);
                    Thread.Sleep(1);
                }
            }
        }
    }
}