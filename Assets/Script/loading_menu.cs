using UnityEngine;
using System.Collections;

public class loading_menu : MonoBehaviour {

	public Texture2D insertCB;
	public Texture2D instructions;
	public Texture2D instructionsMONO;
	private bool show;

	void Start(){
		show = true;
		if (PlayerPrefs.GetInt ("vrEnable") == 1) {
			StartCoroutine (StartGame ());
		} else {
			StartCoroutine (StartGame2 ());
		}
	}

	IEnumerator StartGame(){
		yield return new WaitForSeconds(4);
		StartCoroutine (StartGame2());
	}

	IEnumerator StartGame2(){
		show = false;
		yield return new WaitForSeconds(5);
		Application.LoadLevel ("game");
	}

	void OnGUI(){
		if (show) {
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), insertCB);
		} else {
			if (PlayerPrefs.GetInt ("vrEnable") == 1) GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), instructions);
			else GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), instructionsMONO);
		}
	}



}
