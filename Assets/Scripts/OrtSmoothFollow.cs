using UnityEngine;
using System.Collections;

public class OrtSmoothFollow : MonoBehaviour {

	public Transform charTarget;
	public float smoothTime;
	private Vector3 velocity;

	// Use this for initialization
	void Start () {
		smoothTime = 0.3f;
		velocity = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 charTargetPosition = charTarget.position;
		charTargetPosition.y = transform.position.y;
		transform.position = Vector3.SmoothDamp (transform.position, charTargetPosition, ref velocity, smoothTime);
	}
}
