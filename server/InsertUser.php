<?php
//Variables for the connection
	$servername = "localhost";
	$username =  "root";
	$password = "";
	$dbName = "leaderboardemo";
	
//Variable from the user	

	//Make Connection
	$conn = new mysqli($servername, $username, $password, $dbName);
	//Check Connection
	if(!$conn){
		die("Connection Failed. ". mysqli_connect_error());
	}
	if (isset($_POST["namepost"]) && isset($_POST["_passwordpost"])){
			$name = $_POST["namepost"];
	$_password =  $_POST["_passwordpost"];


		}
		else{
			$name = null;
			$_password = null;
		}
		$sql = "INSERT INTO user (name, Password)
			VALUES ('".$name."','".$_password."')";
	$result = mysqli_query($conn ,$sql);
	if(!result) echo "there was an error";
	else echo "Everything ok.";

?>