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
	if (isset($_POST["id_user_post"]) && isset($_POST["_highscore"])){
			$id_user = $_POST["id_user_post"];
	$_highscore =  $_POST["_highscore"];
    $_timecreate =  $_POST["_timecreate"];

		}
		else{
			$id_user = null;
			$_highscore = null;
			$timecreate = null;
		}
		$sql = "INSERT INTO highscore (id_user, highscore, timecreate)
			VALUES ('".$id_user."','".$_highscore."','".$_timecreate."')";
	$result = mysqli_query($conn ,$sql);
	if(!result) echo "there was an error";
	else echo "insert highscore successful.";

?>