using System.Diagnostics;
using System.Text.RegularExpressions;
using CppCode;
using Microsoft.VisualBasic.ApplicationServices;

namespace UI
{
    public partial class MemberLogin : Form
    {
        /// <summary>
        /// 로그인 시 유저 아이디를 저장
        /// </summary>
        private string curUserId = "";

        /// <summary>
        /// 로그인 시 서버로부터 부여받은 유저 접속 번호를 저장
        /// </summary>
        private int userNum;

        public MemberLogin()
        {
            InitializeComponent(); // 폼의 버튼, 라벨을 저장
            this.StartPosition = FormStartPosition.Manual; // 시작위치 수동 설정
            this.Location = new Point(500, 250); // 폼 위치 설정
            ActiveControl = null; // 현재 포커스 제거
            Icon = new Icon(Path.Combine(Application.StartupPath, "Resources", "climbing_icon.ico"));
        }

        /// <summary>
        /// 사용자가 로그인 버튼 클릭 시 로그인 성공 폼으로 이동하는 기능
        /// </summary>
        private void MemberLoginButton_Click(object sender, EventArgs e)
        {
            string userID = UserId.Text.Trim();
            string password = PasswordWrite.Text.Trim();
            if (userID == "아이디 입력")
                userID = "";
            if (password == "비밀번호 입력")
                password = "";

            // 아이디가 입력되지 않았을 시 예외 처리
            if (string.IsNullOrWhiteSpace(userID))
            {
                MessageBox.Show("아이디가 입력되지 않았습니다.", "아이디 입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 비밀번호가 입력되지 않았을 시 예외 처리
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("비밀번호가 입력되지 않았습니다.", "비밀번호 입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 잘못된 형식의 아이디가 입력되었을 시 예외 처리
            if (!Regex.IsMatch(userID, @"^[a-zA-Z0-9]{6,16}$"))
            {
                MessageBox.Show("6~16글자 사이의 영어와 숫자를 사용하여 ID를 입력해 주세요", "아이디 입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 잘못된 형식의 비밀번호가 입력되었을 시 예외 처리
            if (!Regex.IsMatch(password, @"^[a-zA-Z0-9!@#$%^&*()-_=+[\]{}|;:'"",.<>?/]{8,64}$"))
            {
                MessageBox.Show("8-64글자 사이의 영어, 숫자, 특수기호만를 사용하여 비밀번호를 입력해 주세요", "비밀번호 입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadingForm loadingForm = null;
            Thread loadingThread = new Thread(() =>
            {
                loadingForm = new LoadingForm("로그인 중입니다.");
                loadingForm.StartPosition = FormStartPosition.CenterScreen;
                Application.Run(loadingForm);
            });

            loadingThread.SetApartmentState(ApartmentState.STA);
            loadingThread.Start();
            KeyValuePair<int, string> loginResult = ClientSocket.login(userID, password);

            if (loadingForm != null && loadingForm.IsHandleCreated)
            {
                loadingForm.Invoke(new Action(() =>
                {
                    loadingForm.Close();
                }));
            }
            // 로그인 과정에서 에러가 발생한 경우
            if (loginResult.Key < 0)
            {
                // 에러 코드에 맞는 에러 메시지 출력
                MessageBox.Show(PrintError.Print(loginResult.Key), "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 로그인 성공 시 유저의 ID와 유저 접속 번호를 필드에 저장
            curUserId = userID;
            userNum = loginResult.Key;
            string userNickName = loginResult.Value;

            // 유저의 ID와 닉네임을 메인 메뉴 폼으로 전달하여 메인 화면 실행
            MainMenu mainMenu = new MainMenu(userID, userNickName);
            this.Hide();
            mainMenu.ShowDialog();

            // 메인 메뉴 폼이 종료될 시 로그아웃 실행
            Logout();
        }

        private void PressEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MemberLoginButton_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// 로그아웃을 실행하는 함수
        /// </summary>
        private void Logout()
        {
            // 소켓 통신을 통해 로그아웃 요청을 서버로 전송

            LoadingForm loadingForm = null;
            Thread loadingThread = new Thread(() =>
            {
                loadingForm = new LoadingForm("로그아웃 중입니다.");
                loadingForm.StartPosition = FormStartPosition.CenterScreen;
                Application.Run(loadingForm);
            });

            loadingThread.SetApartmentState(ApartmentState.STA);
            loadingThread.Start();
            int socketResult = ClientSocket.logout(curUserId, userNum);

            if (loadingForm != null && loadingForm.IsHandleCreated)
            {
                loadingForm.Invoke(new Action(() =>
                {
                    loadingForm.Close();
                }));
            }

            if (socketResult != 0)
            {
                // 로그아웃 실패 시 에러 메시지 출력
                MessageBox.Show(PrintError.Print(socketResult), "로그아웃 과정에서 오류가 발생했습니다.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            UserId.Text = "";
            PasswordWrite.Text = "";
            this.Show();
        }

        /// <summary>
        /// 사용자가 회원가입 버튼 클릭시 회원가입 폼으로 이동하는 기능 
        /// </summary>>
        private void MemberRegisterButton_Click(object sender, EventArgs e)
        {
            MemberRegister memberRegister = new MemberRegister();
            this.Hide();
            memberRegister.ShowDialog();    // 새로운 폼을 엽니다.
            UserId.Text = "";
            PasswordWrite.Text = "";
            this.Show();                    // 현재 폼을 숨깁니다.
        }

        /// <summary>
        /// 사용자가 프로그램 종료 버튼을 클릭하면 나오는 기능
        /// </summary>
        private void ProgramExit_Click(object sender, EventArgs e)
        {
            // 종료 확인 메시지 박스 표시
            // '예' 버튼 클릭 시 프로세스를 종료합니다.
            if (MessageBox.Show("정말로 종료하시겠습니까?", "프로그램 종료",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string processName = "Climbing Game Streaming";          // 종료할 프로세스 이름
                try
                {
                    // 해당 프로세스 종료
                    foreach (var process in Process.GetProcessesByName(processName))
                    {
                        process.Kill();             // 프로세스 종료
                        process.WaitForExit();      // 프로세스 종료 대기
                        Application.Exit();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"프로세스를 종료하는데 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 사용자가 프로그램 닫기 X를 눌렀을때 나오는 기능
        /// </summary>
        private void MemberLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 종료 확인 메시지 박스 표시
            // '예' 버튼 클릭 시 프로세스를 종료합니다.
            if (MessageBox.Show("정말로 종료하시겠습니까?", "프로그램 종료",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string processName = "Climbing Game Streaming";          // 종료할 프로세스 이름
                try
                {
                    // 해당 프로세스 종료
                    foreach (var process in Process.GetProcessesByName(processName))
                    {
                        process.Kill();             // 프로세스 종료
                        process.WaitForExit();      // 프로세스 종료 대기
                        Application.Exit();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"프로세스를 종료하는데 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // '아니오' 버튼 클릭 시 폼 닫기를 취소
                e.Cancel = true; // 폼을 그대로 유지
            }
        }

        /// <summary>
        /// iSCSI 이니시에이터 설정을 초기화합니다.
        /// </summary>
        private void ClearDisk_Click(object sender, EventArgs e)
        {
            LoadingForm loadingForm = null;
            Thread loadingThread = new Thread(() =>
            {
                loadingForm = new LoadingForm("디스크 초기화 중입니다.");
                loadingForm.StartPosition = FormStartPosition.CenterScreen;
                Application.Run(loadingForm);
            });

            loadingThread.SetApartmentState(ApartmentState.STA);
            loadingThread.Start();
            ISCSI.clearSettings();

            if (loadingForm != null && loadingForm.IsHandleCreated)
            {
                loadingForm.Invoke(new Action(() =>
                {
                    loadingForm.Close();
                }));
            }

            MessageBox.Show("디스크 초기화가 완료되었습니다.");
        }

        private void FocusOut(object sender, EventArgs e)
        {
            ActiveControl = null; // 현재 포커스 제거
        }

        private void PasswordWrite_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
