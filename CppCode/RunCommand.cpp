#include "RunCommand.hpp"

using namespace System;
using namespace System::Diagnostics;
using namespace System::IO;

String^ CppCode::RunCommand::run(String^ command)
{
	Process^ process = gcnew Process();
	ProcessStartInfo^ startInfo = gcnew ProcessStartInfo();

	startInfo->FileName = "cmd.exe";
	startInfo->Arguments = "/C " + command;
	startInfo->RedirectStandardOutput = true;
	startInfo->UseShellExecute = false;
	startInfo->CreateNoWindow = true;

	process->StartInfo = startInfo;

	String^ output = "";
	try
	{
		process->Start();
		output = process->StandardOutput->ReadToEnd()->Trim();
		process->WaitForExit();
	}
	catch (Exception^ e)
	{
		// 에러 발생 시 상황 추가하기
	}
	return output;
}

String^ CppCode::RunCommand::powershell(String^ command)
{
	Process^ process = gcnew Process();
	ProcessStartInfo^ startInfo = gcnew ProcessStartInfo();

	startInfo->FileName = "powershell.exe";
	startInfo->Arguments = "-Command " + command;
	startInfo->RedirectStandardOutput = true;
	startInfo->RedirectStandardError = true;
	startInfo->UseShellExecute = false;
	startInfo->CreateNoWindow = true;

	process->StartInfo = startInfo;

	String^ output = "";
	try
	{
		process->Start();
		output = process->StandardOutput->ReadToEnd()->Trim();
		process->WaitForExit();
	}
	catch (Exception^ e)
	{
		// 에러 발생 시 상황 추가하기
	}
	return output;
}