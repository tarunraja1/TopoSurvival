using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level01 : MonoBehaviour {
    public List<GameObject> waypoints, players;
    private GameObject player1, player2;
    public static GameObject bridge { get; set;  }
    public static bool riverStarted;
    // Use this for initialization
    void Start () 
    {
        waypoints = new List<GameObject>();    
        players = new List<GameObject>();
        players.Add(GameObject.Find("Player1"));
        players.Add(GameObject.Find("Player2"));
        
    }
	
    // Update is called once per frame
    void Update () 
    {
        
            var gos = GameObject.FindGameObjectsWithTag("currentObjective");    
            foreach ( var g in gos)
            {
                waypoints.Add(g);
                g.tag = "inWaypoints";
            }
            foreach (var point in waypoints)
            {
                foreach (var p in players)
                {
                    //create arrow in direction of point on each player
                    
                    var dirVector = point.transform.position - p.transform.position;
                    
                }
                
            }
	}
    public static void CreateBridge()
    {
        bridge = (GameObject)Instantiate(Resources.Load("BridgeInstance"));
        Destroy(GameObject.Find("Smoke"));
        Destroy(GameObject.Find("BridgeStart"));

    }
    public static void StartRiverPuzzle()
    {
        riverStarted = true;
        var position = new Vector3(229, 29, 150);
        var go = (GameObject)Instantiate(Resources.Load("RockTrigger"), position, Quaternion.identity);
    }
    public static void CreateDam()
    {
        Destroy(GameObject.Find("player1Rockpile"));
        Destroy(GameObject.Find("player2Rockpile"));
        Destroy(GameObject.Find("player1RockDrop"));
        Destroy(GameObject.Find("player2RockDrop"));
        var position = new Vector3(222F, 26F, 155);
        var go = (GameObject)Instantiate(Resources.Load("Rock01"), position, Quaternion.identity);
        position = new Vector3(222, 26, 159);
        go = (GameObject)Instantiate(Resources.Load("Rock01"), position, Quaternion.identity);
        position = new Vector3(217, 26, 155);
        go = (GameObject)Instantiate(Resources.Load("Rock01"), position, Quaternion.identity);
        position = new Vector3(217, 26, 160);
        go = (GameObject)Instantiate(Resources.Load("Rock01"), position, Quaternion.identity);
        position = new Vector3(215, 26, 162);
        go = (GameObject)Instantiate(Resources.Load("Rock01"), position, Quaternion.identity);
        position = new Vector3(210, 26, 159);
        go = (GameObject)Instantiate(Resources.Load("Rock01"), position, Quaternion.identity);
        position = new Vector3(210, 26, 162);
        go = (GameObject)Instantiate(Resources.Load("Rock01"), position, Quaternion.identity);
        position = new Vector3(205, 26, 162);
        go = (GameObject)Instantiate(Resources.Load("Rock01"), position, Quaternion.identity);
        position = new Vector3(208, 26, 167);
        go = (GameObject)Instantiate(Resources.Load("Rock01"), position, Quaternion.identity);
        position = new Vector3(216, 26, 159);
        go = (GameObject)Instantiate(Resources.Load("Rock01"), position, Quaternion.identity);
        position = new Vector3(216, 29, 159);
        go = (GameObject)Instantiate(Resources.Load("BridgePrefab"));
        go.transform.position = position;
        Destroy(GameObject.Find("RockTrigger(Clone)"));
    }
}



