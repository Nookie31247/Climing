package com.nsu.climing_server_new.service;

import com.nsu.climing_server_new.exception.ErrorCode;
import com.nsu.climing_server_new.exception.ServiceException;
import org.springframework.stereotype.Service;

import java.util.HashMap;
import java.util.Map;

@Service
/// 동시접속자를 관리하는 서비스
public class CurrentMemberService {
    /// 유저 접속 번호의 시작 번호
    private final int START_USER_NUM = 11;

    /// 최대 동시 접속 가능한 유저의 수를 지정
    private final int MAXIMUM_USER = 10;

    /// 현재 접속한 유저를 저장하는 Map
    /// key: 유저 접속 번호
    /// Value: Member Entity의 id(key)값
    private final Map<Integer, Long> currentUsers = new HashMap<Integer, Long>();

    /// 동시접속자 목록에 유저를 추가합니다.
    /// 최대 동시 접속 가능한 유저를 초과했을 경우 예외처리를 통한 에러코드를 전송합니다.
    /// @param userId Member Entity의 id값
    public void addCurrentUser(Long userId) {
        boolean isAdded = false;
        for(int i = START_USER_NUM; i < START_USER_NUM + MAXIMUM_USER; i++) {
            if(!currentUsers.containsKey(i)) {
                currentUsers.put(i, userId);
                isAdded = true;
                break;
            }
        }
        if(!isAdded) {
            throw new ServiceException(ErrorCode.SESSION_FULL);
        }
    }
}
