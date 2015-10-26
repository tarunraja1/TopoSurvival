using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {
	private bool OnMainMenu = true;
	private bool OnOptions = false;
	public GUISkin customGUISkin;
	public float hrvtlSlider = 0.0f;

	public GameObject target;//the target object
	private float speed = .75f;//a speed modifier
	private Vector3 point;//the coord to the point where the camera looks at

	// Use this for initialization
	void Start () {
            hrvtlSlider = 10F;
		point = target.transform.position;//get target's coords
		transform.LookAt(point);//makes the camera look to it 
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (point,new Vector3(0.0f,1.0f,0.0f),20 * Time.deltaTime * speed);
		AudioListener.volume = hrvtlSlider/10.0f;
	}
	
	void OnGUI (){

		GUI.skin = customGUISkin;
		MainMenu ();
		Options ();


	}
	
	void MainMenu(){
		
		if (OnMainMenu) {
			
			if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 - 125, 300, 100), "New Game")) {
				Application.LoadLevel("level0scene");
			}
			
			if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 , 300, 100), "Options")) {
				OnMainMenu = false;
				OnOptions = true;
			}
			
			if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 + 125, 300, 100), "Exit")) {
				Application.Quit();
			}
		}
	}

	void Options(){

		if (OnOptions) {

			GUI.Label(new Rect(Screen.width/2-155,Screen.height/2, 150, 50),"Volume");
			hrvtlSlider = GUI.HorizontalSlider (new Rect (Screen.width / 2 + 5, Screen.height / 2+ 10 , 150, 50),hrvtlSlider,0.0f,10.0f);
			if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 + 125, 300, 100), "Back")) {
				OnMainMenu=true;
				OnOptions= false;
				
			}
		}
	}
}
