#pragma once

#include <WinSock2.h>
#include <ws2tcpip.h>
#include <string>
#include <msclr/marshal_cppstd.h>   // String to std::string 변환

#using <mscorlib.dll>
#pragma comment(lib, "ws2_32.lib")  // Winsock 라이브러리 링크

#include "WireGuardVPN.hpp"
#include "iSCSI.hpp"

using namespace System::Collections::Generic;
using namespace System;

namespace CppCode
{
    /// <summary>
    /// 게임 데이터를 서버에서 가져오기 위한 클래스
    /// </summary>
    public ref class Game {
    public:
        String^ name;       // 게임 이름
		String^ imageUrl;   // 게임 이미지 URL
		String^ company;    // 게임 제작사
		String^ genre;      // 게임 장르
		String^ dirPath;	// 게임 실행 경로
		int identifyNum;	// 게임 식별 번호
    };

    /// <summary>
    /// 클라이언트 소켓 통신을 담당하는 클래스
    /// </summary>
    public ref class ClientSocket
    {
    private:
        /// <summary>
        /// 서버의 IP 주소
        /// </summary>
        static const String^ serverIp = "122.42.50.148";

        /// <summary>
        /// 서버의 포트 번호
        /// </summary>
        static const int serverPort = 37624;

        static WSADATA* wsaData;
        static SOCKET sock;
        static sockaddr_in* serverAddr;

		/// <summary>
		/// WinSock을 초기화하고 소켓을 생성하여 서버에 연결합니다.
		/// </summary>
		/// <returns>에러 코드</returns>
		static int openSocket();

		/// <summary>
		/// 소켓을 닫고 WinSock을 해제합니다.
        /// </summary>
        static void closeSocket();

        /// <summary>
        /// 서버로 JSON 요청을 전송합니다.
        /// </summary>
        /// <param name="requestJson">서버로 전송할 JSON 파일</param>
        static void sendRequest(String^ requestJson);

        /// <summary>
        /// 서버 응답을 받을 때, 완전한 JSON 파일을 받을 때까지 반복해서 읽기
        /// </summary>
        /// <returns>서버로부터 응답받은 JSON 파일</returns>
        static String^ receiveResponse();

        /// <summary>
        /// 로그인 JSON 파일 생성
        /// </summary>
        /// <param name="userId">아이디</param>
        /// <param name="hashedPassword">암호화된 비밀번호</param>
        /// <returns>JSON 형식의 String</returns>
        static String^ createLoginJson(String^ userId, String^ hashedPassword);

        /// <summary>
        /// 로그아웃 JSON 파일 생성
        /// </summary>
        /// <param name="userId">아이디</param>
        /// <param name="userConnectNumber">유저 접속 번호</param>
        /// <returns>JSON 형식의 String</returns>
        static String^ createLogoutJson(String^ userId, int userConnectNumber);

        /// <summary>
        /// 회원가입 JSON 파일 생성
        /// </summary>
        /// <param name="userId">아이디</param>
        /// <param name="hashedPassword">암호화된 비밀번호</param>
        /// <param name="nickname">닉네임</param>
        /// <returns>JSON 형식의 String</returns>
        static String^ createSignupJson(String^ userId, String^ hashedPassword, String^ nickname);

        /// <summary>
        /// 회원탈퇴 JSON 파일 생성
        /// </summary>
        /// <param name="userId">아이디</param>
        /// <param name="hashedPassword">암호화된 비밀번호</param>
        /// <returns>JSON 형식의 String</returns>
        static String^ createDeleteUserJson(String^ userId, String^ hashedPassword);

        /// <summary>
        /// 게임 리스트 요청 JSON 파일 생성
        /// </summary>
        /// <returns>JSON 형식의 String</returns>
        static String^ createGetGameListJson();

        /// <summary>
        /// 비밀번호를 SHA-256 알고리즘으로 암호화
        /// </summary>
        /// <param name="password">암호화 전 비밀번호</param>
        /// <returns>암호화된 비밀번호</returns>
        static String^ passwordHashing(String^ password);

    public:
        /// <summary>
        /// 로그인 정보를 서버로 전송하여 로그인을 시도합니다.
        /// 로그인 후 VPN에 연결하여 iSCSI 가상 하드디스크를 마운트합니다.
        /// </summary>
        /// <param name="id">아이디</param>
        /// <param name="pw">비밀번호</param>
        /// <returns>에러 코드</returns>
        static KeyValuePair<int, String^> login(String^ id, String^ pw);

        /// <summary>
        /// 로그아웃 정보를 서버로 전송하여 로그아웃을 시도합니다.
        /// 로그아웃 시 iSCSI와 VPN 연결을 해제합니다.
        /// </summary>
        /// <param name="id">아이디</param>
        /// <param name="userNum">유저 접속 번호</param>
        /// <returns>에러 코드</returns>
        static int logout(String^ id, int userNum);

        /// <summary>
        /// 회원가입 정보를 서버로 전송하여 회원가입을 시도합니다.
        /// </summary>
        /// <param name="id">아이디</param>
        /// <param name="pw">비밀번호</param>
        /// <param name="name">닉네임</param>
        /// <returns>에러 코드</returns>
        static int addUser(String^ id, String^ pw, String^ name);

        /// <summary>
        /// 회원탈퇴 정보를 서버로 전송하여 회원탈퇴를 시도합니다.
        /// </summary>
        /// <param name="id">아이디</param>
        /// <param name="pw">비밀번호</param>
        /// <returns>에러 코드</returns>
        static int deleteUser(String^ id, String^ pw);

        /// <summary>
        /// 서버에 저장된 게임 정보를 불러옵니다.
        /// </summary>
        /// <returns>Game 클래스 타입의 리스트</returns>
        static List<Game^>^ ClientSocket::getGameList();
    };
}