using UnityEngine;
using System.Collections;

public class info_menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void Update(){
		if(Input.GetKeyUp(KeyCode.Escape))Application.LoadLevel("main_menu");
	}
}
