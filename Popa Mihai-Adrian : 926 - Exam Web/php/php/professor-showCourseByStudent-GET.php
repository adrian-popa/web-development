<?php

$con = mysqli_connect("localhost", "root", "", "web_exam");
if (!$con) {
    die("Could not connect: " . mysqli_error());
}

$studentName = $_GET["studentName"];

$result_courses = mysqli_query($con, "SELECT * FROM Courses WHERE participants LIKE '%" . $studentName . "%'");

$currentIndex = 0;

echo "<div id='page-1' class='page-content' style='display: block;'>";

while ($row_course = mysqli_fetch_array($result_courses)) {
    if ($currentIndex != 0 && $currentIndex % 4 == 0) {
        echo "</div>";
        echo "<div id='page-" . ($currentIndex / 4 + 1) . "' class='page-content'>";
    }

    echo "<div id='course-" . $row_course['id'] . "' class='card'>";
    echo "<div class='container'>";
    echo "<h3><b>ID: " . $row_course['id'] . " - " . $row_course['coursename'] . "</b></h3>";
    echo "<p>Participants: " . $row_course['participants'] . "</p>";
    echo "<p>Grades: " . $row_course['grades'] . "</p>";
    echo "<label for='grade'>New grades: </label>";
    echo "<input type='text' id='grades'>";
    echo "</div>";
    echo "</div>";

    $currentIndex += 1;
}

echo "</div>";

mysqli_close($con);
?>
