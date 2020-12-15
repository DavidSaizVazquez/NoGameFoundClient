using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace NoGameFoundClient
{
    class ServerConnection
    {
        Socket server;
        IPAddress ip;
        IPEndPoint port;
        bool connected = false;
        static ServerConnection connection = null;


        private ServerConnection()
        {
            this.ip = IPAddress.Parse("127.0.0.1");
            this.port = new IPEndPoint(this.ip, 9990);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public static ServerConnection getInstance()
        {
          
            if (connection == null)
            {
                connection = new ServerConnection();
            }
            return connection;
        }


        public int ConnectToServer()
        {
            Debug.Log("Connecting to server");
            connected = true;
            try
            {
                server.Connect(port);//Intentamos conectar el socket     
                Console.WriteLine("Connected");
                   
                return 0;
            }
            catch (SocketException)
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
                String msg = Encoding.ASCII.GetString(data, 0, dataSize);
                return msg;
           
        }
        public void DisconnectFromServer()
        {
            string mensaje = "0/";

            byte[] msg = Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

        public bool isConnected()
        {
            return connected;
        }
        public void setConnected(bool connected)
        {
            this.connected = connected;
        }

   
    }

}
