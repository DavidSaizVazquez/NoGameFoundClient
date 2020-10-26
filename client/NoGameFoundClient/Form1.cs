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
            serverConnection = new ServerConnectionThread("192.168.1.115", 13555);

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
                disconnectServerBtn.Enabled = true;
            }

            else

            {
                serverConnectionProgressBar.Visible = false;
                pregressBarLbl.Visible = false;
                serverStatusLbl.Text = "Status: Server Connection error";
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

            if(serverResponse.Equals("1/0"))
            {
                this.user = userTextBox.Text;
                serverConnection.SendMessage("6/" + this.user);
                String spamResponse = await Task.Run(() => serverConnection.ListenForMessage());       
                PISpamCheckBox.Checked = spamResponse.Equals("6/1");
                profileInformationGroup.Visible = true;
                SpamModifyButton.Enabled = true;
                LoginGroupBox.Visible = false;

                loginStatusLbl.Visible = true;
                loginStatusLbl.ForeColor = Color.Green;
                loginStatusLbl.Text = "Logged In!";

            }
            else
            {
                loginStatusLbl.Visible = true;
                errorDialogLabel.Visible = true;
                loginStatusLbl.ForeColor = Color.Red;
                loginStatusLbl.Text = "User not found, please register";
                errorDialogLabel.Text = "Login Error";
                Console.WriteLine("Login Error");
            }

        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            serverConnection.DisconnectFromServer();
            serverStatusLbl.Text = "Disconnected";
            serverStatusLbl.ForeColor = Color.Black;
            disconnectServerBtn.Enabled = false;
        }

        async private void RegisterButton_Click(object sender, EventArgs e)
        {
            
            String usr = registerUsrTextBox.Text;
            String pass = registerPasswordTextBox.Text;
            String age = registerAgeTextBox.Text;
            String mail = registerMailTextBox.Text;
            if (String.IsNullOrEmpty(usr) || String.IsNullOrEmpty(pass) || String.IsNullOrEmpty(age) || String.IsNullOrEmpty(mail))
            {
                loginStatusLbl.Visible = true;
                loginStatusLbl.ForeColor = Color.Red;
                loginStatusLbl.Text = "Fill in all fields";
            }
            else
            {
                loginStatusLbl.Visible = false;

                int spam = spamCheckBox.Checked ? 1:0;

                serverConnection.SendMessage("2/" + usr + "," + pass + "," + age + "," + mail + "," + spam);


                String serverResponse = await Task.Run(() => serverConnection.ListenForMessage());

                if (serverResponse.Equals("2/0"))
                {
                    LoginGroupBox.Visible = true;
                    RegistergroupBox.Visible = false;
                    errorDialogLabel.Visible = false;

                }

                else
                {
                    errorDialogLabel.Visible = true;
                    errorDialogLabel.Text = "Register Error: User already exists";
                    Console.WriteLine("Register Error: User already exists");
                }
            }

            
        }

        private void registerLinkLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            loginStatusLbl.Visible = false;
            errorDialogLabel.Visible = false;
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

            getAgeLabel.Text = serverResponse.Replace("3/", "");
        }

        async private void SpamModifyButton_Click(object sender, EventArgs e)
        {
            serverConnection.SendMessage("5/" + this.user + "," + (PISpamCheckBox.Checked ? 1:0));

            String serverResponse = await Task.Run(() => serverConnection.ListenForMessage());
            

            if (serverResponse == "5/0")
            {
                
                Console.WriteLine("Change spam successful!");
            }
            else
            {
                errorDialogLabel.Text = "Change spam error";
                Console.WriteLine("Change spam error");
                /**
                 * Notify error in ui
                 */
            }
        }


        private void registerAgeTextBox_TextChanged(object sender, EventArgs e)
        {
            int n;
            if (!int.TryParse(registerAgeTextBox.Text, out n))
            {
                registerAgeTextBox.Text = "";
            }

        }
    }
}