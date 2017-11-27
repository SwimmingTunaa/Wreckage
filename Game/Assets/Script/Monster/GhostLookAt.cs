using UnityEngine;
using System.Collections;

public class GhostLookAt : MonoBehaviour {

	public bool LookAtOn;
	public Transform target;
	public GameObject visual;

	private CameraTake player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<CameraTake>();
	}
	
	// Update is called once per frame
	void Update () {
		if(LookAtOn)
			transform.LookAt(target);
		if(visual !=null)
			visual.SetActive (player.cameraUp);
	}
}
