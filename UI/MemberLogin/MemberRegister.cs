using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;

namespace Climing
{
    public partial class MemberRegister : Form
    {


        public MemberRegister()
        {

            InitializeComponent(); // 폼의 버튼, 라벨을 저장
            InitializePlaceholder(); // 텍스트 박스 기본 글자 저장
            this.StartPosition = FormStartPosition.Manual; // 시작위치 수동 설정
            this.Location = new Point(500, 250); // 폼 위치 설정
        }



        private static void SetPlaceholder(TextBox textBox, string placeholder) //로그인,회원가입 폼 로드시 텍스트 박스 기본 글자 설정하는 기능
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
        private void NickNameCheck_Click(object sender, EventArgs e)// 닉네임 확인을 누를시 나오는 기능
        {
            string nickname = TxtUserNickName.Text.Trim();

            // 닉네임 비어 있는지 확인
            if (string.IsNullOrEmpty(nickname))
            {
                NickNameCheckLabel.Text = "닉네임을 입력하세요.";
                NickNameCheckLabel.ForeColor = Color.Red;
                return;
            }

            // 닉네임 길이 확인 (문자 길이)
            if (nickname.Length > 16)
            {
                NickNameCheckLabel.Text = "닉네임이 16자보다 큽니다.";
                NickNameCheckLabel.ForeColor = Color.Red;
                return;
            }

            // 닉네임의 바이트 수 확인 (UTF-8)
            byte[] nicknameBytes = System.Text.Encoding.UTF8.GetBytes(nickname);
            if (nicknameBytes.Length > 16)
            {
                NickNameCheckLabel.Text = "닉네임이 16바이트를 초과합니다.";
                NickNameCheckLabel.ForeColor = Color.Red;
                return;
            }

            // 닉네임 확인 (데이터베이스 연결 후 기능 사용)
            //if (UserDatabase.Contains(nickname)) // UserDatabase는 데이터베이스 연결 객체로 가정
            //{
            //    NickNameCheckLabel.Text = "이미 사용 중인 닉네임입니다.";
            //    NickNameCheckLabel.ForeColor = Color.Red;
            //}
            //else // 데이터베이스에 중복 닉네임이 없으면 작성한 닉네임 사용 가능!
            //{
            //    NickNameCheckLabel.Text = "사용 가능한 닉네임입니다.";
            //    NickNameCheckLabel.ForeColor = Color.Green; // 사용 가능 메시지를 초록색으로 표시
            //}
        }
        private void UserIdCheck_Click(object sender, EventArgs e) // 아이디 확인을 누를 시 나오는 기능
        {
            string userId = UserIDcheck.Text.Trim();

            // 아이디 비어 있는지 확인
            if (string.IsNullOrEmpty(userId))
            {
                UserIDCheckLabel.Text = "아이디를 입력하세요.";
                UserIDCheckLabel.ForeColor = Color.Red;
                return;
            }

            // 아이디 길이 확인 (UTF-8로 인코딩하여 바이트 배열로 변환)
            byte[] userIdBytes = System.Text.Encoding.UTF8.GetBytes(userId);

            // 바이트 수 확인
            if (userIdBytes.Length > 16)
            {
                UserIDCheckLabel.Text = "UserId가 16바이트를 초과합니다.";
                UserIDCheckLabel.ForeColor = Color.Red;
                return;
            }

            // 아이디 확인 (데이터베이스 연결 후 기능 사용)
            //if (UserDatabase.Contains(userId)) // UserDatabase는 데이터베이스 연결 객체로 가정
            //{
            //    UserIDCheckLabel.Text = "이미 사용 중인 아이디입니다.";
            //    UserIDCheckLabel.ForeColor = Color.Red;
            //}
            //else // 데이터베이스에 중복 아이디가 없으면 작성한 아이디 사용 가능!
            //{
            //    UserIDCheckLabel.Text = "사용 가능한 아이디입니다.";
            //    UserIDCheckLabel.ForeColor = Color.Green; // 사용 가능 메시지를 초록색으로 표시
            //}
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

            // 닉네임 중복 확인 
            //List<string> existingNicknames = new List<string> { "user1", "user2" }; // 기존 닉네임 예시
            //if (existingNicknames.Contains(nickname))
            //{
            //    MessageBox.Show("이미 사용 중인 닉네임입니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            // 회원가입 정보 저장 (여기서는 예시로 콘솔에 출력)
            // 실제로는 데이터베이스에 저장하는 로직을 추가
            Console.WriteLine($"아이디: {userId}, 비밀번호: {password}, 닉네임: {nickname}");

            // 회원가입 성공 메시지
            MessageBox.Show("회원가입 성공!", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 폼 클리어 (텍스트박스 초기화)
            TxtUserNickName.Clear();
            TxtUserID.Clear();
            TxtPassword.Clear();
            TxtPasswordCheck.Clear();
        }// 회원가입 버튼을 누를시 나오는 기능
        private void MemberRegisterCancelButton_Click(object sender, EventArgs e)
        {
            MemberLogin memberLogin = new MemberLogin();
            memberLogin.Show(); // 로그인 폼을 보여줍니다.

            this.Hide(); // 현재 폼을 숨깁니다.
        }// 사용자가 회원가입을 하지않고 취소했을시 기능

        private void MemberRegister_FormClosing(object sender, FormClosingEventArgs e)
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
            else
            {
                // '아니오' 버튼 클릭 시 폼 닫기를 취소
                e.Cancel = true; // 폼을 그대로 유지
            }
        }// 사용자가 프로그램 닫기 X를 눌렀을때 나오는 기능

    }
}
