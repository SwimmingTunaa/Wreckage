using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour {
	
	public Image[] InventorySpaces;
	public GameObject inventoryBag;
	public Image largePic;
	public GameObject largPicParent;
	public Animator anim;
	public AudioClip openSound;
	public GameObject ui;

	private GM gm;
	private bool intOpen;
	public static bool canOpenInventory = true;

	// Use this for initialization
	void Start () 
	{
		gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>();
	
		//set the inventory sprites to transparent.
		//ResetInventory();
	}
	void Update()
	{
		if (gm.lockCursor) 
		{
			if(canOpenInventory && (Input.GetKeyDown (KeyCode.I)))
				openInventory ();
			else 
				if(intOpen && (Input.GetKeyDown (KeyCode.Escape)))
					openInventory ();
		}
	}
	public void openInventory()
	{
		inventoryBag.SetActive (!inventoryBag.activeSelf);
		anim.Play ("Inventory Open", 0, 0f);
		if (inventoryBag.activeSelf) 
		{
			gm.GetComponent<AudioSource> ().PlayOneShot (openSound);
			intOpen = true;
			gm.UIopen = true;
			gm.PausePlayer ();
			gm.ShowMouse ();
		}
		else
			if(!inventoryBag.activeSelf && !ui.GetComponent<UIBehaviour>().pauseMenu.activeInHierarchy)
			{
				intOpen = false;
				gm.UIopen = false;
				largPicParent.SetActive (false);
				gm.ResumePlayer ();
				gm.HideMouse ();
			}
	}

	public void EnlargePicture(Image imageSlot)
	{ 
		if (!largPicParent.gameObject.activeInHierarchy)
		{
			Debug.Log ("pressed");
			if (imageSlot.sprite != null)
			{
				Debug.Log ("pressed2");
				largPicParent.gameObject.SetActive (true);
				largePic.sprite = EventSystem.current.currentSelectedGameObject.GetComponent<Image> ().sprite;
			}
		}
	}

	public void ResetInventory()
	{
		Color alphaColor = new Color (255, 255, 255, 0);
		for (int i = 0; i < InventorySpaces.Length; i++) 
		{
			InventorySpaces[i].color = alphaColor;
			InventorySpaces [i].sprite = null;
		}
	}
}
