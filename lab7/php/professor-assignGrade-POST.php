<?php

$con = mysqli_connect("localhost", "root", "", "web_lab7");
if (!$con) {
    die("Could not connect: " . mysqli_error());
}

$studentId = $_POST["studentId"];
$newGrade = $_POST["newGrade"];

$result_student = mysqli_query($con, "SELECT * FROM student WHERE id='" . $studentId . "'");

$row = mysqli_fetch_array($result_student);

if ($newGrade !== $row['grade']) {
    $result_assign_grade = mysqli_query($con, "UPDATE student SET grade='" . $newGrade . "' WHERE id='" . $studentId . "'");

    if ($result_assign_grade) {
        echo "Grade assigned successfully!";
    } else {
        header("HTTP/1.1 400 Bad Request");
        exit;
    }
} else {
    header("HTTP/1.1 304 Not Modified");
    exit;
}

mysqli_close($con);
?>
