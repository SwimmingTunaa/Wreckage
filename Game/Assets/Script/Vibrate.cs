using UnityEngine;
using System.Collections;

public class Vibrate : MonoBehaviour {

	public float minForce;
	public float maxForce;
	public bool vibrate = true;

	// Use this for initialization
	void Start () 
	{
		if (vibrate) 
		{
			iTween.ShakeRotation (gameObject, iTween.Hash ("x", 3, "y", 1.5, "z", 3, "loopType", "loop", "time", 0.2f));
		
		}
		//iTween.ShakePosition (gameObject, iTween.Hash ("x", 0.01,  "z", 0.01, "loopType", "loop", "time", 0.2f));

	}

	// Update is called once per frame
	void LateUpdate () {
		GetComponent<Rigidbody>().AddRelativeForce(new Vector3(Random.Range(minForce, maxForce),0, Random.Range(minForce, maxForce)),ForceMode.Force);
	}
}
