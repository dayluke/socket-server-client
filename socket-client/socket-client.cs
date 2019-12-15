using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace socket_base
{
    public class socket_client : socket_base
    {
        static void Main(string[] args)
        {
            string ipAddr = "192.168.1.12"; // 127.0.0.1 is localhost
            int port = 54123;

            socket_client client = new socket_client(ipAddr, port);
            client.Init();
            client.Send("Server setup.");

            string user_input = "";

            while (true)
            {
                user_input = Console.ReadLine();

                if (user_input == "")
                {
                    break;
                }
                else
                {
                    client.Send(user_input);
                }
            }
        }

        public socket_client(string address, int port) : base(address, port)
        {
        }

        public void Init()
        {
            socket.Connect(IPAddress.Parse(address), port);
            Receive();
        }

        public void Send(string text)
        {
            byte[] data = Encoding.ASCII.GetBytes(text);
            socket.BeginSend(data, 0, data.Length, SocketFlags.None, (ar) =>
            {
                State so = (State)ar.AsyncState;
                int bytes = socket.EndSend(ar);
                Console.WriteLine("SEND: {0}, {1}", bytes, text);
            }, state);
        }
    }
}
