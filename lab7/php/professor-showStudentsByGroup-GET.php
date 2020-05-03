<?php

$con = mysqli_connect("localhost", "root", "", "web_lab7");
if (!$con) {
    die("Could not connect: " . mysqli_error());
}

$groupId = $_GET["groupId"];

$result_student = mysqli_query($con, "SELECT * FROM student WHERE group_id='" . $groupId . "'");

$currentIndex = 0;

echo "<div id='page-1' class='page-content' style='display: block;'>";

while ($row_student = mysqli_fetch_array($result_student)) {
    if ($currentIndex != 0 && $currentIndex % 4 == 0) {
        echo "</div>";
        echo "<div id='page-" . ($currentIndex / 4 + 1) . "' class='page-content'>";
    }

    echo "<div id='student-" . $row_student['id'] . "' class='card'>";
    echo "<img class='avatar' src='assets/img_avatar.png' alt='Avatar'>";
    echo "<div class='container'>";
    echo "<h3><b>ID: " . $row_student['id'] . " - " . $row_student['username'] . "</b></h3>";
    echo "<p>Group: " . $row_student['group_id'] . "</p>";
    echo "<label for='grade'>Grade: </label>";
    echo "<input type='number' id='grade' value='" . $row_student['grade'] . "'>";
    echo "</div>";
    echo "</div>";

    $currentIndex += 1;
}

echo "</div>";

mysqli_close($con);
?>
