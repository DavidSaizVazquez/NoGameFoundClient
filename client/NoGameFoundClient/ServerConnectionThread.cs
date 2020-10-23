using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace NoGameFoundClient
{
    class ServerConnectionThread
    {
        Socket server;
        IPAddress ip;
        IPEndPoint port;


        public ServerConnectionThread(String ip, int port)
        {
            this.ip = IPAddress.Parse(ip);
            this.port = new IPEndPoint(this.ip, port);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public int ConnectToServer()
        {
            try
            {
                server.Connect(port);//Intentamos conectar el socket     
                Console.WriteLine("Connected");
                return 0;
            }
            catch (SocketException ex)
            {
                Console.WriteLine("Server Connection Error");
                return -1;
               
            }
        }

        public void SendMessage(String message)
        {
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(message);
            server.Send(msg);
        }

        public String ListenForMessage()
        {
            byte[] data = new byte[1024];
            int dataSize = server.Receive(data);
            return Encoding.ASCII.GetString(data, 0, dataSize);

        }

        public void DisconnectFromServer()
        {
            string mensaje = "0/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
                 
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

    }
}
