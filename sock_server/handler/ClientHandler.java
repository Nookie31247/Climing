package handler;

import model.Request;
import model.Response;

import java.io.*;
import java.net.Socket;

/**
 * ClientHandler.java - 클라이언트의 요청을 처리하는 클래스
 * - 클라이언트와의 연결을 유지하며 요청을 수신하고 응답을 전송함.
 * - 멀티스레드 환경에서 동작할 수 있도록 Runnable을 구현함.
 */
public class ClientHandler implements Runnable {
    private final Socket clientSocket; // 클라이언트 소켓

    /**
     * 생성자: 클라이언트 소켓을 초기화
     * @param socket 클라이언트 소켓
     */
    public ClientHandler(Socket socket) {
        this.clientSocket = socket;
    }

    @Override
    public void run() {
        try (
                ObjectInputStream input = new ObjectInputStream(clientSocket.getInputStream());
                ObjectOutputStream output = new ObjectOutputStream(clientSocket.getOutputStream())
        ) {
            while (true) {
                // 클라이언트 요청 읽기
                Request request = (Request) input.readObject();
                if (request == null) {
                    System.out.println("클라이언트 연결 종료.");
                    break;
                }

                System.out.println("요청 수신: " + request.getType() + " - 사용자 ID: " + request.getId());

                // 요청 처리 후 응답 생성
                Response response = RequestHandler.processRequest(request);

                // 클라이언트에게 응답 전송
                output.writeObject(response);
                output.flush();
                System.out.println("응답 전송 완료: " + response.getMessage());
            }
        } catch (IOException | ClassNotFoundException e) {
            System.err.println("클라이언트 통신 오류: " + e.getMessage());
        } finally {
            try {
                clientSocket.close();
                System.out.println("클라이언트 소켓 닫힘.");
            } catch (IOException e) {
                System.err.println("소켓 닫기 실패: " + e.getMessage());
            }
        }
    }
}
