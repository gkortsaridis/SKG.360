using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class main_menu : MonoBehaviour {
	
	public Texture2D logo;
	public Texture2D play_idle, play_clicked;
	public Texture2D info_idle, info_clicked;
	public Texture2D cbOn_idle, cbOn_clicked;
	public Texture2D cbOff_idle, cbOff_clicked;

	private int logoPosX, logoPosY, logoSizeX, logoSizeY;
	private int playPosX, playPosY, playSizeX, playSizeY;
	private int infPosX, infPosY, infSizeX, infSizeY;
	private int cbPosX, cbPosY, cbSizeX, cbSizeY;

	// Use this for initialization
	void Start () {
		logoSizeX = Screen.width / 3;
		logoSizeY = logoSizeX;
		logoPosX = Screen.width / 10;
		logoPosY = Screen.height / 2 - logoSizeY / 2;

		playSizeX = Screen.width / 4;
		playSizeY = Screen.height / 6;
		playPosX = Screen.width / 2 + Screen.width / 10;
		playPosY = Screen.height / 2 - Screen.height / 7;

		cbSizeX = Screen.width / 7;
		cbSizeY = cbSizeX;
		cbPosX = (playPosX + playSizeX / 2) - Screen.width / 10 - cbSizeX/2;
		cbPosY = Screen.height / 2 + Screen.height / 15;

		infSizeX = cbSizeX;
		infSizeY = infSizeX;
		infPosX = (playPosX + playSizeX / 2) + Screen.width / 10 - infSizeX/2;
		infPosY = cbPosY;

		string url = "http://www.google.com";
		WWW www = new WWW(url);
		StartCoroutine(WaitForRequest(www));
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
	}
	
	void OnGUI(){

		GUI.DrawTexture (new Rect (logoPosX, logoPosY, logoSizeX, logoSizeY), logo);

		GUI.skin.button.normal.background = play_idle;
		GUI.skin.button.hover.background = play_idle;
		GUI.skin.button.active.background = play_clicked;
		if(GUI.Button(new Rect(playPosX,playPosY,playSizeX,playSizeY),""))Application.LoadLevel("loading");
		
		GUI.skin.button.normal.background = info_idle;
		GUI.skin.button.hover.background = info_idle;
		GUI.skin.button.active.background = info_clicked;
		if(GUI.Button(new Rect(infPosX,infPosY,infSizeX,infSizeY),""))Application.LoadLevel("info");

		if(PlayerPrefs.GetInt("vrEnable") == 1)
		{
			GUI.skin.button.normal.background = cbOn_idle;
			GUI.skin.button.hover.background = cbOn_idle;
			GUI.skin.button.active.background = cbOn_clicked;
			if(GUI.Button(new Rect(cbPosX,cbPosY,cbSizeX,cbSizeY),""))PlayerPrefs.SetInt("vrEnable",0);
		}
		else
		{
			GUI.skin.button.normal.background = cbOff_idle;
			GUI.skin.button.hover.background = cbOff_idle;
			GUI.skin.button.active.background = cbOff_clicked;
			if(GUI.Button(new Rect(cbPosX,cbPosY,cbSizeX,cbSizeY),""))PlayerPrefs.SetInt("vrEnable",1);
		}

	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
		// check for errors
		if (www.error == null)
		{
			Debug.Log("WWW Ok!: " + www.data);
			PlayerPrefs.SetInt("isConnected",1);
		} else {
			Debug.Log("WWW Error: "+ www.error);
			PlayerPrefs.SetInt("isConnected",0);
			AndroidDialogAndToastBinding.instance.toastLong("You have no internet connection. You can still play the game, but you cannot take part on the competition.");
		}    
	}


}
