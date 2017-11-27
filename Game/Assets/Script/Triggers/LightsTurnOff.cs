using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LightsTurnOff : MonoBehaviour {

	[Header("Light On/Off")]
	public Light[] lightObjs;
	public float turnOffDelay = 0.8f;
	public AudioClip clip;
	public AudioClip scaryClip;
	public LightFlickerPulse lightPulse;
	public float soundDelay = 1;

	[Header("Switch Light Models")]
	public List<Renderer> objsToSwitch;
	public Material newMat;

	private bool hasTriggered;

	IEnumerator OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && !hasTriggered) 
		{
			hasTriggered = true;
			if(scaryClip != null)
				GetComponent<AudioSource> ().PlayOneShot (scaryClip);
			if (lightPulse != null) 
			{
				lightPulse.enabled = true;
				yield return new WaitForSeconds (soundDelay);
			}
			for (int i = 0; i < lightObjs.Length; i++) 
			{
				lightObjs [i].enabled = false;
				if(clip != null)
					lightObjs [i].GetComponent<AudioSource> ().PlayOneShot (clip);
				objsToSwitch [i].material = newMat;
				yield return new WaitForSeconds (turnOffDelay);
			}

		}

	}

	void SwitchObjects()
	{
		
	}
}
