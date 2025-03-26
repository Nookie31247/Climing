package climing;

import javax.xml.transform.Result;
import java.sql.*;
import java.util.ArrayList;
import java.util.List;

/// 데이터베이스와 연결하여 데이터를 주고받는 클래스
public class DBManager {
    static Connection connection = null;

    /// 게임 데이터를 저장하기 위한 클래스
    public static class Game {
        public String name;         // 게임 이름
        public String imageUrl;     // 게임 이미지 URL
        public String company;      // 게임 제작사
        public String genre;        // 게임 장르
        public String dirPath;      // 게임 실행 경로
        public int identifyNum;     // 게임 식별 번호
    }

    // 생성자에서 DB와 연결
    DBManager() {
        try {
            Class.forName("com.mysql.cj.jdbc.Driver");
            connection = DriverManager.getConnection("jdbc:mysql://localhost:3306/climingDB",
                    "climingService",
                    "qwer1234");
        } catch (SQLException | ClassNotFoundException e) {
            ErrorLogManager.getError(e);
        }
    }

    /**
     * ID와 비밀번호를 입력하면 해당 값이 올바르게 입력되었는지 확인합니다.
     * 로그인 시 사용합니다.
     * @param id 로그인할 아이디
     * @param pw 로그인할 비밀번호
     * @return 로그인 성공 시 유저 닉네임을 반환하고, 로그인 실패 시 에러 코드를 반환
     */
    public static String checkAccount(String id, String pw){
        PreparedStatement preparedStatement = null;
        ResultSet resultSet = null;

        try {
            // SQL 쿼리문을 입력합니다.
            preparedStatement = connection.prepareStatement("SELECT username FROM user " +
                    "WHERE id = ? AND password = ?");
            preparedStatement.setString(1, id);
            preparedStatement.setString(2, pw);
            resultSet = preparedStatement.executeQuery();

            if(resultSet.next()) {
                // 쿼리문의 결과가 있을 경우(ID, 비밀번호가 올바르게 입력되었을 경우) 해당 유저의 닉네임을 반환합니다.
                return resultSet.getString("username");
            }
            else {
                // 쿼리문의 결과가 없을 경우(ID, 비밀번호가 틀렸을 경우) "-1"을 반환합니다.
                return "-1";
            }
        } catch (SQLException e) {
            // 기타 SQL 실행 과정에서 에러가 발생했을 경우 "-7"을 반환합니다.
            ErrorLogManager.getError(e);
            return "-7";
        } finally {
            // 명령어 실행이 끝난 후 PreparedStatement와 ResultSet를 초기화합니다.
            try {
                if(resultSet != null) {
                    resultSet.close();
                }

                if(preparedStatement != null) {
                    preparedStatement.close();
                }
            } catch (SQLException e) {
                ErrorLogManager.getError(e);
            }
        }
    }

