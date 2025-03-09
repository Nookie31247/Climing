namespace Climing
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
            SetPlaceholder(TxtUserNickName, "닉네임 입력");
            SetPlaceholder(TxtUserID, "아이디 입력");        
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
            PassWordLabel = new Label();
            PassWordChecklabel = new Label();
            TxtUserID = new TextBox();
            TxtPassword = new TextBox();
            TxtPasswordCheck = new TextBox();
            TxtUserNickName = new TextBox();
            UserNamelabel = new Label();
            MemberRegisterLabel = new Label();
            닉네임글자제한 = new Label();
            아이디길이제한 = new Label();
            비밀번호제한 = new Label();
            NickNameCheck = new Button();
            UserIDcheck = new Button();
            NickNameCheckLabel = new Label();
            UserIDCheckLabel = new Label();
            PasswordLabel2 = new Label();
            PasswordCheckLabel2 = new Label();
            SuspendLayout();
            // 
            // MemberRegisterButton
            // 
            MemberRegisterButton.BackColor = Color.FromArgb(255, 128, 0);
            MemberRegisterButton.ForeColor = Color.White;
            MemberRegisterButton.Location = new Point(212, 323);
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
            MemberRegisterCancelButton.Location = new Point(342, 323);
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
            IDlabel.Location = new Point(179, 171);
            IDlabel.Name = "IDlabel";
            IDlabel.Size = new Size(34, 14);
            IDlabel.TabIndex = 2;
            IDlabel.Text = "아이디";
            // 
            // PassWordLabel
            // 
            PassWordLabel.AutoSize = true;
            PassWordLabel.ForeColor = Color.White;
            PassWordLabel.Location = new Point(170, 216);
            PassWordLabel.Name = "PassWordLabel";
            PassWordLabel.Size = new Size(43, 14);
            PassWordLabel.TabIndex = 3;
            PassWordLabel.Text = "비밀번호";
            // 
            // PassWordChecklabel
            // 
            PassWordChecklabel.AutoSize = true;
            PassWordChecklabel.ForeColor = Color.White;
            PassWordChecklabel.Location = new Point(149, 261);
            PassWordChecklabel.Name = "PassWordChecklabel";
            PassWordChecklabel.Size = new Size(64, 14);
            PassWordChecklabel.TabIndex = 4;
            PassWordChecklabel.Text = "비밀번호 확인";
            // 
            // TxtUserID
            // 
            TxtUserID.Location = new Point(219, 168);
            TxtUserID.Name = "TxtUserID";
            TxtUserID.Size = new Size(150, 22);
            TxtUserID.TabIndex = 5;
            // 
            // TxtPassword
            // 
            TxtPassword.Location = new Point(219, 213);
            TxtPassword.Name = "TxtPassword";
            TxtPassword.Size = new Size(150, 22);
            TxtPassword.TabIndex = 6;
            TxtPassword.UseSystemPasswordChar = true;
            // 
            // TxtPasswordCheck
            // 
            TxtPasswordCheck.Location = new Point(219, 258);
            TxtPasswordCheck.Name = "TxtPasswordCheck";
            TxtPasswordCheck.Size = new Size(150, 22);
            TxtPasswordCheck.TabIndex = 7;
            TxtPasswordCheck.UseSystemPasswordChar = true;
            // 
            // TxtUserNickName
            // 
            TxtUserNickName.Location = new Point(219, 123);
            TxtUserNickName.Name = "TxtUserNickName";
            TxtUserNickName.Size = new Size(150, 22);
            TxtUserNickName.TabIndex = 9;
            // 
            // UserNamelabel
            // 
            UserNamelabel.AutoSize = true;
            UserNamelabel.ForeColor = Color.White;
            UserNamelabel.Location = new Point(179, 126);
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
            MemberRegisterLabel.Location = new Point(219, 60);
            MemberRegisterLabel.Name = "MemberRegisterLabel";
            MemberRegisterLabel.Size = new Size(148, 45);
            MemberRegisterLabel.TabIndex = 10;
            MemberRegisterLabel.Text = "회원가입";
            // 
            // 닉네임글자제한
            // 
            닉네임글자제한.AutoSize = true;
            닉네임글자제한.Location = new Point(443, 126);
            닉네임글자제한.Name = "닉네임글자제한";
            닉네임글자제한.Size = new Size(186, 14);
            닉네임글자제한.TabIndex = 11;
            닉네임글자제한.Text = "한글 2~5글자 이내, 영문 16자이내로 작성";
            // 
            // 아이디길이제한
            // 
            아이디길이제한.AutoSize = true;
            아이디길이제한.Location = new Point(443, 171);
            아이디길이제한.Name = "아이디길이제한";
            아이디길이제한.Size = new Size(158, 14);
            아이디길이제한.TabIndex = 12;
            아이디길이제한.Text = "영문,숫자를 조합해 16자이내로 작성";
            // 
            // 비밀번호제한
            // 
            비밀번호제한.AutoSize = true;
            비밀번호제한.Location = new Point(443, 216);
            비밀번호제한.Name = "비밀번호제한";
            비밀번호제한.Size = new Size(128, 14);
            비밀번호제한.TabIndex = 13;
            비밀번호제한.Text = "영문,숫자를 포함한 64자이내";
            // 
            // NickNameCheck
            // 
            NickNameCheck.ForeColor = Color.Black;
            NickNameCheck.Location = new Point(375, 123);
            NickNameCheck.Name = "NickNameCheck";
            NickNameCheck.Size = new Size(62, 23);
            NickNameCheck.TabIndex = 14;
            NickNameCheck.Text = "닉네임확인";
            NickNameCheck.UseVisualStyleBackColor = true;
            NickNameCheck.Click += NickNameCheck_Click;
            // 
            // UserIDcheck
            // 
            UserIDcheck.ForeColor = Color.Black;
            UserIDcheck.Location = new Point(375, 168);
            UserIDcheck.Name = "UserIDcheck";
            UserIDcheck.Size = new Size(62, 23);
            UserIDcheck.TabIndex = 16;
            UserIDcheck.Text = "아이디확인";
            UserIDcheck.UseVisualStyleBackColor = true;
            UserIDcheck.Click += UserIdCheck_Click;
            // 
            // NickNameCheckLabel
            // 
            NickNameCheckLabel.AutoSize = true;
            NickNameCheckLabel.Location = new Point(219, 148);
            NickNameCheckLabel.Name = "NickNameCheckLabel";
            NickNameCheckLabel.Size = new Size(0, 14);
            NickNameCheckLabel.TabIndex = 17;
            // 
            // UserIDCheckLabel
            // 
            UserIDCheckLabel.AutoSize = true;
            UserIDCheckLabel.Location = new Point(219, 193);
            UserIDCheckLabel.Name = "UserIDCheckLabel";
            UserIDCheckLabel.Size = new Size(0, 14);
            UserIDCheckLabel.TabIndex = 18;
            // 
            // PasswordLabel2
            // 
            PasswordLabel2.AutoSize = true;
            PasswordLabel2.Location = new Point(221, 238);
            PasswordLabel2.Name = "PasswordLabel2";
            PasswordLabel2.Size = new Size(0, 14);
            PasswordLabel2.TabIndex = 19;
            // 
            // PasswordCheckLabel2
            // 
            PasswordCheckLabel2.AutoSize = true;
            PasswordCheckLabel2.Location = new Point(219, 283);
            PasswordCheckLabel2.Name = "PasswordCheckLabel2";
            PasswordCheckLabel2.Size = new Size(0, 14);
            PasswordCheckLabel2.TabIndex = 20;
            // 
            // MemberRegister
            // 
            AccessibleDescription = "";
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(30, 30, 30);
            ClientSize = new Size(704, 441);
            Controls.Add(PasswordCheckLabel2);
            Controls.Add(PasswordLabel2);
            Controls.Add(UserIDCheckLabel);
            Controls.Add(NickNameCheckLabel);
            Controls.Add(UserIDcheck);
            Controls.Add(NickNameCheck);
            Controls.Add(비밀번호제한);
            Controls.Add(아이디길이제한);
            Controls.Add(닉네임글자제한);
            Controls.Add(MemberRegisterLabel);
            Controls.Add(TxtUserNickName);
            Controls.Add(UserNamelabel);
            Controls.Add(TxtPasswordCheck);
            Controls.Add(TxtPassword);
            Controls.Add(TxtUserID);
            Controls.Add(PassWordChecklabel);
            Controls.Add(PassWordLabel);
            Controls.Add(IDlabel);
            Controls.Add(MemberRegisterCancelButton);
            Controls.Add(MemberRegisterButton);
            Font = new Font("Roboto", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximumSize = new Size(1080, 720);
            MinimumSize = new Size(720, 480);
            Name = "MemberRegister";
            Text = "Climing";
            FormClosing += MemberRegister_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button MemberRegisterButton;
        private Button MemberRegisterCancelButton;
        private Label IDlabel;
        private Label PassWordLabel;
        private Label PassWordChecklabel;
        private TextBox TxtUserID;
        private TextBox TxtPassword;
        private TextBox TxtPasswordCheck;
        private TextBox TxtUserNickName;
        private Label UserNamelabel;
        private Label MemberRegisterLabel;
        private Label 비밀번호제한;
        private Label 아이디길이제한;
        private Label 닉네임글자제한;
        private Button NickNameCheck;
        private Button UserIDcheck;
        private Label NickNameCheckLabel;
        private Label UserIDCheckLabel;
        private Label PasswordLabel2;
        private Label PasswordCheckLabel2;
    }
}