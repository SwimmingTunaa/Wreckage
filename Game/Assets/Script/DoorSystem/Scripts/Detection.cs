////////////////////////////////////
//Last edited by: Alexander Ameye //
//on: Wednesday, 11/11/2015          //
////////////////////////////////////

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Detection : MonoBehaviour
{
	// INSPECTOR SETTINGS
	[Header("Detection Settings")]
	[Tooltip("Within this radius the player is able to open/close the door")]
	public float Reach = 4F;
	[Tooltip("The tag that triggers the object to be openable")]
	public string TriggerTag = "Door";
	public AudioClip[] damLockedAudio;

	// PRIVATE SETTINGS
	[HideInInspector] public bool InReach;
	private GameObject gm;

	//DEBUGGING (DEBUG PANEL)
	public Color DebugRayColor = Color.green;
	/*[Header("Debug Settings")]

	public bool InGameDebugger = false;*/

	/*string CategoryDetection = "Detection";
	string TitleReach = "Reach";
	string TitleInReach = "InReach";

	string CategoryDoor = "Door";
	string TitleHitTag = "HitTag";
	string TitleHingeSide = "HingeSide";
	string TitleCurrentAngle = "CurrentAngle";
	string TitleSpeed = "Speed";
	string TitleTimesMoveable = "TimesMoveable";
	string TitleRunning = "Running";*/

	//START FUNCTION
	void Start()
	{
		gm = GameObject.FindGameObjectWithTag ("GameController");
	}

	//UPDATE FUNCTION
	void Update ()
	{
		// Set origin of ray to 'center of screen' and direction of ray to 'cameraview'.
		Ray ray = new Ray(transform.position, transform.forward);

		RaycastHit hit; // Variable reading information about the collider hit.

		// Cast a ray from the center of screen towards where the player is looking.
		if (Physics.Raycast (ray, out hit, Reach)) {
			//DEBUGGING (DEBUG PANEL)
			//DebugPanel.Log(TitleHitTag, CategoryDoor, hit.collider.tag);

			if (hit.collider.tag == TriggerTag) {
				InReach = true;

				if (Input.GetKeyDown (KeyCode.E)) {
					// Give the object that was hit the name 'Door'.
					GameObject Door = hit.transform.gameObject;

					// Get access to the 'DoorOpening' script attached to the door that was hit.
					Door dooropening = Door.GetComponent<Door> ();

					// Check whether the door is opening/closing or not.
					if ((dooropening.Running == false && !dooropening.locked) || (dooropening.locked && gm.GetComponent<GM> ().keynumber.Contains (dooropening.keyNumber))) {
						// Open/close the door by running the 'Open' function in the 'DoorOpening' script.
						StartCoroutine (hit.collider.GetComponent<Door> ().Open ());
						dooropening.locked = false;
						dooropening.GetComponent<AudioSource> ().PlayOneShot (dooropening.doorSounds [Random.Range (0, 2)]);
					} else if (dooropening.locked) 
					{
						dooropening.GetComponent<AudioSource> ().PlayOneShot (dooropening.doorSounds [2]);
						gm.GetComponent <AudioSource>().PlayOneShot (damLockedAudio[Random.Range (0,damLockedAudio.Length)]);
					}

					
				}
			}

			else InReach = false;

		}

		else
		{
			InReach = false;

			//DEBUGGING (DEBUG PANEL)
			//DebugPanel.Break(TitleHitTag);
		}

		/*//DEBUGGING (DEBUG PANEL)
		DebugPanel.Log(TitleInReach, CategoryDetection, InReach);
		DebugPanel.Log(TitleReach, CategoryDetection, Reach);

		if (InReach == true)
		{
			DebugPanel.Log (TitleHingeSide, CategoryDoor, hit.collider.GetComponent<Door>().HingeSide);
			DebugPanel.Log (TitleSpeed, CategoryDoor, hit.collider.GetComponent<Door> ().Speed);
			DebugPanel.Log (TitleTimesMoveable, CategoryDoor, hit.collider.GetComponent<Door> ().TimesMoveable);
			DebugPanel.Log (TitleCurrentAngle, CategoryDoor, Mathf.Round(hit.transform.eulerAngles.y) + "°");
			DebugPanel.Log (TitleRunning, CategoryDoor, hit.collider.GetComponent<Door> ().Running);
		}

		else
		{
			DebugPanel.Break (TitleHingeSide);
			DebugPanel.Break (TitleSpeed);
			DebugPanel.Break (TitleTimesMoveable);
			DebugPanel.Break (TitleCurrentAngle);
			DebugPanel.Break (TitleRunning);
		}

	*/
		// Draw the ray as a colored line for debugging purposes.
		Debug.DrawRay (ray.origin, ray.direction*Reach, DebugRayColor);
	}
}
