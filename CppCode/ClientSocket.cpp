#include "ClientSocket.hpp"

using namespace CppCode;
using namespace Newtonsoft::Json;
using namespace Newtonsoft::Json::Linq;

// ==================== 소켓 생성, 삭제, 데이터 송수신 ====================

int ClientSocket::openSocket() {
	// 서버 IP 주소를 String^에서 std::string으로 변환
    msclr::interop::marshal_context context;
    std::string serverIp_string = context.marshal_as<std::string>(const_cast<String^>(serverIp));

	// Winsock 초기화
	wsaData = new WSADATA();
	if (WSAStartup(MAKEWORD(2, 2), wsaData) != 0) {
		Console::WriteLine("[!] WinSock 초기화를 실패하였습니다.");
		return -52;
	}

	// 소켓 생성
	sock = socket(AF_INET, SOCK_STREAM, 0);
	if (sock == INVALID_SOCKET) {
		Console::WriteLine("[!] 소켓 생성을 실패하였습니다.");
		WSACleanup();
		return -52;
	}

	// 서버 주소 설정
	serverAddr = new sockaddr_in();
	serverAddr->sin_family = AF_INET;
	serverAddr->sin_port = htons(serverPort);
	inet_pton(AF_INET, serverIp_string.c_str(), &serverAddr->sin_addr);

    // 서버 연결
    if (connect(sock, (sockaddr*)serverAddr, sizeof(*serverAddr)) == SOCKET_ERROR) {
        Console::WriteLine("[!] 서버 연결에 실패하였습니다.\n");
        closesocket(sock);
        WSACleanup();
        return -51;
    }

    Console::WriteLine("[!] 서버에 연결되었습니다!\n");
	return 0;
}
void ClientSocket::closeSocket()
{
	// 소켓 종료
	closesocket(sock);
	WSACleanup();
	Console::WriteLine("서버 연결 종료");
}

void ClientSocket::sendRequest(String^ requestJson) {
	// UTF-8 인코딩을 유지하면서 String^을 std::string으로 변환
	// 
	// JSON 파일을 UTF-8 인코딩으로 변환
	array<Byte>^ utf8Bytes = Text::Encoding::UTF8->GetBytes(requestJson);
	// UTF-8 바이트 배열을 std::string으로 변환
	pin_ptr<Byte> pinnedBytes = &utf8Bytes[0];
	std::string requestJson_string(reinterpret_cast<char*>(pinnedBytes), utf8Bytes->Length);

	requestJson_string += "\n";
	 
	send(sock, requestJson_string.c_str(), static_cast<int>(requestJson_string.length()), 0);
}

String^ ClientSocket::receiveResponse() {
    std::string response;
    char buffer[4096];		// 응답 버퍼. 한 번에 최대 4096바이트까지 읽을 수 있음
    int bytesReceived;

	// \0 문자열을 만날 때 까지 응답을 받아 response에 저장
    while ((bytesReceived = recv(sock, buffer, sizeof(buffer) - 1, 0)) > 0) {
        buffer[bytesReceived] = '\0';  // 문자열 종료 처리
        response += buffer;
        if (response.find("\n") != std::string::npos) break; // 개행 문자 도착 시 종료
    }

	// ANSI에서 UTF-8로 변환
	array<Byte>^ utf8Bytes = gcnew array<Byte>(response.length());
	for (size_t i = 0; i < response.length(); i++) {
		utf8Bytes[i] = static_cast<Byte>(response[i]);
	}

	// UTF-8을 UTF-16으로 변환
	return Text::Encoding::UTF8->GetString(utf8Bytes);
}

// ==================== 서버로 전송하는 요청 ====================

