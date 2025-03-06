#include <iostream>
#include <winsock2.h>
#include <ws2tcpip.h>
#include <string>
#include <json/json.h>  // JSON 처리 (jsoncpp 사용)

#pragma comment(lib, "ws2_32.lib")  // Winsock 라이브러리 링크

#define SERVER_IP "127.0.0.1"  // 서버 IP
#define SERVER_PORT 8080   // 서버 포트

using namespace std;

// 서버로 JSON 요청을 전송하는 함수 (개행 문자 추가)
void sendRequest(SOCKET sock, const string& requestJson) {
    string jsonWithNewline = requestJson + "\n"; // 개행 문자 추가
    send(sock, jsonWithNewline.c_str(), jsonWithNewline.length(), 0);
}


// 서버 응답을 받을 때, 완전한 JSON을 받을 때까지 반복해서 읽기
string receiveResponse(SOCKET sock) {
    string response;
    char buffer[4096];
    int bytesReceived;

    while ((bytesReceived = recv(sock, buffer, sizeof(buffer) - 1, 0)) > 0) {
        buffer[bytesReceived] = '\0';  // 문자열 종료 처리
        response += buffer;
        if (response.find("\n") != string::npos) break; // 개행 문자 도착 시 종료
    }
    
    return response;
}

// 로그인 요청 JSON 생성
string createLoginJson(const string& userId, const string& hashedPassword) {
    Json::Value request;
    request["type"] = "Login";
    request["user_id"] = userId;
    request["hashed_user_password"] = hashedPassword;

    Json::StreamWriterBuilder writer;
    return Json::writeString(writer, request);
}

// 로그아웃 요청 JSON 생성
string createLogoutJson(const string& userId, int userConnectNumber) {
    Json::Value request;
    request["type"] = "Logout";
    request["user_id"] = userId;
    request["user_connect_number"] = userConnectNumber;

    Json::StreamWriterBuilder writer;
    return Json::writeString(writer, request);
}

// 회원가입 요청 JSON 생성
string createSignupJson(const string& userId, const string& hashedPassword, const string& nickname) {
    Json::Value request;
    request["type"] = "addUser";
    request["user_id"] = userId;
    request["hashed_user_password"] = hashedPassword;
    request["user_nickname"] = nickname;

    Json::StreamWriterBuilder writer;
    return Json::writeString(writer, request);
}

// 회원탈퇴 요청 JSON 생성
string createDeleteUserJson(const string& userId, const string& hashedPassword) {
    Json::Value request;
    request["type"] = "deleteUser";
    request["user_id"] = userId;
    request["hashed_user_password"] = hashedPassword;

    Json::StreamWriterBuilder writer;
    return Json::writeString(writer, request);
}

int main() {
    WSADATA wsaData;
    SOCKET sock;
    sockaddr_in serverAddr;

    // Winsock 초기화
    if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) {
        cerr << "[!] WinSock 초기화를 실패하였습니다.\n";
        return 1;
    }

    // 소켓 생성
    sock = socket(AF_INET, SOCK_STREAM, 0);
    if (sock == INVALID_SOCKET) {
        cerr << "[!] 소켓 생성을 실패하였습니다.\n";
        WSACleanup();
        return 1;
    }

    // 서버 주소 설정
    serverAddr.sin_family = AF_INET;
    serverAddr.sin_port = htons(SERVER_PORT);
    inet_pton(AF_INET, SERVER_IP, &serverAddr.sin_addr);

    // 서버 연결
    if (connect(sock, (sockaddr*)&serverAddr, sizeof(serverAddr)) == SOCKET_ERROR) {
        cerr << "[!] 서버 연결에 실패하였습니다.\n";
        closesocket(sock);
        WSACleanup();
        return 1;
    }

    cout << "[!] 서버에 연결되었습니다!\n";

    // 로그인 요청 테스트
    string loginJson = createLoginJson("testUsers", "hashedPassword123");
    sendRequest(sock, loginJson);
    cout << "서버 응답: " << receiveResponse(sock) << endl;

    // 로그아웃 요청 테스트
    string logoutJson = createLogoutJson("testUser", 1234);
    sendRequest(sock, logoutJson);
    cout << "서버 응답: " << receiveResponse(sock) << endl;

    // 회원가입 요청 테스트
    string signupJson = createSignupJson("newUser", "hashedPass456", "NewNick");
    sendRequest(sock, signupJson);
    cout << "서버 응답: " << receiveResponse(sock) << endl;

    // 회원탈퇴 요청 테스트
    string deleteJson = createDeleteUserJson("testUser", "hashedPassword123");
    sendRequest(sock, deleteJson);
    cout << "서버 응답: " << receiveResponse(sock) << endl;

    // 소켓 종료
    closesocket(sock);
    WSACleanup();
    cout << "서버 연결 종료\n";
    return 0;
}
