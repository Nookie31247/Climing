#define _CRT_SECURE_NO_WARNINGS
#include "socket_common.h"
#include <iostream>

#ifdef _WIN32
#include <winsock2.h>
#include <ws2tcpip.h>
#pragma comment(lib, "ws2_32.lib")  // Windows 소켓 라이브러리 링크
#else
#include <sys/socket.h>
#include <arpa/inet.h>
#include <unistd.h>
#endif

// 🔹 소켓 초기화 (Windows 전용)
void initializeWinsock() {
#ifdef _WIN32
    WSADATA wsaData;
    if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) {
        std::cerr << "소켓 초기화 실패" << std::endl;
        exit(EXIT_FAILURE);
    }
#endif
}

// 🔹 서버에 연결하는 함수
SOCKET connectToServer() {
    SOCKET sock = socket(AF_INET, SOCK_STREAM, 0);
    if (sock == INVALID_SOCKET) {
        std::cerr << "소켓 생성 실패" << std::endl;
#ifdef _WIN32
        WSACleanup();
#endif
        exit(EXIT_FAILURE);
    }

    struct sockaddr_in serv_addr;
    serv_addr.sin_family = AF_INET;
    serv_addr.sin_port = htons(SocketCommon::SERVER_PORT);

    if (inet_pton(AF_INET, SocketCommon::SERVER_IP, &serv_addr.sin_addr) <= 0) {
        std::cerr << "잘못된 주소 형식" << std::endl;
        SocketCommon::closeSocket(sock);
#ifdef _WIN32
        WSACleanup();
#endif
        exit(EXIT_FAILURE);
    }

    if (connect(sock, (struct sockaddr*)&serv_addr, sizeof(serv_addr)) == SOCKET_ERROR) {
        std::cerr << "서버 연결 실패" << std::endl;
        SocketCommon::closeSocket(sock);
#ifdef _WIN32
        WSACleanup();
#endif
        exit(EXIT_FAILURE);
    }

    std::cout << "서버에 성공적으로 연결되었습니다!" << std::endl;
    return sock;
}

// 로그인 요청 함수
void sendLoginRequest(SOCKET sock) {
    SocketCommon::Request loginRequest;
    loginRequest.type = SocketCommon::LOGIN;
    strcpy(loginRequest.id, "test_user");
    strcpy(loginRequest.password, "securepassword");

    send(sock, reinterpret_cast<char*>(&loginRequest), sizeof(loginRequest), 0);

    SocketCommon::CommonResponse response;
    recv(sock, reinterpret_cast<char*>(&response), sizeof(response), 0);

    std::cout << "서버 응답: " << response.message << std::endl;
}

// 회원가입 요청 함수
void sendSignupRequest(SOCKET sock) {
    SocketCommon::Request signupRequest;
    signupRequest.type = SocketCommon::SIGNUP;
    strcpy(signupRequest.id, "new_user");
    strcpy(signupRequest.password, "newpassword");

    send(sock, reinterpret_cast<char*>(&signupRequest), sizeof(signupRequest), 0);

    SocketCommon::CommonResponse response;
    recv(sock, reinterpret_cast<char*>(&response), sizeof(response), 0);

    std::cout << "서버 응답: " << response.message << std::endl;
}

// 로그아웃 요청 함수
void sendLogoutRequest(SOCKET sock) {
    SocketCommon::Request logoutRequest;
    logoutRequest.type = SocketCommon::LOGOUT;
    strcpy(logoutRequest.id, "test_user");

    send(sock, reinterpret_cast<char*>(&logoutRequest), sizeof(logoutRequest), 0);

    SocketCommon::CommonResponse response;
    recv(sock, reinterpret_cast<char*>(&response), sizeof(response), 0);

    std::cout << "서버 응답: " << response.message << std::endl;
}

// 게임 리스트 요청 함수
void sendGameListRequest(SOCKET sock) {
    SocketCommon::Request gameListRequest;
    gameListRequest.type = SocketCommon::GAMESLIST;

    send(sock, reinterpret_cast<char*>(&gameListRequest), sizeof(gameListRequest), 0);

    SocketCommon::GameListResponse response;
    recv(sock, reinterpret_cast<char*>(&response), sizeof(response), 0);

    if (response.success) {
        std::cout << "게임 리스트:" << std::endl;
        for (int i = 0; i < response.game_count; i++) {
            std::cout << "- " << response.games[i] << std::endl;
        }
    }
    else {
        std::cout << "게임 리스트를 불러오지 못했습니다." << std::endl;
    }
}

// 계정 삭제 요청 함수
void sendAccountDeleteRequest(SOCKET sock) {
    SocketCommon::Request deleteRequest;
    deleteRequest.type = SocketCommon::ACCOUNT_DELETE;
    strcpy(deleteRequest.id, "test_user");
    strcpy(deleteRequest.password, "securepassword");

    send(sock, reinterpret_cast<char*>(&deleteRequest), sizeof(deleteRequest), 0);

    SocketCommon::CommonResponse response;
    recv(sock, reinterpret_cast<char*>(&response), sizeof(response), 0);

    std::cout << "서버 응답: " << response.message << std::endl;
}

// 클라이언트 메인 실행 함수
int main() {
    initializeWinsock();   // Windows용 소켓 초기화
    SOCKET sock = connectToServer();  // 서버 연결

    int choice;
    do {
        std::cout << "\n=== 클라이언트 메뉴 ===\n";
        std::cout << "1. 로그인\n";
        std::cout << "2. 회원가입\n";
        std::cout << "3. 로그아웃\n";
        std::cout << "4. 게임 리스트 요청\n";
        std::cout << "5. 계정 삭제\n";
        std::cout << "0. 종료\n";
        std::cout << "선택: ";
        std::cin >> choice;

        switch (choice) {
        case 1:
            sendLoginRequest(sock);
            break;
        case 2:
            sendSignupRequest(sock);
            break;
        case 3:
            sendLogoutRequest(sock);
            break;
        case 4:
            sendGameListRequest(sock);
            break;
        case 5:
            sendAccountDeleteRequest(sock);
            break;
        case 0:
            std::cout << "클라이언트 종료" << std::endl;
            break;
        default:
            std::cout << "잘못된 선택입니다." << std::endl;
        }
    } while (choice != 0);

    // 소켓 종료
    SocketCommon::closeSocket(sock);
#ifdef _WIN32
    WSACleanup();
#endif
    return 0;
}