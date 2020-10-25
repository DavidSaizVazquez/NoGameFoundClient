using NoGameFoundClient;
using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        ServerConnectionThread serverConnection;

        public Form1()
        {
            InitializeComponent();
        }

        
        async private void Form1_Shown(object sender, EventArgs e)
        {
            serverStatusLbl.Text = "Status: conecting...";
            serverConnection = new ServerConnectionThread("192.168.53.105", 9005);

            int connectionSuccess = await Task.Run(() =>
            {
                return serverConnection.ConnectToServer();
            });
            

            if (connectionSuccess == 0)
            {
                
                serverConnectionProgressBar.Visible = false;
                pregressBarLbl.Visible = false;
                serverStatusLbl.Text = "Status: Connected!";
                serverStatusLbl.ForeColor = Color.Green;
                LoginButton.Enabled = true;
                RegisterButton.Enabled = true;
            }

            else

            {
                serverConnectionProgressBar.Visible = false;
                pregressBarLbl.Visible = false;
                serverStatusLbl.Text = "Status: Connection Timed out";
                serverStatusLbl.ForeColor = Color.Red;
            }

        }

        async private void LoginButton_Click(object sender, EventArgs e)
        {

            String usr = userTextBox.Text;
            String pass = passwordTextBox.Text;

            serverConnection.SendMessage("0/" + usr + "," + pass);

            String serverResponse = await Task.Run(() => serverConnection.ListenForMessage());
            /**
             * Execute code to do with the login response
             */

        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            serverConnection.DisconnectFromServer();
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            String usr = registerUsrTextBox.Text;
            String pass = registerPasswordTextBox.Text;
            String age = registerAgeTextBox.Text;
            String mail = registerMailTextBox.Text;
            Boolean spam = spamCheckBox.Checked;

            serverConnection.SendMessage("1/" + usr + "," + pass + "," + age + "," + mail + "," + spam);

            LoginGroupBox.Visible = true;
            RegistergroupBox.Visible = false;
        }

        private void registerLinkLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginGroupBox.Visible = false;
            RegistergroupBox.Visible = true;
        }

    
    }
}