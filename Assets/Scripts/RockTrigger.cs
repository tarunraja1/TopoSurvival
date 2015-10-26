using UnityEngine;
using System.Collections;

public class RockTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider collider)
    {
        print(gameObject.name);
        if(collider.name == "Player1" || collider.name == "Player2")
        {
            switch (gameObject.name)
            {
            case "RiverStart":
                RockPuzzle.CreatePickups();
                break;
            case "P1Pickup":
                if(collider.name == "Player1")
                    RockPuzzle.P1Pickup(collider.name);
                break;
            case "P1Drop":
                if(collider.name == "Player1")
                    RockPuzzle.P1Drop(collider.name);
                break;
            case "P2Pickup":
                if(collider.name == "Player2")
                    RockPuzzle.P2Pickup(collider.name);
                break;
            case "P2Drop":
                if(collider.name == "Player2")
                    RockPuzzle.P2Drop(collider.name);
                break;
            case "GameOver":
                RockPuzzle.GameOver();
                break;
            }
        }           
    }
}