    /**
     * 새로운 계정을 추가합니다. 회원가입 시 사용됩니다.
     * @param id 추가할 아이디
     * @param pw 추가할 비밀번호
     * @param username 추가할 닉네임
     * @return 에러 코드
     */
    public static int createAccount(String id, String pw, String username) {
        PreparedStatement preparedStatement = null;
        ResultSet resultSet = null;

        try {
            // 매개변수로 입력받은 ID와 동일한 ID가 이미 있는지 확인합니다.
            preparedStatement = connection.prepareStatement("SELECT COUNT(*) FROM user WHERE id = ?");
            preparedStatement.setString(1, id);
            resultSet = preparedStatement.executeQuery();

            if (resultSet.next() && resultSet.getInt(1) != 0) {
                // 이미 존재하는 ID의 경우 -2를 반환합니다.
                return -2;
            }
            preparedStatement.close();
            resultSet.close();

            // 매개변수로 입력받은 닉네임과 동일한 닉네임이 있는지 확인합니다.
            preparedStatement = connection.prepareStatement("SELECT COUNT(*) FROM user WHERE username = ?");
            preparedStatement.setString(1, username);
            resultSet = preparedStatement.executeQuery();

            if (resultSet.next() && resultSet.getInt(1) != 0) {
                // 이미 존재하는 닉네임의 경우 -3을 반환합니다.
                return -3;
            }
            preparedStatement.close();
            resultSet.close();

            // ID와 닉네임이 중복되지 않았을 경우 새로운 유저 정보를 데이터베이스에 추가합니다.
            preparedStatement = connection.prepareStatement("INSERT INTO user (id, password, username) " +
                    "VALUES (?, ?, ?)");
            preparedStatement.setString(1, id);
            preparedStatement.setString(2, pw);
            preparedStatement.setString(3, username);
            preparedStatement.executeUpdate();

            //모든 작업이 성공적으로 완료되었을 시 0을 반환합니다.
            return 0;
        } catch (SQLException e) {
            // 기타 SQL 관련 에러 발생 시 -7을 반환합니다.
            ErrorLogManager.getError(e);
            return -7;
        } finally {
            try {
                if(resultSet != null) {
                    resultSet.close();
                    resultSet = null;
                }

                if(preparedStatement != null) {
                    preparedStatement.close();
                    preparedStatement = null;
                }
            } catch (SQLException e) {
                ErrorLogManager.getError(e);
            }
        }
    }

    /**
     * 서버에 저장된 계정을 삭제합니다. 회원탈퇴 시 사용됩니다.
     * @param id 삭제할 아이디
     * @param pw 삭제할 비밀번호
     * @return 에러 코드
     */
    public static int deleteAccount(String id, String pw) {
        PreparedStatement preparedStatement = null;
        ResultSet resultSet = null;

        try {
            // 현재 유저를 삭제하는 SQL 쿼리문을 입력합니다.
            preparedStatement = connection.prepareStatement("DELETE FROM user " +
                    "WHERE id = ? AND password = ?");
            preparedStatement.setString(1, id);
            preparedStatement.setString(2, pw);
            int result = preparedStatement.executeUpdate();

            if (result == 1)
                // 유저가 정상적으로 삭제되었을 경우 0을 반환합니다.
                return 0;
            else
                // 입력한 ID와 비밀번호가 잘못되었을 경우 -1을 반환합니다.
                return -1;
        } catch (SQLException e) {
            // 기타 SQL 에러가 발생했을 경우 -7을 반환합니다.
            ErrorLogManager.getError(e);
            return -7;
        } finally {
            try {
                if(preparedStatement != null) {
                    preparedStatement.close();
                    preparedStatement = null;
                }
            } catch (SQLException e) {
                ErrorLogManager.getError(e);
            }
        }
    }

    /**
     * 서버에 저장된 게임 리스트를 불러옵니다.
     * @return 게임 리스트
     */
    public static Game[] getGameList() {
        PreparedStatement preparedStatement = null;
        ResultSet resultSet = null;

        List<Game> gameList = new ArrayList<>();
        try {
            preparedStatement = connection.prepareStatement("select * from game");
            resultSet = preparedStatement.executeQuery();

            while(resultSet.next()) {
                Game game = new Game();

                game.name = resultSet.getString("name");
                game.imageUrl = resultSet.getString("imageUrl");
                game.company = resultSet.getString("company");
                game.genre = resultSet.getString("genre");
                game.dirPath = resultSet.getString("dirPath");
                game.identifyNum = resultSet.getInt("identifyNum");

                gameList.add(game);
            }
        } catch (SQLException e) {
            ErrorLogManager.getError(e);
            return null;
        } finally {
            try {
                if(preparedStatement != null) {
                    preparedStatement.close();
                    preparedStatement = null;
                }
            } catch (SQLException e) {
                ErrorLogManager.getError(e);
            }
        }
        return gameList.toArray(new Game[0]);
    }
}
