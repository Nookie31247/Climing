namespace WinFormsApp2
{
    partial class MemberRegister
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
            SetPlaceholder(txtUsername, "닉네임 입력");
            SetPlaceholder(txtUserID, "아이디 입력");
            SetPlaceholder(txtPassword, "비밀번호 입력");
            SetPlaceholder(txtPasswordCheck, "비밀번호 확인 입력");
        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MemberRegisterButton = new Button();
            MemberRegisterCancelButton = new Button();
            IDlabel = new Label();
            PassWordlabel = new Label();
            PassWordChecklabel = new Label();
            txtUserID = new TextBox();
            txtPassword = new TextBox();
            txtPasswordCheck = new TextBox();
            txtUsername = new TextBox();
            UserNamelabel = new Label();
            MemberRegisterLabel = new Label();
            process1 = new System.Diagnostics.Process();
            닉네임글자제한 = new Label();
            아이디길이제한 = new Label();
            비밀번호제한 = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // MemberRegisterButton
            // 
            MemberRegisterButton.BackColor = Color.Orange;
            MemberRegisterButton.ForeColor = Color.White;
            MemberRegisterButton.Location = new Point(248, 313);
            MemberRegisterButton.Name = "MemberRegisterButton";
            MemberRegisterButton.Size = new Size(90, 35);
            MemberRegisterButton.TabIndex = 0;
            MemberRegisterButton.Text = "회원가입";
            MemberRegisterButton.UseVisualStyleBackColor = false;
            MemberRegisterButton.Click += MemberRegisterButton_Click;
            // 
            // MemberRegisterCancelButton
            // 
            MemberRegisterCancelButton.BackColor = Color.FromArgb(255, 192, 128);
            MemberRegisterCancelButton.Location = new Point(362, 313);
            MemberRegisterCancelButton.Name = "MemberRegisterCancelButton";
            MemberRegisterCancelButton.Size = new Size(90, 35);
            MemberRegisterCancelButton.TabIndex = 1;
            MemberRegisterCancelButton.Text = "취소";
            MemberRegisterCancelButton.UseVisualStyleBackColor = false;
            MemberRegisterCancelButton.Click += MemberRegisterCancelButton_Click;
            // 
            // IDlabel
            // 
            IDlabel.AutoSize = true;
            IDlabel.ForeColor = Color.White;
            IDlabel.Location = new Point(208, 165);
            IDlabel.Name = "IDlabel";
            IDlabel.Size = new Size(34, 14);
            IDlabel.TabIndex = 2;
            IDlabel.Text = "아이디";
            // 
            // PassWordlabel
            // 
            PassWordlabel.AutoSize = true;
            PassWordlabel.ForeColor = Color.White;
            PassWordlabel.Location = new Point(199, 192);
            PassWordlabel.Name = "PassWordlabel";
            PassWordlabel.Size = new Size(43, 14);
            PassWordlabel.TabIndex = 3;
            PassWordlabel.Text = "비밀번호";
            // 
            // PassWordChecklabel
            // 
            PassWordChecklabel.AutoSize = true;
            PassWordChecklabel.ForeColor = Color.White;
            PassWordChecklabel.Location = new Point(178, 217);
            PassWordChecklabel.Name = "PassWordChecklabel";
            PassWordChecklabel.Size = new Size(64, 14);
            PassWordChecklabel.TabIndex = 4;
            PassWordChecklabel.Text = "비밀번호 확인";
            // 
            // txtUserID
            // 
            txtUserID.Location = new Point(248, 162);
            txtUserID.Name = "txtUserID";
            txtUserID.Size = new Size(150, 22);
            txtUserID.TabIndex = 5;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(248, 189);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(150, 22);
            txtPassword.TabIndex = 6;
            // 
            // txtPasswordCheck
            // 
            txtPasswordCheck.Location = new Point(248, 217);
            txtPasswordCheck.Name = "txtPasswordCheck";
            txtPasswordCheck.Size = new Size(150, 22);
            txtPasswordCheck.TabIndex = 7;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(248, 134);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(150, 22);
            txtUsername.TabIndex = 9;
            // 
            // UserNamelabel
            // 
            UserNamelabel.AutoSize = true;
            UserNamelabel.ForeColor = Color.White;
            UserNamelabel.Location = new Point(208, 137);
            UserNamelabel.Name = "UserNamelabel";
            UserNamelabel.Size = new Size(34, 14);
            UserNamelabel.TabIndex = 8;
            UserNamelabel.Text = "닉네임";
            // 
            // MemberRegisterLabel
            // 
            MemberRegisterLabel.AutoSize = true;
            MemberRegisterLabel.Font = new Font("맑은 고딕", 24F, FontStyle.Regular, GraphicsUnit.Point, 129);
            MemberRegisterLabel.ForeColor = Color.White;
            MemberRegisterLabel.Location = new Point(250, 77);
            MemberRegisterLabel.Name = "MemberRegisterLabel";
            MemberRegisterLabel.Size = new Size(148, 45);
            MemberRegisterLabel.TabIndex = 10;
            MemberRegisterLabel.Text = "회원가입";
            // 
            // process1
            // 
            process1.StartInfo.Domain = "";
            process1.StartInfo.LoadUserProfile = false;
            process1.StartInfo.Password = null;
            process1.StartInfo.StandardErrorEncoding = null;
            process1.StartInfo.StandardInputEncoding = null;
            process1.StartInfo.StandardOutputEncoding = null;
            process1.StartInfo.UseCredentialsForNetworkingOnly = false;
            process1.StartInfo.UserName = "";
            process1.SynchronizingObject = this;
            process1.ErrorDataReceived += process1_ErrorDataReceived;
            process1.Exited += process1_Exited;
            // 
            // 닉네임글자제한
            // 
            닉네임글자제한.AutoSize = true;
            닉네임글자제한.Location = new Point(485, 134);
            닉네임글자제한.Name = "닉네임글자제한";
            닉네임글자제한.Size = new Size(186, 14);
            닉네임글자제한.TabIndex = 11;
            닉네임글자제한.Text = "한글 2~6글자 이내, 영문 16자이내로 작성";
            // 
            // 아이디길이제한
            // 
            아이디길이제한.AutoSize = true;
            아이디길이제한.Location = new Point(485, 165);
            아이디길이제한.Name = "아이디길이제한";
            아이디길이제한.Size = new Size(158, 14);
            아이디길이제한.TabIndex = 12;
            아이디길이제한.Text = "영문,숫자를 조합해 16자이내로 작성";
            // 
            // 비밀번호제한
            // 
            비밀번호제한.AutoSize = true;
            비밀번호제한.Location = new Point(485, 196);
            비밀번호제한.Name = "비밀번호제한";
            비밀번호제한.Size = new Size(201, 14);
            비밀번호제한.TabIndex = 13;
            비밀번호제한.Text = "영문,숫자, 대문자, 특수기호를 포함한 32자이내";
            // 
            // button1
            // 
            button1.Location = new Point(404, 130);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 14;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // MemberRegister
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 30);
            ClientSize = new Size(704, 441);
            Controls.Add(button1);
            Controls.Add(비밀번호제한);
            Controls.Add(아이디길이제한);
            Controls.Add(닉네임글자제한);
            Controls.Add(MemberRegisterLabel);
            Controls.Add(txtUsername);
            Controls.Add(UserNamelabel);
            Controls.Add(txtPasswordCheck);
            Controls.Add(txtPassword);
            Controls.Add(txtUserID);
            Controls.Add(PassWordChecklabel);
            Controls.Add(PassWordlabel);
            Controls.Add(IDlabel);
            Controls.Add(MemberRegisterCancelButton);
            Controls.Add(MemberRegisterButton);
            Font = new Font("Roboto", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            Name = "MemberRegister";
            Text = "MemberRegister";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button MemberRegisterButton;
        private Button MemberRegisterCancelButton;
        private Label IDlabel;
        private Label PassWordlabel;
        private Label PassWordChecklabel;
        private TextBox txtUserID;
        private TextBox txtPassword;
        private TextBox txtPasswordCheck;
        private TextBox txtUsername;
        private Label UserNamelabel;
        private Label MemberRegisterLabel;
        private System.Diagnostics.Process process1;
        private Label 비밀번호제한;
        private Label 아이디길이제한;
        private Label 닉네임글자제한;
        private Button button1;
    }
}