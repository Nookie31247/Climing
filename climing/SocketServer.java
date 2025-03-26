package climing;

import java.io.*;
import java.net.*;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.nio.file.*;

public class SocketServer {
    private final String SERVER_IP = "192.168.219.130"; // 서버 IP, 사설 IP 주소 입력할 것
    private final int PORT = 37624; // 서버 포트
    public static int clientCount = 0; // 접속한 클라이언트 수
    private final LocalDateTime startTime = LocalDateTime.now(); // 서버 시작 시간
    private final String LOG_DIR = "logs"; // 로그 폴더명
    private final String LOG_FILE = getLogFileName(); // 로그 파일명

    public void socketStart() {
        try {
            // 로그 폴더 생성 (존재하지 않으면)
            Files.createDirectories(Paths.get(LOG_DIR));

            try (ServerSocket serverSocket = new ServerSocket(PORT, 50, InetAddress.getByName(SERVER_IP))) {
                // 서버 시작 시간 포맷
                String formattedStartTime = startTime.format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));

                // 시스템 정보 가져오기
                String osName = System.getProperty("os.name");
                String javaVersion = System.getProperty("java.version");
                long freeMemory = Runtime.getRuntime().freeMemory() / (1024 * 1024);
                long totalMemory = Runtime.getRuntime().totalMemory() / (1024 * 1024);

                // 서버 상태 출력 및 로그 저장
                String serverStartLog =
                        "[ 소켓 응답 대기중 ]\n" +
                                "   [1] 서버 IP : " + SERVER_IP + "\n" +
                                "   [2] 서버 포트 : " + PORT + "\n" +
                                "   [3] 서버 시작 시간 : " + formattedStartTime + "\n" +
                                "   [4] 운영 체제 : " + osName + "\n" +
                                "   [5] Java 버전 : " + javaVersion + "\n" +
                                "   [6] JVM 메모리 사용량 : " + freeMemory + "MB / " + totalMemory + "MB \n";

                System.out.println(serverStartLog);
                log(serverStartLog);

                // 클라이언트 요청을 계속 수락
                while (true) {
                    Socket clientSocket = serverSocket.accept();
                    clientCount++; // 클라이언트 수 증가

                    // 현재 서버 실행 시간 계산
                    LocalDateTime now = LocalDateTime.now();
                    String uptime = calculateUptime(startTime, now);

                    String clientLog =
                            "[ 새 클라이언트 연결 ]\n" +
                                    "   [1] 클라이언트 IP : " + clientSocket.getInetAddress() + "\n" +
                                    "   [2] 현재 접속 클라이언트 수: " + clientCount + "\n" +
                                    "   [3] 서버 실행 시간: " + uptime + "\n";

                    System.out.println(clientLog);
                    log(clientLog);

                    // 클라이언트 요청을 처리하는 별도의 스레드 생성
                    new ClientHandler(clientSocket).start();
                }
            }
        } catch (IOException e) {
            logError("서버 실행 오류", e);
        }
    }

    // 서버 실행 시간 계산 함수
    private String calculateUptime(LocalDateTime start, LocalDateTime now) {
        long hours = java.time.Duration.between(start, now).toHours();
        long minutes = java.time.Duration.between(start, now).toMinutes() % 60;
        long seconds = java.time.Duration.between(start, now).getSeconds() % 60;
        return hours + "시간 " + minutes + "분 " + seconds + "초";
    }

    // 클라이언트가 종료될 때 호출할 함수
    public synchronized void clientDisconnected(InetAddress clientAddress) {
        clientCount--; // 접속 클라이언트 수 감소
        String disconnectLog =
                "[!] 클라이언트 연결 종료됨: " + clientAddress + "\n" +
                "[!] 현재 접속 클라이언트 수: " + clientCount + " ]";
        System.out.println(disconnectLog);
        log(disconnectLog);
    }

    // 로그 파일명 자동 생성 (YYYY_MM_DD_n.logs)
    private String getLogFileName() {
        String date = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy_MM_dd"));
        int n = 1;
        String fileName;

        do {
            fileName = String.format("%s/%s_%d.logs", LOG_DIR, date, n);
            n++;
        } while (Files.exists(Paths.get(fileName))); // 같은 이름이 존재하면 n 증가

        return fileName;
    }

    // 로그 기록 함수
    private void log(String message) {
        try (FileWriter fw = new FileWriter(LOG_FILE, true);
             BufferedWriter bw = new BufferedWriter(fw);
             PrintWriter out = new PrintWriter(bw)) {

            String timestamp = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
            out.println("[" + timestamp + "] " + message);

        } catch (IOException e) {
            System.err.println("로그 파일 기록 중 오류 발생");
            e.printStackTrace();
        }
    }

    // 오류 로그 기록 함수
    private void logError(String errorMessage, Exception e) {
        try (FileWriter fw = new FileWriter(LOG_FILE, true);
             BufferedWriter bw = new BufferedWriter(fw);
             PrintWriter out = new PrintWriter(bw)) {

            String timestamp = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
            out.println("[" + timestamp + "] [ERROR] " + errorMessage);
            e.printStackTrace(out);

        } catch (IOException ex) {
            System.err.println("로그 파일 기록 중 오류 발생");
            ex.printStackTrace();
        }
    }
}
