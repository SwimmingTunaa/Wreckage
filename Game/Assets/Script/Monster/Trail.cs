using UnityEngine;
using System.Collections;

public class Trail : MonoBehaviour {

	public GameObject trailOBj;
	public float interval;

	private float timer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{

		timer += Time.deltaTime;
		if (timer >= interval)
		{
			Instantiate (trailOBj, transform.position + Vector3.down / 2, transform.rotation);
			timer = 0;
		}
	}
}