KeyValuePair<int, String^> ClientSocket::login(String^ id, String^ pw) {
	//비밀번호를 암호화하여 저장
	String^ hashedPassword = passwordHashing(pw);
	
	//소켓 생성. 실패 시 에러 코드 반환
	int socketErrorCode = openSocket();
	if (socketErrorCode != 0) {
		return KeyValuePair<int, String^>(socketErrorCode, nullptr);
	}

	String^ loginJson = createLoginJson(id, hashedPassword);	//로그인 JSON 파일 생성
	sendRequest(loginJson);										//서버에 JSON 파일 전송	
	String^ response = receiveResponse();						//서버로부터 응답받은 JSON 파일
	closeSocket();												//소켓 종료

	// 서버로부터 응답받은 JSON 파일을 Dictionary로 변환
	Dictionary<String^, Object^>^ jsonDict = JsonConvert::DeserializeObject<Dictionary<String^, Object^>^>(response);

	int errorCode = Convert::ToInt32(jsonDict["error_code"]); 
	
	if (errorCode != 0) {
		return KeyValuePair<int, String^>(errorCode, nullptr);		// 에러 코드 반환
	}

	int userNum = Convert::ToInt32(jsonDict["user_connect_number"]);
	String^ nickname = Convert::ToString(jsonDict["user_nickname"]);	
	String^ serverPubKey = Convert::ToString(jsonDict["vpn_server_public_key"]);
	String^ serverVpnIp = Convert::ToString(jsonDict["vpn_server_ipv4"]);
	String^ vpnEndpoint = Convert::ToString(jsonDict["vpn_endpoint_ipv4"]);

	// 예외 처리 보강하기

	int vpnResult = WireGuardVPN::connectVPN(userNum, serverPubKey, serverVpnIp, vpnEndpoint);	// VPN에 연결
	
	// VPN 연결 실패 시 에러 코드 반환
	if (vpnResult != 0) {
		return KeyValuePair<int, String^>(vpnResult, nullptr);		// 에러 코드 반환
	}

	ISCSI::connect(userNum, serverVpnIp);	// iSCSI Target에 연결

	return KeyValuePair<int, String^>(userNum, nickname);
}

int ClientSocket::logout(String^ id, int userNum) {
	//소켓 생성. 실패 시 에러 코드 반환
	int socketErrorCode = openSocket();
	if (socketErrorCode != 0) {
		return socketErrorCode;
	}

	String^ logoutJson = createLogoutJson(id, userNum);		// 로그아웃 JSON 파일 생성
	sendRequest(logoutJson);								// 서버에 JSON 파일 전송
	String^ response = receiveResponse();					// 서버로부터 응답받은 JSON 파일
	closeSocket();											// 소켓 종료

	// 서버로부터 응답받은 JSON 파일을 Dictionary로 변환
	Dictionary<String^, Object^>^ jsonDict = JsonConvert::DeserializeObject<Dictionary<String^, Object^>^>(response);

	ISCSI::disconnect();			// iSCSI 연결 해제
	WireGuardVPN::disconnectVPN();		// VPN 연결 해제

	return Convert::ToInt32(jsonDict["error_code"]);	// 에러 코드 반환
}

int ClientSocket::addUser(String^ id, String^ pw, String^ name) {
	String^ hashedPassword = passwordHashing(pw);	// 비밀번호를 암호화하여 저장

	//소켓 생성. 실패 시 에러 코드 반환
	int socketErrorCode = openSocket();
	if (socketErrorCode != 0) {
		return socketErrorCode;
	}
	String^ signupJson = createSignupJson(id, hashedPassword, name);	// 회원가입 JSON 파일 생성
	sendRequest(signupJson);										// 서버에 JSON 파일 전송
	String^ response = receiveResponse();							// 서버로부터 응답받은 JSON 파일
	closeSocket();														// 소켓 종료

	// 서버로부터 응답받은 JSON 파일을 Dictionary로 변환
	Dictionary<String^, Object^>^ jsonDict = JsonConvert::DeserializeObject<Dictionary<String^, Object^>^>(response);
	
	return Convert::ToInt32(jsonDict["error_code"]);	// 에러 코드 반환
}

int ClientSocket::deleteUser(String^ id, String^ pw) {
	String^ hashedPassword = passwordHashing(pw);	// 비밀번호를 암호화하여 저장

	//소켓 생성. 실패 시 에러 코드 반환
	int socketErrorCode = openSocket();
	if (socketErrorCode != 0) {
		return socketErrorCode;
	}

	String^ deleteUserJson = createDeleteUserJson(id, hashedPassword);	// 회원탈퇴 JSON 파일 생성
	sendRequest(deleteUserJson);										// 서버에 JSON 파일 전송
	String^ response = receiveResponse();								// 서버로부터 응답받은 JSON 파일
	closeSocket();														// 소켓 종료

	// 서버로부터 응답받은 JSON 파일을 Dictionary로 변환
	Dictionary<String^, Object^>^ jsonDict = JsonConvert::DeserializeObject<Dictionary<String^, Object^>^>(response);
	
	return Convert::ToInt32(jsonDict["error_code"]);	// 에러 코드 반환
}

