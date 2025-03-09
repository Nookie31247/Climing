
namespace Climing
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
            SetPlaceholder(UserId, "아이디 입력");
            SetPlaceholder(PasswordWrite, "비밀번호 입력");
        }


        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        /// 
        private void InitializeComponent()
        {
            UserId = new TextBox();
            PasswordWrite = new TextBox();
            Login = new Button();
            Password = new Label();
            ID = new Label();
            LoginLabel = new Label();
            MemberRegister = new Button();
            ProgramExit = new Button();
            Climing = new Label();
            SuspendLayout();
            // 
            // UserId
            // 
            UserId.Location = new Point(274, 137);
            UserId.Margin = new Padding(5, 6, 5, 6);
            UserId.Name = "UserId";
            UserId.PlaceholderText = "아이디 입력";
            UserId.Size = new Size(169, 35);
            UserId.TabIndex = 0;
            UserId.Click += UserId_Enter;
            UserId.DoubleClick += UserId_Enter;
            UserId.Enter += UserId_Enter;
            UserId.Leave += UserId_Leave;
            // 
            // PasswordWrite
            // 
            PasswordWrite.Location = new Point(274, 187);
            PasswordWrite.Margin = new Padding(5, 6, 5, 6);
            PasswordWrite.Name = "PasswordWrite";
            PasswordWrite.PlaceholderText = "비밀번호 입력";
            PasswordWrite.RightToLeft = RightToLeft.No;
            PasswordWrite.Size = new Size(169, 35);
            PasswordWrite.TabIndex = 1;
            PasswordWrite.Click += PasswordWrite_Enter;
            PasswordWrite.DoubleClick += PasswordWrite_Enter;
            PasswordWrite.Enter += PasswordWrite_Enter;
            PasswordWrite.Leave += PasswordWrite_Leave;
            // 
            // Login
            // 
            Login.AccessibleRole = AccessibleRole.None;
            Login.BackColor = Color.Orange;
            Login.ForeColor = Color.Black;
            Login.Location = new Point(343, 243);
            Login.Margin = new Padding(5, 6, 5, 6);
            Login.Name = "Login";
            Login.Size = new Size(100, 35);
            Login.TabIndex = 2;
            Login.Text = "로그인";
            Login.UseVisualStyleBackColor = false;
            Login.Click += MemberLoginButton_Click;
            // 
            // Password
            // 
            Password.AutoSize = true;
            Password.Font = new Font("맑은 고딕", 15.75F);
            Password.ForeColor = SystemColors.ButtonHighlight;
            Password.Location = new Point(165, 187);
            Password.Margin = new Padding(5, 0, 5, 0);
            Password.Name = "Password";
            Password.Size = new Size(102, 30);
            Password.TabIndex = 3;
            Password.Text = "비밀번호:";
            // 
            // ID
            // 
            ID.AutoSize = true;
            ID.Font = new Font("맑은 고딕", 15.75F);
            ID.ForeColor = SystemColors.ButtonHighlight;
            ID.Location = new Point(186, 140);
            ID.Margin = new Padding(5, 0, 5, 0);
            ID.Name = "ID";
            ID.Size = new Size(81, 30);
            ID.TabIndex = 4;
            ID.Text = "아이디:";
            // 
            // LoginLabel
            // 
            LoginLabel.AutoSize = true;
            LoginLabel.Font = new Font("맑은 고딕", 24F, FontStyle.Regular, GraphicsUnit.Point, 129);
            LoginLabel.Location = new Point(297, 86);
            LoginLabel.Margin = new Padding(5, 0, 5, 0);
            LoginLabel.Name = "LoginLabel";
            LoginLabel.Size = new Size(116, 45);
            LoginLabel.TabIndex = 5;
            LoginLabel.Text = "로그인";
            // 
            // MemberRegister
            // 
            MemberRegister.BackColor = Color.FromArgb(255, 128, 0);
            MemberRegister.ForeColor = Color.Black;
            MemberRegister.Location = new Point(343, 287);
            MemberRegister.Name = "MemberRegister";
            MemberRegister.Size = new Size(115, 35);
            MemberRegister.TabIndex = 6;
            MemberRegister.Text = "회원가입";
            MemberRegister.UseVisualStyleBackColor = false;
            MemberRegister.Click += MemberRegisterButton_Click;
            // 
            // ProgramExit
            // 
            ProgramExit.BackColor = Color.Blue;
            ProgramExit.Font = new Font("맑은 고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            ProgramExit.Location = new Point(288, 348);
            ProgramExit.Name = "ProgramExit";
            ProgramExit.Size = new Size(125, 40);
            ProgramExit.TabIndex = 7;
            ProgramExit.Text = "프로그램 종료";
            ProgramExit.UseVisualStyleBackColor = false;
            ProgramExit.Click += ProgramExit_Click;
            // 
            // Climing
            // 
            Climing.AutoSize = true;
            Climing.Font = new Font("맑은 고딕", 24F, FontStyle.Regular, GraphicsUnit.Point, 129);
            Climing.Location = new Point(283, 19);
            Climing.Name = "Climing";
            Climing.Size = new Size(130, 45);
            Climing.TabIndex = 8;
            Climing.Text = "Climing";
            // 
            // MemberLogin
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(30, 30, 30);
            ClientSize = new Size(704, 441);
            Controls.Add(Climing);
            Controls.Add(ProgramExit);
            Controls.Add(MemberRegister);
            Controls.Add(LoginLabel);
            Controls.Add(ID);
            Controls.Add(Password);
            Controls.Add(Login);
            Controls.Add(PasswordWrite);
            Controls.Add(UserId);
            Font = new Font("맑은 고딕", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(5, 6, 5, 6);
            Name = "MemberLogin";
            Text = "Climing";
            FormClosing += MemberLogin_FormClosing;
            ResumeLayout(false);
            PerformLayout();

        }


        #endregion

        private TextBox UserId;
        private TextBox PasswordWrite;
        private Button Login;
        private Label Password;
        private Label ID;
        private Label LoginLabel;
        private Button MemberRegister;
        private Button ProgramExit;
        private Label Climing;
    }
}
