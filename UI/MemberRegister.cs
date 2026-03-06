using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;
using System.Text.RegularExpressions;
using CppCode;

namespace UI
{
    public partial class MemberRegister : Form
    {
        ClientSocket socket = new ClientSocket();

        public MemberRegister()
        {
            InitializeComponent(); // 폼의 버튼, 라벨을 저장
            Icon = new Icon(Path.Combine(Application.StartupPath, "Resources", "climbing_icon.ico"));
            this.StartPosition = FormStartPosition.Manual; // 시작위치 수동 설정
            this.Location = new Point(500, 250); // 폼 위치 설정
            ActiveControl = null; // 현재 포커스 제거
        }

        private void MemberRegisterButton_Click(object sender, EventArgs e)
        {
            // 텍스트박스에서 입력 값을 가져옵니다.
            string nickname = TxtUserNickName.Text.Trim(); // 닉네임
            string userId = TxtUserID.Text.Trim(); // 사용자 아이디
            string password = TxtPassword.Text.Trim(); // 비밀번호
            string confirmPassword = TxtPasswordCheck.Text.Trim(); // 비밀번호 확인

            // 입력 값 검증
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(nickname))
            {
                MessageBox.Show("모든 필드를 입력하세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 비밀번호 확인
            if (password != confirmPassword)
            {
                MessageBox.Show("비밀번호가 일치하지 않습니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(userId, @"^[a-zA-Z0-9]{6,16}$"))
            {
                MessageBox.Show("6~16글자 사이의 영어와 숫자만 사용한 ID를 입력해 주세요", "아이디 입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(password, @"^[a-zA-Z0-9!@#$%^&*()-_=+[\]{}|;:'"",.<>?/]{8,64}$"))
            {
                MessageBox.Show("8-64글자 사이의 영어, 숫자, 특수기호만를 사용한 비밀번호를 입력해 주세요", "비밀번호 입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(nickname, @"^[a-zA-Z0-9가-힣]{2,16}$"))
            {
                MessageBox.Show("2-16 글자 사이의 한글, 영어, 숫자만를 사용한 닉네임을 입력해 주세요", "닉네임 입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadingForm loadingForm = null;
            Thread loadingThread = new Thread(() =>
            {
                loadingForm = new LoadingForm("회원가입 중입니다.");
                loadingForm.StartPosition = FormStartPosition.CenterScreen;
                Application.Run(loadingForm);
            });

            loadingThread.SetApartmentState(ApartmentState.STA);
            loadingThread.Start();
            int socketResult = ClientSocket.addUser(userId, password, nickname);

            if (loadingForm != null && loadingForm.IsHandleCreated)
            {
                loadingForm.Invoke(new Action(() =>
                {
                    loadingForm.Close();
                }));
            }

            if(socketResult != 0)
            {
                MessageBox.Show(PrintError.Print(socketResult), "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("회원가입이 완료되었습니다.");

            // 폼 클리어 (텍스트박스 초기화)
            TxtUserNickName.Clear();
            TxtUserID.Clear();
            TxtPassword.Clear();
            TxtPasswordCheck.Clear();

            // 회원가입이 끝나고 난후 회원가입 폼을 닫고 로그인 폼이 나오도록 설정
            this.Close();

        }// 회원가입 버튼을 누를시 나오는 기능

        private void PressEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MemberRegisterButton_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void MemberRegisterCancelButton_Click(object sender, EventArgs e)
        {
            this.Close(); // 현재 폼을 숨깁니다.
        }// 사용자가 회원가입을 하지않고 취소했을시 기능

        private void MemberRegister_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 종료 확인 메시지 박스 표시
            if (MessageBox.Show("정말로 종료하시겠습니까?", "프로그램 종료",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // '예' 버튼 클릭 시 프로세스를 종료합니다.

                string processName = "UI"; // 종료할 프로세스 이름

                try
                {
                    // 해당 프로세스 종료
                    foreach (var process in Process.GetProcessesByName(processName))
                    {
                        process.Kill(); // 프로세스 종료
                        process.WaitForExit(); // 프로세스 종료 대기

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
        }// 사용자가 프로그램 닫기 X를 눌렀을때 나오는 기능

    }
}
