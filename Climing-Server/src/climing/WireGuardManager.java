package climing;

import java.util.*;

/// WireGuard VPN Peer 추가 / 제거 모듈
public class WireGuardManager {
    // 클라이언트 Public Key를 저장합니다.
    // Integer는 유저 접속 번호, String은 Publick Key를 저장합니다.
    final static private HashMap<Integer, String> userData = new HashMap<>();

    final static private String serverVPNIp = "192.168.135.1";


     /// 서버의 WireGuard 공개키를 반환합니다.
    public static String getServerPublicKey() {
        try {
            return RunUbuntuCommand.run("wg show wg public-key");
        } catch (Exception e) {
            ErrorLogManager.getError(e);
            return "-13";
        }
    }

    /// 서버의 VPN IP 주소를 반환합니다.
    public static String getServerVPNIp() {
        return serverVPNIp;
    }

    /**
     * WireGuard VPN에 유저(Peer)를 추가합니다.
     * @param userNum 유저 접속 번호
     * @param clientPublicKey 클라이언트 공개키
     * @return VPN Peer 추가 성공 여부
     */
    public static boolean addPeer(int userNum, String clientPublicKey) {
        // 클라이언트 비밀 키, 클라이언트 공개 키, 클라이언트 IP 주소
        String ipAddress;

        // 유저 접속 번호를 통해 클라이언트 IP 주소 생성
        // 클라이언트 IP 주소의 D 클래스를 유저 접속 번호로 설정
        ipAddress = "192.168.135." + userNum;

        try {
            // 클라이언트 공개키와 IP 주소를 입력해서 WireGuard에 peer 정보를 등록합니다.
            RunUbuntuCommand.run("wg set wg peer " + clientPublicKey + " allowed-ips " + ipAddress);

            // 유저 정보를 저장합니다.
            userData.put(userNum, clientPublicKey);
            return true;
        } catch (Exception e) {
            ErrorLogManager.getError(e);
            return false;
        }
    }

    /**
     * WireGuard VPN에서 현재 유저(Peer)를 삭제합니다.
     * @param userNum 유저 접속 번호
     * @return Peer 삭제 성공 여부
     */
    public static boolean removePeer(int userNum) {
        // userData에 저장된 클라이언트 공개키를 입력하여 WireGuard에 저장된 peer 정보를 제거합니다.
        try {
            RunUbuntuCommand.run("wg set wg peer " + userData.get(userNum) + " remove");
            return true;
        } catch (Exception e) {
            ErrorLogManager.getError(e);
            return false;
        }
    }
}
