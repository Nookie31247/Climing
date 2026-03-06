namespace UI
{
    partial class MainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            nicknameLabel = new Label();
            gameListPannel = new FlowLayoutPanel();
            userInfoButton = new Button();
            SuspendLayout();
            // 
            // nicknameLabel
            // 
            nicknameLabel.AutoSize = true;
            nicknameLabel.BackColor = Color.Transparent;
            nicknameLabel.Font = new Font("맑은 고딕", 14F);
            nicknameLabel.ForeColor = Color.White;
            nicknameLabel.Location = new Point(1705, 38);
            nicknameLabel.Name = "nicknameLabel";
            nicknameLabel.Size = new Size(69, 25);
            nicknameLabel.TabIndex = 13;
            nicknameLabel.Text = "닉네임";
            // 
            // gameListPannel
            // 
            gameListPannel.BackColor = Color.Transparent;
            gameListPannel.Location = new Point(9, 108);
            gameListPannel.Margin = new Padding(0);
            gameListPannel.Name = "gameListPannel";
            gameListPannel.Size = new Size(1886, 924);
            gameListPannel.TabIndex = 17;
            // 
            // userInfoButton
            // 
            userInfoButton.BackColor = Color.White;
            userInfoButton.Location = new Point(1640, 18);
            userInfoButton.Name = "userInfoButton";
            userInfoButton.Size = new Size(60, 60);
            userInfoButton.TabIndex = 18;
            userInfoButton.UseVisualStyleBackColor = false;
            userInfoButton.Click += UserInfoButton_Click;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1904, 1041);
            Controls.Add(gameListPannel);
            Controls.Add(userInfoButton);
            Controls.Add(nicknameLabel);
            Name = "MainMenu";
            Text = "Climbing Game Streaming";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label nicknameLabel;
        private FlowLayoutPanel gameListPannel;
        private Button userInfoButton;
    }
}