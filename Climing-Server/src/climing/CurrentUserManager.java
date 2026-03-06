package climing;

/*
    - 로그인한 모든 유저는 각각의 유저 접속 번호를 부여받습니다.
    - 유저 접속 번호는 11부터 시작합니다.
    - WireGuard VPN의 D클래스 IP 주소, iSCSI Target 번호, Btrfs 서브볼륨의 이름과
      유저 접속 번호를 모두 동일하게 설정하여 알아보기 쉽게 하도록 합니다.
 */


/// 접속 유저 관리 모듈
public class CurrentUserManager {
    /// 유저 접속 번호의 시작 번호
    private static final int startUserNum = 11;    // 를 지정합니다.

    /// 최대 동시 접속 가능한 유저의 수를 지정
    private static final int numOfUser = 10;

    private static class CurUserData {
        /// 유저 접속 번호
        public int userNum = 0;

        /// 유저 id
        public String id = "";
    }

    /// 현재 접속 유저를 저장하는 CurUserData의 배열
    static CurUserData[] user = new CurUserData[numOfUser];    // CurUserDatd의 객체를 최대 동시 접속 가능한 유저의 수만큼 생성합니다.

    public static void init () {
        // CurrentUserManager 클래스 생성 시 유저 번호를 시작 번호부터
        // 최대 동시 접속 가능한 유저 수만큼 미리 등록해놓습니다.
        for (int i = 0; i < numOfUser; i++) {
            user[i] = new CurUserData();
            user[i].userNum = i + startUserNum;
        }
    }

    /**
     * 로그인 기능
     * @param id 로그인할 유저의 아이디
     * @param pw 로그인할 유저의 비밀번호
     * @param clientPublicKey 로그인할 유저의 클라이언트의 WireGuard 공개키
     * @return 유저 접속 번호
     */
    public static int login(String id, String pw, String clientPublicKey) {
        // 로그인하고자 하는 유저가 이미 로그인되어 있는지 확인합니다.
        // 중복 로그인의 경우 오류 코드 -2를 반환합니다.
        for (int i = 0; i < numOfUser; i++) {
            if(user[i].id.equals(id)) {
                return -4;
            }
         }

        int  curUserNum = -1;    //새롭게 추가할 유저 정보
        //빈 자리를 찾으면 유저를 할당합니다.
        for (int i = 0; i < numOfUser; i++) {
            if(user[i].id.equals("")) {
                curUserNum = user[i].userNum;
                user[i].id = id;
                break;
            }
        }

        // 만약 세션이 가득 찬 경우(유저 접속 번호가 가득 찬 경우) -5을 반환합니다.
        if(curUserNum == -1) {
            return -5;
        }

        // WireGuard VPN에 Peer를 추가합니다. Peer 추가 실패 시 에러 코드 -13을 반환합니다.
        if(WireGuardManager.addPeer(curUserNum, clientPublicKey) == false)
            return -13;

        // 새로운 Btrfs 서브볼륨을 생성합니다. 생성 실패 시 에러 코드 -12를 반환합니다.
        if(BtrfsManger.addVolume(curUserNum) == false)
            return -12;

        // 새로운 iSCSI Target을 추가하고 생성한 서브볼륨과 연결합니다. 실패 시 에러 코드 -11을 반환합니다.
        if(ISCSIManager.addTarget(curUserNum) == false)
            return -11;

        return curUserNum;    // 유저 접속 번호 반환
    }

    /**
     * 로그아웃 기능
     * @param userId 로그아웃할 아이디
     * @param userNum 로그아웃할 유저의 유저 접속 번호
     * @return 에러 코드
     */
    public static int logout(String userId, int userNum) {
        // 유저 접속 번호가 현재 할당되어 있는지 확인합니다.
        boolean isUserExist = false;
        for(int i = 0; i < numOfUser; i++) {
            if(userNum == user[i].userNum && user[i].id.equals(userId)) {
                user[i].id = "";
                isUserExist = true;
                break;
            }
        }
        //유저 접속 번호가 할당되어있지 않을 경우(잘못된 유저 접속 번호가 입력되었을 경우) 에러 코드 -6을 반환합니다.
        if(!isUserExist) {
            return -6;
        }

        // iscsi Target을 제거합니다. 제거 실패 시 오류 코드 -11을 반환합니다.
        if(ISCSIManager.removeTarget(userNum) == false)
            return -11;

        // btrfs 서브볼륨을 제거합니다. 제거 실패 시 오류 코드 -12를 반환합니다.
        if(BtrfsManger.removeVolume(userNum) == false)
            return -12;

        // WireGuard VPN Peer를 제거합니다. 제거 실패 시 오류 코드 -11을 반환합니다.
        if(WireGuardManager.removePeer(userNum) == false)
            return -13;

        // 성공적으로 유저 제거 시 0을 반환합니다.
        return 0;
    }
}

