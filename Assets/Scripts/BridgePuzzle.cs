using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BridgePuzzle : MonoBehaviour {
    public static int bridgeKeys  {  get; set;  }
    public static int triggered  {  get; set;  }
    private static bool axe, rope;
    private static float LevelTime { get; set; }
	// Use this for initialization
    void Start () 
    {
        CreateTrigger("Start", 167.5F, 100F, 153.5F);
        triggered = 1;
        bridgeKeys = 0;
        Move1.P1Tips = "Get to the waypoint";
        Move1.P2Tips = "Get to the waypoint";
    }
    
    // Update is called once per frame
    void Update () 
    {
        LevelTime += Time.deltaTime;
    }
    
    public static void CreateRopeAndAxe()
    {
        Destroy(GameObject.Find("Start"));
        Move1.P1Tips = "Split up and get the objects";
        Move1.P2Tips = "Split up and get the objects";
        var go = CreateTrigger("Axe", 194, 100, 53.5F);
		var goChild = CreateGameObject("Axe", "AxeMesh", 193f, 102f, 53.23f);
		goChild.transform.Rotate(52.1f, 232, 319.78f);
		goChild.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        //go.AddComponent<GameObject>((GameObject)Instantiate(Resources.Load("BridgeTrigger")));Add when get objects
        go = (GameObject)Instantiate(Resources.Load("BridgeTrigger"));
        go.name = "Rope";
        go.transform.position = new Vector3(198F, 100F, 226F);
		goChild = CreateGameObject("Rope", "RopeMesh", 198.21f, 100.44f, 228.8f);
        Move1.AddDialog(2, "Oh man, It's always something isn't it");
        Move1.AddDialog(1, "Yesterday a stream, today a giant FISSURE");
        Move1.AddDialog(1, "Lets split up. There might be another way around. We'll meet up here again.");
        SavedVars.Instance.StartCoroutine(Move1.Talk());
        triggered = 2;
    }

    public static void GetAxe(string colliderName)
    {
        axe = true;
        Destroy(GameObject.Find("Axe"));
		Destroy(GameObject.Find("AxeMesh"));
        CreateBridgePoints(colliderName);
    }
    
	public static void GetRope(string colliderName)
    {
        rope = true;
        Destroy(GameObject.Find("Rope"));
		Destroy(GameObject.Find("RopeMesh"));
        CreateBridgePoints(colliderName);
    }
    
    public static void CreateBridgePoints(string cName)
    {
        if(rope && axe && cName == "Player1")
        {
            Move1.AddDialog(1, "Is that a rope?");
            Move1.AddDialog(2, "Yeah, my route was a dead end, but i thought this would be useful");
            Move1.AddDialog(1, "So was mine, but I found an axe");
            Move1.AddDialog(2, "Nifty!");
            Move1.AddDialog(1, "We should be able to make a bridge with those trees");
            SavedVars.Instance.StartCoroutine(Move1.Talk());
            CreateTrigger("bridge1", 167, 100, 149.5F);
            CreateTrigger("bridge2", 167, 100, 156.5F);
            triggered = 3;
            Move1.P1Tips = "Get back to the chasm";
            Move1.P2Tips = "Get back to the chasm";
        } else if (rope && axe && cName == "Player2")
		{
			Move1.AddDialog(2, "Is that a rope?");
			Move1.AddDialog(1, "Yeah, my route was a dead end, but i thought this would be useful");
			Move1.AddDialog(2, "So was mine, but I found an axe");
			Move1.AddDialog(1, "Nifty!");
			Move1.AddDialog(2, "We should be able to make a bridge with those trees");
			SavedVars.Instance.StartCoroutine(Move1.Talk());
			CreateTrigger("bridge1", 167, 100, 149.5F);
			CreateTrigger("bridge2", 167, 100, 156.5F);
			triggered = 3;
                        Move1.P1Tips = "Get back to the chasm";
                        Move1.P2Tips = "Get back to the chasm";
		}
    }

    public static void P1Pressed()
    {
        bridgeKeys += 1;
        CreateBridge();
    }

    public static void P2Pressed()
    {
        bridgeKeys += 1;
        CreateBridge();
    }
    
    public static void GameOver()
    {

        SavedVars.Instance.score +=(int)( 4000 - LevelTime * 10);
        SavedVars.Instance.speed = Move1.Speed;
        SavedVars.Instance.nextLevel = "BearTrapLevel";
        Application.LoadLevel("CampScene");
    }
   
    private static void CreateBridge()
    {
        if(GameObject.Find("Smoke") == null)
            CreateGameObject("Smoke", "Smoke", 142, 100, 153);
        if (bridgeKeys > 10)
        {
            CreateGameObject("BridgeInstance", "Bridge", 142, 100, 153);
            Destroy(GameObject.Find("bridge1"));
            Destroy(GameObject.Find("bridge2"));
            Destroy(GameObject.Find("Smoke"));
            Move1.P1Tips = "Get to the waypoint";
            Move1.P2Tips = "Get to the waypoint";
            Move1.AddDialog(2, "Great idea! it worked!");
            SavedVars.Instance.StartCoroutine(Move1.Talk());
            CreateTrigger("End", 15, 100, 191);
            triggered = 5;
        }
    }
    
    private static GameObject CreateTrigger(string name, float x, float y, float z)
    {
        return CreateGameObject("BridgeTrigger", name, x, y, z);
    }

    public static GameObject CreateGameObject(string resource, string name, float x, float y, float z)
    {
        var go = (GameObject)Instantiate(Resources.Load(resource));
        go.name = name;
        go.transform.position = new Vector3(x, y, z);
        return go;
    }
}
