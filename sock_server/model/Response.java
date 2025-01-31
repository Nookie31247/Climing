package model;

import java.io.Serializable;

 // Response.java - 서버가 클라이언트에게 보내는 응답을 나타내는 클래스
 // - 응답 성공 여부와 메시지를 포함함.
 // - 직렬화를 지원하여 네트워크 통신에서 객체를 전송할 수 있도록 설계됨.
public class Response implements Serializable {
    private static final long serialVersionUID = 1L; // 직렬화 버전 UID

    private boolean success;  // 요청 처리 성공 여부 (true: 성공, false: 실패)
    private String message;   // 응답 메시지

    // 기본 생성자 (기본적으로 실패 상태)
    public Response() {
        this.success = false;
        this.message = "";
    }

    // 응답 객체 생성자
    // @param success 요청 성공 여부
    // @param message 응답 메시지
    public Response(boolean success, String message) {
        this.success = success;
        this.message = message;
    }

    // Getter 메서드
    public boolean isSuccess() { return success; }
    public String getMessage() { return message; }

    // Setter 메서드
    public void setSuccess(boolean success) { this.success = success; }
    public void setMessage(String message) { this.message = message; }

    // 객체를 직렬화 가능한 바이트 배열로 변환하는 메서드
    // @return 바이트 배열
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

     // 바이트 배열을 Response 객체로 변환하는 메서드
     // @param data 바이트 배열
     // @return Response 객체
    public static Response fromBytes(byte[] data) {
        try {
            java.io.ByteArrayInputStream bais = new java.io.ByteArrayInputStream(data);
            java.io.ObjectInputStream ois = new java.io.ObjectInputStream(bais);
            return (Response) ois.readObject();
        } catch (Exception e) {
            e.printStackTrace();
            return new Response();
        }
    }
}
