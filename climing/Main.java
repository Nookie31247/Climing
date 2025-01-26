package climing;

import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        DBManager db = new DBManager();

        int selectNum;
        String id, pw, name;

        String resultStr;
        int resultInt;

        while(true) {
            System.out.println("1. 로그인");
            System.out.println("2. 회원가입");
            System.out.println("3. 회원탈퇴");
            System.out.println("0. 프로그램 종료");
            System.out.print("-> ");
            selectNum = sc.nextInt();
            System.out.println();

            switch (selectNum) {
                case 0:
                    return;

                case 1:
                    System.out.print("아이디 -> ");
                    id = sc.next();
                    System.out.print("비밀번호 ->");
                    pw = sc.next();

                    resultStr = db.checkAccount(id, pw);
                    if(resultStr.equals("-1"))
                        System.out.println("ID나 비밀번호가 잘못되었습니다.");
                    else
                        System.out.println("로그인 성공. 환영합니다 " + resultStr);
                    break;

                case 2:
                    System.out.print("아이디 -> ");
                    id = sc.next();
                    System.out.print("비밀번호 ->");
                    pw = sc.next();
                    System.out.print("닉네임 -> ");
                    name = sc.next();

                    resultInt = db.createAccount(id, pw, name);
                    if(resultInt == -2)
                        System.out.println("이미 사용중인 id입니다");
                    else if(resultInt == -3)
                        System.out.println("이미 사용중인 닉네임입니다");
                    else
                        System.out.println("회원가입 성공");
                    break;

                case 3:
                    System.out.print("아이디 -> ");
                    id = sc.next();
                    System.out.print("비밀번호 ->");
                    pw = sc.next();

                    resultInt = db.deleteAccount(id, pw);
                    if(resultInt == -1)
                        System.out.println("잘못된 아이디 혹은 비밀번호");
                    else
                        System.out.println("회원탈퇴 성공");
                    break;

                default:
                    System.out.println("잘못된 번호입니다.");
            }
        }
    }
}