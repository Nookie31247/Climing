namespace UI
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MemberRegister));
            MemberRegisterButton = new Button();
            MemberRegisterCancelButton = new Button();
            TxtUserID = new TextBox();
            TxtPassword = new TextBox();
            TxtPasswordCheck = new TextBox();
            TxtUserNickName = new TextBox();
            PasswordLabel2 = new Label();
            SuspendLayout();
            // 
            // MemberRegisterButton
            // 
            MemberRegisterButton.BackColor = Color.FromArgb(224, 224, 224);
            MemberRegisterButton.Font = new Font("Microsoft Sans Serif", 8.999999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            MemberRegisterButton.ForeColor = Color.Black;
            MemberRegisterButton.Location = new Point(133, 732);
            MemberRegisterButton.Name = "MemberRegisterButton";
            MemberRegisterButton.Size = new Size(90, 95);
            MemberRegisterButton.TabIndex = 4;
            MemberRegisterButton.Text = "회원가입";
            MemberRegisterButton.UseVisualStyleBackColor = false;
            MemberRegisterButton.Click += MemberRegisterButton_Click;
            // 
            // MemberRegisterCancelButton
            // 
            MemberRegisterCancelButton.BackColor = Color.FromArgb(255, 192, 192);
            MemberRegisterCancelButton.Font = new Font("Microsoft Sans Serif", 8.999999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            MemberRegisterCancelButton.ForeColor = Color.Black;
            MemberRegisterCancelButton.Location = new Point(252, 732);
            MemberRegisterCancelButton.Name = "MemberRegisterCancelButton";
            MemberRegisterCancelButton.Size = new Size(90, 95);
            MemberRegisterCancelButton.TabIndex = 5;
            MemberRegisterCancelButton.Text = "취소";
            MemberRegisterCancelButton.UseVisualStyleBackColor = false;
            MemberRegisterCancelButton.Click += MemberRegisterCancelButton_Click;
            // 
            // TxtUserID
            // 
            TxtUserID.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TxtUserID.Location = new Point(164, 448);
            TxtUserID.Name = "TxtUserID";
            TxtUserID.PlaceholderText = "아이디 입력";
            TxtUserID.Size = new Size(150, 26);
            TxtUserID.TabIndex = 1;
            TxtUserID.KeyDown += PressEnter;
            // 
            // TxtPassword
            // 
            TxtPassword.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TxtPassword.Location = new Point(164, 545);
            TxtPassword.Name = "TxtPassword";
            TxtPassword.PlaceholderText = "비밀번호 입력";
            TxtPassword.Size = new Size(150, 26);
            TxtPassword.TabIndex = 2;
            TxtPassword.UseSystemPasswordChar = true;
            TxtPassword.KeyDown += PressEnter;
            // 
            // TxtPasswordCheck
            // 
            TxtPasswordCheck.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TxtPasswordCheck.Location = new Point(164, 643);
            TxtPasswordCheck.Name = "TxtPasswordCheck";
            TxtPasswordCheck.PlaceholderText = "비밀번호 확인";
            TxtPasswordCheck.Size = new Size(150, 26);
            TxtPasswordCheck.TabIndex = 3;
            TxtPasswordCheck.UseSystemPasswordChar = true;
            TxtPasswordCheck.KeyDown += PressEnter;
            // 
            // TxtUserNickName
            // 
            TxtUserNickName.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TxtUserNickName.Location = new Point(164, 353);
            TxtUserNickName.Name = "TxtUserNickName";
            TxtUserNickName.PlaceholderText = "닉네임 입력";
            TxtUserNickName.Size = new Size(150, 26);
            TxtUserNickName.TabIndex = 0;
            TxtUserNickName.KeyDown += PressEnter;
            // 
            // PasswordLabel2
            // 
            PasswordLabel2.AutoSize = true;
            PasswordLabel2.Location = new Point(613, 376);
            PasswordLabel2.Name = "PasswordLabel2";
            PasswordLabel2.Size = new Size(0, 15);
            PasswordLabel2.TabIndex = 19;
            // 
            // MemberRegister
            // 
            AccessibleDescription = "";
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(16, 45, 84);
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1904, 1041);
            Controls.Add(PasswordLabel2);
            Controls.Add(TxtUserNickName);
            Controls.Add(TxtPasswordCheck);
            Controls.Add(TxtPassword);
            Controls.Add(TxtUserID);
            Controls.Add(MemberRegisterCancelButton);
            Controls.Add(MemberRegisterButton);
            DoubleBuffered = true;
            Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "MemberRegister";
            Text = "Climbing Game Streaming";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button MemberRegisterButton;
        private Button MemberRegisterCancelButton;
        private TextBox TxtUserID;
        private TextBox TxtPassword;
        private TextBox TxtPasswordCheck;
        private TextBox TxtUserNickName;
        private Label PasswordLabel2;
    }
}