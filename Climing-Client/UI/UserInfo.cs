using CppCode;

namespace UI
{
    public partial class UserInfo : Form
    {
        /// <summary>
        /// 로그인한 유저의 ID를 저장합니다.
        /// </summary>
        string userId;

        /// <summary>
        /// 로그인한 유저의 닉네임을 저장합니다.
        /// </summary>
        string nickname;

        /// <summary>
        /// 창이 종료될 때 기존 창에 특정 정보를 전달합니다.
        /// </summary>
        bool closeMainForm;

        public UserInfo(string userId, string nickname)
        {
            InitializeComponent();
            Icon = new Icon(Path.Combine(Application.StartupPath, "Resources", "climbing_icon.ico"));
            this.StartPosition = FormStartPosition.CenterParent;    // 처음 창이 보일 때 기존 창 가운데 부분에 보이도록 설정
            closeMainForm = false;
            ActiveControl = null; // 현재 포커스 제거

            this.userId = userId;           // 메인메뉴 폼에서 가져온 ID를 필드에 저장
            this.nickname = nickname;       // 메인메뉴 폼에서 가져온 닉네임을 필드에 저장
            IdLabel.Text = userId;
            NicknameLabel.Text = nickname;
        }

        public bool GetCloseMainForm()
        {
            return closeMainForm;
        }

        private void CloseButton_Click(Object sender, EventArgs e)
        {
            this.Close();
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            // 종료 확인 메시지 박스 표시
            if (MessageBox.Show("정말 로그아웃하시겠습니까?", "로그아웃",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                closeMainForm = true;
                this.Close(); 
            }
        }
        private void UnregisterButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("정말 탈퇴하시겠습니까?", "회원탈퇴",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LoadingForm loadingForm = null;
                Thread loadingThread = new Thread(() =>
                {
                    loadingForm = new LoadingForm("로그아웃 중입니다.");
                    loadingForm.StartPosition = FormStartPosition.CenterScreen;
                    Application.Run(loadingForm);
                });

                loadingThread.SetApartmentState(ApartmentState.STA);
                loadingThread.Start();
                int socketResult = ClientSocket.deleteUser(userId, unregisterTextBox.Text);

                if (loadingForm != null && loadingForm.IsHandleCreated)
                {
                    loadingForm.Invoke(new Action(() =>
                    {
                        loadingForm.Close();
                    }));
                }
                
                if (socketResult == 0)
                {
                    closeMainForm = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(PrintError.Print(socketResult), "비밀번호 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                } 
            }
        }

        private void FocusOut(object sender, EventArgs e)
        {
            ActiveControl = null; // 현재 포커스 제거
        }
    }
}
