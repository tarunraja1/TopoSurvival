using UnityEngine;
using System.Collections;

public class FallDetectionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnTriggerEnter(Collider collider)
    {
        if(collider.name == "Player1")
        {
            Move1.P1Tips = "You fell off the cliff! Be careful!";
            Move1.Death();
        }
        else if(collider.name == "Player2")
        {
            Move1.P2Tips = "You fell off the cliff! Be careful!";
            Move1.Death();
        }
    }
}
