#include <iostream>
#include "ISCSIModule.h" // ISCSIModule 헤더 파일 포함

int main() {
    ISCSIModule initiator; // ISCSIModule 객체 생성
    int userConnectionNumber = 15; // 예시로 15를 사용
    std::string vpnIP = "192.168.1.100"; // VPN IP 주소

    // iSCSI 접속 시도
    if (initiator.connect(userConnectionNumber, vpnIP)) { // 접속 성공 시
        std::cout << "iSCSI 연결 성공." << std::endl; // 성공 메시지 출력
    }
    else { // 접속 실패 시
        std::cerr << "iSCSI 연결 실패." << std::endl; // 에러 메시지 출력
    }

    // iSCSI 접속 해제 시도
    if (initiator.disconnect(userConnectionNumber)) { // 해제 성공 시
        std::cout << "iSCSI 연결 해제 성공." << std::endl; // 성공 메시지 출력
    }
    else { // 해제 실패 시
        std::cerr << "iSCSI 연결 해제 실패." << std::endl; // 에러 메시지 출력
    }

    return 0; // 프로그램 종료
}
