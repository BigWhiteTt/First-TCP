using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TCP服务器端
{
    class Program
    {
        static void Main(string[] args)
        {
            StartServer();
            Console.ReadKey();
        }

        static byte[] data = new byte[1024];

        static void StartServer()
        {
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse("192.168.40.28");
            IPEndPoint endPoint = new IPEndPoint(ip, 80);
            server.Bind(endPoint);
            server.Listen(0);
            server.BeginAccept(AcceptCallBack, server);


        }
        static void AcceptCallBack(IAsyncResult ar)
        {
            Socket server = ar.AsyncState as Socket;
            Socket client = server.EndAccept(ar);
            client.Send(Encoding.UTF8.GetBytes("Hello world !"));
            client.BeginReceive(data, 0, 1024, SocketFlags.None, ReceiveCallBack, client);
            server.BeginAccept(AcceptCallBack, server);
        }
        static void ReceiveCallBack(IAsyncResult ar)
        {
            Socket client = null;
            try
            {
                client = ar.AsyncState as Socket;
                int count = client.EndReceive(ar);
                if(count == 0)
                {
                    client.Close();
                    return;
                }
                Console.WriteLine(Encoding.UTF8.GetString(data, 0, count));
                client.BeginReceive(data, 0, 1024, SocketFlags.None, ReceiveCallBack, client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (client != null)
                {
                    client.Close();
                }
            }
        }
    }
}
