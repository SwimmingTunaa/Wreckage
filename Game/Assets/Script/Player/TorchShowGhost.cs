using UnityEngine;
using System.Collections;

public class TorchShowGhost : MonoBehaviour {

	public GameObject ghostVisual;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Ghost")
		{
			ghostVisual.SetActive (true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Ghost") 
		{
			
			ghostVisual.SetActive (false);
		}
	}
}
