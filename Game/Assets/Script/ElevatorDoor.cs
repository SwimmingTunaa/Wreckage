using UnityEngine;
using System.Collections;

public class ElevatorDoor : MonoBehaviour {
	
	public bool open;
	public Animator doorAnim;
	public GameObject[] goToTurnOn;
	public GameObject otherElevator;
	public AudioSource audioSource;
	public AudioClip door;
	public AudioClip ding;
	public AudioClip lightBreak;
	public LightFlickerPulse lfp;
	public float waitTime;
	public float lightBreakWaitTime;
	public Transform posToMove;

	private bool triggered;
	// Use this for initialization
	void Start ()
	{
		doorAnim.SetBool ("Open", open);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//doorAnim.SetBool ("Open", open);
	}

	IEnumerator OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Player") && !triggered) 
		{
			triggered = true;
			audioSource.PlayOneShot (door);
			doorAnim.SetBool ("Open", false);
			audioSource.Play ();
			yield return new WaitForSeconds (waitTime);//wait around for a bit then turn on light pulse
			lfp.enabled = true;
			goToTurnOn [0].SetActive (true);
			yield return new WaitForSeconds (lightBreakWaitTime);//wait for till light break
			lfp.GetComponent<AudioSource>().PlayOneShot (lightBreak);//lightbreak
			yield return new WaitForSeconds (1);
			goToTurnOn [0].SetActive (false);
			lfp.gameObject.SetActive (false);
			yield return new WaitForSeconds (4);
			goToTurnOn [2].SetActive (true);
			goToTurnOn [1].SetActive (true);//sound turn on
			yield return new WaitForSeconds(20);
			other.transform.position = posToMove.position;
			goToTurnOn [4].SetActive (true);
			goToTurnOn [3].SetActive (true);
			otherElevator.GetComponentInChildren<Animator> ().SetBool ("Open", true);
			otherElevator.GetComponentInChildren<AudioSource> ().PlayOneShot (ding);
		}
			
	}

}
