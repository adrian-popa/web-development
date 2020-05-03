<?php

$con = mysqli_connect("localhost", "root", "", "web_lab7");
if (!$con) {
    die("Could not connect: " . mysqli_error());
}

$username = $_GET["username"];

$result_student = mysqli_query($con, "SELECT * FROM student WHERE username='" . $username . "'");

if ($row_student = mysqli_fetch_array($result_student)) {
    echo $row_student['grade'];
} else {
    header("HTTP/1.1 400 Bad Request");
    exit;
}

mysqli_close($con);
?>
