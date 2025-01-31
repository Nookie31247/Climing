package handler;

import model.Request;
import model.Response;

import java.util.HashMap;
import java.util.List;
import java.util.ArrayList;

/**
 * DatabaseHandler.java - 사용자 데이터 및 게임 리스트를 관리하는 클래스
 * - 회원가입, 계정 삭제, 게임 리스트 반환 등의 기능을 제공함.
 */
public class DatabaseHandler {
    // 가상의 데이터베이스 (테스트용)
    private static final HashMap<String, String> userDatabase = new HashMap<>();
    private static final List<String> gameList = new ArrayList<>();

    // 초기 데이터 설정
    static {
        userDatabase.put("test_user", "securepassword"); // 테스트 사용자 추가
        gameList.add("Game 1");
        gameList.add("Game 2");
        gameList.add("Game 3");
        gameList.add("Game 4");
    }

    /**
     * 회원가입 처리 메서드
     * @param request 회원가입 요청 객체
     * @return 회원가입 결과 응답 객체
     */
    public static Response handleSignup(Request request) {
        if (userDatabase.containsKey(request.getId())) {
            return new Response(false, "회원가입 실패: 중복된 아이디");
        }
        userDatabase.put(request.getId(), request.getPassword());
        return new Response(true, "회원가입 성공");
    }

    /**
     * 게임 리스트 반환 메서드
     * @return 게임 리스트 응답 객체
     */
    public static Response getGameList() {
        return new Response(true, "게임 목록: " + String.join(", ", gameList));
    }

    /**
     * 계정 삭제 처리 메서드
     * @param request 계정 삭제 요청 객체
     * @return 계정 삭제 결과 응답 객체
     */
    public static Response handleAccountDelete(Request request) {
        if (userDatabase.containsKey(request.getId()) &&
                userDatabase.get(request.getId()).equals(request.getPassword())) {
            userDatabase.remove(request.getId());
            return new Response(true, "계정 삭제 완료");
        }
        return new Response(false, "계정 삭제 실패: 아이디 또는 비밀번호 오류");
    }
}
