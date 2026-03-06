#pragma once
#include "RunCommand.hpp"
#using <mscorlib.dll>
using namespace System;

namespace CppCode
{
	/// <summary>
	/// ISCSI 이니시에이터 연결을 담당하는 클래스
	/// </summary>
	public ref class ISCSI
	{
	private:
		/// <summary>
		/// 서버 ip 주소 (iSCSI 포탈 주소)
		/// </summary>
		static String^ serverAddress;

		/// <summary>
		/// iSCSI iqn 이름
		/// </summary>
		static String^ iqnName;

		/// <summary>
		/// iSCSI에 연결된 볼륨의 드라이브 문자를 찾습니다.
		/// </summary>
		/// <param name="targetLabel">드라이브 이름 (레이블)</param>
		/// <returns>드라이브 문자 (예: C:\)</returns>
		static String^ findDrive(String^ targetLabel);

		/// <summary>
		/// iSCSI에 연결된 볼륨의 드라이브 문자
		/// </summary>
		static String^ driveLetter = nullptr;
	public:
		/// <summary>
		/// 드라이브 문자를 반환합니다. 드라이브 마운트 실패 시 nullptr를 반환합니다.
		/// </summary>
		/// <returns>드라이브 문자</returns>
		static String^ getDriveLetter();

		/// <summary>
		///	iSCSI 접속 시도
		/// </summary>
		/// <param name="userNum">유저 접속 번호</param>
		/// <param name="inputServerAddress">서버 ip 주소</param>
		/// <returns>에러 코드 반환</returns>
		static int connect(int userNum, String^ inputServerAddress);

		/// <summary>
		/// iSCSI 접속을 해제합니다.
		/// </summary>
		/// <returns>에러 코드 반환</returns>
		static int disconnect();

		/// <summary>
		/// iSCSI 관련 설정을 삭제 및 초기화합니다.
		/// </summary>
		/// <returns>에러 코드 반환</returns>
		static int clearSettings();
	};
}