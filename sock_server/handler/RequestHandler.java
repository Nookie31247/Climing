package handler;

import manager.SessionManager;
import model.Request;
import model.Response;
import handler.DatabaseHandler;

/**
 * RequestHandler.java - 클라이언트의 요청을 처리하는 클래스
 * - 클라이언트 요청을 받아서 해당하는 동작을 수행하고 응답을 반환함.
 */
public class RequestHandler {

    /**
     * 클라이언트의 요청을 처리하는 메서드
     * @param request 클라이언트가 보낸 요청 객체
     * @return 요청에 대한 응답 객체
     */
    public static Response processRequest(Request request) {
        switch (request.getType()) {
            case LOGIN:
                return SessionManager.handleLogin(request); // 로그인 처리
            case SIGNUP:
                return DatabaseHandler.handleSignup(request); // 회원가입 처리
            case LOGOUT:
                return SessionManager.handleLogout(request); // 로그아웃 처리
            case GAMESLIST:
                return DatabaseHandler.getGameList(); // 게임 리스트 반환
            case ACCOUNT_DELETE:
                return DatabaseHandler.handleAccountDelete(request); // 계정 삭제 처리
            default:
                return new Response(false, "알 수 없는 요청 유형입니다.");
        }
    }
}
