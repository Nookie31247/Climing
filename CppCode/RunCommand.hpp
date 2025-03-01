#pragma once
#using <mscorlib.dll>
using namespace System;

namespace CppCode
{
	public ref class RunCommand
	{
	public:
		String^ run(String^ command);
		String^ powershell(String^ command);
	};
}
