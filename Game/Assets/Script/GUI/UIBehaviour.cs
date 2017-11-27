using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBehaviour : MonoBehaviour {

	public GameObject pauseMenu;

	private GM gm;

	void Start () 
	{
		gm = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GM> ();
	}
	

	void Update () 
	{
		if (gm.lockCursor) 
		{
			if (Input.GetKeyDown (KeyCode.Escape))
			{
				if(!gm.UIopen)
					PauseMenu (!pauseMenu.activeSelf);
			}
		}
	}

	 public void PauseMenu (bool onOff)
	{
		pauseMenu.SetActive (onOff);
		if (pauseMenu.activeSelf) 
		{
			Inventory.canOpenInventory = false;
			gm.PausePlayer ();
			Time.timeScale = 0.000001f;
			gm.ShowMouse ();
		} 
		else if(!pauseMenu.activeSelf)
		{
			if(!gm.gameObject.GetComponent<Inventory>().inventoryBag.activeInHierarchy)
				gm.HideMouse ();
			Inventory.canOpenInventory = true;
			gm.ResumePlayer ();
			Time.timeScale = 1;
		}
	}

	void OnApplicationPause(bool focus)
	{
		if (focus) 
		{
			PauseMenu (true);
		} 
	}

	public void EnanbleUIElements(GameObject target) //turns on/off ui
	{
		target.SetActive (true);
	}

	public void DisableUIElements(GameObject target) //turns on/off ui
	{
		target.SetActive (false);
	}
}
