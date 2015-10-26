using UnityEngine;
using System.Collections;

public class DestroyBearTrap : MonoBehaviour {
	private static bool p1turn,p2turn,correctPosition;
	private static int count,pullPosition;
	public Vector3 force;
	private static float LevelTime;
	public GameObject invisibleCollider,invisibleCollider2;
	// Use this for initialization
	void Start () {
		p1turn = false; 
		p2turn = false;
		correctPosition = false;
		count = 0;
		invisibleCollider = GameObject.Find("InvisibleCollider");
		invisibleCollider2 = GameObject.Find("InvisibleCollider 1");
	} 
	
	// Update is called once per frame
	void Update () {
		LevelTime += Time.deltaTime;
		
		if (gameObject.tag == "RockPullPosition" && count < 4) {
			
			if (Input.GetKeyDown (KeyCode.P) && p2turn) {
				
				p2turn = false;
				p1turn = true;
				Move1.P2Tips = "";
				count++;
			}
			
			else if (Input.GetKeyDown (KeyCode.R) && p1turn) {
				
				p2turn = true;
				p1turn = false;
				Move1.P1Tips = "";
				count++;	
			}
		}
		
		if (count == 4 && gameObject.name =="Rock01"){
			
			if(pullPosition == BearPuzzleScript.chosenPosition) 
				correctPosition= true;
			
			Move1.P1Tips = "";
			Move1.P2Tips = "";
			GameObject[] lt = GameObject.FindGameObjectsWithTag ("RockPullPosition");
			foreach(GameObject go in lt){
				Destroy(go);
			}
			rigidbody.AddForce(Vector3.back *1000f);
			count =5;
			if(!correctPosition)
			{
				var position = gameObject.transform.position;
				position.x = 420F;
				position.y = 36F;
				position.z = 170F;
				var newObject = (GameObject)Instantiate (Resources.Load ("HoleTrigger"), position, Quaternion.identity);
				newObject.name = "EnterHole";
				newObject.tag = "TriggerPosition";
				newObject.transform.localScale = new Vector3 (4.4f,1f,35f);
				invisibleCollider.collider.isTrigger= false;
				invisibleCollider2.collider.isTrigger= false;
				Move1.AddDialog(1, "......  ");
				Move1.AddDialog(1, ".......");
				Move1.AddDialog(1, "Oh no. We broke the ground.");
				Move1.AddDialog(2, "Perfect. Now we have to waste time getting me in and out. Let's Hurry.");
				SavedVars.Instance.StartCoroutine(Move1.Talk());
			}
			else{
				Move1.AddDialog(1, "......  ");
				Move1.AddDialog(1, ".......");
				Move1.AddDialog(1, "Whew. we didn't break the ground.");
				Move1.AddDialog(2, "Good. let's go.");
				SavedVars.Instance.StartCoroutine(Move1.Talk());

			}
		}
		if (count == 6 && gameObject.name == "EnterHole") {
			
			if (Input.GetKeyDown (KeyCode.Space)) {
				
				Move1.P2Tips = "";
				Destroy (GameObject.Find ("EnterHole"));
				BearPuzzleScript.player2.transform.position = new Vector3 (408F, 31F, 168F);
				count = 7;
				var position = gameObject.transform.position;
				position.x = 393F;
				position.y = 36F;
				position.z = 165F;
				var newObject = (GameObject)Instantiate (Resources.Load ("HoleTrigger"), position, Quaternion.identity);
				newObject.name = "DropRope";
				newObject.tag = "TriggerPosition";
				newObject.transform.localScale= new Vector3 (4.4f,1f,40f);
				//BridgePuzzle.CreateGameObject("Rope","RopeMesh",355F,36F,167F);
				Move1.AddDialog(2, "Hey, Buddy lend me some rope.");
				Move1.AddDialog(1, "Right.");
				SavedVars.Instance.StartCoroutine(Move1.Talk());
			}
		}
		if (count == 7 && gameObject.name == "DropRope") {

			if (Input.GetKeyDown (KeyCode.Space)) {
				print ("drop");
				Move1.P1Tips = "";
				Destroy (GameObject.Find ("DropRope"));
				var position = gameObject.transform.position;
				position.x = 402F;
				position.y = 29F;
				position.z = 168F;
				
				var newObject = (GameObject)Instantiate (Resources.Load ("PullPosition"), position, Quaternion.identity);
				newObject.name = "Rope";
				newObject.tag = "TriggerPosition";
				BridgePuzzle.CreateGameObject("Rope","RopeMesh",403F,29F,169F);
				count = 8;
			}
		}
		if(count == 8 && gameObject.name == "Rope"){
			if (Input.GetKeyDown(KeyCode.Space)){
				Move1.P2Tips = "";
				Destroy(GameObject.Find("Rope"));
				Destroy(GameObject.Find("RopeMesh"));
				BearPuzzleScript.player2.transform.position = new Vector3(390F,37F,165F);
				count = 9;
				Move1.AddDialog(1, "Lets go.");
				SavedVars.Instance.StartCoroutine(Move1.Talk());
			}

		}
		if (count == 9) {
				
		}
	} 
	
