using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace TCP客户端
{
    class Program
    {
        static void Main(string[] args)
        {

            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(new IPEndPoint(IPAddress.Parse("192.168.40.28"), 80));

            byte[] data = new byte[1024];
            int count = client.Receive(data);
            Console.WriteLine(Encoding.UTF8.GetString(data, 0, count));
            while (true)
            {
                string s = Console.ReadLine();
                if (s == "c")
                {
                    client.Close();
                    return;
                }
                client.Send(Encoding.UTF8.GetBytes(s));
            }
        }
    }
}
