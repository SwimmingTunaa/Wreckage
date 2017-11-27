using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRigidBody : MonoBehaviour {

	public int triggerCount = 1;
	public GameObject targetObj;
	public bool active;


	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && targetObj && targetObj.GetComponent<Rigidbody>() && triggerCount > 0)
		{
			targetObj.GetComponent<Rigidbody> ().isKinematic = active;
			triggerCount -= 1;
		}

	}
}
