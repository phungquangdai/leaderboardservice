using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Login : MonoBehaviour {

	public string inputUserName;
	public string inputPassword;
	public string Highscore;

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
}
