#ifndef ISCSIMODULE_H
#define ISCSIMODULE_H

#include <string> // 문자열을 사용하기 위한 헤더
#include <iscsi.h> // libiscsi 헤더 (iSCSI 관련 함수 및 구조체 정의)
#include <windows.h> // Windows API 헤더 (Windows 함수 및 타입 사용)

class ISCSIModule {
public:
    ISCSIModule(); // 생성자: ISCSIModule 객체 생성 시 호출
    ~ISCSIModule(); // 소멸자: ISCSIModule 객체 소멸 시 호출

    // iSCSI 타겟에 연결하는 함수
    // userConnectionNumber: 사용자 접속 번호 (IQN 이름의 일부로 사용)
    // vpnIP: 서버의 VPN IP 주소
    bool connect(int userConnectionNumber, const std::string& vpnIP);

    // iSCSI 타겟과의 연결을 해제하는 함수
    // userConnectionNumber: 사용자 접속 번호 (사용하지 않을 경우도 있음)
    bool disconnect(int userConnectionNumber);

private:
    iscsi_session_t* session; // iSCSI 세션 포인터: iSCSI 세션 정보를 저장하는 포인터
    // 사용 가능한 드라이브 문자를 가져오는 함수
    std::string getAvailableDriveLetter();

    // 드라이브를 마운트하는 함수
    // iqn: iSCSI Qualified Name
    // driveLetter: 드라이브 문자 (예: "D")
    bool mountDrive(const std::string& iqn, const std::string& driveLetter);

    // 드라이브를 언마운트하는 함수
    // driveLetter: 언마운트할 드라이브 문자 (예: "D")
    bool unmountDrive(const std::string& driveLetter);
};

#endif // ISCSIMODULE_H
