package weblab8.controller;

import weblab8.domain.User;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import java.io.IOException;

public class IntroController extends HttpServlet {

    public IntroController() {
        super();
    }

    public void doGet(HttpServletRequest request,
                        HttpServletResponse response) throws ServletException, IOException {
        HttpSession session = request.getSession();
        User user = (User) session.getAttribute("user");

        RequestDispatcher rd;

        if (user != null) {
            rd = request.getRequestDispatcher("/game.jsp");
        } else {
            rd = request.getRequestDispatcher("/login.jsp");
        }

        session.setAttribute("error", null);

        rd.forward(request, response);
    }
}
