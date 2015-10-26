using UnityEngine;
using System.Collections;

public class endingcamerascript : MonoBehaviour {

	public int Speed{ get; set; }
	public float LevelTime{ get; set; }

	// Use this for initialization
	void Start () {
	
		LevelTime = 0;
		Speed = 5;

	}
	
	// Update is called once per frame
	void Update () {
	
		LevelTime += Time.deltaTime;

		if ((int)LevelTime < 14)
		{
			transform.Translate(Vector3.forward * Time.deltaTime * 7);
		}

		if ((int)LevelTime >= 14)
		{
			transform.Rotate(Vector3.down * Time.deltaTime * 3);
			transform.Translate(Vector3.right * Time.deltaTime * 10);
		}
	}
}
