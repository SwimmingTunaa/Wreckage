using UnityEngine;
using System.Collections;

public class PromptAudio : MonoBehaviour {

	public float triggerCount = 1;
	public AudioClip clipToPlay;
	public AudioSource audioSource;
	public float delay = 0;

	private GameObject gm;

	void Start()
	{
		gm = GameObject.FindGameObjectWithTag ("GameController");
	}
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player" && triggerCount > 0) 
		{
			if (audioSource == null)
				gm.GetComponent <AudioSource> ().PlayOneShot (clipToPlay);
			else
				audioSource.PlayOneShot (clipToPlay);
			triggerCount -= 1;
			Destroy (gameObject, delay);

		}
	}
}
