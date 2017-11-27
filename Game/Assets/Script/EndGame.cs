using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

	public ScreenFade fade;
	public float waitTime;

	private bool triggered;
	private float timer;

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			timer += Time.deltaTime;
			print ("end");
			if(timer > waitTime)
				fade.EndScene ();

			triggered = true;

		}
	}
}
