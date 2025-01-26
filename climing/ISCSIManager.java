package climing;

public class ISCSIManager {
    RunUbuntuCommand command = new RunUbuntuCommand();
    ErrorLogManager error = new ErrorLogManager();

    /// iSCSI에 새로운 타겟을 추가하고, 추가한 타겟에 생성한 서브볼륨을 연결합니다.
    /// 유저 접속 번호를 입력받습니다.
    /// 타겟의 번호와 서브볼륨의 이름이 동일하지 않으면 에러가 발생할 수 있습니다.
    public boolean addTarget (int userNum) {
        try {
            //새로운 타겟을 생성합니다.
            command.run("tgtadm --lld iscsi --op new --mode target --tid " + userNum + " -T iqn.2024-11.com.climing:" + userNum);

            // 생성한 타겟에 lun을 추가하여 Btrfs 서브볼륨과 연결합니다.
            command.run("tgtadm --lld iscsi --op new --mode logicalunit --tid " + userNum + " --lun 1 -b /gamedisk/" + userNum + "/disk.img");

            // 생성한 타겟에 허용할 IP 주소를 바인딩합니다.
            command.run("tgtadm --lld iscsi --op bind --mode target --tid " + userNum + " -I 192.168.135." + userNum);

            return true;
        } catch (Exception e) {
            error.getError(e);
            return false;
        }

    }

    /// iSCSI 타겟을 제거합니다.
    /// 유저 접속 번호을 입력받습니다.
    public boolean removeTarget (int userNum) {
        try {
            // 생성한 타겟을 제거합니다.
            command.run("tgtadm --lld iscsi --op delete --mode target --tid " + userNum + " --force");
            return true;

        } catch (Exception e) {
            error.getError(e);
            return false;
        }
    }
}
