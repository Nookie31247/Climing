#include "iSCSI.hpp"

int CppCode::ISCSI::connect(int userNum, String^ inputServerAddress)
{
	// iqn 이름과 서버 주소를 클래스 변수에 저장
	iqnName = "iqn.2024-11.com.climing:" + userNum;
	serverAddress = inputServerAddress;
	
	try
	{
		RunCommand::runPowerShell("Start-Service -Name MSiSCSI");
		// iSCSI 포탈 생성
		RunCommand::runPowerShell("New-IscsiTargetPortal -TargetPortalAddress \"" + serverAddress + "\"");

		// 생성한 포탈에서 iSCSI 타겟 연결
		RunCommand::runPowerShell(L"Connect-IscsiTarget -NodeAddress \"" + iqnName + "\"");
	}
	catch (Exception^ e)
	{
		return -20;
	}

	Threading::Thread::Sleep(5000);		//타겟 연결 후 5초 대기

	String^ checkDriveLetter;

	for (int i = 0; i < 10; i++) {
		checkDriveLetter = findDrive("#Climing#_GameDisk");
		if (checkDriveLetter != nullptr) {
			driveLetter = checkDriveLetter;
			break;
		}
		// 드라이브 문자를 찾지 못한 경우 아직 하드디스크가 마운트되지 않았다고 판단하고 1초 대기
		Threading::Thread::Sleep(1000);		
	}

	// 드라이브를 찾지 못한 경우의 예외 처리 추가하기

	return 0;
}

int CppCode::ISCSI::disconnect()
{
	
	try
	{
		// iSCSI 타겟 연결 해제
		RunCommand::runPowerShell("Disconnect-IscsiTarget -NodeAddress \"" + iqnName + "\" -Confirm:$false");

		// iSCSI 포탈 제거
		RunCommand::runPowerShell("Remove-IscsiTargetPortal -TargetPortalAddress \"" + serverAddress + "\"");

		// 드라이브 문자 초기화
		driveLetter = nullptr;
	}
	catch (Exception^ e)
	{
		//iSCSI 타겟이나 포탈을 제거할 수 없는 경우
		return -21;
	}

	return 0;
}

int CppCode::ISCSI::clearSettings()  
{  
	try  
	{  
		// iSCSI 타겟 관련 설정 삭제  
		RunCommand::runPowerShell("Get-IscsiTarget | Where-Object { $_.NodeAddress -like \" * climing * \" } | ForEach - Object{ Disconnect - IscsiTarget - NodeAddress $_.NodeAddress - Confirm:$false }");

		// iSCSI 포탈 관련 설정 삭제  
		RunCommand::runPowerShell("Get-IscsiTargetPortal | Where-Object { $_.TargetPortalAddress -like \"192.168.135.*\" } | ForEach - Object{ Remove - IscsiTargetPortal - TargetPortalAddress $_.TargetPortalAddress - Confirm:$false }");

		// iSCSI 서비스 강제 재시작  
		RunCommand::runPowerShell("Restart-Service -Name MSiSCSI -Force");
	}  
	catch (Exception^ e)  
	{  
		// 설정을 지우는 동안 오류가 발생한 경우  
		return -20;
	}  
	return 0;  
}

String^ CppCode::ISCSI::getDriveLetter()
{
	return driveLetter;
}

String^ CppCode::ISCSI::findDrive(String^ targetLabel)
{
	// PC의 볼륨 목록을 저장
	array<String^>^ drives = IO::Directory::GetLogicalDrives();

	for each (String^ drive in drives)
	{
		try
		{
			IO::DriveInfo^ info = gcnew IO::DriveInfo(drive);
			if (info->IsReady) {
				String^ volumeLabel = info->VolumeLabel;	// 현재 확인하고자 하는 볼륨 레이블을 저장
				Console::WriteLine("현재 확인하는 드라이브: " + volumeLabel);
				Console::WriteLine("내가 입력한 드라이브: " + targetLabel);

				if (String::Compare(volumeLabel, targetLabel, true) == 0)
				{
					Console::WriteLine("드라이브 문자 반환 " + info->Name);
					return info->Name;
				}
			}
		}
		catch (Exception^ e) {

		}
	}

	return nullptr;
}