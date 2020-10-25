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
            this.button3 = new System.Windows.Forms.Button();
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
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.PIMailTextBox = new System.Windows.Forms.TextBox();
            this.PIAgeTextBox = new System.Windows.Forms.TextBox();
            this.PISpamCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.PIPasswordTextBox = new System.Windows.Forms.TextBox();
            this.PIModifyButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.PIUserTextBox = new System.Windows.Forms.TextBox();
            this.LoginGroupBox.SuspendLayout();
            this.RegistergroupBox.SuspendLayout();
            this.profileInformationGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // UserLbl
            // 
            this.UserLbl.AutoSize = true;
            this.UserLbl.Font = new System.Drawing.Font("Calibri", 14.25F);
            this.UserLbl.Location = new System.Drawing.Point(53, 27);
            this.UserLbl.Name = "UserLbl";
            this.UserLbl.Size = new System.Drawing.Size(46, 23);
            this.UserLbl.TabIndex = 1;
            this.UserLbl.Text = "User";
            // 
            // userTextBox
            // 
            this.userTextBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.userTextBox.Location = new System.Drawing.Point(149, 31);
            this.userTextBox.Name = "userTextBox";
            this.userTextBox.Size = new System.Drawing.Size(164, 20);
            this.userTextBox.TabIndex = 3;
            // 
            // LoginButton
            // 
            this.LoginButton.Enabled = false;
            this.LoginButton.Location = new System.Drawing.Point(192, 101);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(75, 23);
            this.LoginButton.TabIndex = 5;
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
            this.LoginGroupBox.Location = new System.Drawing.Point(13, 12);
            this.LoginGroupBox.Name = "LoginGroupBox";
            this.LoginGroupBox.Size = new System.Drawing.Size(344, 170);
            this.LoginGroupBox.TabIndex = 6;
            this.LoginGroupBox.TabStop = false;
            this.LoginGroupBox.Text = "Login";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(69, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Don\'t have an account?";
            // 
            // registerLinkLbl
            // 
            this.registerLinkLbl.AutoSize = true;
            this.registerLinkLbl.Location = new System.Drawing.Point(188, 141);
            this.registerLinkLbl.Name = "registerLinkLbl";
            this.registerLinkLbl.Size = new System.Drawing.Size(49, 13);
            this.registerLinkLbl.TabIndex = 19;
            this.registerLinkLbl.TabStop = true;
            this.registerLinkLbl.Text = "Register!";
            this.registerLinkLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.registerLinkLbl_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F);
            this.label1.Location = new System.Drawing.Point(51, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 23);
            this.label1.TabIndex = 10;
            this.label1.Text = "Password";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.passwordTextBox.Location = new System.Drawing.Point(149, 66);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(164, 20);
            this.passwordTextBox.TabIndex = 11;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(594, 316);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(126, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "Disconnect from server";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.DisconnectButton_Click);
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
            this.RegistergroupBox.Location = new System.Drawing.Point(12, 12);
            this.RegistergroupBox.Name = "RegistergroupBox";
            this.RegistergroupBox.Size = new System.Drawing.Size(344, 251);
            this.RegistergroupBox.TabIndex = 11;
            this.RegistergroupBox.TabStop = false;
            this.RegistergroupBox.Text = "Register";
            this.RegistergroupBox.Visible = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(81, 184);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(264, 38);
            this.richTextBox1.TabIndex = 17;
            this.richTextBox1.Text = "Do you want to receive notifications of upcomming Game news and updates?";
            // 
            // registerMailTextBox
            // 
            this.registerMailTextBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.registerMailTextBox.Location = new System.Drawing.Point(149, 153);
            this.registerMailTextBox.Name = "registerMailTextBox";
            this.registerMailTextBox.PasswordChar = '*';
            this.registerMailTextBox.Size = new System.Drawing.Size(164, 20);
            this.registerMailTextBox.TabIndex = 16;
            // 
            // registerAgeTextBox
            // 
            this.registerAgeTextBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.registerAgeTextBox.Location = new System.Drawing.Point(149, 112);
            this.registerAgeTextBox.Name = "registerAgeTextBox";
            this.registerAgeTextBox.PasswordChar = '*';
            this.registerAgeTextBox.Size = new System.Drawing.Size(164, 20);
            this.registerAgeTextBox.TabIndex = 15;
            // 
            // spamCheckBox
            // 
            this.spamCheckBox.AutoSize = true;
            this.spamCheckBox.Location = new System.Drawing.Point(60, 184);
            this.spamCheckBox.Name = "spamCheckBox";
            this.spamCheckBox.Size = new System.Drawing.Size(15, 14);
            this.spamCheckBox.TabIndex = 14;
            this.spamCheckBox.UseVisualStyleBackColor = true;
            // 
            // mailLbl
            // 
            this.mailLbl.AutoSize = true;
            this.mailLbl.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mailLbl.Location = new System.Drawing.Point(59, 149);
            this.mailLbl.Name = "mailLbl";
            this.mailLbl.Size = new System.Drawing.Size(43, 23);
            this.mailLbl.TabIndex = 13;
            this.mailLbl.Text = "Mail";
            // 
            // ageLabel
            // 
            this.ageLabel.AutoSize = true;
            this.ageLabel.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ageLabel.Location = new System.Drawing.Point(57, 108);
            this.ageLabel.Name = "ageLabel";
            this.ageLabel.Size = new System.Drawing.Size(39, 23);
            this.ageLabel.TabIndex = 12;
            this.ageLabel.Text = "Age";
            // 
            // registerPasswordLbl
            // 
            this.registerPasswordLbl.AutoSize = true;
            this.registerPasswordLbl.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registerPasswordLbl.Location = new System.Drawing.Point(57, 62);
            this.registerPasswordLbl.Name = "registerPasswordLbl";
            this.registerPasswordLbl.Size = new System.Drawing.Size(86, 23);
            this.registerPasswordLbl.TabIndex = 10;
            this.registerPasswordLbl.Text = "Password";
            // 
            // registerPasswordTextBox
            // 
            this.registerPasswordTextBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.registerPasswordTextBox.Location = new System.Drawing.Point(149, 66);
            this.registerPasswordTextBox.Name = "registerPasswordTextBox";
            this.registerPasswordTextBox.PasswordChar = '*';
            this.registerPasswordTextBox.Size = new System.Drawing.Size(164, 20);
            this.registerPasswordTextBox.TabIndex = 11;
            // 
            // RegisterButton
            // 
            this.RegisterButton.Enabled = false;
            this.RegisterButton.Location = new System.Drawing.Point(149, 222);
            this.RegisterButton.Name = "RegisterButton";
            this.RegisterButton.Size = new System.Drawing.Size(75, 23);
            this.RegisterButton.TabIndex = 5;
            this.RegisterButton.Text = "Register";
            this.RegisterButton.UseVisualStyleBackColor = true;
            this.RegisterButton.Click += new System.EventHandler(this.RegisterButton_Click);
            // 
            // usrLbl
            // 
            this.usrLbl.AutoSize = true;
            this.usrLbl.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usrLbl.Location = new System.Drawing.Point(59, 27);
            this.usrLbl.Name = "usrLbl";
            this.usrLbl.Size = new System.Drawing.Size(46, 23);
            this.usrLbl.TabIndex = 1;
            this.usrLbl.Text = "User";
            // 
            // registerUsrTextBox
            // 
            this.registerUsrTextBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.registerUsrTextBox.Location = new System.Drawing.Point(149, 31);
            this.registerUsrTextBox.Name = "registerUsrTextBox";
            this.registerUsrTextBox.Size = new System.Drawing.Size(164, 20);
            this.registerUsrTextBox.TabIndex = 3;
            // 
            // serverConnectionProgressBar
            // 
            this.serverConnectionProgressBar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.serverConnectionProgressBar.Location = new System.Drawing.Point(227, 328);
            this.serverConnectionProgressBar.MarqueeAnimationSpeed = 10;
            this.serverConnectionProgressBar.Name = "serverConnectionProgressBar";
            this.serverConnectionProgressBar.Size = new System.Drawing.Size(280, 10);
            this.serverConnectionProgressBar.Step = 3;
            this.serverConnectionProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.serverConnectionProgressBar.TabIndex = 12;
            // 
            // pregressBarLbl
            // 
            this.pregressBarLbl.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pregressBarLbl.AutoSize = true;
            this.pregressBarLbl.BackColor = System.Drawing.SystemColors.Control;
            this.pregressBarLbl.Location = new System.Drawing.Point(226, 313);
            this.pregressBarLbl.Name = "pregressBarLbl";
            this.pregressBarLbl.Size = new System.Drawing.Size(114, 13);
            this.pregressBarLbl.TabIndex = 13;
            this.pregressBarLbl.Text = "Connecting to server...";
            // 
            // serverStatusLbl
            // 
            this.serverStatusLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.serverStatusLbl.AutoSize = true;
            this.serverStatusLbl.Location = new System.Drawing.Point(0, 328);
            this.serverStatusLbl.Name = "serverStatusLbl";
            this.serverStatusLbl.Size = new System.Drawing.Size(40, 13);
            this.serverStatusLbl.TabIndex = 14;
            this.serverStatusLbl.Text = "Status:";
            // 
            // profileInformationGroup
            // 
            this.profileInformationGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.profileInformationGroup.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.profileInformationGroup.Controls.Add(this.richTextBox2);
            this.profileInformationGroup.Controls.Add(this.PIMailTextBox);
            this.profileInformationGroup.Controls.Add(this.PIAgeTextBox);
            this.profileInformationGroup.Controls.Add(this.PISpamCheckBox);
            this.profileInformationGroup.Controls.Add(this.label3);
            this.profileInformationGroup.Controls.Add(this.label4);
            this.profileInformationGroup.Controls.Add(this.label5);
            this.profileInformationGroup.Controls.Add(this.PIPasswordTextBox);
            this.profileInformationGroup.Controls.Add(this.PIModifyButton);
            this.profileInformationGroup.Controls.Add(this.label6);
            this.profileInformationGroup.Controls.Add(this.PIUserTextBox);
            this.profileInformationGroup.Location = new System.Drawing.Point(369, 12);
            this.profileInformationGroup.Name = "profileInformationGroup";
            this.profileInformationGroup.Size = new System.Drawing.Size(344, 251);
            this.profileInformationGroup.TabIndex = 15;
            this.profileInformationGroup.TabStop = false;
            this.profileInformationGroup.Text = "Profile Information";
            this.profileInformationGroup.Visible = false;
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Location = new System.Drawing.Point(81, 184);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(264, 38);
            this.richTextBox2.TabIndex = 17;
            this.richTextBox2.Text = "Do you want to receive notifications of upcomming Game news and updates?";
            // 
            // PIMailTextBox
            // 
            this.PIMailTextBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.PIMailTextBox.Location = new System.Drawing.Point(149, 153);
            this.PIMailTextBox.Name = "PIMailTextBox";
            this.PIMailTextBox.PasswordChar = '*';
            this.PIMailTextBox.Size = new System.Drawing.Size(164, 20);
            this.PIMailTextBox.TabIndex = 16;
            // 
            // PIAgeTextBox
            // 
            this.PIAgeTextBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.PIAgeTextBox.Location = new System.Drawing.Point(149, 112);
            this.PIAgeTextBox.Name = "PIAgeTextBox";
            this.PIAgeTextBox.PasswordChar = '*';
            this.PIAgeTextBox.Size = new System.Drawing.Size(164, 20);
            this.PIAgeTextBox.TabIndex = 15;
            // 
            // PISpamCheckBox
            // 
            this.PISpamCheckBox.AutoSize = true;
            this.PISpamCheckBox.Location = new System.Drawing.Point(60, 184);
            this.PISpamCheckBox.Name = "PISpamCheckBox";
            this.PISpamCheckBox.Size = new System.Drawing.Size(15, 14);
            this.PISpamCheckBox.TabIndex = 14;
            this.PISpamCheckBox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(59, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 23);
            this.label3.TabIndex = 13;
            this.label3.Text = "Mail";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(57, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 23);
            this.label4.TabIndex = 12;
            this.label4.Text = "Age";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(57, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 23);
            this.label5.TabIndex = 10;
            this.label5.Text = "Password";
            // 
            // PIPasswordTextBox
            // 
            this.PIPasswordTextBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.PIPasswordTextBox.Location = new System.Drawing.Point(149, 66);
            this.PIPasswordTextBox.Name = "PIPasswordTextBox";
            this.PIPasswordTextBox.PasswordChar = '*';
            this.PIPasswordTextBox.Size = new System.Drawing.Size(164, 20);
            this.PIPasswordTextBox.TabIndex = 11;
            // 
            // PIModifyButton
            // 
            this.PIModifyButton.Enabled = false;
            this.PIModifyButton.Location = new System.Drawing.Point(134, 222);
            this.PIModifyButton.Name = "PIModifyButton";
            this.PIModifyButton.Size = new System.Drawing.Size(97, 23);
            this.PIModifyButton.TabIndex = 5;
            this.PIModifyButton.Text = "Modify Profile";
            this.PIModifyButton.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(59, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 23);
            this.label6.TabIndex = 1;
            this.label6.Text = "User";
            // 
            // PIUserTextBox
            // 
            this.PIUserTextBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.PIUserTextBox.Location = new System.Drawing.Point(149, 31);
            this.PIUserTextBox.Name = "PIUserTextBox";
            this.PIUserTextBox.Size = new System.Drawing.Size(164, 20);
            this.PIUserTextBox.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
