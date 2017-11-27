using UnityEngine;
using System.Collections;

public class BatteryPickUp : MonoBehaviour {

	public int filmAdd;
	public AudioClip[] pickUpSound;
	
	private int filmAddStart;

	void Start()
	{
		filmAddStart = filmAdd;
	}
	

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			int amountDiff = other.GetComponent<CameraTake>().maxFilmAmount - other.GetComponent<CameraTake>().filmCurrentAmount;
			
			if(amountDiff < filmAdd)
				filmAdd = amountDiff;
			else 
				filmAdd = filmAddStart;
			

			if(other.GetComponent<CameraTake>().filmCurrentAmount < other.GetComponent<CameraTake>().maxFilmAmount) 
			{
				other.GetComponent<AudioSource>().PlayOneShot(pickUpSound[0]);
				other.GetComponent<CameraTake>().filmCurrentAmount += filmAdd;
				Destroy(gameObject);
			}
		}
	}
	
}