List<Game^>^ ClientSocket::getGameList() {
	openSocket();											// 소켓 생성
	String^ getGameListJson = createGetGameListJson();		// 게임 리스트 요청 JSON 생성
	sendRequest(getGameListJson);							// 서버에 JSON 파일 전송
	String^ response = receiveResponse();					// 서버로부터 응답받은 JSON 파일
	closeSocket();											// 소켓 종료

	// 게임 정보를 저장하기 위한 리스트
	List<Game^>^ gameList = gcnew List<Game^>();

	// JSON 파일로부터 게임 정보 읽어서 List<Game^>^ 타입으로 저장
	try {
		JObject^ jsonResponse = JObject::Parse(response);

		if (jsonResponse->ContainsKey("gameList")) {
			JArray^ jsonArray = safe_cast<JArray^>(jsonResponse["gameList"]);

			for each (JObject ^ gameJson in jsonArray) {
				Game^ game = gcnew Game();
				game->name = gameJson["name"]->ToString();
				game->imageUrl = gameJson["imageUrl"]->ToString();
				game->company = gameJson["company"]->ToString();
				game->genre = gameJson["genre"]->ToString();
				game->dirPath = gameJson["dirPath"]->ToString();
				game->identifyNum = gameJson["identifyNum"]->ToObject<int>();
				gameList->Add(game);
			}
		}
	}
	catch (JsonReaderException^ e) {
		Console::WriteLine("JSON 파싱 오류: " + e->Message);
	}

	return gameList;
}

// ==================== 서버로 보낼 JSON 파일 생성 ====================

String^ ClientSocket::createLoginJson(String^ userId, String^ hashedPassword) {
    Dictionary<String^, Object^>^ request = gcnew Dictionary<String^, Object^>();
    request["type"] = "Login";
    request["user_id"] = userId;
    request["hashed_user_password"] = hashedPassword;
    request["client_public_key"] = WireGuardVPN::getClientPublicKey();

    return JsonConvert::SerializeObject(request);
}

String^ ClientSocket::createLogoutJson(String^ userId, int userConnectNumber) {
    Dictionary<String^, Object^>^ request = gcnew Dictionary<String^, Object^>();
    request["type"] = "Logout";
    request["user_id"] = userId;
	request["user_connect_number"] = userConnectNumber;

	String^ jsonString = JsonConvert::SerializeObject(request);
	Console::WriteLine(request["user_connect_number"]);
	Console::WriteLine(jsonString);

	return JsonConvert::SerializeObject(request);
}

String^ ClientSocket::createSignupJson(String^ userId, String^ hashedPassword, String^ nickname) {
    Dictionary<String^, Object^>^ request = gcnew Dictionary<String^, Object^>();
    request["type"] = "addUser";
    request["user_id"] = userId;
    request["hashed_user_password"] = hashedPassword;
    request["user_nickname"] = nickname;

	return JsonConvert::SerializeObject(request);
}

String^ ClientSocket::createDeleteUserJson(String^ userId, String^ hashedPassword) {
    
	Dictionary<String^, Object^>^ request = gcnew Dictionary<String^, Object^>();
    request["type"] = "deleteUser";
    request["user_id"] = userId;
    request["hashed_user_password"] = hashedPassword;

    return JsonConvert::SerializeObject(request);
}

String^ ClientSocket::createGetGameListJson() {
	Dictionary<String^, Object^>^ request = gcnew Dictionary<String^, Object^>();
	request["type"] = "getGameList";
	return JsonConvert::SerializeObject(request);
}

String^ ClientSocket::passwordHashing(String^ password) {
	Security::Cryptography::SHA256^ sha256 = Security::Cryptography::SHA256::Create();
	array<Byte>^ data = Text::Encoding::UTF8->GetBytes(password);
	array<Byte>^ hash = sha256->ComputeHash(data);

	Text::StringBuilder^ hashString = gcnew Text::StringBuilder();
	hashString->Clear();
	for (int i = 0; i < hash->Length; i++) {
		hashString->Append(hash[i].ToString("x2"));
	}

	return hashString->ToString();
}