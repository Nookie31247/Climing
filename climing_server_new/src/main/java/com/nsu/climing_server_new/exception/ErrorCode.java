package com.nsu.climing_server_new.exception;

import lombok.Getter;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;

@Getter
@RequiredArgsConstructor
public enum ErrorCode {
    EMAIL_NOT_FOUND(HttpStatus.NOT_FOUND, "USER_001", "존재하지 않는 이메일입니다.."),
    INVALID_PASSWORD(HttpStatus.UNAUTHORIZED, "USER_O002", "비밀번호가 잘못되었습니다."),
    EMAIL_ALREADY_EXIST(HttpStatus.CONFLICT, "USER_003", "이미 존재하는 이메일입니다."),
    ALREADY_LOGGED_IN(HttpStatus.CONFLICT, "USER_004", "이미 로그인 중인 아이디입니다."),
    ACCOUNT_DISABLED(HttpStatus.UNAUTHORIZED, "USER_005", "회원탈퇴된 유저입니다."),

    WRONG_CONNECTION_NUM(HttpStatus.NOT_FOUND, "SESSION_001", "유저 접속 번호가 잘못되었습니다."),
    SESSION_FULL(HttpStatus.TOO_MANY_REQUESTS, "SESSION_002", "최대 접속 가능한 유저가 초과되었습니다."),

    SERVER_ERROR(HttpStatus.INTERNAL_SERVER_ERROR, "SERVER_001", "기타 서버 에러가 발생했습니다.");

    private final HttpStatus status;
    private final String code;
    private final String message;


    @Override
    public String toString() {
        return getCode() + " - " + getMessage();
    }
}
