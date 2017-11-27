using UnityEngine;
using System.Collections;

public class FrostScreen : MonoBehaviour {

	public float frostRange;
	public AudioClip frostAudio;

	private GameObject ghost;
	private FrostEffect frostEffect;
	private bool audioPlayed;

	void Start () {
		frostEffect = GetComponentInChildren<FrostEffect>();
		ghost = GameObject.FindGameObjectWithTag ("Ghost");
	}
	
	// Update is called once per frame
	void Update () {
		float dist = Vector3.Distance(ghost.transform.position,transform.position);

		if (dist <= frostRange) {
			frostEffect.FrostAmount = Mathf.Clamp (2/dist, 0, 0.45f);
			if (!audioPlayed) {
				audioPlayed = true;
				if (!GetComponent <AudioSource> ().isPlaying)
					GetComponent <AudioSource> ().PlayOneShot (frostAudio);
			}
		}
		else 
		{
			frostEffect.FrostAmount = 0;
			audioPlayed = false;
		}
	}
}
