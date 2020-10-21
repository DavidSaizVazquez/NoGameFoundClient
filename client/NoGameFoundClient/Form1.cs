using NoGameFoundClient;
using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        ServerConnectionThread serverConnection;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            serverConnection = new ServerConnectionThread("192.168.53.105", 9005);
            Thread thread = new Thread(new ThreadStart(serverConnection.ConnectToServer));
            thread.Start();
         

        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
                       

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            
            String usr = userTextBox.Text;
            String pass = passwordTextBox.Text;

            serverConnection.SendMessage("0/" + usr + "," + pass);


        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            serverConnection.DisconnectFromServer();

        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
