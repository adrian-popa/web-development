<?php

$con = mysqli_connect("localhost", "root", "", "web_lab7");
if (!$con) {
    die("Could not connect: " . mysqli_error());
}

$username = $_POST["username"];
$password = $_POST["password"];

$result_users = mysqli_query($con, "SELECT * FROM users WHERE username='" . $username . "' AND password='" . $password . "'");

$user = mysqli_fetch_array($result_users);

if ($user) {
    $jsonData = array(
        "id" => $user['id'],
        "username" => $user['username']
    );
    echo json_encode($jsonData);
} else {
    header("HTTP/1.1 401 Unauthorized");
    exit;
}

mysqli_close($con);
?>
