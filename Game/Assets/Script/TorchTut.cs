using UnityEngine;
using System.Collections;

public class TorchTut : MonoBehaviour {

	public GameObject eButton;
	public GameObject text = null;
	public string messageToSay;
	public AudioClip clip = null;
	public GameObject itemPopUp;



	private bool hasBeenPressed = false;
	private GameObject gm;

	void Start ()
	{
		gm = GameObject.FindGameObjectWithTag ("GameController");
	}

	void OnTriggerStay (Collider other)
	{
		if (other.gameObject.tag == "Player" && !eButton.activeSelf && !hasBeenPressed) {
			eButton.SetActive (true);
		}

		if (other.gameObject.tag == "Player" && Input.GetKeyDown (KeyCode.E) && !hasBeenPressed) {
			eButton.SetActive (false);
			if (itemPopUp != null) 
			{
				itemPopUp.SetActive (true);
				gm.GetComponent<AudioSource> ().PlayOneShot (clip);
			}
			hasBeenPressed = true;
		} 


	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == "Player" &&  eButton.activeSelf && eButton.activeSelf)
		{
			eButton.SetActive (false);
		}
	}
}
