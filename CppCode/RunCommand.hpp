#pragma once
#using <mscorlib.dll>
using namespace System;

namespace CppCode
{
	/// <summary>
	/// cmd 혹은 powershell 명령어를 실행하는 클래스
	/// </summary>
	public ref class RunCommand
	{
	public:
		/// <summary>
		///	cmd 명령어를 실행. 에러 발생 시 에러 메시지 반환.
		/// </summary>
		/// <param name="command">실행할 명령어</param>
		/// <returns>명령어 실행 결과</returns>
		static String^ runCmd(String^ command);

		/// <summary>
		///	powrshell 명령어를 실행합니다. 에러 발생 시 에러 메시지 반환.
		/// </summary>
		/// <param name="command">실행할 명령어</param>
		/// <returns>명령어 실행 결과</returns>
		static String^ runPowerShell(String^ command);
	};
}
