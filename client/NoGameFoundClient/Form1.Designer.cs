namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.UserLbl = new System.Windows.Forms.Label();
            this.userTextBox = new System.Windows.Forms.TextBox();
            this.LoginButton = new System.Windows.Forms.Button();
            this.LoginGroupBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.registerLinkLbl = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.disconnectServerBtn = new System.Windows.Forms.Button();
            this.RegistergroupBox = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.registerMailTextBox = new System.Windows.Forms.TextBox();
            this.registerAgeTextBox = new System.Windows.Forms.TextBox();
            this.spamCheckBox = new System.Windows.Forms.CheckBox();
            this.mailLbl = new System.Windows.Forms.Label();
            this.ageLabel = new System.Windows.Forms.Label();
            this.registerPasswordLbl = new System.Windows.Forms.Label();
            this.registerPasswordTextBox = new System.Windows.Forms.TextBox();
            this.RegisterButton = new System.Windows.Forms.Button();
            this.usrLbl = new System.Windows.Forms.Label();
            this.registerUsrTextBox = new System.Windows.Forms.TextBox();
            this.serverConnectionProgressBar = new System.Windows.Forms.ProgressBar();
            this.pregressBarLbl = new System.Windows.Forms.Label();
            this.serverStatusLbl = new System.Windows.Forms.Label();
            this.profileInformationGroup = new System.Windows.Forms.GroupBox();
            this.getAgeLabel = new System.Windows.Forms.Label();
            this.getMailLabel = new System.Windows.Forms.Label();
            this.getAgeButton = new System.Windows.Forms.Button();
            this.getMailButton = new System.Windows.Forms.Button();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.PISpamCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SpamModifyButton = new System.Windows.Forms.Button();
            this.errorDialogLabel = new System.Windows.Forms.Label();
            this.loginStatusLbl = new System.Windows.Forms.Label();
            this.LoginGroupBox.SuspendLayout();
            this.RegistergroupBox.SuspendLayout();
            this.profileInformationGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // UserLbl
            // 
            this.UserLbl.AutoSize = true;
            this.UserLbl.Font = new System.Drawing.Font("Calibri", 14.25F);
            this.UserLbl.Location = new System.Drawing.Point(97, 50);
            this.UserLbl.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.UserLbl.Name = "UserLbl";
            this.UserLbl.Size = new System.Drawing.Size(82, 41);
            this.UserLbl.TabIndex = 1;
            this.UserLbl.Text = "User";
            // 
            // userTextBox
            // 
            this.userTextBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.userTextBox.Location = new System.Drawing.Point(273, 57);
            this.userTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.userTextBox.Name = "userTextBox";
            this.userTextBox.Size = new System.Drawing.Size(297, 29);
            this.userTextBox.TabIndex = 3;
            // 
            // LoginButton
            // 
            this.LoginButton.Enabled = false;
            this.LoginButton.Location = new System.Drawing.Point(352, 186);
            this.LoginButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(138, 42);
            this.LoginButton.TabIndex = 0;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // LoginGroupBox
            // 
            this.LoginGroupBox.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.LoginGroupBox.Controls.Add(this.label2);
            this.LoginGroupBox.Controls.Add(this.registerLinkLbl);
            this.LoginGroupBox.Controls.Add(this.label1);
            this.LoginGroupBox.Controls.Add(this.passwordTextBox);
            this.LoginGroupBox.Controls.Add(this.LoginButton);
            this.LoginGroupBox.Controls.Add(this.UserLbl);
            this.LoginGroupBox.Controls.Add(this.userTextBox);
            this.LoginGroupBox.Location = new System.Drawing.Point(24, 22);
            this.LoginGroupBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.LoginGroupBox.Name = "LoginGroupBox";
            this.LoginGroupBox.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.LoginGroupBox.Size = new System.Drawing.Size(631, 314);
            this.LoginGroupBox.TabIndex = 6;
            this.LoginGroupBox.TabStop = false;
            this.LoginGroupBox.Text = "Login";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(127, 260);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(217, 25);
            this.label2.TabIndex = 20;
            this.label2.Text = "Don\'t have an account?";
            // 
            // registerLinkLbl
            // 
            this.registerLinkLbl.AutoSize = true;
            this.registerLinkLbl.Location = new System.Drawing.Point(345, 260);
            this.registerLinkLbl.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.registerLinkLbl.Name = "registerLinkLbl";
            this.registerLinkLbl.Size = new System.Drawing.Size(89, 25);
            this.registerLinkLbl.TabIndex = 1;
            this.registerLinkLbl.TabStop = true;
            this.registerLinkLbl.Text = "Register!";
            this.registerLinkLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.registerLinkLbl_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F);
            this.label1.Location = new System.Drawing.Point(94, 114);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 41);
            this.label1.TabIndex = 10;
            this.label1.Text = "Password";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.passwordTextBox.Location = new System.Drawing.Point(273, 122);
            this.passwordTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(297, 29);
            this.passwordTextBox.TabIndex = 11;
            // 
            // disconnectServerBtn
            // 
            this.disconnectServerBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.disconnectServerBtn.Enabled = false;
            this.disconnectServerBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.disconnectServerBtn.Location = new System.Drawing.Point(1089, 583);
            this.disconnectServerBtn.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.disconnectServerBtn.Name = "disconnectServerBtn";
            this.disconnectServerBtn.Size = new System.Drawing.Size(231, 42);
            this.disconnectServerBtn.TabIndex = 10;
            this.disconnectServerBtn.Text = "Disconnect from server";
            this.disconnectServerBtn.UseVisualStyleBackColor = true;
            this.disconnectServerBtn.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // RegistergroupBox
            // 
            this.RegistergroupBox.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.RegistergroupBox.Controls.Add(this.richTextBox1);
            this.RegistergroupBox.Controls.Add(this.registerMailTextBox);
            this.RegistergroupBox.Controls.Add(this.registerAgeTextBox);
            this.RegistergroupBox.Controls.Add(this.spamCheckBox);
            this.RegistergroupBox.Controls.Add(this.mailLbl);
            this.RegistergroupBox.Controls.Add(this.ageLabel);
            this.RegistergroupBox.Controls.Add(this.registerPasswordLbl);
            this.RegistergroupBox.Controls.Add(this.registerPasswordTextBox);
            this.RegistergroupBox.Controls.Add(this.RegisterButton);
            this.RegistergroupBox.Controls.Add(this.usrLbl);
            this.RegistergroupBox.Controls.Add(this.registerUsrTextBox);
            this.RegistergroupBox.Location = new System.Drawing.Point(22, 22);
            this.RegistergroupBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.RegistergroupBox.Name = "RegistergroupBox";
            this.RegistergroupBox.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.RegistergroupBox.Size = new System.Drawing.Size(631, 463);
            this.RegistergroupBox.TabIndex = 11;
            this.RegistergroupBox.TabStop = false;
            this.RegistergroupBox.Text = "Register";
            this.RegistergroupBox.Visible = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(149, 340);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(484, 70);
            this.richTextBox1.TabIndex = 17;
            this.richTextBox1.Text = "Do you want to receive notifications of upcomming Game news and updates?";
            // 
            // registerMailTextBox
            // 
            this.registerMailTextBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.registerMailTextBox.Location = new System.Drawing.Point(273, 282);
            this.registerMailTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.registerMailTextBox.Name = "registerMailTextBox";
            this.registerMailTextBox.Size = new System.Drawing.Size(297, 29);
            this.registerMailTextBox.TabIndex = 3;
            // 
            // registerAgeTextBox
            // 
            this.registerAgeTextBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.registerAgeTextBox.Location = new System.Drawing.Point(273, 207);
            this.registerAgeTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.registerAgeTextBox.Name = "registerAgeTextBox";
            this.registerAgeTextBox.Size = new System.Drawing.Size(297, 29);
            this.registerAgeTextBox.TabIndex = 2;
            this.registerAgeTextBox.TextChanged += new System.EventHandler(this.registerAgeTextBox_TextChanged);
            // 
            // spamCheckBox
            // 
            this.spamCheckBox.AutoSize = true;
            this.spamCheckBox.Location = new System.Drawing.Point(110, 340);
            this.spamCheckBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.spamCheckBox.Name = "spamCheckBox";
            this.spamCheckBox.Size = new System.Drawing.Size(22, 21);
            this.spamCheckBox.TabIndex = 4;
            this.spamCheckBox.UseVisualStyleBackColor = true;
            // 
            // mailLbl
            // 
            this.mailLbl.AutoSize = true;
            this.mailLbl.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mailLbl.Location = new System.Drawing.Point(108, 275);
            this.mailLbl.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.mailLbl.Name = "mailLbl";
            this.mailLbl.Size = new System.Drawing.Size(79, 41);
            this.mailLbl.TabIndex = 13;
            this.mailLbl.Text = "Mail";
            // 
            // ageLabel
            // 
            this.ageLabel.AutoSize = true;
            this.ageLabel.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ageLabel.Location = new System.Drawing.Point(105, 199);
            this.ageLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.ageLabel.Name = "ageLabel";
            this.ageLabel.Size = new System.Drawing.Size(71, 41);
            this.ageLabel.TabIndex = 12;
            this.ageLabel.Text = "Age";
            // 
            // registerPasswordLbl
            // 
            this.registerPasswordLbl.AutoSize = true;
            this.registerPasswordLbl.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registerPasswordLbl.Location = new System.Drawing.Point(105, 114);
            this.registerPasswordLbl.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.registerPasswordLbl.Name = "registerPasswordLbl";
            this.registerPasswordLbl.Size = new System.Drawing.Size(149, 41);
            this.registerPasswordLbl.TabIndex = 10;
            this.registerPasswordLbl.Text = "Password";
            // 
            // registerPasswordTextBox
            // 
            this.registerPasswordTextBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.registerPasswordTextBox.Location = new System.Drawing.Point(273, 122);
            this.registerPasswordTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.registerPasswordTextBox.Name = "registerPasswordTextBox";
            this.registerPasswordTextBox.PasswordChar = '*';
            this.registerPasswordTextBox.Size = new System.Drawing.Size(297, 29);
            this.registerPasswordTextBox.TabIndex = 1;
            // 
            // RegisterButton
            // 
            this.RegisterButton.Enabled = false;
            this.RegisterButton.Location = new System.Drawing.Point(273, 410);
            this.RegisterButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.RegisterButton.Name = "RegisterButton";
            this.RegisterButton.Size = new System.Drawing.Size(138, 42);
            this.RegisterButton.TabIndex = 5;
            this.RegisterButton.Text = "Register";
            this.RegisterButton.UseVisualStyleBackColor = true;
            this.RegisterButton.Click += new System.EventHandler(this.RegisterButton_Click);
            // 
            // usrLbl
            // 
            this.usrLbl.AutoSize = true;
            this.usrLbl.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usrLbl.Location = new System.Drawing.Point(108, 50);
            this.usrLbl.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.usrLbl.Name = "usrLbl";
            this.usrLbl.Size = new System.Drawing.Size(82, 41);
            this.usrLbl.TabIndex = 1;
            this.usrLbl.Text = "User";
            // 
            // registerUsrTextBox
            // 
            this.registerUsrTextBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.registerUsrTextBox.Location = new System.Drawing.Point(273, 57);
            this.registerUsrTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.registerUsrTextBox.Name = "registerUsrTextBox";
            this.registerUsrTextBox.Size = new System.Drawing.Size(297, 29);
            this.registerUsrTextBox.TabIndex = 0;
            // 
            // serverConnectionProgressBar
            // 
            this.serverConnectionProgressBar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.serverConnectionProgressBar.Location = new System.Drawing.Point(416, 613);
            this.serverConnectionProgressBar.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.serverConnectionProgressBar.MarqueeAnimationSpeed = 10;
            this.serverConnectionProgressBar.Name = "serverConnectionProgressBar";
            this.serverConnectionProgressBar.Size = new System.Drawing.Size(513, 18);
            this.serverConnectionProgressBar.Step = 3;
            this.serverConnectionProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.serverConnectionProgressBar.TabIndex = 12;
            // 
            // pregressBarLbl
            // 
            this.pregressBarLbl.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pregressBarLbl.AutoSize = true;
            this.pregressBarLbl.BackColor = System.Drawing.SystemColors.Control;
            this.pregressBarLbl.Location = new System.Drawing.Point(414, 585);
            this.pregressBarLbl.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.pregressBarLbl.Name = "pregressBarLbl";
            this.pregressBarLbl.Size = new System.Drawing.Size(207, 25);
            this.pregressBarLbl.TabIndex = 13;
            this.pregressBarLbl.Text = "Connecting to server...";
            // 
            // serverStatusLbl
            // 
            this.serverStatusLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.serverStatusLbl.AutoSize = true;
            this.serverStatusLbl.Location = new System.Drawing.Point(0, 611);
            this.serverStatusLbl.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.serverStatusLbl.Name = "serverStatusLbl";
            this.serverStatusLbl.Size = new System.Drawing.Size(74, 25);
            this.serverStatusLbl.TabIndex = 14;
            this.serverStatusLbl.Text = "Status:";
            // 
            // profileInformationGroup
            // 
            this.profileInformationGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.profileInformationGroup.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.profileInformationGroup.Controls.Add(this.getAgeLabel);
            this.profileInformationGroup.Controls.Add(this.getMailLabel);
            this.profileInformationGroup.Controls.Add(this.getAgeButton);
            this.profileInformationGroup.Controls.Add(this.getMailButton);
            this.profileInformationGroup.Controls.Add(this.richTextBox2);
            this.profileInformationGroup.Controls.Add(this.PISpamCheckBox);
            this.profileInformationGroup.Controls.Add(this.label3);
            this.profileInformationGroup.Controls.Add(this.SpamModifyButton);
            this.profileInformationGroup.Location = new System.Drawing.Point(677, 22);
            this.profileInformationGroup.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.profileInformationGroup.Name = "profileInformationGroup";
            this.profileInformationGroup.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.profileInformationGroup.Size = new System.Drawing.Size(631, 463);
            this.profileInformationGroup.TabIndex = 15;
            this.profileInformationGroup.TabStop = false;
            this.profileInformationGroup.Text = "Profile Information";
            this.profileInformationGroup.Visible = false;
            // 
            // getAgeLabel
            // 
            this.getAgeLabel.AutoSize = true;
            this.getAgeLabel.Location = new System.Drawing.Point(259, 98);
            this.getAgeLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.getAgeLabel.Name = "getAgeLabel";
            this.getAgeLabel.Size = new System.Drawing.Size(0, 25);
            this.getAgeLabel.TabIndex = 21;
            // 
            // getMailLabel
            // 
            this.getMailLabel.AutoSize = true;
            this.getMailLabel.Location = new System.Drawing.Point(259, 50);
            this.getMailLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.getMailLabel.Name = "getMailLabel";
            this.getMailLabel.Size = new System.Drawing.Size(0, 25);
            this.getMailLabel.TabIndex = 20;
            // 
            // getAgeButton
            // 
            this.getAgeButton.Location = new System.Drawing.Point(110, 89);
            this.getAgeButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.getAgeButton.Name = "getAgeButton";
            this.getAgeButton.Size = new System.Drawing.Size(138, 42);
            this.getAgeButton.TabIndex = 1;
            this.getAgeButton.Text = "Get Age";
            this.getAgeButton.UseVisualStyleBackColor = true;
            this.getAgeButton.Click += new System.EventHandler(this.getAgeButton_Click);
            // 
            // getMailButton
            // 
            this.getMailButton.Location = new System.Drawing.Point(110, 35);
            this.getMailButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.getMailButton.Name = "getMailButton";
            this.getMailButton.Size = new System.Drawing.Size(138, 42);
            this.getMailButton.TabIndex = 0;
            this.getMailButton.Text = "Get Mail";
            this.getMailButton.UseVisualStyleBackColor = true;
            this.getMailButton.Click += new System.EventHandler(this.getMailButton_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Location = new System.Drawing.Point(149, 340);
            this.richTextBox2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(484, 70);
            this.richTextBox2.TabIndex = 17;
            this.richTextBox2.Text = "Do you want to receive notifications of upcomming Game news and updates?";
            // 
            // PISpamCheckBox
            // 
            this.PISpamCheckBox.AutoSize = true;
            this.PISpamCheckBox.Location = new System.Drawing.Point(110, 340);
            this.PISpamCheckBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.PISpamCheckBox.Name = "PISpamCheckBox";
            this.PISpamCheckBox.Size = new System.Drawing.Size(22, 21);
            this.PISpamCheckBox.TabIndex = 2;
            this.PISpamCheckBox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(108, 275);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(201, 41);
            this.label3.TabIndex = 13;
            this.label3.Text = "Modify Spam";
            // 
            // SpamModifyButton
            // 
            this.SpamModifyButton.Enabled = false;
            this.SpamModifyButton.Location = new System.Drawing.Point(246, 410);
            this.SpamModifyButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.SpamModifyButton.Name = "SpamModifyButton";
            this.SpamModifyButton.Size = new System.Drawing.Size(178, 42);
            this.SpamModifyButton.TabIndex = 3;
            this.SpamModifyButton.Text = "Modify Spam";
            this.SpamModifyButton.UseVisualStyleBackColor = true;
            this.SpamModifyButton.Click += new System.EventHandler(this.SpamModifyButton_Click);
            // 
            // errorDialogLabel
            // 
            this.errorDialogLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.errorDialogLabel.AutoSize = true;
            this.errorDialogLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorDialogLabel.ForeColor = System.Drawing.Color.Red;
            this.errorDialogLabel.Location = new System.Drawing.Point(6, 580);
            this.errorDialogLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.errorDialogLabel.Name = "errorDialogLabel";
            this.errorDialogLabel.Size = new System.Drawing.Size(0, 25);
            this.errorDialogLabel.TabIndex = 16;
            // 
            // loginStatusLbl
            // 
            this.loginStatusLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.loginStatusLbl.AutoSize = true;
            this.loginStatusLbl.Location = new System.Drawing.Point(6, 541);
            this.loginStatusLbl.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.loginStatusLbl.Name = "loginStatusLbl";
            this.loginStatusLbl.Size = new System.Drawing.Size(0, 25);
            this.loginStatusLbl.TabIndex = 17;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1329, 637);
            this.Controls.Add(this.loginStatusLbl);
            this.Controls.Add(this.errorDialogLabel);
            this.Controls.Add(this.profileInformationGroup);
            this.Controls.Add(this.serverStatusLbl);
            this.Controls.Add(this.pregressBarLbl);
            this.Controls.Add(this.serverConnectionProgressBar);
            this.Controls.Add(this.RegistergroupBox);
            this.Controls.Add(this.disconnectServerBtn);
            this.Controls.Add(this.LoginGroupBox);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "Form1";
            this.Text = "Log In Screen";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.LoginGroupBox.ResumeLayout(false);
            this.LoginGroupBox.PerformLayout();
            this.RegistergroupBox.ResumeLayout(false);
            this.RegistergroupBox.PerformLayout();
            this.profileInformationGroup.ResumeLayout(false);
            this.profileInformationGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox LoginGroupBox;
        private System.Windows.Forms.Label UserLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox userTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.LinkLabel registerLinkLbl;
        private System.Windows.Forms.Label registerPasswordLbl;
        private System.Windows.Forms.Label usrLbl;
        private System.Windows.Forms.Label mailLbl;
        private System.Windows.Forms.Label ageLabel;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox registerUsrTextBox;
        private System.Windows.Forms.TextBox registerPasswordTextBox;
        private System.Windows.Forms.TextBox registerAgeTextBox;
        private System.Windows.Forms.TextBox registerMailTextBox;
        private System.Windows.Forms.CheckBox spamCheckBox;
        private System.Windows.Forms.Button RegisterButton;
        private System.Windows.Forms.GroupBox RegistergroupBox;
        private System.Windows.Forms.GroupBox profileInformationGroup;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label getAgeLabel;
        private System.Windows.Forms.Label getMailLabel;
        private System.Windows.Forms.Button getAgeButton;
        private System.Windows.Forms.Button getMailButton;
        private System.Windows.Forms.CheckBox PISpamCheckBox;
        private System.Windows.Forms.Button SpamModifyButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar serverConnectionProgressBar;
        private System.Windows.Forms.Button disconnectServerBtn;
        private System.Windows.Forms.Label pregressBarLbl;
        private System.Windows.Forms.Label serverStatusLbl;
        private System.Windows.Forms.Label errorDialogLabel;
        private System.Windows.Forms.Label loginStatusLbl;
    }
}

