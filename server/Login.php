<?php
	$servername = "localhost";
	$username =  "root";
	$password = "";
	$dbName = "leaderboardemo";
	
	//Make Connection
	$conn = new mysqli($servername, $username, $password, $dbName);
	//Check Connection
	if(!$conn){
		die("Connection Failed. ". mysqli_connect_error());
	}

		if (isset($_POST["namepost"]) && isset($_POST["_passwordpost"])){
			$user_name = $_POST["namepost"];
	$user_pass =  $_POST["_passwordpost"];
		}
		else{
			$user_name = null;
			$user_pass = null;
		}
		
	$sql = "SELECT Password FROM user WHERE name = '".$user_name."'"; 
	$result = mysqli_query($conn ,$sql);
	
	if(mysqli_num_rows($result) > 0){
		//show data for each row
		while($row = mysqli_fetch_assoc($result)){
			if ($row ['Password'] == $user_pass ){
				echo "login success";
				echo $row;
			} else {
				echo "user not found";
				echo "password is =".$row['Password']; 
			}
	}}	else {
				echo "user not found";
				echo "password is =".$row['Password']; 
			}
	
	
	
	


?>