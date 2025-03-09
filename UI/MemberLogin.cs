using System.Diagnostics;

namespace Climing
{

    public partial class MemberLogin : Form
    {
        public MemberLogin()
        {
            InitializeComponent(); // 폼의 버튼, 라벨을 저장
            InitializePlaceholder(); // 텍스트 박스 기본 글자 저장
            this.StartPosition = FormStartPosition.Manual; // 시작위치 수동 설정
            this.Location = new Point(500, 250); // 폼 위치 설정
        }

        private static void SetPlaceholder(TextBox textBox, string placeholder)// 로그인,회원가입 폼 로드시 텍스트 박스 기본 글자 설정하는 기능
        {
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;
            textBox.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
                }
            };
            textBox.Enter += (sender, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };
        }

        private void UserId_Enter(object sender, EventArgs e) // 사용자가 아이디 텍스트박스 클릭시 처리하는 기능

        {
            if (UserId.Text == "아이디 입력")
            {
                UserId.Text = "";
                UserId.ForeColor = Color.Black;
            }
        }

        private void UserId_Leave(object sender, EventArgs e) // 사용자가 아이디 텍스트박스 이탈시 처리하는 기능
        {
            if (string.IsNullOrWhiteSpace(UserId.Text))
            {
                UserId.Text = "아이디 입력";
                UserId.ForeColor = Color.Gray;
            }
        }

        private void PasswordWrite_Enter(object sender, EventArgs e) // 사용자가 비밀번호 텍스트박스 클릭시 처리하는 기능
        {
            if (PasswordWrite.Text == "비밀번호 입력")
            {
                PasswordWrite.Text = "";
                PasswordWrite.ForeColor = Color.Black;
                PasswordWrite.UseSystemPasswordChar = true; // 비밀번호 입력 시 문자를 숨김
            }
        }

        private void PasswordWrite_Leave(object sender, EventArgs e) // 사용자가 비밀번호 텍스트박스 이탈시 처리하는 기능
        {
            if (string.IsNullOrWhiteSpace(PasswordWrite.Text))
            {
                PasswordWrite.Text = "비밀번호 입력";
                PasswordWrite.ForeColor = Color.Gray;
                PasswordWrite.UseSystemPasswordChar = false; // 플레이스홀더가 보일 때는 숨김 해제
            }
        }

        private void MemberLoginButton_Click(object sender, EventArgs e) // 사용자가 로그인 버튼 클릭 시 로그인 성공 폼으로 이동하는 기능
        {

            string userID = UserId.Text.Trim();
            string password = PasswordWrite.Text.Trim();
            if (userID == "아이디 입력") userID = "";
            if (password == "비밀번호 입력") password = "";

            if (string.IsNullOrWhiteSpace(userID))
            {
                MessageBox.Show("아이디가 입력되지 않았습니다.", "아이디 입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("비밀번호가 입력되지 않았습니다.", "비밀번호 입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 아이디와 비밀번호 확인 데이터베이스 필요
            //if (userDatabase.ContainsKey(userID))
            //{
            //    if (userDatabase[userID] == password)
            //    {
            //        MessageBox.Show("로그인 성공! 환영합니다.", "로그인 성공", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    }
            //    else
            //    {
            //        MessageBox.Show("비밀번호가 틀렸습니다.", "로그인 실패", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("없는 정보입니다.", "로그인 실패", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }


        private void MemberRegisterButton_Click(object sender, EventArgs e)// 사용자가 회원가입 버튼 클릭시 회원가입 폼으로 이동하는 기능 
        {
            MemberRegister memberRegister = new MemberRegister();
            memberRegister.Show(); // 새로운 폼을 엽니다.

            this.Hide(); // 현재 폼을 숨깁니다.
        }


        private void ProgramExit_Click(object sender, EventArgs e)// 사용자가 프로그램 종료 버튼을 클릭하면 나오는 기능
        {

            // 종료 확인 메시지 박스 표시
            if (MessageBox.Show("정말로 종료하시겠습니까?", "프로그램 종료",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // '예' 버튼 클릭 시 프로세스를 종료합니다.

                string processName = "ClimingMemberLogin"; // 종료할 프로세스 이름

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

        }

        private void MemberLogin_FormClosing(object sender, FormClosingEventArgs e)// 사용자가 프로그램 닫기 X를 눌렀을때 나오는 기능
        {
            // 종료 확인 메시지 박스 표시
            if (MessageBox.Show("정말로 종료하시겠습니까?", "프로그램 종료",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // '예' 버튼 클릭 시 프로세스를 종료합니다.

                string processName = "MemberLogin"; // 여기에 종료할 프로세스 이름을 입력하세요.

                try
                {
                    // 해당 프로세스 종료
                    foreach (var process in Process.GetProcessesByName(processName))
                    {
                        process.Kill(); // 프로세스 종료
                        process.WaitForExit(); // 프로세스 종료 대기
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
    }
}
