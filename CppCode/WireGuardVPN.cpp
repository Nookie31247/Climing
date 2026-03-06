#include "WireGuardVPN.hpp"

using namespace CppCode;

void WireGuardVPN::initSet()
{
	// config 파일 경로 설정
    configPath = "C:\\Program Files\\WireGuard\\" + deviceName + ".conf";

	// 클라이언트 비밀키 생성
    clientPrivateKey = RunCommand::runCmd("wg genkey");

	// 개인키를 이용하여 공개키 생성
    clientPublicKey = RunCommand::runCmd("echo " + clientPrivateKey + " | wg pubkey");
}

void WireGuardVPN::createVPNConfig(int userNum, String^ serverPublicKey, String^ serverIP, String^ endpoint)
{
    // configPath에 파일이 존재하면 삭제
    if (IO::File::Exists(configPath))
    {
        IO::File::Delete(configPath);
    }

    IO::StreamWriter^ configFile = gcnew IO::StreamWriter(configPath);

	configFile->WriteLine("[Interface]");                                   // Interface 설정
	configFile->WriteLine("PrivateKey = " + clientPrivateKey);              // 클라이언트 비밀키
	configFile->WriteLine("Address = 192.168.135." + userNum + "/24\n");	// 클라이언트 IP
	configFile->WriteLine("[Peer]");                                        // Peer 설정
	configFile->WriteLine("PublicKey = " + serverPublicKey);				// 서버 공개키   
	configFile->WriteLine("AllowedIPS = " + serverIP);					    // 서버 VPN IP
	configFile->WriteLine("Endpoint = " + endpoint);                        // 서버의 VPN 접속주소    
	configFile->WriteLine("PersistentKeepalive = " + persistentKeepalive);  // 연결 유지 시간
    configFile->Close();
}

String^ WireGuardVPN::getClientPublicKey()
{
	// 비밀키 혹은 공개키의 자릿수가 44자리가 아닐 경우 에러를 발생합니다.
	if (clientPrivateKey->Length != 44 || clientPublicKey->Length != 44)
	{
		throw gcnew Exception("Invalid key length");;
	}
    return clientPublicKey;
}

int WireGuardVPN::connectVPN(int userNum, String^ serverPublicKey, String^ serverIP, String^ endpoint)
{
	// config 파일 생성
    createVPNConfig(userNum, serverPublicKey, serverIP, endpoint);

	// config 파일을 통해 VPN 연결
	// 에러 발생 시 -30을 반환
    try
    {
		RunCommand::runCmd("wireguard /installtunnelservice \"" + configPath + "\"");
    }
	catch (Exception^ e)
	{
		return -30;
	}

	Threading::Thread::Sleep(1000); // VPN 연결 후 초기 1초 대기

	// VPN 연결 테스트
    for (int i = 0; i < 5; i++)
    {
        String^ pingTestOutput = RunCommand::runCmd("ping " + serverIP + " -n 1");

        if (pingTestOutput->Contains("TTL="))
        {
            // VPN 연결 성공
            return 0;
        }
        Threading::Thread::Sleep(1000); // 연결 실패 시 1초 대기 후 재시도
    }
	// VPN 연결 실패
	return -31;
}

int WireGuardVPN::disconnectVPN()  
{  
	// VPN 연결 해제
	// 에러 발생 시 -30을 반환
   try  
   {
	   RunCommand::runCmd("wireguard /uninstalltunnelservice " + deviceName);
   }  
   catch (Exception^ e)  
   {  
	   return -30;
   }  

   // config 파일 삭제  
   IO::File::Delete(configPath);

   return 0;
}