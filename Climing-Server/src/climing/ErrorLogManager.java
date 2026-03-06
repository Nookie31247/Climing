package climing;

/// 에러 상황에 대한 로그를 기록하기 위한 클래스
public class ErrorLogManager {
    /**
     * Exception 타입의 에러를 기록
     * @param error 에러
     */
    public static void getError(Exception error) {
        error.printStackTrace();
    }

    /**
     * String 타입의 에러를 기록
     * @param error 에러
     */
    public static void getError(String error) {
        System.err.println(error);
    }
}
