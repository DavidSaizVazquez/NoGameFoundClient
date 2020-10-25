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

        String user;
       

        public Form1()
        {
            InitializeComponent();
        }

        
        async private void Form1_Shown(object sender, EventArgs e)
        {
            serverStatusLbl.Text = "Status: conecting...";
            serverConnection = new ServerConnectionThread("10.0.2.15", 13550);

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

            serverConnection.SendMessage("1/" + usr + "," + pass);

            String serverResponse = await Task.Run(() => serverConnection.ListenForMessage());
            /**
             * Execute code to do with the login response
             */

            if(serverResponse == "1/0")
            {
                this.user = userTextBox.Text;
                serverConnection.SendMessage("6/" + this.user);
                String spamResponse = await Task.Run(() => serverConnection.ListenForMessage());
                PISpamCheckBox.Checked = spamResponse.Replace("/6", "") == "1";

            }
            else
            {
                Console.WriteLine("Login Error");
                /**
                 * Notify error in ui
                 */
            }

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

            serverConnection.SendMessage("2/" + usr + "," + pass + "," + age + "," + mail + "," + spam);

            LoginGroupBox.Visible = true;
            RegistergroupBox.Visible = false;
        }

        private void registerLinkLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginGroupBox.Visible = false;
            RegistergroupBox.Visible = true;
        }

        async private void getMailButton_Click(object sender, EventArgs e)
        {
            serverConnection.SendMessage("4/" + this.user);
            String serverResponse = await Task.Run(() => serverConnection.ListenForMessage());

            getMailLabel.Text = serverResponse.Replace("4/", "");

        }

        async private void getAgeButton_Click(object sender, EventArgs e)
        {
            serverConnection.SendMessage("3/" + this.user);
            String serverResponse = await Task.Run(() => serverConnection.ListenForMessage());

            getMailLabel.Text = serverResponse.Replace("3/", "");
        }

        async private void SpamModifyButton_Click(object sender, EventArgs e)
        {
            serverConnection.SendMessage("5/" + this.user + "," + (PISpamCheckBox.Checked ? 1:0));

            String serverResponse = await Task.Run(() => serverConnection.ListenForMessage());
            /**
             * Execute code to do with the login response
             */

            if (serverResponse == "5/0")
            {
                Console.WriteLine("Change spam successful!");
            }
            else
            {
                Console.WriteLine("Change spam error");
                /**
                 * Notify error in ui
                 */
            }
        }
    }
}