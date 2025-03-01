#pragma once
#include "RunCommand.hpp"
#using <mscorlib.dll>
using namespace System;

namespace CppCode
{
	public ref class ISCSI
	{
	private:
		RunCommand^ command = gcnew RunCommand();

	public:
		void connect(int userNum, String^ serverAddress);
		void disconnect();
	};
}