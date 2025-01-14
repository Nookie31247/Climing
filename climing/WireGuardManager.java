package climing;

import java.util.*;

/// WireGuard VPN Peer 추가 / 제거 모듈
public class WireGuardManager {

    // 클라이언트 Public Key를 저장합니다.
    // Integer는 유저 접속 번호, String은 Publick Key를 저장합니다.
    final private HashMap<Integer, String> userData = new HashMap<>();

    //우분투 명령어를 자동으로 입력하기 위해 사용합니다.
    final private RunUbuntuCommand command = new RunUbuntuCommand();

    /// 사용자 추가 메소드
    /// 유저 접속 번호를 입력받아 클라이언트 비밀키를 반환합니다.
    /// -1 반환시 리눅스 서버 에러입니다.
    public String addPeer(int userNum) {
        try {
            // 클라이언트 비밀 키, 클라이언트 공개 키, 클라이언트 IP 주소
            String privateKey, publicKey, ipAddress;

            // 클라이언트 비밀키 생성
            privateKey = command.run("wg genkey");

            //클라이언트 비밀키를 통해 클라이언트 공개키 생성
            publicKey = command.run("echo " + privateKey + " | wg pubkey");

            // 유저 접속 번호를 통해 클라이언트 IP 주소 생성
            // 클라이언트 IP 주소의 D 클래스를 유저 접속 번호로 설정
            ipAddress = "192.168.135." + userNum;

            // 클라이언트 공개키와 IP 주소를 입력해서 WireGuard에 peer 정보를 등록합니다.
            command.run("wg set wg peer " + publicKey + " allowed-ips " + ipAddress);

            // 유저 정보를 저장합니다.
            userData.put(userNum, publicKey);
            return privateKey;
        } catch (Exception e) {
            e.printStackTrace();
            return "-1";
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
            e.printStackTrace();
            return false;
        }
    }
}
