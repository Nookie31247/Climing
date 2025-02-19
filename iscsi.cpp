#include "iscsi.h"

void Iscsi::executeCommand(const std::wstring& command) {
    // 파이프를 위한 보안 속성 설정
    SECURITY_ATTRIBUTES sa;
    sa.nLength = sizeof(SECURITY_ATTRIBUTES);
    sa.bInheritHandle = TRUE;
    sa.lpSecurityDescriptor = NULL;

    HANDLE hReadPipe, hWritePipe;
    if (!CreatePipe(&hReadPipe, &hWritePipe, &sa, 0)) {
        std::cerr << "파이프 생성 실패." << std::endl;
        return;
    }

    PROCESS_INFORMATION pi;
    ZeroMemory(&pi, sizeof(pi));
    STARTUPINFO si;
    ZeroMemory(&si, sizeof(si));
    si.cb = sizeof(si);
    si.dwFlags |= STARTF_USESTDHANDLES;
    si.hStdOutput = hWritePipe;
    si.hStdError = hWritePipe;

    // PowerShell을 사용하여 명령어 실행
    std::wstring powershellCommand = L"powershell.exe -Command \"" + command + L"\"";
    if (!CreateProcess(NULL, const_cast<LPWSTR>(powershellCommand.c_str()), NULL, NULL, TRUE, 0, NULL, NULL, &si, &pi)) {
        std::cerr << "프로세스 생성 실패." << std::endl;
        CloseHandle(hWritePipe);
        CloseHandle(hReadPipe);
        return;
    }

    CloseHandle(hWritePipe);

    char buffer[128];
    DWORD bytesRead;
    while (ReadFile(hReadPipe, buffer, sizeof(buffer) - 1, &bytesRead, NULL) && bytesRead > 0) {
        buffer[bytesRead] = '\0';
        std::cout << buffer;
    }

    CloseHandle(hReadPipe);
    CloseHandle(pi.hProcess);
    CloseHandle(pi.hThread);
}

// 관리자 권한 체크 함수
bool Iscsi::isAdmin() {
    HANDLE token;
    if (OpenProcessToken(GetCurrentProcess(), TOKEN_QUERY, &token)) {
        DWORD size;
        GetTokenInformation(token, TokenElevation, NULL, 0, &size);
        std::vector<BYTE> elevation(size);
        if (GetTokenInformation(token, TokenElevation, elevation.data(), size, &size)) {
            return *reinterpret_cast<DWORD*>(elevation.data()) != 0;
        }
    }
    return false;
}

// 관리자 권한으로 다시 실행하는 함수
void Iscsi::runAsAdmin() {
    wchar_t modulePath[MAX_PATH];
    GetModuleFileNameW(NULL, modulePath, MAX_PATH);
    std::wstring command = L"\"" + std::wstring(modulePath) + L"\" %*";
    ShellExecuteW(NULL, L"runas", L"powershell.exe", (L"-Command " + command).c_str(), NULL, SW_SHOWNORMAL);
    exit(0);
}
