using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace UnityStandardAssets.Utility
{
	public class ButtonPrompt : MonoBehaviour {


		public enum Mode
		{
			ItemPopUp = 0,
			Key = 1,
			Text = 2,
			Torch= 3
		}

		public Mode action = Mode.Text;
		public int triggerCount = 1;
		public bool repeatTrigger = false;
		public GameObject eButton;
		public GameObject text = null;
		public string messageToSay;
		public AudioClip clip = null;
		public GameObject itemPopUp;
		public int KeyNo;
		public bool destroy = true;


		private bool hasBeenPressed = false;
		private GameObject gm;

		void Start ()
		{
			gm = GameObject.FindGameObjectWithTag ("GameController");
		}

		public void DoEvent()
		{
			triggerCount--;

			if (triggerCount == 0 || repeatTrigger) 
			{
				
				switch (action) 
				{
					case Mode.ItemPopUp:
						if (itemPopUp != null) {
						itemPopUp.SetActive (!itemPopUp.activeSelf);
							if (clip != null)
								gm.GetComponent<AudioSource> ().PlayOneShot (clip);
						hasBeenPressed = !hasBeenPressed;
						}
					break;
					case Mode.Key:
					hasBeenPressed = true;
						if(!gm.GetComponent<GM> ().keynumber.Contains(KeyNo))
							{
								gm.GetComponent<GM> ().keynumber.Add (KeyNo);
								if (messageToSay != null) {
									text.SetActive (false);
									text.GetComponent<Text> ().text = messageToSay;
									text.SetActive (true);
								}
								if (clip != null)
									gm.GetComponent<AudioSource> ().PlayOneShot (clip);
							}
					break;
					case Mode.Text:
					hasBeenPressed = true;
						if (messageToSay != null) {
							text.SetActive (false);
							text.GetComponent<Text> ().text = messageToSay;
							text.SetActive (true);
						}
					break;
						gm.GetComponent<AudioSource> ().PlayOneShot (clip);
						break;
				}
			}

		}
		void OnTriggerStay (Collider other)
		{
			if (other.gameObject.tag == "Player" && !eButton.activeSelf) 
			{
				eButton.SetActive (true);
			//	print ("intrigger");
			}
			else if( hasBeenPressed)
				eButton.SetActive (false);
			
			if (Input.GetKeyDown (KeyCode.E) && other.gameObject.tag == "Player" && !hasBeenPressed) 
			{

				DoEvent ();
				if (destroy) 
				{
					eButton.SetActive (false);
					Destroy (gameObject, 0.2f);
				}
			}
			else if(Input.GetKeyDown (KeyCode.Escape) && itemPopUp.activeSelf)
			{
				itemPopUp.SetActive (false);
				hasBeenPressed = false;
			}
		}

		void OnTriggerExit (Collider other)
		{
			if (other.gameObject.tag == "Player" &&  eButton.activeSelf)
			{
				eButton.SetActive (false);

				if (itemPopUp.activeSelf && itemPopUp != null) 
				{
					itemPopUp.SetActive (false);
					hasBeenPressed = false;
				}
			}
		}
	}
}
