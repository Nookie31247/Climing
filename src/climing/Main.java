package climing;

public class Main {
    public static void main(String[] args) {
        CurrentUserManager.init();
        DBManager.dbInit();
        SocketServer socket = new SocketServer();
        socket.socketStart();
    }
}