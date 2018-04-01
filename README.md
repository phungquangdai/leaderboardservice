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
#### User should be able to add/update a username and a score
1. User login to game. Check if user is Adminstrator or not. If not, add score to user by 'id_user'
* serverside
Addscore.php
```
$id_user = $_POST["id_user"];
$highscore = $_POST["highscore"];
$timeupdate = $_POST["timeupdate"];
$sql = "INSERT INTO highscore (id_user, highscore, timeupdate)
			VALUES ('".$id_user."','".$highscore."','".$timeupdate."')";
```
* clientside 
Addscore.cs
```
	IEnumerator AddScore(string id_user, string highscore,System.DateTime timeupdate ){
	    double timeStamp = ConvertToToTimestamp(timeupdate);
		WWWForm form = new WWWForm();
		form.AddField("id_user", id_user);
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
2. Admin login to game to see how many users updated their score in a time window.
* serverside
Admin.php
```
$sql = "SELECT id_user,COUNT (*)  FROM highscore GROUP BY id_user WHERE timeupdate > '".$timeupdate1."'" AND timeupdate < '".$timeupdate2."'"
```
3. Admin login to game to see how many times a user updated their score.

Admin.php
```
$sql = "SELECT id_user,COUNT (*)  FROM highscore GROUP BY id_user WHERE id_user = '".$id_user."'
```
4. Admin login to to delete a username and score.

Admin.php
```
$sql = "DELETE user, highscore 
               FROM user u
               JOIN highscore h ON u.id_user = h.id_user
               WHERE u.id_user = '".$id_user."'		  
```

## II.SOCKET connection