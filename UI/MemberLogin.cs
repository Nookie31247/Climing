using System.Diagnostics;

namespace WinFormsApp2
{

    public partial class MemberLogin : Form
    {
        public MemberLogin()
        {
            InitializeComponent();
            InitializePlaceholder();
            this.StartPosition = FormStartPosition.Manual; // 수동 위치 설정
            this.Location = new Point(500, 250); // 폼 위치 설정
        }

        private void SetPlaceholder(TextBox textBox, string placeholder)
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

        private void TxtUserId_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "아이디 입력")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
                txtPassword.UseSystemPasswordChar = true; // 비밀번호 입력 시 문자를 숨김
            }
        }

        private void TxtUserId_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.Text = "아이디 입력";
                txtPassword.ForeColor = Color.Gray;
                txtPassword.UseSystemPasswordChar = false; // 플레이스홀더가 보일 때는 숨김 해제
            }
        }

        private void TxtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "비밀번호 입력")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
                txtPassword.UseSystemPasswordChar = true; // 비밀번호 입력 시 문자를 숨김
            }
        }

        private void TxtPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.Text = "비밀번호 입력";
                txtPassword.ForeColor = Color.Gray;
                txtPassword.UseSystemPasswordChar = false; // 플레이스홀더가 보일 때는 숨김 해제
            }
        }

        private void MemberLoginButton_Click(object sender, EventArgs e)
        {
            // 로그인 버튼 클릭 시 처리 로직
            string userID = txtUserId.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (userID == "아이디 입력") userID = "";
            if (password == "비밀번호 입력") password = "";

            if (string.IsNullOrWhiteSpace(userID))
            {
                MessageBox.Show("아이디가 입력되지 않았습니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("비밀번호가 입력되지 않았습니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //데이터베이스와 연결하여 데이터베이스에 아이디와 비밀번호가 일치하면 아래와 같은
            //성공문구가 뜨게해야함
            MessageBox.Show($"로그인 성공! 환영합니다.", "로그인 성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MemberRegisterButton_Click(object sender, EventArgs e)
        {

            MemberRegister register = new MemberRegister();
            register.Show(); // 새로운 폼을 엽니다.
            this.Hide(); // 현재 폼을 숨깁니다.

        }

        private void ProgramExit_Click_1(object sender, EventArgs e)
        {
            try
            {
                Process[] processes = Process.GetProcessesByName("MemberLogin"); // 로그인 프로세스
                foreach (Process process in processes)
                {
                    process.Kill(); // 프로세스 종료
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("프로세스를 종료할 수 없습니다: " + ex.Message);
            }
        }

        
    }
}
