#include "RunCommand.hpp"

using namespace System::Diagnostics;
using namespace System::IO;

String^ CppCode::RunCommand::runCmd(String^ command)  
{  
	Process^ process = gcnew Process();  
	ProcessStartInfo^ startInfo = gcnew ProcessStartInfo();  

	startInfo->FileName = "cmd.exe";  
	startInfo->Arguments = "/C " + command;  
	startInfo->RedirectStandardOutput = true;  
	startInfo->UseShellExecute = false;  
	startInfo->CreateNoWindow = true;  

	process->StartInfo = startInfo;  

	String^ output = "";	// 명령어 출력을 저장  
	try  
	{  
		process->Start();  
		// 5초간 응답 없으면 프로세스 종료 후 에러 발생
		if (!process->WaitForExit(5000))  
		{  
			process->Kill();  
			throw gcnew Exception("Error: Cmd 응답 없음");  
		}  
		else  
		{  
			output = process->StandardOutput->ReadToEnd()->Trim();  
		}  
	}  
	catch (Exception^ e)  
	{
		// 에러 발생 시	예외 처리
		throw gcnew Exception("Cmd 명령어 오류");
	}  
	return output;
}

String^ CppCode::RunCommand::runPowerShell(String^ command)
{
	Process^ process = gcnew Process();
	ProcessStartInfo^ startInfo = gcnew ProcessStartInfo();

	startInfo->FileName = "powershell.exe";
	startInfo->Arguments = "-NoProfile -ExecutionPolicy Bypass -Command \"" + command + "\"";
	startInfo->RedirectStandardOutput = true;
	startInfo->RedirectStandardError = true;
	startInfo->UseShellExecute = false;
	startInfo->CreateNoWindow = true;

	process->StartInfo = startInfo;

	String^ output = "";
	try
	{
		process->Start(); // 명령어 출력을 저장

		// 5초간 응답 없으면 프로세스 종료 후 에러 발생
		if (!process->WaitForExit(5000))
		{
			process->Kill();
			throw gcnew Exception("Error: powershell 응답 없음");
		}
		else
		{
			output = process->StandardOutput->ReadToEnd()->Trim();
		}
	}
	catch (Exception^ e)
	{
		// 에러 발생 시	예외 처리
		throw gcnew Exception("powershell 명령어 오류");
	}
	return output;
}