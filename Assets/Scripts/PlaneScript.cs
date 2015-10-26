using UnityEngine;
using System.Collections;

public class PlaneScript : MonoBehaviour {
    private static GameObject p2;
    // Use this for initialization
    void Start () 
    {
	CreateTrigger("Trees", 127.5f, 191);
        p2 = GameObject.Find("Player2");
        p2.SetActive(false);
        Move1.AddDialog(1, "wha...what happened? ");
        Move1.AddDialog(1, "where's Steve?  I hope he's alright");
        Move1.P1Tips = "Move Bob with w,a,s and d\nMove to the next Waypoint";
        print("talk");
        StartCoroutine(Move1.Talk());
        
        
    }
	
    // Update is called once per frame
    void Update () 
    {
	
    }

    public static void StartPlayer2()
    {
        SavedVars.Instance.speed = 4;
        NamedDestroy("Trees");
        CreateTrigger("Plane", 125, 181);
        p2.SetActive(true);
        Move1.AddDialog(2, "Bob, Where are you?" );
        Move1.AddDialog(1, "Hey, i hear Steve, he's past those trees");
        Move1.P1Tips = "Move to the next waypoint";
        SavedVars.Instance.StartCoroutine(Move1.Talk());
        Move2.movable = false;
        print(SavedVars.Instance.speed.ToString());
    }

    public static void SpawnBranch()
    {
        NamedDestroy("Plane");
        Move1.AddDialog(2, "Help me, i'm stuck under some rubble");
        Move1.AddDialog(2, "Maybe you can find something to use as leverage");
        Move1.P1Tips = "Get the tree branch from the next waypoint";
        SavedVars.Instance.StartCoroutine(Move1.Talk());
        CreateTrigger("TreeBranch", 141, 158);
    }

    public static void GetBranch()
    {
        NamedDestroy("TreeBranch");
        Move1.P1Tips = "Use The Branch to get Steve out from under the rubble";
        CreateTrigger("Player2Trigger", 123, 161);
    }

    public static void ActivatePlayer2()
    {
        NamedDestroy("Player2Trigger");
        Move1.AddDialog(1, "Are you okay?");
        Move1.AddDialog(2, "Somehow we both are.");
        Move1.AddDialog(1, "Seriously right? lets find a place to set up camp so we can rest");
        Move1.P2Tips = "Move Steve with o,k,l and ;\nGo to the next waypoint";
        Move1.P1Tips = "Go to the next waypoint";
        SavedVars.Instance.StartCoroutine(Move1.Talk());
        CreateTrigger("End", 25.5f, 217.5f);
        Move2.movable = true;
    }

    public static void EndLevel()
    {
        SavedVars.Instance.score = 4000 - Time.time * 10;
        SavedVars.Instance.speed = Move1.Speed;
        SavedVars.Instance.nextLevel = "DamScene";
        Application.LoadLevel("CampScene");
    }

    private static GameObject CreateTrigger(string name, float x, float z)
    {
        return CreateGameObject("PlaneTrigger", name, x, 100, z);
    }
    
    private static void NamedDestroy(string name)
    {
        Destroy(GameObject.Find(name));
    }
    private static GameObject CreateGameObject(string resource, string name, float x, float y, float z)
    {
        var go = (GameObject)Instantiate(Resources.Load(resource));
        go.name = name;
        go.transform.position = new Vector3(x, y, z);
        return go;
    }

}

public class SavedVars : Singleton<SavedVars>
{
    protected SavedVars()
    {
        
    }
    public float speed = 8;
    public float score = 0;
    public string nextLevel = "";
    
}

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
 {
     private static T _instance;
     
     private static object _lock = new object();
     
     public static T Instance
     {
         get
         {
             if (applicationIsQuitting) {
                 Debug.LogWarning("[Singleton] Instance "+ typeof(T) +
                 " already destroyed on application quit." +
                 "Won't create again - returning null.");
                 return null;
             }
             
             lock(_lock)
             {
                 if (_instance == null)
                 {
                     _instance = (T) FindObjectOfType(typeof(T));
                     
                     if (_instance == null)
                     {
                         GameObject singleton = new GameObject();
                         _instance = singleton.AddComponent<T>();
                         singleton.name = "(singleton) "+ typeof(T).ToString();
                         
                         DontDestroyOnLoad(singleton);
                         
                         Debug.Log("[Singleton] An instance of " + typeof(T) + 
                             " is needed in the scene, so '" + singleton +
                             "' was created with DontDestroyOnLoad.");
                     } else {
                         Debug.Log("[Singleton] Using instance already created: " +
                             _instance.gameObject.name);
                     }
                 }
                 
                 return _instance;
             }
         }
     }
     
     private static bool applicationIsQuitting = false;
     /// <summary>
     /// When unity quits, it destroys objects in a random order.
     /// In principle, a Singleton is only destroyed when application quits.
     /// If any script calls Instance after it have been destroyed, 
     ///   it will create a buggy ghost object that will stay on the Editor scene
     ///   even after stopping playing the Application. Really bad!
     /// So, this was made to be sure we're not creating that buggy ghost object.
     /// </summary>
     public void OnDestroy () {
         applicationIsQuitting = true;
     }
 }


