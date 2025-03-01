package climing;

// 에러 상황에 대한 로그를 기록하기 위한 클래스

public class ErrorLogManager {
    /// 에러를 Exception 타입으로 받는 메소드입니다. String 타입으로도 받을 수 있습니다.
    public void getError(Exception error) {
        error.printStackTrace();

    }

    /// 에러를 String 타입으로 받는 메소드입니다. Exception 타입으로도 받을 수 있습니다.
    public void getError(String error) {
        System.err.println(error);
    }
}
