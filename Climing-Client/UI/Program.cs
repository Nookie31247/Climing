using System.Runtime.InteropServices;
using System.Diagnostics;
using CppCode;

namespace UI
{
    internal static class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AllocConsole();
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            // 콘솔 창을 띄우기 위해 사용
            //AllocConsole();
            if (!CheckWireGuard())
                return;

            WireGuardVPN.initSet();     // WireGuard 초기화 실행

            ApplicationConfiguration.Initialize();
            Application.Run(new MemberLogin());
        }

        static bool CheckWireGuard()
        {
            string output = CppCode.RunCommand.runCmd("wg --version");
            if (!output.Contains("wireguard-tools"))
            { 
                MessageBox.Show("WireGuard VPN이 설치되지 않았습니다. 웹 사이트에서 설치해 주시기 바랍니다. 설치 후에도 에러 발생 시 시스템을 재부팅해 주시기 바랍니다.",
                    "VPN 오류",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                // 명시적으로 기본 웹 브라우저로 웹 페이지를 열도록 설정
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "https://www.wireguard.com/install/",
                    UseShellExecute = true // 기본 웹 브라우저로 여는 옵션
                };
                Process.Start(startInfo);

                return false;
            }
            return true;
        }
    }
}