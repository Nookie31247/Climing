package climing;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;

/*
    - 리눅스 명령어를 자동으로 실행하고 출력값을 반환해주는 클래스입니다.
    - 오류가 발생시 오류 코드와 오류 메시지를 출력합니다.
 */

public class RunUbuntuCommand {
    ErrorLogManager error = new ErrorLogManager();

    /// 우분투 명령어를 입력합니다. 콘솔의 출력 결과를 String으로 반환합니다.
    public String run(String command) throws Exception {
        System.out.println("명령어 입력: " + command);       // 디버그용 코드
        
        StringBuilder output = new StringBuilder();         // 출력값을 저장하는 변수
        ProcessBuilder processBuilder = new ProcessBuilder();
        processBuilder.command("bash", "-c", command);  // bash를 통해 명령어 실행
        Process process = processBuilder.start();

        // 명령어 실행 결과 출력
        BufferedReader reader = new BufferedReader(new InputStreamReader(process.getInputStream()));    // 리눅스 출력값을 받아옵니다.
        List<String> lineList = new ArrayList<>();  // reader에서 출력값들을 받아와서 저장합니다.
        String line;
        while ((line = reader.readLine()) != null) {
            lineList.add(line);
        }

        // lineList에서 맨 마지막 줄에 \n을 추가하지 않기 위해 사용합니다
        // lineList를 통해 출력값을 output으로 옮깁니다.
        for(int i = 0; i< lineList.size(); i++) {
            output.append(lineList.get(i));
            if(i != lineList.size() - 1) {
                output.append("\n");
            }
        }

        // 명령어 실행 중 오류가 발생했을 시 오류를 출력합니다.
        BufferedReader errorReader = new BufferedReader(new InputStreamReader(process.getErrorStream()));
        while ((line = errorReader.readLine()) != null) {
            error.getError(line);
        }

        // 명령어 종료 코드가 0이 아닐 시 오류 코드를 출력합니다.
        int exitCode = process.waitFor();
        if (exitCode != 0) {
            error.getError("명령어 실행 중 오류 발생. 종료 코드: " + exitCode);
        }

        // 디버그용 코드
        if(output.isEmpty())
        {
            System.out.println("명령어 출력: 없음");
        }
        else {
            System.out.println("명령어 출력: " + output);
        }
        return output.toString();
    }
}
