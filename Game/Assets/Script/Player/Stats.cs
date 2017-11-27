using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

	public float camBringUpTimer;
	public float takePicTimer;

	private bool stop;
	private bool stopTwo;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButton(1))
		{
			stop = true;
		}
		if (Input.GetMouseButtonDown (0) && Input.GetMouseButton (1)) 
		{
			stopTwo = true;
		}

		if (!stop)
			camBringUpTimer = Mathf.FloorToInt(Time.time);
		else
			Debug.Log (camBringUpTimer);
		if (!stopTwo)
			takePicTimer = Mathf.FloorToInt (Time.time);
		else
			Debug.Log (takePicTimer);
	
	}
}
