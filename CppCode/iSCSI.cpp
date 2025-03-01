#include "iSCSI.hpp"

using namespace System;

void CppCode::ISCSI::connect(int userNum, String^ serverAddress)
{
	String^ iqnName = "iqn.2024-12.com.climing:" + userNum;
	command->powershell(L"Connect-IscsiTarget -NodeAddress '" + iqnName + 
		" -TargetPortalAddress '" + serverAddress + "'");
}

void CppCode::ISCSI::disconnect()
{

}