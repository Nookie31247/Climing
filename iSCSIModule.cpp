// 필요한 헤더 파일 포함
#include <windows.h>
#include "ISCSIModule.h"
#include <iscsi.h> // libiscsi 헤더 파일 포함
#include <iostream> // 입출력 스트림을 위한 헤더 파일 포함
// DDD__REMOVE_DEVICE 상수 정의
#define DDD__REMOVE_DEVICE 0x00000002 // 또는 해당 상수의 적절한 값

// 생성자: 세션 포인터를 nullptr로 초기화
ISCSIModule::ISCSIModule() : session(nullptr) { }

// 소멸자: 세션이 존재하면 해제
ISCSIModule::~ISCSIModule() {
    if (session) {
        iscsi_session_destroy(session); // 세션 해제
    }
}

// iSCSI 타겟에 연결하는 함수
bool ISCSIModule::connect(int userConnectionNumber, const std::string& vpnIP) {
    // IQN 이름 생성: 사용자 접속 번호를 포함
    std::string iqnName = "iqn.2024-12.com.climing:" + std::to_string(userConnectionNumber);
    
    // iSCSI 세션 생성
    session = iscsi_session_create(iqnName.c_str(), vpnIP.c_str(), 3260);
    if (!session) { // 세션 생성 실패 시
        std::cerr << "iSCSI session creation failed." << std::endl; // 에러 메시지 출력
        return false; // 실패 반환
    }

    // iSCSI 타겟에 연결
    if (iscsi_session_connect(session) != 0) { // 연결 실패 시
        std::cerr << "Failed to connect to iSCSI target." << std::endl; // 에러 메시지 출력
        iscsi_session_destroy(session); // 세션 해제
        session = nullptr; // 세션 포인터 초기화
        return false; // 실패 반환
    }

    // 사용 가능한 드라이브 문자 가져오기
    std::string driveLetter = getAvailableDriveLetter();
    if (driveLetter.empty()) { // 사용 가능한 드라이브 문자가 없으면
        std::cerr << "No available drive letters." << std::endl; // 에러 메시지 출력
        iscsi_session_disconnect(session); // 세션 연결 해제
        iscsi_session_destroy(session); // 세션 해제
        session = nullptr; // 세션 포인터 초기화
        return false; // 실패 반환
    }

    // 드라이브 마운트 시도
    if (!mountDrive(iqnName, driveLetter)) { // 마운트 실패 시
        std::cerr << "드라이브 마운트 실패." << std::endl; // 에러 메시지 출력
        iscsi_session_disconnect(session); // 세션 연결 해제
        iscsi_session_destroy(session); // 세션 해제
        session = nullptr; // 세션 포인터 초기화
        return false; // 실패 반환
    }

    // 성공적으로 연결 및 드라이브 마운트 완료
    std::cout << "성공적으로 연결, 드라이브 마운트 성공: " << driveLetter << std::endl; // 성공 메시지 출력
    return true; // 성공 반환
}

// iSCSI 타겟과의 연결을 해제하는 함수
bool ISCSIModule::disconnect(int userConnectionNumber) {
    // IQN 이름 생성: 사용자 접속 번호를 포함
    std::string iqnName = "iqn.2024-12.com.climing:" + std::to_string(userConnectionNumber);

    // 세션이 없으면 연결 해제 불가
    if (!session) {
        std::cerr << "세션이 없어 연결 해제." << std::endl; // 에러 메시지 출력
        return false; // 실패 반환
    }

    // 드라이브 언마운트 시도
    if (!unmountDrive("D")) { // "D"는 예시로 사용한 드라이브 문자
        std::cerr << "드라이브 언마운트 실패." << std::endl; // 에러 메시지 출력
    }

    // iSCSI 타겟 연결 해제
    iscsi_session_disconnect(session); // 세션 연결 해제
    iscsi_session_destroy(session); // 세션 해제
    session = nullptr; // 세션 포인터 초기화
    std::cout << "iSCSI 타겟으로부터 연결 해제." << std::endl; // 성공 메시지 출력
    return true; // 성공 반환
}

// 사용 가능한 드라이브 문자를 가져오는 함수
std::string ISCSIModule::getAvailableDriveLetter() {
    // D부터 Z까지 반복하여 사용 가능한 드라이브 문자 확인
    for (char letter = 'D'; letter <= 'Z'; ++letter) {
        // 해당 드라이브 문자가 사용 중이지 않으면
        if (GetDriveTypeA((std::string(1, letter) + ":").c_str()) == DRIVE_NO_ROOT_DIR) {
            return std::string(1, letter); // 사용 가능한 드라이브 문자 반환
        }
    }
    return ""; // 사용 가능한 드라이브 문자가 없을 경우 빈 문자열 반환
}

// 드라이브를 마운트하는 함수
bool ISCSIModule::mountDrive(const std::string& iqn, const std::string& driveLetter) {
    // 실제 드라이브 마운트 로직 구현
    std::string devicePath = "\\\\.\\iSCSI" + iqn; // iSCSI 장치 경로 (수정 필요)
    // DefineDosDevice를 사용하여 드라이브 문자와 장치 연결
    if (DefineDosDeviceA(DDD_RAW_TARGET_PATH, (driveLetter + ":").c_str(), devicePath.c_str())) {
        return true; // 성공적으로 마운트
    }
    else {
        std::cerr << "Failed to define DOS device: " << GetLastError() << std::endl; // 에러 메시지 출력
        return false; // 마운트 실패
    }
}

// 드라이브를 언마운트하는 함수
bool ISCSIModule::unmountDrive(const std::string& driveLetter) {
    // DefineDosDevice를 사용하여 드라이브 문자 해제
    if (DefineDosDeviceA(DDD__REMOVE_DEVICE, driveLetter.c_str(), nullptr)) {
        return true; // 성공적으로 언마운트
    }
    else {
        std::cerr << "Failed to undefine DOS device: " << GetLastError() << std::endl; // 에러 메시지 출력
        return false; // 언마운트 실패
    }
}
