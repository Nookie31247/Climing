#pragma once
#using <mscorlib.dll>
#include "RunCommand.hpp"
using namespace System;

namespace CppCode
{
    /// <summary>
	/// WireGuard VPN 연결을 담당하는 클래스
    /// </summary>
    public ref class WireGuardVPN
    {
    private:
        /// <summary>
        /// 네트워크 디바이스 이름
        /// </summary>
        static String^ deviceName = "Climing_Service";

        /// <summary>
		/// persistent keepalive 시간. 기본값은 25초입니다.
        /// </summary>
        static String^ persistentKeepalive = "25";

        /// <summary>
		/// config 파일 경로
        /// </summary>
        static String^ configPath;

        /// <summary>
        /// 클라이언트 비밀키
        /// </summary>
        static String^ clientPrivateKey;
        
        /// <summary>
        /// 클라이언트 공개키
        /// </summary>
        static String^ clientPublicKey;

        /// <summary>
		/// config 파일 생성 및 설정
        /// </summary>
		/// <param name="userNum">유저 접속 번호</param>
		/// <param name="serverPublicKey">서버 공개키</param>
		/// <param name="serverIP">서버 VPN IP</param>
		/// <param name="endpoint">서버의 VPN 접속주소</param>
        static void createVPNConfig(int userNum, String^ serverPublicKey, String^ serverIP, String^ endpoint);

    public:
        /// <summary>
        /// 프로그램을 실행할 때 WireGuard 설정을 초기화하기 위해 사용.
        /// config파일 생성 및 클라이언트 공개키/비밀키 설정
        /// </summary>
        static void initSet();

		/// <summary>
        /// 클라이언트 공개키 반환 함수
		/// </summary>
		/// <exception cref="클라이언트 키 에러 시 예외 발생"></exception>
		/// <returns>"클라이언트 공개키"</returns>
        static String^ getClientPublicKey();

		/// <summary>
        /// VPN 연결. VPN 연결 성공 시 0을 반환하고 실패 시 에러 코드 -31을 반환한다.
		/// </summary>
        /// <param name="userNum">유저 접속 번호</param>
		/// <param name="serverPublicKey">서버 공개키</param>
		/// <param name="serverIP">서버 VPN IP</param>
		/// <param name="endpoint">서버의 VPN 접속주소</param>
		/// <returns>"에러 코드: VPN 연결 성공 시 0을 반환하고 실패 시 에러 코드 -30, 기타 에러 발생 시 -31을 반환한다."</returns>
        static int connectVPN(int userNum, String^ serverPublicKey, String^ serverIP, String^ endpoint);

		/// <summary>
		/// VPN 연결을 해제한 후 config 파일을 삭제한다.
		/// </summary>
		/// <returns>"에러 코드: VPN 연결 해제 성공 시 0을 반환하고 실패 시 -30을 반환한다."</returns>
        static int disconnectVPN();
    };
}