	public void OnTriggerEnter(Collider collider)

	{
		if (gameObject.tag == "EndGame") {
						GameOver ();
				}
		if ((collider.name == "Player1"||collider.name == "Player2") && (gameObject.name =="BearTrapPrefab" || gameObject.name =="BigTree" ||gameObject.name == "beartrap")){
			Move1.Movable = false;
			Move2.movable = false;
			StartCoroutine (Respawn ());
			
		}
		
		if ( gameObject.name =="BearTrapPrefab" && collider.name == "Rock01") {
			if(correctPosition){
				Destroy(GameObject.Find("Rock01"));
				Destroy(GameObject.Find ("BearTrapPrefab"));
			}
			else{
				Destroy(GameObject.Find("Rock01"));
				Destroy(GameObject.Find ("BearTrapPrefab"));
				Destroy(GameObject.Find ("Hole"));
			}
		}
		if (gameObject.tag == "TriggerPosition")
		{
			if(collider.name == "Player2" && gameObject.name =="EnterHole"){
				Move1.P2Tips = "Press \"Space\" to Enter Hole";
				count =6;
				
			}
			if(collider.name == "Player1" && gameObject.name =="DropRope"){
				Move1.P1Tips = "Press \"Space\" to Drop Rope";
				count =7;
				
			}
			if(collider.name == "Player2" && gameObject.name =="Rope"){
				Move1.P2Tips = "Press \"Space\" to Climb Up";
				count=8;
			}
		} 
	}
	
	public void OnTriggerStay (Collider collider){
		
		if (gameObject.tag == "RockPullPosition") {
			if (collider.name == "Player2" && (count % 2 == 0)) {
				Move1.P2Tips = "Press \"p\" to Pull.";
				
				p2turn = true;
				switch (gameObject.name) {
				case "Position1":
					pullPosition = 1;
					break;
				case "Position2":
					pullPosition = 2;
					break;
				case "Position3":
					pullPosition = 3;
					break;
				case "Position4":
					pullPosition = 4;
					break;
				} 
			} else if (collider.name == "Player1" && (count % 2 != 0)) { 
				Move1.P1Tips = "Press \"R\" to Pull.";
				p1turn = true;
				switch (gameObject.name) {
				case "Position5":
					pullPosition = 1;
					break;
				case "Position6": 
					pullPosition = 2;
					break;
				case "Position7":
					pullPosition = 3;
					break;
				case "Position8":
					pullPosition = 4;
					break;
				}
			}
			
		}
	}
	IEnumerator Respawn() {
		yield return new WaitForSeconds(3.0f);
		BearPuzzleScript.player1.transform.position = new Vector3(463F,60F,172F);
		BearPuzzleScript.player1.transform.rotation = Quaternion.identity;
		BearPuzzleScript.player2.transform.position = new Vector3(465F,50F,175F);
		Move1.Movable = true;
		Move2.movable = true;
		Move1.P1Tips = "Oops. Be Careful";
		Move1.P2Tips = "Oops. Be Careful";
	}
	public static void GameOver()
	{
		
		SavedVars.Instance.score +=(int)( 4000 - LevelTime * 10);
		SavedVars.Instance.speed = Move1.Speed;
		Application.LoadLevel("finallevel");
	}
}

