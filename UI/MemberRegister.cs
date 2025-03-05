using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class MemberRegister : Form
    {
        public MemberRegister()
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

        private void MemberRegisterCancelButton_Click(object sender, EventArgs e)
        {

            MemberLogin memberLogin = new MemberLogin();
            memberLogin.Show(); // 새로운 폼을 엽니다.
            this.Close(); // 현재 폼을 숨깁니다.

        }

        private void MemberRegisterButton_Click(object sender, EventArgs e)
        {
            // 회원가입 완료
            MessageBox.Show("회원가입 성공!");
        }

        private void process1_Exited(object sender, EventArgs e)
        {

        }

        private void process1_ErrorDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {

        }

        private void 닉네임글자제한_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
