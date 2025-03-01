package climing;

/*
    - 로그인한 모든 유저는 각각의 유저 접속 번호를 부여받습니다.
    - 유저 접속 번호는 11부터 시작합니다.
    - WireGuard VPN의 D클래스 IP 주소, iSCSI Target 번호, Btrfs 서브볼륨의 이름과
      유저 접속 번호를 모두 동일하게 설정하여 알아보기 쉽게 하도록 합니다.
 */

/// 접속 유저 관리 모듈
public class CurrentUserManager {
    WireGuardManager wireGuard = new WireGuardManager();
    BtrfsManger btrfs = new BtrfsManger();
    ISCSIManager iscsi = new ISCSIManager();

    private final int startUserNum = 11;    // 유저 접속 번호의 시작 번호를 지정합니다.
    private final int numOfUser = 10;       // 최대 동시 접속 가능한 유저의 수를 지정합니다.

    // 이너 클래스를 사용해 현재 접속 유저를 저장합니다.
    private static class CurUserData {
        public int userNum = 0;             // 유저 접속 번호
        public String id = null;           // 유저 id
    }

    // CurUserDatd의 객체를 최대 동시 접속 가능한 유저의 수만큼 생성합니다.
    CurUserData[] user = new CurUserData[numOfUser];

    // 생성자
    CurrentUserManager () {
        // CurrentUserManager 클래스 생성 시 유저 번호를 시작 번호부터
        // 최대 동시 접속 가능한 유저 수만큼 미리 등록해놓습니다.
        for (int i = 0; i < numOfUser; i++) {
            user[i] = new CurUserData();
            user[i].userNum = i + startUserNum;
        }
    }

    /// 유저 추가 기능
    /// 사용자의 ID (String)을 입력합니다.
    /// 유저 접속 번호를 반환합니다.
    public int login(String id, String pw, String clientPublicKey) {

        // 데이터베이스 모듈을 통해 ID와 비밀번호가 올바르게 입력되었는지 확인합니다.

        // 로그인하고자 하는 유저가 이미 로그인되어 있는지 확인합니다.
        // 중복 로그인의 경우 오류 코드 -2를 반환합니다.
        for (int i = 0; i < numOfUser; i++) {
            if(user[i].id != null && user[i].id.equals(id)) {
                return -4;
            }
         }

        CurUserData curUser = null;    //새롭게 추가할 유저 정보
        //빈 자리를 찾으면 유저를 할당합니다.
        for (int i = 0; i < numOfUser; i++) {
            if(user[i].id == null) {
                curUser = user[i];
                curUser.id = id;
                break;
            }
        }
        // 만약 세션이 가득 찬 경우(유저 접속 번호가 가득 찬 경우) -5을 반환합니다.
        if(curUser == null) {
            return -5;
        }

        // WireGuard VPN에 Peer를 추가합니다. Peer 추가 실패 시 에러 코드 -13을 반환합니다.
        if(wireGuard.addPeer(curUser.userNum, clientPublicKey) == false)
            return -13;

        // 새로운 Btrfs 서브볼륨을 생성합니다. 생성 실패 시 에러 코드 -12를 반환합니다.
        if(btrfs.addVolume(curUser.userNum) == false)
            return -12;

        // 새로운 iSCSI Target을 추가하고 생성한 서브볼륨과 연결합니다. 실패 시 에러 코드 -11을 반환합니다.
        if(iscsi.addTarget(curUser.userNum) == false)
            return -11;

        return curUser.userNum;    // 유저 접속 번호 반환
    }

    /// 유저 제거 기능 (로그아웃)
    /// 유저 접속 번호를 입력받습니다.
    /// 정상적으로 유저가 제거되었을 시 0을 반환하고,
    /// 잘못된 유저 접속 번호가 입력되었을 시 -6 반환합니다.
    public int logout(int userNum) {
        // 유저 접속 번호가 현재 할당되어 있는지 확인합니다.
        boolean isUserExist = false;
        for(int i = 0; i < numOfUser; i++) {
            if(userNum == user[i].userNum && user[i].id != null) {
                user[i].id = null;
                isUserExist = true;
                break;
            }
        }
        //유저 접속 번호가 할당되어있지 않을 경우(잘못된 유저 접속 번호가 입력되었을 경우) 에러 코드 -6을 반환합니다.
        if(!isUserExist) {
            return -6;
        }

        // iscsi Target을 제거합니다. 제거 실패 시 오류 코드 -11을 반환합니다.
        if(iscsi.removeTarget(userNum) == false)
            return -11;

        // btrfs 서브볼륨을 제거합니다. 제거 실패 시 오류 코드 -12를 반환합니다.
        if(btrfs.removeVolume(userNum) == false)
            return -12;

        // WireGuard VPN Peer를 제거합니다. 제거 실패 시 오류 코드 -11을 반환합니다.
        if(wireGuard.removePeer(userNum) == false)
            return -13;

        // 성공적으로 유저 제거 시 0을 반환합니다.
        return 0;
    }
}

