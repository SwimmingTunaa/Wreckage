using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Notes : MonoBehaviour {

	public GameObject notePlaceHolder;
	public Sprite spriteToShow;
	public AudioClip audioToPlay;

	[HideInInspector] public Animator anim;
	private GM gm;

	// Use this for initialization
	void Start () 
	{
		anim = notePlaceHolder.GetComponent<Animator>();
		gm = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GM>();
	}
	
	// Update is called once per frame
	public void ShowItem()
	{
		if(gm.GetComponent<AudioSource> () != null)
			gm.GetComponent<AudioSource> ().PlayOneShot (audioToPlay);
		gm.UIopen = true;

		if (spriteToShow)
			notePlaceHolder.GetComponent<Image> ().sprite = spriteToShow;
		else
			notePlaceHolder.GetComponent<Image> ().sprite = GetComponent<SpriteRenderer> ().sprite;
		
		if (!notePlaceHolder.activeInHierarchy)
			notePlaceHolder.SetActive (true);
		else
			anim.Play ("Notes Open");
	
	}

	public IEnumerator HideItem()
	{
		if (notePlaceHolder.activeInHierarchy)
		{
			anim.Play ("Notes Close");
			if(gm.GetComponent<AudioSource> () != null)
				gm.GetComponent<AudioSource> ().PlayOneShot (audioToPlay);
			yield return new WaitForSeconds (1f);
			gm.UIopen = false;
		}
	
	}
}
