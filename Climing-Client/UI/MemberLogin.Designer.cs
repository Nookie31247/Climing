
namespace UI
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        /// 
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MemberLogin));
            UserId = new TextBox();
            PasswordWrite = new TextBox();
            Login = new Button();
            MemberRegister = new Button();
            ProgramExit = new Button();
            ClearIscsiSettings = new Button();
            SuspendLayout();
            // 
            // UserId
            // 
            UserId.BackColor = Color.FromArgb(235, 235, 235);
            UserId.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            UserId.Location = new Point(131, 353);
            UserId.Margin = new Padding(5, 6, 5, 6);
            UserId.Name = "UserId";
            UserId.PlaceholderText = "아이디 입력";
            UserId.Size = new Size(169, 26);
            UserId.TabIndex = 0;
            UserId.KeyDown += PressEnter;
            // 
            // PasswordWrite
            // 
            PasswordWrite.BackColor = Color.FromArgb(235, 235, 235);
            PasswordWrite.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            PasswordWrite.Location = new Point(131, 431);
            PasswordWrite.Margin = new Padding(5, 6, 5, 6);
            PasswordWrite.Name = "PasswordWrite";
            PasswordWrite.PlaceholderText = "비밀번호 입력";
            PasswordWrite.RightToLeft = RightToLeft.No;
            PasswordWrite.Size = new Size(169, 26);
            PasswordWrite.TabIndex = 1;
            PasswordWrite.UseSystemPasswordChar = true;
            PasswordWrite.TextChanged += PasswordWrite_TextChanged;
            PasswordWrite.KeyDown += PressEnter;
            // 
            // Login
            // 
            Login.AccessibleRole = AccessibleRole.None;
            Login.AutoSize = true;
            Login.BackColor = Color.FromArgb(226, 226, 223);
            Login.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            Login.ForeColor = Color.Black;
            Login.Location = new Point(101, 191);
            Login.Margin = new Padding(5, 6, 5, 6);
            Login.Name = "Login";
            Login.RightToLeft = RightToLeft.No;
            Login.Size = new Size(85, 39);
            Login.TabIndex = 2;
            Login.Text = "로그인";
            Login.UseVisualStyleBackColor = false;
            Login.Click += MemberLoginButton_Click;
            // 
            // MemberRegister
            // 
            MemberRegister.AutoSize = true;
            MemberRegister.BackColor = Color.FromArgb(226, 226, 223);
            MemberRegister.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            MemberRegister.ForeColor = Color.Black;
            MemberRegister.Location = new Point(299, 191);
            MemberRegister.Name = "MemberRegister";
            MemberRegister.Size = new Size(106, 39);
            MemberRegister.TabIndex = 3;
            MemberRegister.Text = "회원가입";
            MemberRegister.UseVisualStyleBackColor = false;
            MemberRegister.Click += MemberRegisterButton_Click;
            // 
            // ProgramExit
            // 
            ProgramExit.BackColor = Color.Silver;
            ProgramExit.Font = new Font("Microsoft Sans Serif", 9.749999F, FontStyle.Bold, GraphicsUnit.Point, 129);
            ProgramExit.ForeColor = SystemColors.WindowText;
            ProgramExit.Location = new Point(61, 940);
            ProgramExit.Name = "ProgramExit";
            ProgramExit.Size = new Size(125, 40);
            ProgramExit.TabIndex = 4;
            ProgramExit.Text = "프로그램 종료";
            ProgramExit.UseVisualStyleBackColor = false;
            ProgramExit.Click += ProgramExit_Click;
            // 
            // ClearIscsiSettings
            // 
            ClearIscsiSettings.BackColor = Color.Silver;
            ClearIscsiSettings.Font = new Font("Microsoft Sans Serif", 9.749999F, FontStyle.Bold, GraphicsUnit.Point, 129);
            ClearIscsiSettings.ForeColor = SystemColors.WindowText;
            ClearIscsiSettings.Location = new Point(255, 940);
            ClearIscsiSettings.Name = "ClearIscsiSettings";
            ClearIscsiSettings.Size = new Size(125, 40);
            ClearIscsiSettings.TabIndex = 5;
            ClearIscsiSettings.Text = "디스크 초기화";
            ClearIscsiSettings.UseVisualStyleBackColor = false;
            ClearIscsiSettings.Click += ClearDisk_Click;
            // 
            // MemberLogin
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(1, 40, 73);
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1904, 1041);
            Controls.Add(ClearIscsiSettings);
            Controls.Add(ProgramExit);
            Controls.Add(MemberRegister);
            Controls.Add(Login);
            Controls.Add(PasswordWrite);
            Controls.Add(UserId);
            DoubleBuffered = true;
            Font = new Font("맑은 고딕", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            ForeColor = SystemColors.Window;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(5, 6, 5, 6);
            Name = "MemberLogin";
            Text = "Climbing Game Streaming";
            FormClosing += MemberLogin_FormClosing;
            Click += FocusOut;
            ResumeLayout(false);
            PerformLayout();

        }


        #endregion

        private TextBox UserId;
        private TextBox PasswordWrite;
        private Button Login;
        private Button MemberRegister;
        private Button ProgramExit;
        private Button ClearIscsiSettings;
    }
}
