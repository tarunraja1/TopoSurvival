using UnityEngine;
using System.Collections;

public class RockPuzzle : MonoBehaviour {
    private static int triggered;
    private static bool damCreated;
    private static string pu1, pu2;
    private static float LevelTime { get; set; }
	// Use this for initialization
    void Start () 
    {
        CreateTrigger("RiverStart", 122, 100, 147);
        triggered = 0;
        damCreated = false;
        Move1.P1Tips = "Move To the river";
        Move1.P2Tips = "Move To the river";
    }
    
    // Update is called once per frame
    void Update () 
    {
        if(damCreated && Find("DownStream").transform.position.y > 97.5f)
            LowerRiver();
        LevelTime += Time.deltaTime;
    }

    public static void CreatePickups()
    {
        if (triggered == 0)
        {
            Move1.AddDialog(1, "Great, we've got a giant stream in our way");
            Move1.AddDialog(2, "We need to get across. These boulders should be able to create a path");
            Move1.AddDialog(1, "Good idea. We'll need to be quick if we want to beat the stream");
            Move1.P1Tips = "Move to the Red Waypoint";
            Move1.P2Tips = "Move to the Blue Waypoint";
            SavedVars.Instance.StartCoroutine(Move1.Talk());
        }
        DestroyNamed("RiverStart");
        print(triggered);
        if (triggered %2 == 0)
        {    
            switch (triggered)    
            {
				//		   pos			rot 		scale
            case 2:
                CreateRock(116.1F, 99.33F, 157.89F, 0, 0, 0, 2.2518F, 1.608F, 1.943F);
                break;
            case 4:
                CreateRock(109.76F, 99.23f, 159, 36.6f, 168.17F, 150.18F, 1.8f, 1.282f, 1.55f);
                break;
            case 6:
                CreateRock(112.42f, 99.05F, 158, 0, 90, 0, 1.89f, 1.352f, 1.63f);
                break;
            case 8:
                CreateRock(106.6f, 99.5f, 159.3f, 312.3f, 240.43f, 293.81f, 1.579f, 1.128f, -1.363f);
                break;
            case 10:
                CreateRock(104.39f, 99.46f, 159.3738f, 322.24f, 170.59f, 335.38f, 1.668f, 1.191f, 1.44f);
                break;
            case 12:
                CreateRock(101.46f, 99.28f, 159.92f, 307.551f, 124.335f, 221.14f, 1.831f, 1.275f, 1.541f);
                damCreated = true;
                break;
            }
        
            if(triggered < 12)
            {  
                var t1 = CreateTrigger("P1Pickup", 122.5F, 100, 184);
                var t2 = CreateTrigger("P2Pickup", 137.5F, 100, 223);
                Move1.P1Tips = "Pickup rock at the red waypoint";
                Move1.P2Tips = "Pickup rock at the blue waypoint";
                t1.GetComponent<MeshRenderer>().material.color = Color.red;
                t1.transform.Find("Cylinder").GetComponent<MeshRenderer>().material.color = Color.red;
                t2.GetComponent<MeshRenderer>().material.color = Color.blue;
                t2.transform.Find("Cylinder").GetComponent<MeshRenderer>().material.color = Color.blue;
            }
        }   
    }

    public static void P1Pickup(string name)
    {
        DestroyNamed("P1Pickup");
        pu1 = name;
        Move1.P1Tips = "Drop the rock at the red waypoint";
        var trigger = CreateTrigger("P1Drop", 121.5F, 100, 164);
        trigger.GetComponent<MeshRenderer>().material.color = Color.red;
        trigger.transform.Find("Cylinder").GetComponent<MeshRenderer>().material.color = Color.red;
        
    }

    public static void P1Drop(string name)
    {
        if(pu1 == name)
        {
            DestroyNamed("P1Drop");
            Move1.P1Tips = "";
            triggered +=1;
            if( !Exists("P2Drop"))
                CreatePickups();
        }
    }
    
    public static void P2Pickup(string name)
    {
        pu2 = name;
        Move1.P2Tips = "Drop the rock at the blue waypoint";
        DestroyNamed("P2Pickup");
        var trigger = CreateTrigger("P2Drop", 126.5F, 100, 209);
        trigger.GetComponent<MeshRenderer>().material.color = Color.blue;
        trigger.transform.Find("Cylinder").GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    public static void P2Drop(string name)
    {
        if (pu2 == name)
        {
            DestroyNamed("P2Drop");
            triggered +=1;
            if(!Exists("P1Drop"))
                CreatePickups();
        }
    }
    
    private static void LowerRiver()
    {
        var go = Find("DownStream");
        var pos = go.transform.position;
        go.transform.position = new Vector3(pos.x, pos.y - Time.deltaTime / 5, pos.z);
        if(GameObject.Find("GameOver") == null)
            CreateTrigger("GameOver", 7.5f, 100, 70);
    }

    public static void GameOver()
    {
        
        SavedVars.Instance.score += (int) (4000 - LevelTime * 10);
        SavedVars.Instance.speed = Move1.Speed;
        SavedVars.Instance.nextLevel = "BridgeScene";
        Application.LoadLevel("CampScene");
    }

    private static GameObject Find(string name)
    {
        return GameObject.Find(name);
    }
    
    private static bool Exists(string name)
    {
        return Find(name) != null;
    }

    private static void DestroyNamed(string name)
    {
        Destroy(GameObject.Find(name));
    }

    private static GameObject CreateTrigger(string name, float x, float y, float z)
    {
        return CreateObject("RockTrigger", name, x, y, z);
    }

    private static GameObject CreateObject(string resource, string name, float x, float y, float z)
    {
        var go = (GameObject)Instantiate(Resources.Load(resource));
        go.transform.position = new Vector3(x, y, z);
        go.name = name;
        return go;
    }

	private static GameObject CreateRock(float x, float y, float z, float xRot, float yRot, float zRot, float sx, float sy, float sz)
    {
        var go = CreateObject("Rock05", "Rock", x, y, z);
        go.transform.Rotate(xRot, yRot, zRot);
        go.transform.localScale = new Vector3(sx, sy, sz);
        return go;
    }

    
}



