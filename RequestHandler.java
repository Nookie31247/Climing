import org.json.simple.JSONObject;

public class RequestHandler {

    // 로그인 JSON
    public static JSONObject handleLogin(JSONObject request) {
        JSONObject response = new JSONObject();
        response.put("type", "LoginResponse");

        try {
            String userId = (String) request.get("user_id");
            String hashedPassword = (String) request.get("hashed_user_password");

            // 더미 로그인 검증 (테스트용)
            if ("testUser".equals(userId) && "hashedPassword123".equals(hashedPassword)) {
                response.put("user_connect_number", 1001);
                response.put("vpn_server_public_key", "server_public_key");
                response.put("vpn_server_ipv4", "192.168.135.1");
                response.put("vpn_ipv4", "vpn://192.168.135.1");
                response.put("user_nickname", "player_one");
                response.put("error_code", 0);
            } else {
                response.put("error_code", 1);
                response.put("message", "Invalid username or password.");
            }
        } catch (Exception e) {
            response.put("error_code", 99);
            response.put("message", "Server error occurred.");
            e.printStackTrace();
        }

        return response;
    }


    // 로그아웃 JSON
    public static JSONObject handleLogout(JSONObject request) {
        JSONObject response = new JSONObject();
        response.put("type", "LogoutResponse");
        response.put("error_code", 0);
        return response;
    }

    // 로그인 JSON
    public static JSONObject handleRegister(JSONObject request) {
        JSONObject response = new JSONObject();
        response.put("type", "addUserResponse");
        response.put("error_code", 0);
        return response;
    }

    // 계정삭제 JSON
    public static JSONObject handleDeleteUser(JSONObject request) {
        JSONObject response = new JSONObject();
        response.put("type", "deleteUserResponse");
        response.put("error_code", 0);
        return response;
    }
}
