# leaderboardservice
how to implement leaderboard service


## Getting Started

These instructions will show how to create leaderboard service with HTTP REST.

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

A step by step series of examples that tell you have to get a development env running

Say what the step will be

```
Give the example
```

And repeat

```
until finished
```

End with an example of getting some data out of the system or using it for a little demo

## Running the tests

Explain how to run the automated tests for this system

### Break down into end to end tests

Explain what these tests test and why

```
Give an example
```

### And coding style tests

Explain what these tests test and why

```
Give an example
```

## Deployment

Add additional notes about how to deploy this on a live system

## Built With

* [Dropwizard](http://www.dropwizard.io/1.0.2/docs/) - The web framework used
* [Maven](https://maven.apache.org/) - Dependency Management
* [ROME](https://rometools.github.io/rome/) - Used to generate RSS Feeds

## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/PurpleBooth/b24679402957c63ec426) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags). 

## Authors

* **Billie Thompson** - *Initial work* - [PurpleBooth](https://github.com/PurpleBooth)

See also the list of [contributors](https://github.com/your/project/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Hat tip to anyone who's code was used
* Inspiration
* etc
