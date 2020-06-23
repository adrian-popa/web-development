<?php

$con = mysqli_connect("localhost", "root", "", "web_exam");
if (!$con) {
    die("Could not connect: " . mysqli_error());
}

$result_person = mysqli_query($con, "SELECT * FROM Persons WHERE role='student'");

$personData = array();

while ($row_person = mysqli_fetch_array($result_person)) {
    array_push($personData, $row_person["name"]);
}

echo json_encode($personData);

mysqli_close($con);
?>
