#include <windows.h> // Windows API 헤더 포함
#include <iostream> // 입출력 스트림을 위한 헤더 포함
#include <string> // 문자열 클래스를 사용하기 위한 헤더 포함
#include <vector> // 동적 배열을 위한 벡터 클래스 헤더 포함
#include <sstream> // 스트림을 사용하기 위한 헤더 포함

// 명령어를 실행하고 출력을 얻는 함수
void executeCommand(const std::wstring& command) {
    // 파이프를 위한 보안 속성 설정
    SECURITY_ATTRIBUTES sa; // SECURITY_ATTRIBUTES의 구조체를 사용하기위해 sa변수로 지정
    sa.nLength = sizeof(SECURITY_ATTRIBUTES); // 구조체 크기 설정
    sa.bInheritHandle = TRUE; // 핸들 상속 가능 설정
    sa.lpSecurityDescriptor = NULL; // 보안 설명자는 NULL로 설정

    HANDLE hReadPipe, hWritePipe; // 읽기 및 쓰기 핸들 선언
    // 파이프 생성 (읽기와 쓰기용)
    if (!CreatePipe(&hReadPipe, &hWritePipe, &sa, 0)) {
        std::cerr << "파이프 생성 실패." << std::endl; // 실패 시 에러 메시지 출력
        return; // 함수 종료
    }

    PROCESS_INFORMATION pi; // 프로세스 정보를 저장할 구조체 ,  HANDLE hProcess / HANDLE hThread / DWORD dwProcessId / DWORD dwThreadId와 같은 구조체 선언 변수 사용가능
    ZeroMemory(&pi, sizeof(pi)); // 구조체 초기화, RtlZeroMemory(Destination,Length) memset((Destination),0,(Length)) 원형
    STARTUPINFO si; // 시작 정보 구조체, 변수 si
    ZeroMemory(&si, sizeof(si)); // 구조체 초기화, RtlZeroMemory(Destination,Length) memset((Destination),0,(Length)) 원형
    si.cb = sizeof(si); // 구조체 크기 설정, cb = character byte
    si.dwFlags |= STARTF_USESTDHANDLES; // 표준 핸들 사용 플래그 설정
    si.hStdOutput = hWritePipe; // 표준 출력을 쓰기 파이프로 설정
    si.hStdError = hWritePipe; // 표준 오류 출력을 쓰기 파이프로 설정

    // 명령어를 실행할 프로세스 생성
    if (!CreateProcess(NULL, const_cast<LPWSTR>(command.c_str()), NULL, NULL, TRUE, 0, NULL, NULL, &si, &pi)) {
        std::cerr << "프로세스 생성 실패." << std::endl; // 실패 시 에러 메시지 출력
        CloseHandle(hWritePipe); // 쓰기 핸들 닫기
        CloseHandle(hReadPipe); // 읽기 핸들 닫기
        return; // 함수 종료
    }

    CloseHandle(hWritePipe); // 쓰기 핸들 닫기 (더 이상 필요 없음)

    char buffer[128]; // 출력 데이터를 저장할 버퍼
    DWORD bytesRead; // 읽은 바이트 수
    // 파이프에서 읽기
    while (ReadFile(hReadPipe, buffer, sizeof(buffer) - 1, &bytesRead, NULL) && bytesRead > 0) {
        buffer[bytesRead] = '\0'; // 문자열 종료 문자 추가
        std::cout << buffer; // 읽은 내용 출력
    }

    CloseHandle(hReadPipe); // 읽기 핸들 닫기
    CloseHandle(pi.hProcess); // 프로세스 핸들 닫기
    CloseHandle(pi.hThread); // 스레드 핸들 닫기
}

// 관리자 권한 체크 함수
bool isAdmin() {
    HANDLE token; // 토큰 핸들
    // 현재 프로세스의 토큰 열기
    if (OpenProcessToken(GetCurrentProcess(), TOKEN_QUERY, &token)) {
        DWORD size; // 크기 변수
        // 토큰의 고급 정보 크기 요청
        GetTokenInformation(token, TokenElevation, NULL, 0, &size);
        std::vector<BYTE> elevation(size); // 고급 정보 저장용 벡터 생성
        // 고급 정보 가져오기
        if (GetTokenInformation(token, TokenElevation, elevation.data(), size, &size)) {
            return *reinterpret_cast<DWORD*>(elevation.data()) != 0; // 고급 권한 여부 반환
        }
    }
    return false; // 관리자 권한이 아닐 경우 false 반환
}

// 관리자 권한으로 다시 실행하는 함수
void runAsAdmin() {
    wchar_t modulePath[MAX_PATH]; // 모듈 경로 저장용 배열
    GetModuleFileNameW(NULL, modulePath, MAX_PATH); // 현재 모듈의 경로 가져오기
    // 관리자 권한으로 실행할 명령어 생성
    std::wstring command = L"\"" + std::wstring(modulePath) + L"\" %*";
    ShellExecuteW(NULL, L"runas", L"cmd.exe", (L"/C " + command).c_str(), NULL, SW_SHOWNORMAL); // 명령어 실행
    exit(0); // 프로그램 종료
}

int main(int argc, wchar_t* argv[]) {
    if (!isAdmin()) { // 관리자 권한 확인
        runAsAdmin(); // 관리자 권한으로 재실행
    }

    if (argc < 3) { // 인자가 부족할 경우
        std::wcerr << L"사용법: " << argv[0] << L" <USER_CONN_NUM> <VPN_IP>" << std::endl; // 사용법 출력
        return 1; // 에러 코드 반환
    }

    std::wstring user_conn_num = std::to_wstring(7); // 사용자 접속 번호 저장
    std::wstring vpn_ip = L"192.168.135"+ std::to_wstring(user_conn_num); // VPN IP 주소 저장
    std::wstring iqn = L"iqn.2024-12.com.climing:" + std::to_wstring(7); // IQN 생성

    // iSCSI 타겟에 연결
    executeCommand(L"iscsicli AddTarget " + vpn_ip + L" " + iqn);
    
    if (!checkConnection(vpn_ip, iqn)) {
        std::wcerr << L"iSCSI 타겟에 연결 실패." << std::endl;
        return 1; // 에러 코드 반환
    }
    else {
        std::wcout << L"iSCSI 타겟에 성공적으로 연결되었습니다." << std::endl;
    }
    // 사용 가능한 드라이브 문자를 찾기
    executeCommand(L"wmic logicaldisk get name");

    // 드라이브 문자를 찾고 마운트
    for (wchar_t drive = L'A'; drive <= L'Z'; ++drive) { // A부터 Z까지 반복
        std::wstring drive_letter = drive + std::wstring(L":\\"); // 드라이브 문자 생성
        if (_waccess(drive_letter.c_str(), 0) != 0) { // 드라이브가 사용 중이지 않으면
            // 사용 가능한 드라이브 문자를 찾으면 마운트
            std::wstring command = L"iscsicli SetTarget " + vpn_ip + L" " + iqn + L" /Drive:" + drive_letter;
            executeCommand(command); // 드라이브 마운트 명령 실행
            break; // 루프 종료
        }
    }

    return 0; // 프로그램 종료
}
