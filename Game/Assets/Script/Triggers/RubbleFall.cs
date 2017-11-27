using UnityEngine;
using System.Collections;

public class RubbleFall : MonoBehaviour {

	public GameObject rubbleOBJ;
	public AudioClip audioClip;
	public GameObject otherTriggerToDestroy;
	public int chanceOfActivating;

	private bool beenTriggered;
	private GameObject gm;

	// Use this for initialization
	void Start () 
	{
		beenTriggered = false;
		gm = GameObject.FindGameObjectWithTag ("GameController");
	}
		
	void OnTriggerEnter(Collider other)
	{
		int randomNumber = Random.Range (1, 100);
		if (other.gameObject.tag == "Player" && !rubbleOBJ.activeSelf && !beenTriggered && randomNumber <= chanceOfActivating) {
			if (audioClip != null)
				gm.GetComponent<AudioSource> ().PlayOneShot (audioClip);
			if (rubbleOBJ != null)
				rubbleOBJ.SetActive (true);
			if (otherTriggerToDestroy != null)
				Destroy (otherTriggerToDestroy);
			CameraShake.shakeActive = true;
			other.GetComponent<CameraShake> ().shakeDuration = 1.5f;
			beenTriggered = true;
			Destroy (gameObject);
		} else 
		{
			Destroy (gameObject);
			if(otherTriggerToDestroy != null)
				Destroy (otherTriggerToDestroy);
		}
	}


}
