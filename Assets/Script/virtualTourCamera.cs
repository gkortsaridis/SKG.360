using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class virtualTourCamera : MonoBehaviour {

	//CAMERA POINT VARIABLES
	public static string cameraPoint;
	private string lastSeen;
	private float timeSeen;
	private float PosX;
	private float PosY;
	private float SizeX;
	private float SizeY;
	private float PosXbackup;
	private float PosYbackup;
	private float SizeXbackup;
	private float SizeYbackup;
	private bool isLooking;
	[SerializeField] private float delay;
	[SerializeField] private Texture2D circle_idle;
	[SerializeField] private Texture2D circle_focus;

	[SerializeField] private GameObject panoramaSphere;
	[SerializeField] private Texture[] panos;
	[SerializeField] private Texture blackTransparent;
	[SerializeField] private Texture redTransparent;
	[SerializeField] private Texture greenTransparent;
	[SerializeField] private Font myFont;
	[SerializeField] private string[] corrects;
	private string[,] answers;
	[SerializeField] private AudioClip correct_sound;
	[SerializeField] private AudioClip wrong_sound;



	private string mytext;
	private GUIStyle style;
	private int curPanorama;
	private GameObject pic1;
	private GameObject pic2;
	private GameObject pic3;
	private GameObject pic4;
	private GameObject pic5;
	private bool iscorrect , gonnaShow;
	private AudioSource audioSource;



	// Use this for initialization
	void Start () {

		PlayerPrefs.SetInt ("allcorect",1);

		audioSource = GetComponent<AudioSource> ();

		CardboardOnGUI.IsGUIVisible = true;
		CardboardOnGUI.onGUICallback += this.OnGUI;

		if (PlayerPrefs.GetInt ("vrEnable") == 1) {
			GetComponentInParent<Cardboard> ().VRModeEnabled = true;
		} else {
			GetComponentInParent<Cardboard> ().VRModeEnabled = false;
		} 

		answers = new string[5,5];

		answers[0,0] = "Aristotelous Square";
		answers[0,1] = "Upper Town";
		answers[0,2] = "Galerius Arch";
		answers[0,3] = "Hagios Dimitrios";
		answers[0,4] = "Roman Forum";

		answers[1,0] = "Hagios Dimitrios";
		answers[1,1] = "Aristotelous Square";
		answers[1,2] = "Hagia Sofia";
		answers[1,3] = "Rotunda";
		answers[1,4] = "White Tower";

		answers[2,0] = "Roman Forum";
		answers[2,1] = "Hagios Dimitros";
		answers[2,2] = "Hagia Sofia";
		answers[2,3] = "Aristotelous Square";
		answers[2,4] = "Music Concert Hall";

		answers[3,0] = "White Tower";
		answers[3,1] = "Aristotelous Square";
		answers[3,2] = "Thessaloniki Port";
		answers[3,3] = "Heptapirgion";
		answers[3,4] = "Archaeologic Museum";

		answers[4,0] = "Thessaloniki Port";
		answers[4,1] = "Rotunda";
		answers[4,2] = "Navarinou Square";
		answers[4,3] = "Thessaloniki International Fair";
		answers[4,4] = "Aristotelous Square";

		//Finding the GameObjects
		pic1 = GameObject.FindGameObjectWithTag("pic1");
		pic2 = GameObject.FindGameObjectWithTag("pic2");
		pic3 = GameObject.FindGameObjectWithTag("pic3");
		//pic4 = GameObject.FindGameObjectWithTag("pic4");
		//pic5 = GameObject.FindGameObjectWithTag("pic5");


		//Initiating variables
		lastSeen = "null";
		timeSeen=Time.time;
		SizeXbackup = SizeX = Screen.width/15;
		SizeYbackup = SizeY = SizeX;
		PosXbackup = PosX = Screen.width / 4 - SizeX/2;
		PosYbackup = PosY = Screen.height/2-SizeY/2;
		cameraPoint = "null";
		Screen.sleepTimeout = SleepTimeout.NeverSleep; 
		isLooking = false;

		style = new GUIStyle("label");

		//Rendering the first panorama 	 	 	 	
		curPanorama = 0;
		panoramaSphere.GetComponent<Renderer>().material.mainTexture = panos[curPanorama];

	}

	void OnDestroy(){
		CardboardOnGUI.onGUICallback -= this.OnGUI;
	}
	        
	IEnumerator checkAnswer(string guess){

		gonnaShow = true;
		//Debug.Log ("My guess : " + guess);
		iscorrect = (guess == corrects [curPanorama]);
			
		if (iscorrect) {
			audioSource.clip = correct_sound;
		}else{
			audioSource.clip = wrong_sound;
			PlayerPrefs.SetInt("allcorect",0);
		}
		audioSource.Play ();

		yield return new WaitForSeconds (1.5f);
		gonnaShow = false;

		if (curPanorama < 4) {
			curPanorama++;
			panoramaSphere.GetComponent<Renderer> ().material.mainTexture = panos [curPanorama];
		} else {
			Application.LoadLevel("end");
		}
	}


	// Update is called once per frame
	void Update () {

		if(Input.GetKeyUp(KeyCode.Escape))Application.LoadLevel("main_menu");

		RaycastHit hit;
		isLooking=false;

		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
		{
			cameraPoint = hit.collider.tag;

			if(cameraPoint == lastSeen)
			{
				//Debug.Log("Keep looking "+cameraPoint);
				if(Time.time - timeSeen > delay)
				{	
					Handheld.Vibrate();
					//Debug.Log("SELECTED "+cameraPoint);
					SizeX = SizeXbackup;
					SizeY = SizeYbackup;
					PosX = PosXbackup;
					PosY = PosYbackup;
					isLooking = false;
					lastSeen = "null";

					StartCoroutine(checkAnswer(cameraPoint));
					
				}
				else
				{
					//here
					SizeX=((Time.time - timeSeen)/delay)*SizeXbackup+Screen.width/50;
					SizeY=SizeX;
					PosX=Screen.width/4-SizeXbackup/2-((Time.time - timeSeen)/delay*SizeXbackup)/2+SizeXbackup/2-Screen.width/100;
					PosY=Screen.height/2-SizeYbackup/2-((Time.time - timeSeen)/delay*SizeYbackup)/2+SizeYbackup/2-Screen.width/100;
					isLooking=true;
				}
			}
			else
			{	
				SizeX = SizeXbackup;
				SizeY = SizeYbackup;
				PosX = PosXbackup;
				PosY = PosYbackup;
				lastSeen = cameraPoint;
				timeSeen = Time.time;
				isLooking=false;
				//Debug.Log("Looked else! at "+timeSeen);
			}
			
			
		}
		else
		{		
			cameraPoint = "null";
			SizeX = SizeXbackup;
			SizeY = SizeYbackup;
			PosX = PosXbackup;
			PosY = PosYbackup;
			lastSeen = cameraPoint;
			timeSeen = Time.time;
		}

	
	}


	void OnGUI(){
		if (!CardboardOnGUI.OKToDraw(this)) return;
		if(isLooking && !gonnaShow)
		{
			GUI.Label(new Rect(Screen.width/2-SizeX/2,Screen.height/2-SizeY/2,SizeX,SizeY),circle_focus);

			GUI.DrawTexture(new Rect(0,3*Screen.height/6,Screen.width,Screen.height/6),blackTransparent);
			
			style.fontSize = Screen.width/50; 	
			style.font = myFont;
			style.normal.textColor = Color.white;
			style.alignment = TextAnchor.MiddleCenter;
				
			if(cameraPoint == "pic1")
			{
				mytext = answers[curPanorama,0];
			}
			else if(cameraPoint == "pic2")
			{
				mytext = answers[curPanorama,1];
			}
			else if(cameraPoint == "pic3")
			{
				mytext = answers[curPanorama,2];
			}
			else if(cameraPoint == "pic4")
			{
				mytext = answers[curPanorama,3];
			}
			else if(cameraPoint == "pic5")
			{
				mytext = answers[curPanorama,4];
			}


			GUI.Label(new Rect(Screen.width/4,3*Screen.height/6,Screen.width/2,Screen.height/6), mytext,style);
		}
		else
		{
			GUI.Label(new Rect(Screen.width/2-Screen.width/100,Screen.height/2-Screen.width/100,Screen.width/50,Screen.width/50),circle_idle);

			if(gonnaShow){
				if(iscorrect){
					GUI.DrawTexture(new Rect(0,3*Screen.height/6,Screen.width,Screen.height/6),greenTransparent);
					GUI.Label(new Rect(Screen.width/4,3*Screen.height/6,Screen.width/2,Screen.height/6), "CORRECT!",style);
				}else{
					GUI.DrawTexture(new Rect(0,3*Screen.height/6,Screen.width,Screen.height/6),redTransparent);
					GUI.Label(new Rect(Screen.width/4,3*Screen.height/6,Screen.width/2,Screen.height/6), "WRONG",style);
				}
			}
		}

	}
	
}
