using UnityEngine;
using System.Collections;

public class TurnOnLight : MonoBehaviour {

	public Light[] lightObj;
	public float startDelay;
	public float delayBetween;

	private bool triggered;
	// Use this for initialization
	IEnumerator OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player" && !triggered)
		{
			yield return new WaitForSeconds (startDelay);
			foreach (Light l in lightObj) 
			{
				l.enabled = true;
				yield return new WaitForSeconds (delayBetween);
			}
			triggered = true;
		}
	}
}
