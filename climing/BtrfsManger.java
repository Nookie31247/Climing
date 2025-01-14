package climing;

/// Btrfs 서브볼륨 관리 모듈
public class BtrfsManger {
    RunUbuntuCommand command = new RunUbuntuCommand();

    /// btrfs 서브볼륨을 추가하기 위한 메소드입니다.
    /// 유저 접속 번호를 입력받습니다.
    /// 볼륨 생성 성공 시 true, 실패 시 false를 반환합니다.
    public boolean addVolume(int userNum) {
        try {
            // 새로운 Btrfs 서브볼륨을 생성합니다.
            // 생성 경로는 /gamedisk/ 입니다.
            command.run("btrfs subvolume snapshot /gamedisk/original /gamedisk/" + userNum);
            return true;
        } catch (Exception e) {
            e.printStackTrace();
            return false;
        }
    }

    /// btrfs 서브볼륨을 제거하기 위한 메소드입니다.
    /// 유저 접속 번호를 입력받습니다.
    /// 볼륨 제거 성공 시 true, 실패 시 false를 반환합니다.
    public boolean removeVolume(int userNum) {
        try {
            // 생성한 서브볼륨을 제거합니다.
            command.run("btrfs subvolume delete /gamedisk/" + userNum);
            return true;
        } catch (Exception e) {
            e.printStackTrace();
            return false;
        }
    }
}
