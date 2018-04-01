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
	
	$sql = "SELECT id_user, name, Password FROM user";
	$result = mysqli_query($conn ,$sql);
	
	
	if(mysqli_num_rows($result) > 0){
		//show data for each row
		while($row = mysqli_fetch_assoc($result)){
			echo "id_user: ".$row['id_user'] ."|". " Name: ".$row['name']."|". " Pass: ".$row['Password']. ";";
		}
	}
	
	
	
	


?>