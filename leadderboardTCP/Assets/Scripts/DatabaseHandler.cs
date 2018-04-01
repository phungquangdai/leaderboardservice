using UnityEngine;
using System;
using System.Data;
using System.Text;

using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

using MySql.Data;
using MySql.Data.MySqlClient;

public class DatabaseHandler : MonoBehaviour {

	public string host, database, user, password;
	public bool pooling = true;

	private string connectionString;
	private MySqlConnection con = null;
	private MySqlCommand cmd = null;
	private MySqlDataReader rdr = null;
	private MD5 _md5Hash;

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
	void onApplicationQuit(){
		if (con != null) {
			if (con.State.ToString () != "Closed") {
				con.Close ();
				Debug.Log ("Mysql connection closed");
			}
			con.Dispose ();
		}
	}
		
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
	public string GetConnectionState(){
		return con.State.ToString ();
	}
}
