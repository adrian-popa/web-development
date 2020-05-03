<?php

$con = mysqli_connect("localhost", "root", "", "web_lab7");
if (!$con) {
    die("Could not connect: " . mysqli_error());
}

$username = $_POST["username"];
$password = $_POST["password"];

$result_professor = mysqli_query($con, "SELECT * FROM professor WHERE username='" . $username . "' AND password='" . $password . "'");

if (mysqli_fetch_array($result_professor)) {
    $jsonData = array(
        "username" => $username,
        "userType" => "professor");
    echo json_encode($jsonData);
} else {
    $result_student = mysqli_query($con, "SELECT * FROM student WHERE username='" . $username . "' AND password='" . $password . "'");

    if (mysqli_fetch_array($result_student)) {
        $jsonData = array(
            "username" => $username,
            "userType" => "student");
        echo json_encode($jsonData);
    } else {
        header("HTTP/1.1 401 Unauthorized");
        exit;
    }
}

mysqli_close($con);
?>
