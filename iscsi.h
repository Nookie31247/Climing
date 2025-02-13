#include <iostream> // 입출력 스트림을 위한 헤더 포함
#include <windows.h> // Windows API 헤더 포함
#include <string> // 문자열 클래스를 사용하기 위한 헤더 포함
#include <vector> // 동적 배열을 위한 벡터 클래스 헤더 포함
#include <sstream> // 스트림을 사용하기 위한 헤더 포함

#ifndef ISCSI_H
#define ISCSI_H
class Iscsi {
public:
	void executeCommand(const std::wstring& command);
	bool isAdmin();
	void runAsAdmin();
};
#endif // ISCSI_H
