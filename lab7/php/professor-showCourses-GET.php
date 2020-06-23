<?php

$con = mysqli_connect("localhost", "root", "", "web_exam");
if (!$con) {
    die("Could not connect: " . mysqli_error());
}

$professorId = $_GET["professorId"];

$result_course = mysqli_query($con, "SELECT * FROM Courses WHERE professorid='" . $professorId . "'");

$courseData = array();

while ($row_course = mysqli_fetch_array($result_course)) {
    array_push($courseData, $row_course["coursename"]);
}

echo json_encode($courseData);

mysqli_close($con);
?>
