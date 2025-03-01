package climing;

import java.util.*;

/// WireGuard VPN Peer 추가 / 제거 모듈
public class WireGuardManager {
    ErrorLogManager error = new ErrorLogManager();

    // 클라이언트 Public Key를 저장합니다.
    // Integer는 유저 접속 번호, String은 Publick Key를 저장합니다.
    final private HashMap<Integer, String> userData = new HashMap<>();

    //우분투 명령어를 자동으로 입력하기 위해 사용합니다.
    final private RunUbuntuCommand command = new RunUbuntuCommand();

    public String getServerPublicKey() {
        try {
            return command.run("wg show wg public-key");
        } catch (Exception e) {
            error.getError(e);
            return "-13";
        }
    }

    /// 사용자 추가 메소드
    /// 유저 접속 번호를 입력받아 클라이언트 비밀키를 반환합니다.
    /// -1 반환시 리눅스 서버 에러입니다.
    public boolean addPeer(int userNum, String clientPublicKey) {
        // 클라이언트 비밀 키, 클라이언트 공개 키, 클라이언트 IP 주소
        String ipAddress;

        // 유저 접속 번호를 통해 클라이언트 IP 주소 생성
        // 클라이언트 IP 주소의 D 클래스를 유저 접속 번호로 설정
        ipAddress = "192.168.135." + userNum;

        try {
            // 클라이언트 공개키와 IP 주소를 입력해서 WireGuard에 peer 정보를 등록합니다.
            command.run("wg set wg peer " + clientPublicKey + " allowed-ips " + ipAddress);

            // 유저 정보를 저장합니다.
            userData.put(userNum, clientPublicKey);
            return true;
        } catch (Exception e) {
            error.getError(e);
            return false;
        }
    }

    /// 사용자 삭제 메소드
    /// 사용자 제거 성공 시 true, 실패 시 false를 반환합니다.
    public boolean removePeer(int userNum) {
        // userData에 저장된 클라이언트 공개키를 입력하여 WireGuard에 저장된 peer 정보를 제거합니다.
        try {
            command.run("wg set wg peer " + userData.get(userNum) + " remove");
            return true;
        } catch (Exception e) {
            error.getError(e);
            return false;
        }
    }
}
