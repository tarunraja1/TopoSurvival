using UnityEngine;
using System.Collections;

public class endingscript : MonoBehaviour {

	public GUIText P1{ get; set; }
	public GUIText P2{ get; set; }
	public GUIText TheEnd{ get; set; }
	public GUITexture Player1 { get; set; }
	public GUITexture Player2{ get; set; }
	public int Speed{ get; set; }
	public GUITexture Blackscreen{ get; set; }
	public float LevelTime{ get; set; }
	public Transform MoveShip { get; set; }

	// Use this for initialization
	void Start () {
		LevelTime = 0;
		Speed = 20;
		P1 = GameObject.Find("P1convo").GetComponent<GUIText>();
		Player1 = GameObject.Find("Player1").GetComponent<GUITexture>();
		P2 = GameObject.Find("P2convo").GetComponent<GUIText>();
		Player2 = GameObject.Find("Player2").GetComponent<GUITexture>();
		Blackscreen = GameObject.Find("Black Screen").GetComponent<GUITexture>();
		MoveShip = GameObject.Find("cruiseship").GetComponent<Transform>();
		TheEnd = GameObject.Find ("TheEnd").GetComponent<GUIText> ();
		P1.text = "";
		P2.text = "";
		TheEnd.text = "";
		Blackscreen.enabled = false;

	
	}
	
	// Update is called once per frame
	void Update () {
	
		LevelTime += Time.deltaTime;

		if ((int)LevelTime == 1)
		{
			P1.text = "Steve: Bob! LOOK! There's a cruise ship here.";
			P2.text = "";
		}

		if ((int)LevelTime == 4)
		{
			P1.text = "";
			P2.text = "Bob: I can't believe this! Out of all the possibilities, we find a cruise ship.";
		}

		if ((int)LevelTime == 7)
		{
			P1.text = "Steve: Yeah! We can finally get out of this island. Let's go aboard before it leaves.";
			P2.text = "";
		}

		if ((int)LevelTime == 10)
		{
			P1.text = "";
			P2.text = "Bob: Wow. I hope I'm not dreaming. Let's go home man.";
		}

		if ((int)LevelTime == 13)
		{
			P1.text = "";
			P2.text = "";
		}

		if ((int)LevelTime >= 14) {
						Player1.enabled = false;
						Player2.enabled = false;
						transform.Translate (Vector3.left * Time.deltaTime * Speed);
				}

		if ((int)LevelTime >= 30) {
			Blackscreen.enabled = true;
			TheEnd.text = "The End Score: "+ SavedVars.Instance.score;
		}
	}
}
