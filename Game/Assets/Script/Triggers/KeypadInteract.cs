using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class KeypadInteract : MonoBehaviour {
	
	public GameObject[] guiTurnOn;
	public bool stopPlayer;
	public bool showMouse;
	public int keyNumber;
	public int KeyNoToAccess = 0;
	public string messageToSay;
	public Text numVisual;
	public AudioClip aClip;

	[HideInInspector] public bool done;
	private GM gm;

	void Start()
	{
		gm = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GM> ();
	}
		
	public void BringUpGUI(FirstPersonController fp)
	{
		if(!gm.keynumber.Contains (KeyNoToAccess)) // check if player has security card (Key).
		{
			if (guiTurnOn [1] != null && messageToSay !=null) 
			{
				if (!guiTurnOn [2].activeInHierarchy)
					guiTurnOn [2].SetActive (true);	
				guiTurnOn [1].SetActive (false);
				guiTurnOn [1].GetComponent<Text> ().text = messageToSay;
				guiTurnOn [1].SetActive (true);
			}
		}

		if (!guiTurnOn [0].activeInHierarchy && gm.keynumber.Contains (KeyNoToAccess) && !gm.UIopen) 
		{
			fp.enabled = false;
			gm.GetComponent<AudioSource> ().PlayOneShot (aClip);
			gm.UIopen = true;
			ClickControl.canClick = true;
			reset ();
			guiTurnOn [0].SetActive (true);
			if (showMouse)
				gm.ShowMouse ();
		} 
	}

	public void CloseGUI()
	{
		if (gm.UIopen) 
		{					
			if (guiTurnOn [0].activeSelf && gm.keynumber.Contains (KeyNoToAccess) ) 
			{
				guiTurnOn [0].SetActive (false);
				gm.UIopen = false;
				//other.GetComponent<CameraTake> ().canTakePic = false;
				if (showMouse) 
					gm.HideMouse ();
			}
		}
	}

	public void reset()
	{
		numVisual.text = "";
		ClickControl.playerCode = "";
		ClickControl.totalDigits = 0;
	}
}
