using UnityEngine;
using System.Collections;

public class end_menu : MonoBehaviour {

	public Texture2D remove;

	void Start () {
		StartCoroutine (showRemove());
	}


	IEnumerator showRemove(){
		yield return new WaitForSeconds (2.5f);

		if (PlayerPrefs.GetInt ("isConnected") == 1 && PlayerPrefs.GetInt("allcorect") == 1) {
			Application.LoadLevel ("result");
		} else {
			Application.LoadLevel ("main_menu");
		}

	}



	void OnGUI(){
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),remove);
	}
	
	
}
