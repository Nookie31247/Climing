namespace WinFormsApp2
{
    partial class MemberLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializePlaceholder()
        {
            SetPlaceholder(txtUserId, "아이디 입력");
            SetPlaceholder(txtPassword, "비밀번호 입력");
        }


        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        /// 
        private void InitializeComponent()
        {
            txtUserId = new TextBox();
            txtPassword = new TextBox();
            MemberLoginButton = new Button();
            Password = new Label();
            ID = new Label();
            LoginLabel = new Label();
            MemberRegisterButton = new Button();
            ProgramExit = new Button();
            SuspendLayout();
            // 
            // txtUserId
            // 
            txtUserId.Location = new Point(246, 190);
            txtUserId.Margin = new Padding(5, 6, 5, 6);
            txtUserId.Name = "txtUserId";
            txtUserId.Size = new Size(169, 35);
            txtUserId.TabIndex = 0;
            txtUserId.Click += TxtPassword_Enter;
            txtUserId.DoubleClick += TxtUserId_Leave;
            txtUserId.Enter += TxtUserId_Enter;
            txtUserId.Leave += TxtUserId_Leave;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(246, 240);
            txtPassword.Margin = new Padding(5, 6, 5, 6);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(169, 35);
            txtPassword.TabIndex = 1;
            txtPassword.Click += TxtPassword_Enter;
            txtPassword.DoubleClick += TxtPassword_Leave;
            txtPassword.Enter += TxtPassword_Enter;
            txtPassword.Leave += TxtPassword_Leave;
            // 
            // MemberLoginButton
            // 
            MemberLoginButton.AccessibleRole = AccessibleRole.None;
            MemberLoginButton.BackColor = Color.Orange;
            MemberLoginButton.Location = new Point(425, 240);
            MemberLoginButton.Margin = new Padding(5, 6, 5, 6);
            MemberLoginButton.Name = "MemberLoginButton";
            MemberLoginButton.Size = new Size(100, 35);
            MemberLoginButton.TabIndex = 2;
            MemberLoginButton.Text = "로그인";
            MemberLoginButton.UseVisualStyleBackColor = false;
            MemberLoginButton.Click += MemberLoginButton_Click;
            // 
            // Password
            // 
            Password.AutoSize = true;
            Password.ForeColor = SystemColors.ButtonHighlight;
            Password.Location = new Point(139, 240);
            Password.Margin = new Padding(5, 0, 5, 0);
            Password.Name = "Password";
            Password.Size = new Size(97, 30);
            Password.TabIndex = 3;
            Password.Text = "비밀번호";
            // 
            // ID
            // 
            ID.AutoSize = true;
            ID.ForeColor = SystemColors.ButtonHighlight;
            ID.Location = new Point(162, 190);
            ID.Margin = new Padding(5, 0, 5, 0);
            ID.Name = "ID";
            ID.Size = new Size(76, 30);
            ID.TabIndex = 4;
            ID.Text = "아이디";
            // 
            // LoginLabel
            // 
            LoginLabel.AutoSize = true;
            LoginLabel.Font = new Font("맑은 고딕", 24F, FontStyle.Regular, GraphicsUnit.Point, 129);
            LoginLabel.Location = new Point(269, 120);
            LoginLabel.Margin = new Padding(5, 0, 5, 0);
            LoginLabel.Name = "LoginLabel";
            LoginLabel.Size = new Size(116, 45);
            LoginLabel.TabIndex = 5;
            LoginLabel.Text = "로그인";
            // 
            // MemberRegisterButton
            // 
            MemberRegisterButton.BackColor = Color.FromArgb(255, 192, 128);
            MemberRegisterButton.ForeColor = Color.White;
            MemberRegisterButton.Location = new Point(533, 240);
            MemberRegisterButton.Name = "MemberRegisterButton";
            MemberRegisterButton.Size = new Size(115, 35);
            MemberRegisterButton.TabIndex = 6;
            MemberRegisterButton.Text = "회원가입";
            MemberRegisterButton.UseVisualStyleBackColor = false;
            MemberRegisterButton.Click += MemberRegisterButton_Click;
            // 
            // ProgramExit
            // 
            ProgramExit.Font = new Font("맑은 고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            ProgramExit.Location = new Point(269, 353);
            ProgramExit.Name = "ProgramExit";
            ProgramExit.Size = new Size(125, 40);
            ProgramExit.TabIndex = 7;
            ProgramExit.Text = "프로그램 종료";
            ProgramExit.UseVisualStyleBackColor = true;
            ProgramExit.Click += ProgramExit_Click_1;
            // 
            // MemberLogin
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 30);
            ClientSize = new Size(704, 441);
            Controls.Add(ProgramExit);
            Controls.Add(MemberRegisterButton);
            Controls.Add(LoginLabel);
            Controls.Add(ID);
            Controls.Add(Password);
            Controls.Add(MemberLoginButton);
            Controls.Add(txtPassword);
            Controls.Add(txtUserId);
            Font = new Font("맑은 고딕", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            ForeColor = Color.White;
            Margin = new Padding(5, 6, 5, 6);
            Name = "MemberLogin";
            Text = "Climing";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private TextBox txtUserId;
        private TextBox txtPassword;
        private Button MemberLoginButton;
        private Label Password;
        private Label ID;
        private Label LoginLabel;
        private Button MemberRegisterButton;
        private Button ProgramExit;
    }
}
