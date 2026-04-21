package com.nsu.climing_server_new.service;

import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Service;

@Service
@Slf4j
/// iSCSI 가상 하드디스크 서비스를 관리하는 서비스입니다.
/// 본 프로젝트에서는 실제 iSCSI 가상 하드디스크 타겟을 생성하지 않고 단순 로그만 출력합니다.
public class ISCSIManageService {
    private final boolean[] currentUser = new boolean[256];

    public void addTarget(int userNum) {
        // 새로운 타겟을 생성하는 명령어
        String createTargetCommand = "tgtadm --lld iscsi --op new --mode target --tid " + userNum +
                " -T iqn.2024-11.com.climing:" + userNum;

    }
}
