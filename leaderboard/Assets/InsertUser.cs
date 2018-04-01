using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InsertUser : MonoBehaviour {

	public string inputUserName;
	public string inputPassword;

	string CreateUserURL = "http://localhost/leaderboard/InsertUser.php";

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log ("start");
			StartCoroutine( CreateUser (inputUserName, inputPassword));
		}
	}

	IEnumerator CreateUser(string username1, string password1){
		WWWForm form = new WWWForm();
		form.AddField("namepost", username1);
		form.AddField("_passwordpost", password1);
		var www = UnityWebRequest.Post(CreateUserURL,form);
		yield return www.SendWebRequest();
		if (www.isNetworkError || www.isHttpError) {
			Debug.Log ("error" + www.ToString ());
		} else {
			Debug.Log ("okie" + www.ToString ());
		}
	}
}
