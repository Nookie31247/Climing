#include "iscsi.h"
int main(int argc, wchar_t* argv[]) {
    Iscsi iscsi; // Iscsi 클래스 변수 iscsi
    if (!iscsi.isAdmin()) { // 관리자 권한 확인
        iscsi.runAsAdmin(); // 관리자 권한으로 재실행
    }

    if (argc < 3) { // 인자가 부족할 경우
        std::wcerr << L"사용법: " << argv[0] << L" <USER_CONN_NUM> <VPN_IP>" << std::endl; // 사용법 출력
        return 1; // 에러 코드 반환
    }

    std::wstring user_conn_num = std::to_wstring(7); // 사용자 접속 번호 저장
    std::wstring vpn_ip = L"192.168.135." + user_conn_num; // VPN IP 주소 저장
    std::wstring iqn = L"iqn.2024-12.com.climing:" + std::to_wstring(7); // IQN 생성

    // iSCSI 타겟에 연결
    iscsi.executeCommand(L"iscsicli AddTarget " + vpn_ip + L" " + iqn);

    // 사용 가능한 드라이브 문자를 찾기
    iscsi.executeCommand(L"wmic logicaldisk get name");

    // 드라이브 문자를 찾고 마운트
    for (wchar_t drive = L'A'; drive <= L'Z'; ++drive) { // A부터 Z까지 반복
        std::wstring drive_letter = drive + std::wstring(L":\\"); // 드라이브 문자 생성
        if (_waccess(drive_letter.c_str(), 0) != 0) { // 드라이브가 사용 중이지 않으면
            // 사용 가능한 드라이브 문자를 찾으면 마운트
            std::wstring command = L"iscsicli SetTarget " + vpn_ip + L" " + iqn + L" /Drive:" + drive_letter;
            iscsi.executeCommand(command); // 드라이브 마운트 명령 실행
            break; // 루프 종료
        }
    }
    return 0; // 프로그램 종료
}
