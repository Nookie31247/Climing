package climing;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;

/// 클라이언트에서 받은 JSON 파일을 해석하여 서버에서 필요한 기능을 수행한 후 JSON 응답 파일을 만드는 클래스
public class RequestHandler {
    /// 로그인 JSON
    public static JSONObject handleLogin(JSONObject request) {
        JSONObject response = new JSONObject();
        response.put("type", "LoginResponse");

        try {
            String userId = (String) request.get("user_id");
            String hashedPassword = (String) request.get("hashed_user_password");
            String clientPublicKey = (String) request.get("client_public_key");

            String idCheck = DBManager.checkAccount(userId, hashedPassword);

            if (idCheck.equals("-1") || idCheck.equals("-7")) {
                response.put("error_code", Integer.parseInt(idCheck));
                response.put("message", "Wrong ID or Password");
            }
            else {
                int userConNumber = CurrentUserManager.login(userId, hashedPassword, clientPublicKey);
                response.put("user_connect_number", userConNumber);
                response.put("vpn_server_public_key", WireGuardManager.getServerPublicKey());
                response.put("vpn_server_ipv4", WireGuardManager.getServerVPNIp());
                response.put("vpn_endpoint_ipv4", "122.42.50.148:44444");
                response.put("user_nickname", idCheck);
                if (userConNumber < 0)
                    response.put("error_code", userConNumber);
                else
                    response.put("error_code", 0);
            }
        } catch (Exception e) {
            response.put("error_code", -50);
            response.put("message", "Server error occurred.");
            e.printStackTrace();
        }

        return response;
    }


    /// 로그아웃 JSON
    public static JSONObject handleLogout(JSONObject request) {
        JSONObject response = new JSONObject();
        response.put("type", "LogoutResponse");

        String userId = (String) request.get("user_id");
        int userNum = ((Number) request.get("user_connect_number")).intValue();

        response.put("error_code", CurrentUserManager.logout(userId, userNum));
        return response;
    }

    /// 회원가입 JSON
    public static JSONObject handleRegister(JSONObject request) {
        JSONObject response = new JSONObject();
        response.put("type", "addUserResponse");

        String userId = (String) request.get("user_id");
        String hashedPassword = (String) request.get("hashed_user_password");
        String userNickname = (String) request.get("user_nickname");

        response.put("error_code", DBManager.createAccount(userId, hashedPassword, userNickname));
        return response;
    }

    /// 계정삭제 JSON
    public static JSONObject handleDeleteUser(JSONObject request) {
        JSONObject response = new JSONObject();
        response.put("type", "deleteUserResponse");

        String userId = (String) request.get("user_id");
        String hashedPassword = (String) request.get("hashed_user_password");

        response.put("error_code", DBManager.deleteAccount(userId, hashedPassword));
        return response;
    }

    /// 게임 리스트 JSON
    public static JSONObject handleGameList() {
        JSONObject response = new JSONObject();
        response.put("type", "GameListResponse");
        DBManager.Game[] games = DBManager.getGameList();

        if (games == null) {
            return response;
        }


        JSONArray JSONGameArray = new JSONArray();
        for (DBManager.Game game : games) {
            JSONObject gameObject = new JSONObject();

            gameObject.put("name", game.name);
            gameObject.put("imageUrl", game.imageUrl);
            gameObject.put("company", game.company);
            gameObject.put("genre", game.genre);
            gameObject.put("dirPath", game.dirPath);
            gameObject.put("identifyNum", game.identifyNum);
            JSONGameArray.add(gameObject);
        }

        response.put("gameList", JSONGameArray);
        return response;
    }
}
