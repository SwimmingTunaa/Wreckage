using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class WrongCode : MonoBehaviour 
{

	public KeypadInteract[] actTrigger;
	public AudioClip[] clip;


	private GM gm;
	private bool done;
	public Text wrongTexts;

	// Use this for initialization
	void Start () {
		gm = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GM>();
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine(codeDone());

	}

	IEnumerator codeDone()
	{
		if (ClickControl.totalDigits == 4 && !done) 
		{
			if (ClickControl.playerCode == ClickControl.correctCode) 
			{
				done = true;
				for(int i = 0; i < actTrigger.Length; i++)
					actTrigger[i].done = true;
				GetComponentInParent<Animator> ().Play ("CorrectCode");
				GetComponentInParent<AudioSource> ().PlayOneShot (clip[1]);
				GameObject player = GameObject.FindGameObjectWithTag ("Player");
				player.GetComponent<FirstPersonController> ().enabled = true;
				yield return new WaitForSeconds (1);
				for(int i = 0; i < actTrigger.Length; i++)
					gm.keynumber.Add(actTrigger[i].keyNumber);
				GetComponentInParent<Animator> ().gameObject.SetActive (false);
				gm.HideMouse ();

			} else if(ClickControl.canClick)
			{
				ClickControl.canClick = false;
				GetComponentInParent<Animator> ().Play ("WrongCode");
				GetComponentInParent<AudioSource> ().PlayOneShot (clip[2]);
				wrongTexts.text += ClickControl.playerCode + " ";
				yield return new WaitForSeconds (1.5f);
				ClickControl.playerCode = "";
				ClickControl.totalDigits = 0;
				ClickControl.canClick = true;
			}
		}
	}
}
