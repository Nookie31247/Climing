package server;

import handler.RequestHandler;
import manager.SessionManager;
import model.Request;
import model.Response;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;


// ServerMain.java - 서버의 메인 클래스
// 클라이언트의 연결을 수락하고 처리하는 역할을 함.
// 다중 클라이언트 지원을 위해 스레드 풀을 사용함.
public class ServerMain {
    private static final int PORT = ServerConfig.SERVER_PORT; // 서버가 실행될 포트
    private static final int MAX_CLIENTS = ServerConfig.MAX_CLIENTS; // 최대 동시 접속 클라이언트 수
    private static boolean isRunning = true; // 서버 실행 여부 플래그

    public static void main(String[] args) {
        // 최대 클라이언트 수만큼 스레드를 사용할 수 있는 스레드 풀 생성
        ExecutorService clientThreadPool = Executors.newFixedThreadPool(MAX_CLIENTS);

        // 서버 소켓 생성
        try (ServerSocket serverSocket = new ServerSocket(PORT)) {
            System.out.println("서버가 포트 " + PORT + "에서 실행 중입니다.");

            // 서버 실행 상태를 유지하며 클라이언트 연결을 기다림
            while (isRunning) {
                Socket clientSocket = serverSocket.accept(); // 클라이언트 연결 수락
                System.out.println("새 클라이언트 접속했습니다. \n클라이언트: " + clientSocket.getInetAddress());

                // 클라이언트 요청을 별도의 스레드에서 처리
                clientThreadPool.execute(() -> handleClient(clientSocket));
            }
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            // 서버 종료 시 스레드 풀을 정리
            clientThreadPool.shutdown();
        }
    }

    // 클라이언트의 요청을 처리하는 메서드
    // @param clientSocket 클라이언트 소켓
    private static void handleClient(Socket clientSocket) {
        try (
                var input = clientSocket.getInputStream();
                var output = clientSocket.getOutputStream()
        ) {
            byte[] requestData = new byte[1024];
            int bytesRead = input.read(requestData);
            if (bytesRead == -1) {
                System.out.println("클라이언트 연결 종료");
                return;
            }

            // 요청 처리
            Request request = Request.fromBytes(requestData);
            Response response = RequestHandler.processRequest(request);

            // 응답 전송
            output.write(response.toBytes());
            output.flush();

            System.out.println("응답 전송 완료: " + response.getMessage());

        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            try {
                clientSocket.close();
                System.out.println("클라이언트 연결 종료");
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }
}

