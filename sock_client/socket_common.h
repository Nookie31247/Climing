#ifndef SOCKET_COMMON_H
#define SOCKET_COMMON_H

#define _CRT_SECURE_NO_WARNINGS
#include <cstring>  // memset(), strcpy() 사용
#include <vector>   // 동적 배열 지원

// 🔹 OS별 헤더 분기 처리
#ifdef _WIN32
#include <winsock2.h>
#include <ws2tcpip.h>
#pragma comment(lib, "ws2_32.lib")  // Windows 소켓 라이브러리 링크
#else
#include <sys/socket.h>
#include <arpa/inet.h>
#include <unistd.h>
#endif

// 서버 네트워크 정보
namespace SocketCommon {
    constexpr int SERVER_PORT = 8080;      // 서버 포트
    constexpr const char* SERVER_IP = "127.0.0.1";  // 서버 IP

    constexpr int MAX_CLIENTS = 10;         // 최대 동시 접속 클라이언트 수
    constexpr int BUFFER_SIZE = 1024;       // 기본 버퍼 크기

    // 요청 유형 (클라이언트가 서버에 보낼 요청 타입)
    enum RequestType {
        UNKNOWN,        // 알 수 없는 요청 (예외 처리용)
        LOGIN,          // 로그인 요청
        LOGOUT,         // 로그아웃 요청
        SIGNUP,         // 회원가입 요청
        GAMESLIST,      // 게임 리스트 요청
        ACCOUNT_DELETE  // 계정 삭제 요청
    };

    // 클라이언트 요청 구조체 (서버에 보낼 데이터)
    struct Request {
        RequestType type;   // 요청 유형
        char id[32];        // 아이디 (최대 32자)
        char password[32];  // 비밀번호 (최대 32자)

        // 기본 생성자 (초기화)
        Request() : type(UNKNOWN) {
            memset(id, 0, sizeof(id));
            memset(password, 0, sizeof(password));
        }
    };

    // 서버 응답 구조체 (회원가입, 로그인, 계정 삭제 응답)
    struct CommonResponse {
        bool success;       // 요청 성공 여부 (true: 성공, false: 실패)
        char message[128];  // 응답 메시지

        // 기본 생성자 (초기화)
        CommonResponse() : success(false) {
            memset(message, 0, sizeof(message));
        }
    };

    // 게임 리스트 응답 구조체 (게임 리스트 요청 시 반환)
    struct GameListResponse {
        bool success;               // 요청 성공 여부 (true: 게임 목록 반환)
        int game_count;              // 게임 개수
        char games[10][64];          // 최대 10개의 게임 저장 (각각 64자 제한)

        // 기본 생성자 (초기화)
        GameListResponse() : success(false), game_count(0) {
            for (int i = 0; i < 10; i++) {
                memset(games[i], 0, sizeof(games[i]));
            }
        }
    };

    // OS별 소켓 종료 함수 (Windows: closesocket, Linux: close)
    inline void closeSocket(int socket) {
#ifdef _WIN32
        closesocket(socket);
#else
        close(socket);
#endif
    }
}

#endif // SOCKET_COMMON_H
