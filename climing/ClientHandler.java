package climing;

import java.io.*;
import java.net.*;
import org.json.simple.*;
import org.json.simple.parser.*;

/// 클라이언트로부터 JSON 파일을 받아 오는 클래스
public class ClientHandler extends Thread {
    private final Socket clientSocket;

    public ClientHandler(Socket socket) {
        this.clientSocket = socket;
    }

    @Override
    public void run() {
        try (
             BufferedReader reader = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
             PrintWriter writer = new PrintWriter(clientSocket.getOutputStream(), true)) {

            StringBuilder requestBuilder = new StringBuilder();
            String line;
            while ((line = reader.readLine()) != null) {  // 여러 줄을 받을 수 있도록 수정
                requestBuilder.append(line);
                if (line.endsWith("}")) break;  // JSON 종료 문자 '}'를 만나면 종료
            }

            String requestJson = requestBuilder.toString();

            try {
                JSONParser parser = new JSONParser();
                JSONObject request = (JSONObject) parser.parse(requestJson);
                System.out.println("[REQUEST] 받은 요청 : " + request.toJSONString());

                // 요청 유형 확인
                String type = (String) request.get("type");
                JSONObject response;

                switch (type) {
                    case "Login":
                        response = RequestHandler.handleLogin(request);
                        break;
                    case "Logout":
                        response = RequestHandler.handleLogout(request);
                        break;
                    case "addUser":
                        response = RequestHandler.handleRegister(request);
                        break;
                    case "deleteUser":
                        response = RequestHandler.handleDeleteUser(request);
                        break;
                    case "getGameList":
                        response = RequestHandler.handleGameList();
                        break;
                    default:
                        response = new JSONObject();
                        response.put("error_code", 99);
                        response.put("message", "Unknown request type");
                }

                writer.println(response.toJSONString());
                System.out.println(SocketServer.clientCount + "[RESPONSE] 보낸 응답: " + response.toJSONString());

            } catch (ParseException e) {
                System.out.println("[ERROR] JSON 파싱 오류: " + requestJson);
            }
        } catch (IOException e) {
            System.out.println("[ERROR] 클라이언트 연결 오류: " + clientSocket.getInetAddress());
        } finally {
            try {
                System.out.println("[END] 클라이언트 연결 종료됨: " + clientSocket.getInetAddress());
                clientSocket.close();
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }
}
