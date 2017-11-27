using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {

	public Transform target;
	public float speed;
	
	// Update is called once per frame
	void Update () 
	{
		if (target != null)
			transform.RotateAround (target.position, Vector3.up, speed * Time.deltaTime);
	}
}
