package weblab8.controller;

import weblab8.domain.User;
import weblab8.model.DBManager;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import java.io.IOException;

public class LoginController extends HttpServlet {

    public LoginController() {
        super();
    }

    protected void doPost(HttpServletRequest request,
                          HttpServletResponse response) throws ServletException, IOException {
        String username = request.getParameter("username");
        String password = request.getParameter("password");
        RequestDispatcher rd = null;

        HttpSession session = request.getSession();

        DBManager dbmanager = new DBManager();
        User user = dbmanager.authenticate(username, password);
        if (user != null) {
            rd = request.getRequestDispatcher("/game.jsp");
            session.setAttribute("user", user);
        } else {
            rd = request.getRequestDispatcher("/login.jsp");
            session.setAttribute("error", "Invalid credentials, please try again.");
            response.setStatus(HttpServletResponse.SC_UNAUTHORIZED);
        }

        rd.forward(request, response);
    }
}
