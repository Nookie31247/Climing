using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    /// <summary>
    /// 에러 메시지를 띄위기 위한 클래스
    /// </summary>
    class PrintError
    {
        /// <summary>
        /// 에러 코드를 해석하여 에러 메시지를 반환합니다.
        /// </summary>
        /// <param name="errorCode">에러 코드</param>
        /// <returns>에러 메시지</returns>
        public static string Print(int errorCode)
        {
            string errorMessage = "";

            switch (errorCode)
            {
                case 0:
                    errorMessage = "에러 없음";
                    break;
                case -1:
                    errorMessage = "아이디 혹은 비밀번호가 틀렸습니다.";
                    break;
                case -2:
                    errorMessage = "이미 존재하는 아이디입니다.";
                    break;
                case -3:
                    errorMessage = "이미 존재하는 닉네임입니다.";
                    break;
                case -4:
                    errorMessage = "이미 로그인 중인 아이디입니다.";
                    break;
                case -5:
                    errorMessage = "세션이 가득 찼습니다.";
                    break;
                case -6:
                    errorMessage = "잘못된 유저 접속 번호입니다.";
                    break;
                case -10:
                    errorMessage = "기타 iSCSI 관련 에러입니다.";
                    break;
                case -20:
                    errorMessage = "기타 Btrfs 서브볼륨 관련 에러입니다.";
                    break;
                case -21:
                    errorMessage = "Btrfs 서브볼륨 삭제에 실패하였습니다.";
                    break;
                case -30:
                    errorMessage = "기타 WireGuard 관련 에러입니다.";
                    break;
                case -31:
                    errorMessage = " WireGuard VPN 연결에 실패하였습니다.";
                    break;
                case -40:
                    errorMessage = "기타 데이터베이스 관련 에러입니다.";
                    break;
                case -50:
                    errorMessage = "기타 소켓 통신 관련 에러입니다.";
                    break;
                case -51:
                    errorMessage = "서버 연결에 실패하였습니다.";
                    break;
                case -52:
                    errorMessage = "소켓 초기화에 실패하였습니다.";
                    break;
                default:
                    errorMessage = "알 수 없는 에러입니다.";
                    break;
            }

            errorMessage = "Error " + errorCode + ": " + errorMessage;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMessage);
            Console.ResetColor();
            return errorMessage;
        }
    }
}
