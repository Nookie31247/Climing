package server;

/**
 * ServerConfig.java - 서버 설정을 저장하는 클래스
 * - 서버 포트 및 최대 동시 접속 클라이언트 수를 설정함.
 * - 향후 설정을 쉽게 변경할 수 있도록 구성됨.
 */
public class ServerConfig {
    // 서버가 실행될 포트 번호
    public static final int SERVER_PORT = 8080;

    // 최대 동시 접속 가능한 클라이언트 수
    public static final int MAX_CLIENTS = 10;

    // 서버 IP 주소 (필요 시 수정 가능)
    public static final String SERVER_IP = "127.0.0.1";
}
