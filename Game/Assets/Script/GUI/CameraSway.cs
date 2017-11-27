using UnityEngine;
using System.Collections;

public class CameraSway : MonoBehaviour {

	public float speed;
	public float xDistance;
	public float yDistance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float xNewPos;
		float yNewPos;

		xNewPos = Mathf.Sin(Time.time * speed);
		yNewPos = Mathf.Sin(Time.time * speed);

		transform.position = new Vector3 (xNewPos,yNewPos, transform.position.z);
			
	}
}
