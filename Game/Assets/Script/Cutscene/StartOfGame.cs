using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;


public class StartOfGame : MonoBehaviour {

	public Animator anim;
	public AudioClip[] audioClip;
	public GameObject player;
	public GameObject cameraVisual;
    public bool cameraLightReady;
	public bool playStartAnim;


	//private Quaternion startRot;
	// Use this for initialization
	void Awake ()
	{
		//startRot = Camera.main.transform.rotation;
	}
	IEnumerator Start () 
	{
		if (playStartAnim)
		{
			GetComponent <AudioSource> ().PlayOneShot (audioClip [0]); //where am i?
			yield return new WaitForSeconds (8);
			GetComponent <AudioSource> ().PlayOneShot (audioClip [1]);//this is the ship
			player.GetComponent<FirstPersonController> ().enabled = true;
			ReadyCamera ();
			ReadyPlayer ();
			yield return new WaitForSeconds (8);
			GetComponent <AudioSource> ().PlayOneShot (audioClip [2]);//gotta figure out where i am
			//	yield return new WaitForSeconds (1.8f);
			// cameraVisual.SetActive(true);
		}
		else
		{
			ReadyCamera ();
			ReadyPlayer ();
			yield break;
		}
	}


	void ReadyCamera()
	{
		cameraLightReady = true;
		cameraVisual.GetComponent<Animator>().enabled = true;
		cameraVisual.GetComponent<SpriteRenderer>().enabled = true;
	}
	void ReadyPlayer()
	{
		player.GetComponent<FirstPersonController>().m_RunSpeed = 4;
		player.GetComponent<FirstPersonController>().m_WalkSpeed = 3;
	}
}
