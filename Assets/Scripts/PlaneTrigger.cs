using UnityEngine;
using System.Collections;

public class PlaneTrigger : MonoBehaviour {

	// Use this for initialization
    void Start () 
    {
	
    }
	
    // Update is called once per frame
    void Update () 
    {
	
    }

    public void OnTriggerEnter(Collider collider)
    {
        print (collider.name + " " +gameObject.name);
        if (collider.name == "Player1" || collider.name == "Player2")
        {
            switch (gameObject.name)
            {
            case "Trees":
                PlaneScript.StartPlayer2();
                break;
            case "Plane":
                PlaneScript.SpawnBranch();
                break;
            case "TreeBranch":
                PlaneScript.GetBranch();
                break;
            case "Player2Trigger":
                PlaneScript.ActivatePlayer2();
                break;
            case "End":
                PlaneScript.EndLevel();
                break;
            }
        }           
    }
}

 
