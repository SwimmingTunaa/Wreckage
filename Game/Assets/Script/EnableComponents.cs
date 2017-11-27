using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnableComponents : MonoBehaviour {

	public string behaviourName;
	public bool getAudioComponent;
	public GameObject[] targetObject;
	public List<AudioSource> targetAudio;
	public bool enable = true;
	public float delay;

	private bool triggered;

	IEnumerator OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player" && !triggered)
		{
			triggered = true;
			yield return new WaitForSeconds (delay);
			if (targetObject.Length != 0)
			{
				List<AudioSource> tempA = new List<AudioSource>(); 
				for (int i = 0; i < targetObject.Length; i++)
				{
					if (behaviourName != "")
					{
						Behaviour tempComp = targetObject [i].GetComponent (behaviourName) as Behaviour;
						if (tempComp.GetComponent<iTween> () != null)
						{
							tempComp.GetComponent<iTween> ().enabled = enable;
						}
						tempComp.enabled = enable;
					}
					if (getAudioComponent)
					{
						tempA.Add(targetObject[i].GetComponent<AudioSource>());
					}
					if(i == targetObject.Length - 1)
						turnOnAudio	(tempA);
				}
			}
			if (targetAudio != null)
				turnOnAudio	(targetAudio);
		}
	}

	void turnOnAudio(List<AudioSource> tempAudio)
	{
		for (int i = 0; i < tempAudio.Count; i++) 
		{
			tempAudio [i].enabled = enable;
			if(enable)
				iTween.AudioFrom (tempAudio [i].gameObject, 0.5f, 1, 3);
			else
				iTween.AudioFrom (tempAudio [i].gameObject, 0, 1, 3);
		}
	}
}
