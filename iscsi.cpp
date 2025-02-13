#include <iostream>
#include "iscsi.h"
void Iscsi::executeCommand(const std::wstring& command) {
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

    PROCESS_INFORMATION pi; // 프로세스 정보를 저장할 구조체 ,  HANDLE hProcess / HANDLE hThread / DWORD dwProcessId / DWORD dwThreadId와 같은 변수 사용가능
    ZeroMemory(&pi, sizeof(pi)); // 구조체 초기화, RtlZeroMemory(Destination,Length) memset((Destination),0,(Length)) 원형
    STARTUPINFO si; // 시작 정보 구조체
    ZeroMemory(&si, sizeof(si)); // 구조체 초기화
    si.cb = sizeof(si); // 구조체 크기 설정
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
bool Iscsi::isAdmin() {
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
void Iscsi::runAsAdmin() {
    wchar_t modulePath[MAX_PATH]; // 모듈 경로 저장용 배열
    GetModuleFileNameW(NULL, modulePath, MAX_PATH); // 현재 모듈의 경로 가져오기
    // 관리자 권한으로 실행할 명령어 생성
    std::wstring command = L"\"" + std::wstring(modulePath) + L"\" %*";
    ShellExecuteW(NULL, L"runas", L"cmd.exe", (L"/C " + command).c_str(), NULL, SW_SHOWNORMAL); // 명령어 실행
    exit(0); // 프로그램 종료
}

