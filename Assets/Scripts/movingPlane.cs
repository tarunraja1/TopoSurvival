using UnityEngine;
using System.Collections;

public class movingPlane : MonoBehaviour {

    public GUIText P1{ get; set; }
    public GUIText P2{ get; set; }
    public GUITexture Player1 { get; set; }
    public GUITexture Player2{ get; set; }
    public int Speed{ get; set; }
    public GUITexture Blackscreen{ get; set; }
    public float LevelTime{ get; set; }
    public Transform MovePlane { get; set; }
    // Use this for initialization
    void Start () {
        LevelTime = 0;
        Speed = 5;
        P1 = GameObject.Find("P1convo").GetComponent<GUIText>();
        Player1 = GameObject.Find("Player1").GetComponent<GUITexture>();
        P2 = GameObject.Find("P2convo").GetComponent<GUIText>();
        Player2 = GameObject.Find("Player2").GetComponent<GUITexture>();
        Blackscreen = GameObject.Find("Black Screen").GetComponent<GUITexture>();
        MovePlane = GameObject.Find("plane").GetComponent<Transform>();
        P1.text = "";
        P2.text = "";
        Blackscreen.enabled = false;

    }
	
    // Update is called once per frame
    void Update () {
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);
        LevelTime += Time.deltaTime;
        if ((int)LevelTime == 2) {
            P1.text = "Bob: Finally we are hanging out Steve.\n It has been a long time.";
        }
        if ((int)LevelTime == 5)
        {
            P1.text = "";
            P2.text = "Steve: Yeah Bob. What do you think about this jet?\n I'm planning to gift this to my wife.";
        }

        if ((int)LevelTime == 8)
        {
			P1.text = "Bob: It's awesome man! Look at this island.\n I wish we could go there. It'll be so peaceful.";
            P2.text = "";
        }

        if ((int)LevelTime == 11)
        {
            P1.text = "";
			P2.text = "Steve: Yeah it is! But I've heard this place is off limits.";
        }

        if ((int)LevelTime == 14)
        {
			P1.text = "Bob: Yeah, but still";
            P2.text = "";
        }

        if ((int)LevelTime == 16)
        {
            P1.text = "";
            P2.text = "";
        }

        if ((int)LevelTime >= 18 && (int)LevelTime < 26) 
        {
            transform.Translate (Vector3.down * Time.deltaTime * Speed);
            transform.Rotate (Vector3.right * Time.deltaTime * Speed);
        }

        if((int)LevelTime == 20)
        {
			P2.text = "Steve: Wait! Something's wrong with the jet!\n We are descending way too fast.";
            P1.text = "";
        }

        if((int)LevelTime == 24)
        {
			P1.text = "Bob: WHAT'S GOING ON????";
            P2.text = "";
        }

        if ((int)LevelTime == 26)
        {
            P1.text = "";
			P2.text = "Steve: THE PLANES GONNA CRASH!!";
            rigidbody.useGravity = true;
        }


        if ((int)LevelTime == 29) 
        {	P2.text = "";
            Player1.enabled = false;
            Player2.enabled = false;
            Blackscreen.enabled = true;
        }

        if (((int)LevelTime > 32)||(Input.GetKeyDown("space")))
        {
            Application.LoadLevel("TutorialScene");
        }
    }
}
