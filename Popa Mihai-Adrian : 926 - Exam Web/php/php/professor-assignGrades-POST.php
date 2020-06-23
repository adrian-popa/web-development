<?php

$con = mysqli_connect("localhost", "root", "", "web_lab7");
if (!$con) {
    die("Could not connect: " . mysqli_error());
}

$studentName = $_POST["studentName"];
$newGrades = $_POST["newGrades"];

$result_student = mysqli_query($con, "SELECT * FROM Courses WHERE participants LIKE '%" . $studentName . "%'");

$row = mysqli_fetch_array($result_student);

if ($newGrades !== $row['grades']) {
    $result_assign_grade = mysqli_query($con, "UPDATE Courses SET grades='" . $newGrades . "' WHERE participants LIKE '%" . $studentName . "%'");

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
