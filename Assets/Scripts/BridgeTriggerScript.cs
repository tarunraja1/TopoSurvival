using UnityEngine;
using System.Collections;

public class BridgeTriggerScript : MonoBehaviour {
 
    // Use this for initialization
    void Start () {
    }
	
    // Update is called once per frame
    void Update () {
    }

    public void OnTriggerStay(Collider collider)
    {
        if(gameObject.name == "bridge1" || gameObject.name == "bridge2")
        {
            if(BridgePuzzle.bridgeKeys % 2 == 0 && collider.name == "Player1")
            {
                Move1.P2Tips = "";
                Move1.P1Tips = "Press E";
  
                if(Input.GetKeyDown(KeyCode.E))
                    BridgePuzzle.P1Pressed();
                
            }
            else if (BridgePuzzle.bridgeKeys % 2 == 1 && collider.name == "Player2")
            {
                Move1.P2Tips = "Press I";
                Move1.P1Tips = "";

                if(Input.GetKeyDown(KeyCode.I))
                    BridgePuzzle.P2Pressed();
            }
            
        }
    }

    public void OnTriggerLeave(Collider collider)
    {
        if(gameObject.name ==  "bridge1" || gameObject.name == "bridge2")
        {
               Move1.P2Tips = "Get Back to the waypoints";
               Move1.P1Tips = "Get Back to the waypoints";
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        print (collider.name + " " +gameObject.name);
        if (collider.name == "Player1" || collider.name == "Player2")
        {
            switch (BridgePuzzle.triggered)
            {
            case 1:
                BridgePuzzle.CreateRopeAndAxe();
                break;
            case 2:
                if(gameObject.name == "Axe")
                    BridgePuzzle.GetAxe(collider.name);
                else if (gameObject.name == "Rope")
                    BridgePuzzle.GetRope(collider.name);
                break;
            case 3:
                break;
            case 5:
                BridgePuzzle.GameOver();
                break;
            default:
                break;
            }
                
        }
    }
}
