using UnityEngine;
using System.Collections;

public class ShowObject : MonoBehaviour 
{ 
	public GameObject target;
	private CameraTake camTake;
	// Use this for initialization
	void Start () {
		camTake = GameObject.FindGameObjectWithTag ("Player").GetComponent<CameraTake> ();
	}
	
	// Update is called once per frame
	void Update () {
		target.SetActive (camTake.cameraUp);
	}
}
