<?php

$con = mysqli_connect("localhost", "root", "", "web_exam");
if (!$con) {
    die("Could not connect: " . mysqli_error());
}

$name = $_POST["name"];

$result_persons = mysqli_query($con, "SELECT * FROM Persons WHERE name='" . $name . "'");

if ($person = mysqli_fetch_array($result_persons)) {
    $jsonData = array(
        "id" => $person["id"],
        "name" => $person["name"],
        "role" => $person["role"]);
    echo json_encode($jsonData);
} else {
    header("HTTP/1.1 401 Unauthorized");
    exit;
}

mysqli_close($con);
?>
