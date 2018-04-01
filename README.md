# leaderboardservice
how to implement leaderboard service


## Getting Started

These instructions will show how to create leaderboard service with HTTP REST and SOCKET connection.

## I.HTTP REST
### Prerequisites

You need to install something first

```
download xampp and install
download Unity 2017.3.1f1
```

### Database
Create 2 table in mysql
```
table user
id_user INT(10)
name varchar(20)
Password varchar (6)
isAdmind BOOLEAN
```

```
table highscore
id_user INT(10)
highscore varchar(10)
timeupdate (TIMESTAMP)

```
### Implement

1. User login to game.
* serverside Login.php
```
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
```

* clientside Login.cs
```
	public string inputUserName;
	public string inputPassword;

	string LoginURL = "http://localhost/leaderboard/Login.php";

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.L)) {
			StartCoroutine( LogintoDB (inputUserName, inputPassword));
		}
	}

	IEnumerator LogintoDB(string username1, string password1){
		WWWForm form = new WWWForm();
		form.AddField("namepost", username1);
		form.AddField("_passwordpost", password1);
		WWW www = new WWW (LoginURL,form);
		yield return www;
		Debug.Log (www.text);
	}
```
2. User add score to database 
* serverside
Addscore.php
```
	//Make Connection
	$conn = new mysqli($servername, $username, $password, $dbName);
	//Check Connection
	if(!$conn){
		die("Connection Failed. ". mysqli_connect_error());
	}
	if (isset($_POST["username_post"]) && isset($_POST["_highscore"])){
			$username = $_POST["username_post"];
	$_highscore =  $_POST["_highscore"];
    $_timecreate =  $_POST["_timecreate"];

		}
		else{
			$username = null;
			$_highscore = null;
			$timecreate = null;
		}
		$sql = "INSERT INTO highscore (id_user, highscore, timecreate) VALUES(
		          (SELECT
				  id_user 	
				  FROM user
				  WHERE username = '".$username"'),
				  $_highscore,
				  $_timecreate,
				  )
	$result = mysqli_query($conn ,$sql);
	if(!result) echo "there was an error";
	else echo "insert highscore successful.";
```
* clientside 
Addscore.cs
```
	IEnumerator AddScore(string username, string highscore,System.DateTime timeupdate ){
	    double timeStamp = ConvertToToTimestamp(timeupdate);
		WWWForm form = new WWWForm();
		form.AddField("username_post", username);
		form.AddField("highscore", highscore);
		form.AddField("timeupdate", timeStamp);
		var www = UnityWebRequest.Post(AddScoreURL,form);
		yield return www.SendWebRequest();
		if (www.isNetworkError || www.isHttpError) {
			Debug.Log ("error" + www.ToString ());
		} else {
			Debug.Log ("okie" + www.ToString ());
		}
	}
	
	private double ConvertToToTimestamp(System.DateTime value)
    {
        double timeStamp = (System.DateTime.UtcNow - value).TotalSeconds; 
        return timeStamp;
    }
```
3. At serverside, We make file Admin.php and using syntax like below to see how many users updated their score in a time window.
Admin.php
```
$sql = "SELECT id_user,COUNT (*)  FROM highscore GROUP BY id_user WHERE timeupdate > '".$timeupdate1."'" AND timeupdate < '".$timeupdate2."'"
```
4. At serverside, We make file Admin.php and using syntax like below to see how many times a user updated their score.

Admin.php
```
$sql = "SELECT id_user,COUNT (*)  FROM highscore GROUP BY id_user WHERE id_user = '".$id_user."'
```
5.At serverside, We make file Admin.php and using syntax like below to delete a username and score.

Admin.php
```
$sql = "DELETE u, h 
               FROM user u
               JOIN highscore h ON u.id_user = h.id_user
               WHERE u.id_user = '".$id_user."'		  
```

## II.SOCKET CONNECTION
### Prerequisites

You need to install something first

```
create folder Plugins that contains file MySql.Data.dll;System.Data.dll
```

### Database
Create 2 table in mysql
```
table user
id_user INT(10)
name varchar(20)
Password varchar (6)
isAdmind BOOLEAN
```

