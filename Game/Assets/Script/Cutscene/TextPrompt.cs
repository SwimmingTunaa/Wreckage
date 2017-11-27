using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextPrompt : MonoBehaviour {

	public GameObject textObj;
	public string messageToSay;
    public bool active;
	public AudioClip clip;

    private bool beenActivated;
   
	void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.tag == "Player" && !beenActivated)
        {
             if (active)
            {
                textObj.SetActive(false);
                textObj.GetComponent<Text>().text = messageToSay;
                textObj.SetActive(true);
            }
            beenActivated = true;

			if (clip != null) {
				GetComponent<AudioSource> ().PlayOneShot (clip);
			}
        }

            
	}
}
