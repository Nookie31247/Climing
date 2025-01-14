package climing;

/*
에러 코드
-1: 로그인 시도 시 아이디 비밀번호가 잘못 입력됨
-2: 로그인 시도 시 중복 로그인
-3: 로그인 시도 시 세션이 가득 참
-4: 유저 접속 번호 에러
-10: 기타 서버 에러
 */

public class Main {
    public static void main(String[] args) {
        SocketTest socketTest = new SocketTest();
        socketTest.test();
    }
}