```
table highscore
id_user INT(10)
highscore varchar(10)
timeupdate (TIMESTAMP)

```
### Implement
1. make file DatabaseHandler.cs to connect database MYSQL and implement query
* DatabaseHandler.cs
```
	void Awake(){
		DontDestroyOnLoad (this.gameObject);
		connectionString = "Server=" + host + ";Database=" + database + ";User=" + user + ";Password=" + password + ";Pooling=";
		if (pooling) {
			connectionString += "True";
		} else {
			connectionString += "False";
		}
		try{
			con = new MySqlConnection(connectionString);
			con.Open();
			Debug.Log("Mysql state: "+con.State);

			string sql = "SELECT * FROM user";
			cmd = new MySqlCommand(sql, con);
			//			string sql = "SELECT * FROM clothes";
			//			cmd = new MySqlCommand(sql, con);
						rdr = cmd.ExecuteReader();

						while (rdr.Read())
						{
							Debug.Log("???");
							Debug.Log(rdr[0]+" -- "+rdr[1]);
			    		}
				    	rdr.Close();

		}catch(Exception e){
			Debug.Log (e);
		}
	}
```
* then create function getHighScore() to query and return high score of user. Input are username and password like "DAVID|123456"
 
 ```
 	public string getHighScore(string data){
		Debug.Log ("databasehandler.gethighscore");
		string[] items = data.Split ('|');
		string sql = "SELECT h.id_user, h.highscore, h.timecreate FROM highscore h JOIN user u ON h.id_user = u.id_user WHERE u.name = '" + items[0] + "' AND u.Password = '" + items[1] + "'";
		cmd = new MySqlCommand(sql, con);
		using (rdr = cmd.ExecuteReader ()) {
			while (rdr.Read ()) {
				items [0] = rdr [0].ToString();
				items [1] = rdr [1].ToString();

			}
		}
		Debug.Log ("databasehandler" + items[0] + items[1]);
		return items[1].ToString();
	} 
 ```

 2. user login with correct username and password to server. then server will respond result highscore
* client.cs
```
	public void SendMessageToServer()
	{
		string _username = username.text;
		string _password = password.text;
		string message = _username + "|" + _password;
		Connect(server, port, message);
	}
```
```
void Connect(String server, int port, String message)
	{
		try
		{
			// Create a TcpClient.
			TcpClient client = new TcpClient(server, port);

			// Translate the passed message into ASCII and store it as a Byte array.
			Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

			// Get a client stream for reading and writing.
			//  Stream stream = client.GetStream();

			NetworkStream stream = client.GetStream();

			// Send the message to the connected TcpServer. 
			stream.Write(data, 0, data.Length);

			Console.WriteLine("Sent: {0}", message);
			// Receive the TcpServer.response.

			// Buffer to store the response bytes.
			data = new Byte[256];

			// String to store the response ASCII representation.
			String responseData = String.Empty;

			// Read the first batch of the TcpServer response bytes.
			Int32 bytes = stream.Read(data, 0, data.Length);
			responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
			Console.WriteLine("Client Received: {0}", responseData);

			// Close everything.
			stream.Close();
			client.Close();
		}
		catch (ArgumentNullException e)
		{
			Console.WriteLine("ArgumentNullException: {0}", e);
		}
		catch (SocketException e)
		{
			Console.WriteLine("SocketException: {0}", e);
		}

		Console.WriteLine("\n Press Enter to continue...");
		Console.Read();
	}
```

3. server get information from client and query into database to return the result back
* server.cs

```
	void Update () {
		string userinfo = this.GetValue ();
		if (Serverrespond) {
			Serverrespond = false;
			temp = _mysqlHolder.getHighScore (userinfo);
			highscore.text = "high score: " + temp;
		}

	}
```

```
public void ListenForMessages(int port)
	{
		TcpListener server = null;
		try
		{
			// Set the TcpListener on port 13000.
			IPAddress localAddr = IPAddress.Parse("127.0.0.1");

			// TcpListener server = new TcpListener(port);
			server = new TcpListener(localAddr, port);

			// Start listening for client requests.
			server.Start();

			// Buffer for reading data
			Byte[] bytes = new Byte[256];
			String data = null;

			// Enter the listening loop.
			while (true)
			{
				Debug.Log("Waiting for a connection... ");

				// Perform a blocking call to accept requests.
				using (TcpClient client = server.AcceptTcpClient())
				{

					Debug.Log("Connected!");

					data = null;

					// Get a stream object for reading and writing
					NetworkStream stream = client.GetStream();

					int i;

					// Loop to receive all the data sent by the client.
					while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
					{
						// Translate data bytes to a ASCII string.
						data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
						Debug.Log(String.Format("server Received: {0}", data));
						//items = data.Split('|');
						lock (this.valueLock){
							this.value = data;
						}
						Serverrespond = true;
						data = data.ToUpper();

						byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

						// Send back a response.
						stream.Write(msg, 0, msg.Length);
						Debug.Log(String.Format("Sent: {0}", data));
					}
				}
			}
		}
		catch (SocketException e)
		{
			Debug.LogError(String.Format("SocketException: {0}", e));
		}
		finally
		{
			// Stop listening for new clients.
			server.Stop();
		}
	}
```

```
	string GetValue() {
		// this will be filled in below
		string val;
		lock(this.valueLock)
		{
			val = this.value;
		}
		return val;
	}
```


