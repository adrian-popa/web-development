package weblab8.model;

import weblab8.domain.User;

import java.sql.*;

public class DBManager {
    private Statement stmt;

    public DBManager() {
        connect();
    }

    public void connect() {
        try {
            Class.forName("org.gjt.mm.mysql.Driver");
            Connection con = DriverManager.getConnection("jdbc:mysql://192.168.64.2:3306/web_lab8", "root", "");
            stmt = con.createStatement();
        } catch (Exception ex) {
            System.out.println("connection error:" + ex.getMessage());
            ex.printStackTrace();
        }
    }

    public User authenticate(String username, String password) {
        ResultSet rs;
        User u = null;
        System.out.println(username + " " + password);
        try {
            rs = stmt.executeQuery("select * from user where username='" + username + "' and password='" + password + "'");
            if (rs.next()) {
                u = new User(rs.getInt("id"), rs.getString("username"), rs.getString("password"));
            }
            rs.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return u;
    }
}
