package com.nsu.climing_server_new.exception;

import lombok.extern.slf4j.Slf4j;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.RestControllerAdvice;

@Slf4j
@RestControllerAdvice   // 이 클래스는 전역 에러 처리기라는 것을 Spring에 알려준다.
public class GlobalExceptionHandler {
    // @ExceptionHandler를 사용하면 특정 에러가 발생했을 때 여기서 처리할 것을 명시할 수 있다.
    // ServiceException(내가 만든 예외) 가 발생했을 때 여기서 받아서 처리한다.
    @ExceptionHandler(ServiceException.class)
    protected ResponseEntity<ErrorResponse> handelServiceException(ServiceException e) {
        log.error("에러 발생: {}", e.getMessage());

        return ResponseEntity
                .status(e.getErrorCode().getStatus())   // HTTP 상태 코드를 응답 객체에 저장
                .body(ErrorResponse.get(e.getErrorCode()));     // body에는 에러가 왜 발생했는지 보다 구체적인 정보를 담는다.
    }

    // 이건 발생하는 모든 에러를 처리해준다.
    @ExceptionHandler(Exception.class)
    protected ResponseEntity<ErrorResponse> handelException(Exception e) {
        log.error("서버 내부 에러 발생: {}", e.toString());

        return ResponseEntity
                .status(HttpStatus.INTERNAL_SERVER_ERROR)
                .body(ErrorResponse.get(ErrorCode.SERVER_ERROR));
    }
}
