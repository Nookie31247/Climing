package model;

import java.io.Serializable;

/**
 * Request.java - 클라이언트가 서버에 보내는 요청을 나타내는 클래스
 * - 요청 유형과 사용자 정보를 포함함.
 * - 직렬화를 지원하여 네트워크 통신에서 객체를 전송할 수 있도록 설계됨.
 */
public class Request implements Serializable {
    private static final long serialVersionUID = 1L; // 직렬화 버전 UID

    /**
     * 요청 유형을 정의하는 enum
     */
    public enum RequestType {
        UNKNOWN,        // 알 수 없는 요청
        LOGIN,          // 로그인 요청
        LOGOUT,         // 로그아웃 요청
        SIGNUP,         // 회원가입 요청
        GAMESLIST,      // 게임 리스트 요청
        ACCOUNT_DELETE  // 계정 삭제 요청
    }

    private RequestType type;  // 요청 유형
    private String id;         // 사용자 ID
    private String password;   // 사용자 비밀번호

    /**
     * 기본 생성자 (기본적으로 UNKNOWN 타입)
     */
    public Request() {
        this.type = RequestType.UNKNOWN;
        this.id = "";
        this.password = "";
    }

    /**
     * 요청 객체 생성자
     * @param type 요청 유형
     * @param id 사용자 ID
     * @param password 사용자 비밀번호
     */
    public Request(RequestType type, String id, String password) {
        this.type = type;
        this.id = id;
        this.password = password;
    }

    // Getter 메서드
    public RequestType getType() { return type; }
    public String getId() { return id; }
    public String getPassword() { return password; }

    // Setter 메서드
    public void setType(RequestType type) { this.type = type; }
    public void setId(String id) { this.id = id; }
    public void setPassword(String password) { this.password = password; }

    /**
     * 객체를 직렬화 가능한 바이트 배열로 변환하는 메서드
     * @return 바이트 배열
     */
    public byte[] toBytes() {
        try {
            java.io.ByteArrayOutputStream baos = new java.io.ByteArrayOutputStream();
            java.io.ObjectOutputStream oos = new java.io.ObjectOutputStream(baos);
            oos.writeObject(this);
            return baos.toByteArray();
        } catch (Exception e) {
            e.printStackTrace();
            return new byte[0];
        }
    }

    /**
     * 바이트 배열을 Request 객체로 변환하는 메서드
     * @param data 바이트 배열
     * @return Request 객체
     */
    public static Request fromBytes(byte[] data) {
        try {
            java.io.ByteArrayInputStream bais = new java.io.ByteArrayInputStream(data);
            java.io.ObjectInputStream ois = new java.io.ObjectInputStream(bais);
            return (Request) ois.readObject();
        } catch (Exception e) {
            e.printStackTrace();
            return new Request();
        }
    }
}
