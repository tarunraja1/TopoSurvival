using UnityEngine;
using System.Collections;

public class ETrigger : MonoBehaviour {
	public ArrayList targets;
	// Use this for initialization
	void Start () {
		targets = new ArrayList ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if (targets.Count == 2) {
			GUI.Box(new Rect(10, 10, 200, 40), "......!");
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			GameObject plObject = other.gameObject;
			if(!targets.Contains(plObject)){
				targets.Add(plObject);
				Debug.Log("Enter "+ targets.Count);
			}
		}
	}
	void OnTriggerExit(Collider other){
		if (other.CompareTag("Player")) {
			GameObject plObject = other.gameObject;
			targets.Remove(plObject);
			Debug.Log("Exit");
		}
	}

}
