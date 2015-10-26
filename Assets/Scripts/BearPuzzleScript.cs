using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BearPuzzleScript : MonoBehaviour {
	
	public static bool readyforLaunch,launchedCorrectly;
	public static GUIText p1message, p2message;
	private bool p1,p2;
	public float gravity = 9.8f;
	public float angle = 45;
	public static GameObject player1, player2;
	public static int chosenPosition, i, gotTheAxe;
    public static float LevelTime;
	
	// Use this for initialization
	void Start () {
		player1 = GameObject.Find("Player1");
		player2 = GameObject.Find("Player2");
		readyforLaunch = false;
		launchedCorrectly = false;
		p1 = false;
		p2 = false;
		p1message = GameObject.Find("p1message").guiText;
		p2message = GameObject.Find("p2message").guiText;
		chosenPosition = 0;
		i = 0;
		gotTheAxe = 0;
		LevelTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
            LevelTime += Time.deltaTime;
		if (gameObject.tag == "LaunchPosition" && !readyforLaunch)
		{	
			if (Input.GetKeyDown(KeyCode.R)){
				print ("r");
	            Move1.P1Tips  = "";
	            readyforLaunch = true;
	            
			}
		}
		
		else if (readyforLaunch && gameObject.tag == "LaunchTrigger")
		{
                    //Move1.P2Tips = "Press Space to Launch Player 1";
			if (Input.GetKeyDown(KeyCode.Space)&& p2){
				Move1.P2Tips= "";
				print("Launched");
                                print(chosenPosition);
				switch(chosenPosition){
				case 1:
					StartCoroutine(LaunchPlayer(342F,50F, 134F));
					break; 
				case 2:
					StartCoroutine(LaunchPlayer(380F,40F, 169F));
					launchedCorrectly = true;
					break;
				case 3:
					StartCoroutine(LaunchPlayer(372F,40F,169F));
					launchedCorrectly = true;
					break;
				case 4:
					StartCoroutine(LaunchPlayer(409F,60F, 165F));
					break;
				}
				
				
			}
		}
	}
	
	public void OnTriggerEnter(Collider collider)
	{
		if (gameObject.name == "Axe") {
			if (collider.name == "Player 2") {
					gotTheAxe = 1;
				Destroy(GameObject.Find("AxeMesh"));
			}
		}

		if (gameObject.name == "Rope") {
				Destroy(GameObject.Find("RopeMesh"));
			}

		if (gameObject.name == "Rope2") {
			Destroy(GameObject.Find("RopeMesh2"));
		}

		if (gameObject.tag != "LaunchPosition" && gameObject.tag != "LaunchTrigger")
		{
			BearTrapPuzzle.objects.Remove(gameObject);
			Destroy(gameObject);

		} 
	}
	
	public void OnTriggerStay (Collider collider){
		if (gameObject.tag == "LaunchPosition"&& !readyforLaunch) 
		{	
			if(collider.name == "Player1"){ 
				Move1.P1Tips= "Press \"R\" if ready to Launch.";
				p1 =true;
			}
		}
		
		else if (readyforLaunch && gameObject.tag == "LaunchTrigger")
		{
			if(collider.name == "Player2"){
				Move1.P2Tips = "Press Space to Launch Player 1";
				//Move1.P1Tips = "Press \"Space\" to Launch.";
				p2 =true;  
			}
		}

		if (gameObject.tag == "LaunchPosition"&& !readyforLaunch) 
		{	
			
			if(collider.name == "Player1"){ 
				switch(gameObject.name){
				case "Position1":
					chosenPosition = 1;
					break;
				case "Position2":
					chosenPosition= 2;
					break;
				case "Position3":
					chosenPosition = 3;
					break;
				case "Position4":
					chosenPosition = 4;
					break;
				}
			}
		}
	}
	
//	public void OnTriggerExit (Collider collider){
//		
//		
//            if (gameObject.tag == "LaunchPosition" && collider.name == "Player1" ) {	
//                Move1.Movable = true;
//			
//
//		}
//	}
	
	IEnumerator LaunchPlayer(float x ,float y,  float z){

		player1.GetComponent<Move1>().enabled = false;
            Vector3 targetPosition = new Vector3(x,y, z);
		float target_Distance = Vector3.Distance (player1.transform.position, targetPosition);
		
		float initial_velocity = Mathf.Sqrt (target_Distance / (Mathf.Sin (2 * angle*  Mathf.Deg2Rad) / gravity));
		
		float Vx = initial_velocity * Mathf.Cos (angle *  Mathf.Deg2Rad);
		float Vy = initial_velocity * Mathf.Sin (angle*  Mathf.Deg2Rad);
		
		float airTime = (target_Distance / Vx); 
		
		player1.transform.rotation = Quaternion.LookRotation(targetPosition - player1.transform.position);
		
		float elapsedTime = 0;
		
		while (elapsedTime < airTime) {
			player1.transform.Translate(0, (Vy - (gravity * elapsedTime)) * (Time.deltaTime), Vx * (Time.deltaTime));
			elapsedTime += (Time.deltaTime);
			
			yield return null;
		}
		player1.GetComponent<Move1>().enabled = true;
		if(launchedCorrectly){
			
			Destroy(GameObject.FindWithTag ("LaunchTrigger"));
			GameObject[] lt = GameObject.FindGameObjectsWithTag ("LaunchPosition");
			foreach(GameObject go in lt){
				Destroy(go);
			}
                        player1.transform.rotation = Quaternion.identity;
		}
		else {readyforLaunch = false;}
	}
	
	
	
}

