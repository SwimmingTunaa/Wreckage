using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothLookAt : MonoBehaviour {

	public bool enable;
	public float damping;
	public Transform target;


	void LateUpdate () 
	{
		if (enable)
		{
			Quaternion rot = Quaternion.LookRotation (target.position - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, rot, Time.deltaTime * damping);
		}
	}
}
