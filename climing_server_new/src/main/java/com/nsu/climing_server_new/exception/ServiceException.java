package com.nsu.climing_server_new.exception;

import lombok.Getter;

@Getter
public class ServiceException extends RuntimeException {
    private final ErrorCode errorCode;

    public ServiceException(ErrorCode errorCode) {
        super(errorCode.toString());
        this.errorCode = errorCode;
    }
}
