using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace socket_base
{
    public class socket_base
    {
        protected string address;
        protected int port;
        public Socket socket;
        protected State state = new State();
        private const int buffSize = 8 * 1024;
        private EndPoint epFrom = new IPEndPoint(IPAddress.Any, 0);
        private AsyncCallback recv = null;

        static void Main(string[] args)
        {
        }

        public class State
        {
            public byte[] buffer = new byte[buffSize];
        }

        protected socket_base(string ipaddress, int portnumber)
        {
            address = ipaddress;
            port = portnumber;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        protected void Receive()
        {
            socket.BeginReceiveFrom(state.buffer, 0, buffSize, SocketFlags.None, ref epFrom, recv = (ar) =>
            {
                State so = (State)ar.AsyncState;
                int bytes = socket.EndReceiveFrom(ar, ref epFrom);
                socket.BeginReceiveFrom(so.buffer, 0, buffSize, SocketFlags.None, ref epFrom, recv, so);
                Console.WriteLine("RECV: {0}: {1}, {2}", epFrom.ToString(), bytes, Encoding.ASCII.GetString(so.buffer, 0, bytes));
            }, state);
        }
    }
}