<<<<<<< HEAD
            this.ClientSize = new System.Drawing.Size(725, 345);
            this.Controls.Add(this.profileInformationGroup);
            this.Controls.Add(this.serverStatusLbl);
            this.Controls.Add(this.pregressBarLbl);
            this.Controls.Add(this.serverConnectionProgressBar);
            this.Controls.Add(this.RegistergroupBox);
=======
            this.ClientSize = new System.Drawing.Size(534, 542);
>>>>>>> DS_Cmake
            this.Controls.Add(this.button3);
            this.Controls.Add(this.LoginGroupBox);
            this.Name = "Form1";
            this.Text = "Form1";
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

        private System.Windows.Forms.Label UserLbl;
        private System.Windows.Forms.TextBox userTextBox;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.GroupBox LoginGroupBox;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.GroupBox RegistergroupBox;
        private System.Windows.Forms.Label registerPasswordLbl;
        private System.Windows.Forms.TextBox registerPasswordTextBox;
        private System.Windows.Forms.Button RegisterButton;
        private System.Windows.Forms.Label usrLbl;
        private System.Windows.Forms.TextBox registerUsrTextBox;
        private System.Windows.Forms.CheckBox spamCheckBox;
        private System.Windows.Forms.Label mailLbl;
        private System.Windows.Forms.Label ageLabel;
        private System.Windows.Forms.TextBox registerMailTextBox;
        private System.Windows.Forms.TextBox registerAgeTextBox;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.LinkLabel registerLinkLbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar serverConnectionProgressBar;
        private System.Windows.Forms.Label pregressBarLbl;
        private System.Windows.Forms.Label serverStatusLbl;
        private System.Windows.Forms.GroupBox profileInformationGroup;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.TextBox PIMailTextBox;
        private System.Windows.Forms.TextBox PIAgeTextBox;
        private System.Windows.Forms.CheckBox PISpamCheckBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox PIPasswordTextBox;
        private System.Windows.Forms.Button PIModifyButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox PIUserTextBox;
    }
}

