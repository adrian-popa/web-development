<?php

$con = mysqli_connect("localhost", "root", "", "web_lab7");
if (!$con) {
    die("Could not connect: " . mysqli_error());
}

$username = $_GET["username"];

$result_professor = mysqli_query($con, "SELECT * FROM professor WHERE username='" . $username . "'");

if ($row_professor = mysqli_fetch_array($result_professor)) {
    $professorId = $row_professor["id"];
    $result_course = mysqli_query($con, "SELECT * FROM course WHERE professor_id='" . $professorId . "'");

    $courseData = array();

    while ($row_course = mysqli_fetch_array($result_course)) {
        array_push($courseData, $row_course["group_id"]);
    }

    echo json_encode($courseData);
} else {
    header("HTTP/1.1 400 Bad Request");
    exit;
}

mysqli_close($con);
?>
