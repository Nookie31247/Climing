package climing;

import java.sql.*;

/// 데이터베이스와 연결하는 클래스
/// 비밀번호 입력시 반드시 원문이 아닌 암호화한 해시값을 입력할 것
public class DBManager {
    ErrorLogManager error = new ErrorLogManager();
    Connection connection = null;
    PreparedStatement preparedStatement = null;
    ResultSet resultSet = null;

    // 생성자에서 DB와 연결
    DBManager() {
        try {
            Class.forName("com.mysql.cj.jdbc.Driver");
//            connection = DriverManager.getConnection("jdbc:mysql://localhost:3306/climingDB",
//                    "climingService",
//                    "qwer1234");
            connection = DriverManager.getConnection("jdbc:mysql://192.168.219.130:3306/climingDB",
                    "testuser",
                    "12345678");
        } catch (SQLException | ClassNotFoundException e) {
            error.getError(e);
        }
    }

    /// ID와 비밀번호를 입력하면 해당 값이 올바르게 입력되었는지 확인합니다.
    /// 로그인 시 사용합니다.
    public String checkAccount(String id, String pw){
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
            error.getError(e);
            return "-7";
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
                error.getError(e);
            }
        }
    }

    /// ID, 비밀번호, 닉네임을 입력하여 새로운 계정을 추가합니다.
    /// 회원가입 기능에서 사용합니다.
    public int createAccount(String id, String pw, String username) {
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
            error.getError(e);
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
                error.getError(e);
            }
        }
    }

    /// ID와 비밀번호를 입력받아 현재 존재하는 계정을 삭제할 때 사용됩니다.
    /// 회원탈퇴 기능에서 사용합니다.
    public int deleteAccount(String id, String pw) {
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
            error.getError(e);
            return -7;
        } finally {
            try {
                if(preparedStatement != null) {
                    preparedStatement.close();
                    preparedStatement = null;
                }
            } catch (SQLException e) {
                error.getError(e);
            }
        }
    }
}
