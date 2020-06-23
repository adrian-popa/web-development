<%@ page import="weblab8.domain.User" %>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>Lab8 - Login</title>

    <link rel="stylesheet" href="styles.css">

    <script src="js/jquery-3.5.1.js"></script>
</head>
<body>

<%! User user; %>
<% user = (User) session.getAttribute("user");
    if (user == null) {
%>
<div class="login-container">
    <h3 style="margin-bottom: 20px;">Welcome, please login to proceed!</h3>
    <form id="login-form" action="login" method="post">
        <label for="username">Username</label>
        <input type="text" id="username" name="username" required>
        <label for="password">Password</label>
        <input type="password" id="password" name="password" required>
        <% String error = (String) session.getAttribute("error");
            if (error != null && !error.isEmpty()) {
        %>
        <p class="error"><%= error %></p>
        <%
            }
        %>
        <input type="submit" value="Login"/>
    </form>
</div>

<script>
    $(document).ready(function () {
        $('.login-container').click(function () {
            $('p.error').remove();
        });
    });
</script>
<%
    }
%>

</body>
</html>
