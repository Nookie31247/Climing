using System.Diagnostics;
using System.Windows.Forms;
using CppCode;

namespace UI
{
    public partial class MainMenu : Form
    {
        /// <summary>
        /// 서비스할 게임 리스트를 저장합니다.
        /// </summary>
        List<Game>? gameList;

        /// <summary>
        /// 로그인한 유저의 ID를 저장합니다.
        /// </summary>
        string userId;

        /// <summary>
        /// 로그인한 유저의 닉네임을 저장합니다.
        /// </summary>
        string nickname;

        public MainMenu(string userId, string nickname)
        {
            InitializeComponent();
            this.userId = userId;           // 로그인 시 입력받은 ID를 필드에 저장
            this.nickname = nickname;       // 로그인 시 서버로부터 받아온 닉네임을 필드에 저장
            nicknameLabel.Text = nickname;
            Icon = new Icon(Path.Combine(Application.StartupPath, "Resources", "climbing_icon.ico"));
            SetUserInfoButton();
            LoadGame();
        }

        /// <summary>
        /// 서버로부터 게임 데이터를 받아와서 List Pannel로 정렬하는 기능
        /// </summary>
        private void LoadGame()
        {
            gameListPannel.AutoScroll = true;                               // 스크롤바 자동 표시 (내용이 많을 경우)

            gameList = ClientSocket.getGameList();      // C++/CLI에서 데이터 받아오기

            // 리스트가 비어 있으면 메소드 종료
            if (gameList.Count == 0)
            {
                MessageBox.Show("현재 서비스중인 게임이 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // GameCard를 gameListPannel에 추가
            foreach (Game game in gameList)
            {
                GameCard card = new GameCard();
                if (card.SetGame(game))
                    gameListPannel.Controls.Add(card);
                else
                {
                    MessageBox.Show("드라이브 마운트에 실패했습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }
            }
        }

        private void UserInfoButton_Click(Object sender, EventArgs e)
        {
            UserInfo info = new UserInfo(userId, nickname);
            info.ShowDialog();
            if (info.GetCloseMainForm())
            {
                this.Close();
            }
        }

        private void SetUserInfoButton()
        {
            userInfoButton.FlatStyle = FlatStyle.Flat;
            userInfoButton.FlatAppearance.BorderSize = 0;
            userInfoButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            userInfoButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            userInfoButton.BackColor = Color.Transparent;
            userInfoButton.ForeColor = Color.White; // 필요 시 글자색 조절
            userInfoButton.TabStop = false;
        }

        private void FocusOut(object sender, EventArgs e)
        {
            ActiveControl = null; // 현재 포커스 제거
        }
    }
}
