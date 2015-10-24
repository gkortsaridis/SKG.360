using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class result_menu : MonoBehaviour {

	private bool isConnected;
	public Texture2D isConnected_Background, isNotConnected_Background;

	public Button send;

	// Use this for initialization
	void Start () {
		send.gameObject.SetActive(true);
		send.onClick.AddListener(() => SendMail());
	}

	void Update(){
		if(Input.GetKeyUp(KeyCode.Escape))Application.LoadLevel("main_menu");
	}

	void SendMail() {
		Application.OpenURL ("http://goo.gl/forms/HhhwfyccJm");
	}


}
