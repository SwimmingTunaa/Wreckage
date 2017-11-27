using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class ClickControl : MonoBehaviour {

	public static string correctCode = "";
	public static string playerCode = "";
	public static int totalDigits = 0;
	public AudioClip[] clip;
	public static bool canClick = true;
	public static bool firstClick;

	// Use this for initialization
	void Start () {
		//gm = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GM>();
		Debug.Log (correctCode);
		playerCode = "";
	}
	
	// Update is called once per frame
	void Update () {
		//+StartCoroutine (codeDone ());
	}

	public void MousePressed ()
	{
		if (canClick) 
		{
			if (!firstClick)
				firstClick = true;
			GetComponentInParent<AudioSource> ().PlayOneShot (clip[0]);
			playerCode += gameObject.name;
			totalDigits += 1;
		}
	}


}
