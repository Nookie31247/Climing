using CppCode;
using System.Threading.Tasks.Sources;
namespace UI
{
    public partial class Form1 : Form
    {
        WireGuardVPN vpn = new WireGuardVPN();
        public Form1()
        {
            InitializeComponent();
        }

        private void connectVPNButton_Click(object sender, EventArgs e)
        {
            vpn.connectVPN(10, "xzXyB7CPZ4z2L+BK2dXNZKRLH/Mt0Cly43skf1EIGj8=", "192.168.135.1", "192.168.219.130:51820");
        }

        private void disconnectVPNButton_Click(object sender, EventArgs e)
        {
            vpn.disconnectVPN();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = vpn.getClientPublicKey();
            MessageBox.Show(vpn.getClientPublicKey());
        }
    }
}
