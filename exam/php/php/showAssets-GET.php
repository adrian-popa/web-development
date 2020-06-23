<?php

$con = mysqli_connect("localhost", "root", "", "web_exam");
if (!$con) {
    die("Could not connect: " . mysqli_error());
}

$userId = $_GET["userId"];

$result_assets = mysqli_query($con, "SELECT * FROM assets WHERE userid='" . $userId . "'");

$currentIndex = 0;

echo "<div id='page-1' class='page-content' style='display: block;'>";

while ($row_assets = mysqli_fetch_array($result_assets)) {
    if ($currentIndex != 0 && $currentIndex % 4 == 0) {
        echo "</div>";
        echo "<div id='page-" . ($currentIndex / 4 + 1) . "' class='page-content'>";
    }

    echo "<div id='asset-" . $row_assets['id'] . "' class='card " . $row_assets['value'] > 10 ? 'red' : '' . "'>";
    echo "<div class='container'>";
    echo "<h3><b>ID: " . $row_assets['id'] . " - " . $row_assets['name'] . "</b></h3>";
    echo "<p>" . $row_assets['description'] . "</p>";
    echo "<label for='grade'>Grade: </label>";
    echo "<input type='number' id='grade' value='" . $row_assets['grade'] . "'>";
    echo "</div>";
    echo "</div>";

    $currentIndex += 1;
}

echo "</div>";

mysqli_close($con);
?>
