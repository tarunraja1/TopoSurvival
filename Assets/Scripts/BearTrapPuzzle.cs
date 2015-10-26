using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BearTrapPuzzle : MonoBehaviour {
	private bool isActive;
	public static ArrayList objects;
	public int picked;
	
	// Use this for initialization
	void Start () {
		isActive = false;
		picked = 0;
		objects = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () {
		if (isActive) {
			
			if (objects.Count== 0 && picked == 0 ) {
				//Destroy(GameObject.Find("BearTrapStart"));
				CreateObject("Rope",351F,32F,49F);
				BridgePuzzle.CreateGameObject("Rope","RopeMesh",353F,32F,51F);
				CreateObject("Rope2",307F,40F,67F);
				BridgePuzzle.CreateGameObject("Rope","RopeMesh2",309F,40F,69F);
				CreateObject("Axe",447F,30F,99F);
				var goChild = BridgePuzzle.CreateGameObject("Axe","AxeMesh",448F,30F,99F);
				goChild.transform.Rotate(52.1f, 232, 319.78f);
				goChild.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
				picked = 1; 
				
				
			} else if (objects.Count == 0 && picked == 1 ) {

				if(BearPuzzleScript.gotTheAxe ==0){

					Move1.AddDialog(1, "I managed to find a lot of rope.");
					Move1.AddDialog(2, "Great! and I found an axe. Let's meet back at the Bear trap and make the slingshot.");
					SavedVars.Instance.StartCoroutine(Move1.Talk());
				}
				else{
					Move1.AddDialog(2, "I managed to find a lot of rope.");
					Move1.AddDialog(1, "Great! and I found an axe. Let's meet back at the Bear trap and make the slingshot.");
					SavedVars.Instance.StartCoroutine(Move1.Talk());
				}

				var position = gameObject.transform.position;
				position.x = 457F;
				position.y = 35F;
				position.z = 190F;
                var newObject = (GameObject)Instantiate (Resources.Load ("BearTrapTrigger"), position, Quaternion.identity);
                newObject.name = "SlingShot";
				newObject.tag = "TriggerPosition";
				objects.Add (newObject);
				picked = 2;
				
			} else if (objects.Count == 0 && picked == 2) {
				Move1.AddDialog(2, "Alright. Get into a position and i'll launch you from here. ");
				Move1.AddDialog(2,"Be careful we don't want you to launch too close to the trap or too far.");
				Move1.AddDialog(2, "Also we should remember what position we launched in so that we can use it to pull the rock.");
				Move1.AddDialog(1, "Right. The ground looks a little weak under the trap. ");
				Move1.AddDialog(1,"Standing in the wrong position could cause us to pull the rock with too much force and break the ground.");
				SavedVars.Instance.StartCoroutine(Move1.Talk());
				var position = gameObject.transform.position;
				position.x = 457F; 
				position.y = 35;
				position.z = 190F;
				var newObject = (GameObject)Instantiate (Resources.Load ("LaunchPrefab"), position, Quaternion.identity);
				newObject.name = "Launch";
				newObject.tag = "LaunchTrigger";
				picked = 3;
				
				CreateLaunchPositions ("Position1", 437F, 177F);
				CreateLaunchPositions ("Position2", 444F, 177F);
				CreateLaunchPositions ("Position3", 450F, 178F);
				CreateLaunchPositions ("Position4", 456F, 177F);
				
				
			} 
			
			if (BearPuzzleScript.launchedCorrectly && picked == 3) {
				Move1.AddDialog(1, "Ah. Ahhhhhhhhhhhhhhhh");
				Move1.AddDialog(1, ".....................");
				Move1.AddDialog(1, "That went well. Now lets stand in the same position we launched in to pull the rock.");
				Move1.AddDialog(2, "Ok.");
				SavedVars.Instance.StartCoroutine(Move1.Talk());
				
				CreatePullPositions ("Position1", 437F, 177F);
				CreatePullPositions ("Position2", 444F, 177F);
				CreatePullPositions ("Position3", 450F, 178F);
				CreatePullPositions ("Position4", 456F, 177F);
				
				CreatePullPositions ("Position5", 381F, 178F);
				CreatePullPositions ("Position6", 376F, 180F);
				CreatePullPositions ("Position7", 374F, 184F);
				CreatePullPositions ("Position8", 373F, 189F); 
				picked = 4;
			}
		}
		
	}

	public void CreateObject(string name, float x,float y, float z)
	{
		var position = gameObject.transform.position;
		position.x = x;
		position.y = y;
		position.z = z;
		//position = new Vector3(465F, 30F, 148F); 
		var newObject = (GameObject)Instantiate (Resources.Load ("BearTrapTrigger"), position, Quaternion.identity);
		newObject.name = name;
		newObject.tag = "TriggerPosition";
		objects.Add (newObject);
		
	}
	public void CreateLaunchPositions(string name, float x, float z)
	{
		var position = gameObject.transform.position;
		position.x = x;
		position.y = 35F;
		position.z = z;
		//position = new Vector3(465F, 30F, 148F); 
		var newObject = (GameObject)Instantiate(Resources.Load("LaunchPrefab"), position, Quaternion.identity);
		newObject.name = name;
		newObject.tag = "LaunchPosition";
		
	}
	
	public void CreatePullPositions(string name, float x, float z)
	{
		var position = gameObject.transform.position;
		position.x = x;
		position.y = 35F;
		position.z = z;
		//position = new Vector3(465F, 30F, 148F); 
		var newObject = (GameObject)Instantiate(Resources.Load("PullPosition"), position, Quaternion.identity);
		newObject.name = name;
		newObject.tag = "RockPullPosition";
		
	}
	
	public void OnTriggerEnter(Collider collider)
	{
		isActive = true;
		if (gameObject.name == "BearTrapStart") {

			Move1.AddDialog(1, "Whoa! What's That?");
			Move1.AddDialog(2, "It looks like a Bear Trap.");
			Move1.AddDialog(1, "It's huge!");
			Move1.AddDialog(2, "Wouldn't want to go near that it could mean instant death. ");
			Move1.AddDialog(2, "We're going to have to find another way around it.");
			Move1.AddDialog(1, "Hmm.... I got it! How about a slingshot. ");
			Move1.AddDialog(1, "Do you see that rock above the trap? I could launch you over the trap and we");
			Move1.AddDialog(1, "could try to pull the rock down from both sides with these ropey sticks to smash the trap.");
			Move1.AddDialog(2, "Good idea. We'll need some supplies and a lot of rope. ");
			Move1.AddDialog(1, "Alright lets look for some.");
			SavedVars.Instance.StartCoroutine(Move1.Talk());

		}
	                else if (gameObject.name == "BearTrapEnd")
                {
                    SavedVars.Instance.score += 2000 - BearPuzzleScript.LevelTime *10;
                    Application.LoadLevel("FinalLevel");
                }

	}

}
