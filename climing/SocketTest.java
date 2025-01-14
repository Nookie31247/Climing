package climing;

// 소켓 통신을 통한 명령어 실행 임시용

import java.util.Scanner;

public class SocketTest {
    CurrentUserManager currentUserManager = new CurrentUserManager();
    Scanner scanner = new Scanner(System.in);

    public void test() {
        int inputNum;
        String id;
        int userNum;
        while (true) {
            System.out.println("테스트하고자 하는 기능을 입력하세요");
            System.out.println("1. 로그인");
            System.out.println("2. 로그아웃");
            System.out.println("3. 회원가입");
            System.out.println("4. 게임 리스트");
            System.out.println("5. 종료");
            System.out.print("-> ");

            inputNum = scanner.nextInt();
            switch (inputNum) {
                case 1:
                    System.out.print("아이디를 입력하세요 -> ");
                    id = scanner.next();
                    login(id, "1234");
                    break;
                case 2:
                    System.out.print("유저 접속 번호를 입력하세요 -> ");
                    userNum = scanner.nextInt();
                    logout(userNum);
                    break;
                case 3:
                case 4:
                    System.out.println("아직 개발중인 기능입니다.");
                    break;
                case 5:
                    System.out.println("종료합니다.");
                    return;
                default:
                    System.out.println("잘못된 번호입니다.\n");
            }
        }
    }

    /// 유저 접속 번호 반환
    private int login(String id, String pw) {
        int userNum;
        String privateKey;
        // 데이터베이스 모듈을 통해 ID와 비밀번호 조회하기

        userNum = currentUserManager.addUser(id);
        if(userNum == 2 || userNum == 3) {
            return userNum;
        }
        privateKey = currentUserManager.getPrivateKey(userNum);
        if(privateKey.equals("-4")) {
            return -4;
        }
        System.out.println("유저 접속 번호: " + userNum);
        System.out.println("클라이언트 비밀 키: " + privateKey);
        return 0;
    }

    private boolean logout(int userNum) {
        return currentUserManager.removeUser(userNum);
    }
}
