using UnityEngine;
using System.Collections;

public class DoorSlam : MonoBehaviour {

	public GameObject[] door;
	public AudioClip closeClip;
	public AudioClip openClip;
	public float closeAngle = 0;
	public bool closeDoor = true;
	public bool lockDoors;
	public float waitForUnlockTime;

	private bool hasTriggered;

	IEnumerator OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && !hasTriggered) 
		{
			hasTriggered = true;
			for (int i = 0; i < door.Length; i++)
			{
				door [i].GetComponent<DoorScript> ().doorCloseAngle = closeAngle;
				door [i].GetComponent<DoorScript> ().smooth = 11;
				door [i].GetComponent<DoorScript> ().open = !closeDoor;
				if(closeDoor)
					door [i].GetComponent<DoorScript> ().GetComponent<AudioSource> ().PlayOneShot (closeClip);
				else
					door [i].GetComponent<DoorScript> ().GetComponent<AudioSource> ().PlayOneShot (openClip);
				if (lockDoors) 
				{
					door [i].GetComponent<DoorScript> ().key = -1;
				}
				yield return new WaitForSeconds (1);
				door [i].GetComponent<DoorScript> ().smooth = 2;

			}

			if (lockDoors) 
			{
				if (waitForUnlockTime > 0)
				{
					yield return new WaitForSeconds (waitForUnlockTime);
					for (int t = 0; t < door.Length; t++)
					{
						door [t].GetComponent<DoorScript> ().key = 0;
						if (closeDoor)
						{
							door [t].GetComponent<DoorScript> ().open = true;
						}
					}
				}
			}
			//Destroy (gameObject);
		}
	}
}
