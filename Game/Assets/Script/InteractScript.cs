using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class InteractScript : MonoBehaviour {

	public float interactDistance = 5f;
	public GameObject eButton;
	public GameObject camvisual;
	public CameraTake camTake;
	public FirstPersonController fpController;
	private GM gm;

	void Start () 
	{
		gm = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GM>();
	}

	void Update () 
	{
		if (!camTake.cameraUp)
			interact ();
	}

	void interact()
	{
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;
		//Vector3 dir = ghost.transform.position - transform.position;

		if (Physics.Raycast (ray, out hit, interactDistance))
		{
				switch (hit.collider.tag)
				{
					case "Door":
						eButton.SetActive (true);
						Door (hit.collider.transform.parent.GetComponent<DoorScript> ());
						break;
					case "PickUp":
						eButton.SetActive (true);
						if (hit.collider.transform.GetComponent<Notes> () != null)
						{
							PickupNotes (hit.collider.transform.GetComponent<Notes> ());
							break;
						}
						
						if (hit.collider.transform.GetComponent<KeypadInteract> () != null)
						{
							Keypad (hit.collider.transform.GetComponent<KeypadInteract> ());
							break;
						}
							
						break;
					default:
					//hit.collider.GetComponent<Notes> ().HideItem ();
						break;
				}
		}
		else
			eButton.SetActive (false);
		
	}

	void PickupNotes(Notes note)
	{
		if (Input.GetKeyDown (KeyCode.E) && !note.anim.GetCurrentAnimatorStateInfo(0).IsName("Notes Open"))
		{
			fpController.enabled = false;
			note.ShowItem ();
		}
		else
			if (Input.GetKeyDown (KeyCode.Escape))
			{
				fpController.enabled = true;
				if(note.anim.GetCurrentAnimatorStateInfo(0).IsName("Notes Open"))
					StartCoroutine (note.HideItem ());
			}
	}

	void Door(DoorScript door)
	{
		if (Input.GetKeyDown (KeyCode.E))
		{
			if (gm.keynumber.Contains (door.key))
				door.ChangeDoorState ();
			else
				door.GetComponent<AudioSource> ().PlayOneShot (door.lockedClip);
		}
	}

	void Keypad(KeypadInteract keypad)
	{
		if (Input.GetKeyDown (KeyCode.E) && !keypad.done)
		{
			keypad.BringUpGUI(fpController);
		}
		else
			if (Input.GetKeyUp (KeyCode.Escape))
			{
				fpController.enabled = true;
				keypad.CloseGUI();
			}
	}

	IEnumerator autoTakePic()
	{
		camTake.GetComponent<FirstPersonController> ().enabled = false;
		StartCoroutine(camTake.CameraUpAuto ());
		yield return new WaitForSeconds (0.75f);
		StartCoroutine( camTake.TakePitureAction ());
		yield return new WaitForSeconds (0.1f);
		camTake.CameraDown ();
		camTake.GetComponent<FirstPersonController> ().enabled = true;
		yield return new WaitForSeconds (4.8f);
	}
}
