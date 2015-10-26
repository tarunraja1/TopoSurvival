using UnityEngine;
using System.Collections;

public class RiverCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "Player1" || collider.name == "Player2")
        {
            if (collider.name == "Player1")
                Move1.P1Tips = "You died! Don't fall in the water!";
            else
                Move1.P2Tips = "You died! Don't fall in the water!";
            Move1.Death();       
        }
    }
}
