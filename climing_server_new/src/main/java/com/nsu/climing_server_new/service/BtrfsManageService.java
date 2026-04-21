package com.nsu.climing_server_new.service;

import com.nsu.climing_server_new.exception.ErrorCode;
import com.nsu.climing_server_new.exception.ServiceException;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Service;

@Service
@Slf4j
/// Btrfs 볼륨을 관리하는 서비스입니다.
/// 본 프로젝트에서는 실제 볼륨에 접근하지 않고 로그만 출력합니다.
public class BtrfsManageService {
    private final boolean[] currentUser = new boolean[256];

    /// btrfs 서브볼륨을 추가하기 위한 메서드입니다.
    /// @param userNum 유저 접속 번호
    public void addVolume(int userNum) {
        String command = "btrfs subvolume snapshot /gamedisk/original /gamedisk/" + userNum;
        log.info("Ubuntu 명령어 실행: {}", command);
        if(!currentUser[userNum]) {
            currentUser[userNum] = true;
        }
        else {
            // 만약 이미 사용중인 볼륨에 유저를 할당하려고 하면 에러를 출력합니다.
            throw new ServiceException(ErrorCode.SERVER_ERROR);
        }
    }

    /// btrfs 서브볼륨을 제거하기 위한 메서드입니다.
    /// @param userNum 유저 접속 번호
    public void removeVolume(int userNum) {
        String command = "btrfs subvolume delete /gamedisk/" + userNum;
        log.info("Ubuntu 명령어 실행: {}", command);
        if(currentUser[userNum]) {
            currentUser[userNum] = false;
        }
        else {
            throw new ServiceException(ErrorCode.SERVER_ERROR);
        }
    }
}
