using UnityEngine;
using System.Collections;

public class CameraShakeActivate : MonoBehaviour {

	public CameraShake camShake;

	private bool triggered;
	// Use this for initialization
	IEnumerator OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player") && !triggered)
		{
			triggered = true;
			camShake.enabled = true;
			camShake.shakeDuration = 1.5f;
			yield return new WaitForSeconds (1.5f);
			camShake.enabled = false;
		}
	}
}
