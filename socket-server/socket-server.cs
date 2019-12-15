using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace socket_base
{
    public class socket_server : socket_base
    {
        static void Main(string[] args)
        {
            string ipAddr = "192.168.1.12"; // 127.0.0.1 is localhost
            int port = 54123;

            socket_server server = new socket_server(ipAddr, port);
            server.Init();

            Console.ReadKey();
        }

        public socket_server(string address, int port) : base(address, port)
        {
        }

        public void Init()
        {
            socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            socket.Bind(new IPEndPoint(IPAddress.Parse(address), port));
            Receive();
        }
    }
}
