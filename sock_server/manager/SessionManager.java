package manager;

import java.util.HashSet;
import java.util.Set;
import model.Request;
import model.Response;

/**
 * SessionManager.java - 로그인한 사용자 세션을 관리하는 클래스
 * - 로그인한 사용자를 추적하여 세션을 유지함.
 * - 중복 로그인 방지 및 로그아웃 기능을 제공함.
 */
public class SessionManager {
    // 현재 로그인한 사용자 목록 (중복 로그인 방지)
    private static final Set<String> activeSessions = new HashSet<>();

    /**
     * 사용자가 로그인했는지 확인하는 메서드
     * @param id 사용자 ID
     * @return 로그인 상태 (true: 로그인됨, false: 로그인되지 않음)
     */
    public static boolean isLoggedIn(String id) {
        return activeSessions.contains(id);
    }

    /**
     * 로그인 처리 메서드
     * @param request 로그인 요청 객체
     * @return 로그인 결과 응답 객체
     */
    public static Response handleLogin(Request request) {
        if (isLoggedIn(request.getId())) {
            return new Response(false, "이미 로그인된 사용자입니다.");
        }
        activeSessions.add(request.getId()); // 로그인 성공 시 세션 저장
        return new Response(true, "로그인 성공: " + request.getId());
    }

    /**
     * 로그아웃 처리 메서드
     * @param request 로그아웃 요청 객체
     * @return 로그아웃 결과 응답 객체
     */
    public static Response handleLogout(Request request) {
        if (!isLoggedIn(request.getId())) {
            return new Response(false, "로그아웃 실패: 로그인 상태가 아닙니다.");
        }
        activeSessions.remove(request.getId()); // 세션 제거
        return new Response(true, "로그아웃 성공: " + request.getId());
    }
}
