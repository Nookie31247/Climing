using Microsoft.VisualBasic.ApplicationServices;

namespace UI
{
    partial class UserInfo
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
            NicknameLabel = new Label();
            IdLabel = new Label();
            UnregisteraLabel = new Label();
            unregisterTextBox = new TextBox();
            CloseButton = new Button();
            logoutButton = new Button();
            unregisterButton = new Button();
            SuspendLayout();
            // 
            // NicknameLabel
            // 
            NicknameLabel.AutoSize = true;
            NicknameLabel.BackColor = Color.Transparent;
            NicknameLabel.Font = new Font("Microsoft Sans Serif", 28F);
            NicknameLabel.ForeColor = Color.White;
            NicknameLabel.Location = new Point(43, 50);
            NicknameLabel.Margin = new Padding(4, 0, 4, 0);
            NicknameLabel.Name = "NicknameLabel";
            NicknameLabel.Size = new Size(168, 64);
            NicknameLabel.TabIndex = 0;
            NicknameLabel.Text = "닉네임";
            // 
            // IdLabel
            // 
            IdLabel.AutoSize = true;
            IdLabel.BackColor = Color.Transparent;
            IdLabel.Font = new Font("Microsoft Sans Serif", 16F);
            IdLabel.ForeColor = Color.White;
            IdLabel.Location = new Point(43, 123);
            IdLabel.Margin = new Padding(4, 0, 4, 0);
            IdLabel.Name = "IdLabel";
            IdLabel.Size = new Size(92, 37);
            IdLabel.TabIndex = 1;
            IdLabel.Text = "아이디";
            // 
            // UnregisteraLabel
            // 
            UnregisteraLabel.AutoSize = true;
            UnregisteraLabel.BackColor = Color.Transparent;
            UnregisteraLabel.Font = new Font("Microsoft Sans Serif", 20F);
            UnregisteraLabel.ForeColor = Color.White;
            UnregisteraLabel.Location = new Point(43, 418);
            UnregisteraLabel.Margin = new Padding(4, 0, 4, 0);
            UnregisteraLabel.Name = "UnregisteraLabel";
            UnregisteraLabel.Size = new Size(152, 46);
            UnregisteraLabel.TabIndex = 2;
            UnregisteraLabel.Text = "회원탈퇴";
            // 
            // unregisterTextBox
            // 
            unregisterTextBox.Font = new Font("맑은 고딕", 12F);
            unregisterTextBox.Location = new Point(199, 428);
            unregisterTextBox.Margin = new Padding(4, 5, 4, 5);
            unregisterTextBox.Name = "unregisterTextBox";
            unregisterTextBox.PlaceholderText = "비밀번호 입력";
            unregisterTextBox.Size = new Size(227, 39);
            unregisterTextBox.TabIndex = 5;
            unregisterTextBox.UseSystemPasswordChar = true;
            // 
            // CloseButton
            // 
            CloseButton.BackColor = Color.Silver;
            CloseButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            CloseButton.ForeColor = SystemColors.WindowText;
            CloseButton.Location = new Point(610, 512);
            CloseButton.Margin = new Padding(4, 5, 4, 5);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(179, 67);
            CloseButton.TabIndex = 8;
            CloseButton.Text = "닫기";
            CloseButton.UseVisualStyleBackColor = false;
            CloseButton.Click += CloseButton_Click;
            // 
            // logoutButton
            // 
            logoutButton.BackColor = Color.Silver;
            logoutButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            logoutButton.ForeColor = SystemColors.WindowText;
            logoutButton.Location = new Point(610, 57);
            logoutButton.Margin = new Padding(4, 5, 4, 5);
            logoutButton.Name = "logoutButton";
            logoutButton.Size = new Size(179, 67);
            logoutButton.TabIndex = 9;
            logoutButton.Text = "로그아웃";
            logoutButton.UseVisualStyleBackColor = false;
            logoutButton.Click += LogoutButton_Click;
            // 
            // unregisterButton
            // 
            unregisterButton.BackColor = Color.Silver;
            unregisterButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            unregisterButton.ForeColor = SystemColors.WindowText;
            unregisterButton.Location = new Point(610, 418);
            unregisterButton.Margin = new Padding(4, 5, 4, 5);
            unregisterButton.Name = "unregisterButton";
            unregisterButton.Size = new Size(179, 67);
            unregisterButton.TabIndex = 10;
            unregisterButton.Text = "탈퇴하기";
            unregisterButton.UseVisualStyleBackColor = false;
            unregisterButton.Click += UnregisterButton_Click;
            // 
            // UserInfo
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(32, 31, 39);
            ClientSize = new Size(834, 602);
            Controls.Add(unregisterButton);
            Controls.Add(logoutButton);
            Controls.Add(CloseButton);
            Controls.Add(unregisterTextBox);
            Controls.Add(UnregisteraLabel);
            Controls.Add(IdLabel);
            Controls.Add(NicknameLabel);
            Margin = new Padding(4, 5, 4, 5);
            Name = "UserInfo";
            Text = "Settings";
            Click += FocusOut;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label NicknameLabel;
        private Label IdLabel;
        private Label UnregisteraLabel;
        private TextBox unregisterTextBox;
        private Button CloseButton;
        private Button logoutButton;
        private Button unregisterButton;
    }
}