using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FilmCounter : MonoBehaviour {

	public CameraTake camTake;

	void Update () 
	{
		if(camTake.filmCurrentAmount < 10)
			GetComponent<Text> ().text = "0" + camTake.filmCurrentAmount.ToString ();
		else
			GetComponent<Text> ().text = camTake.filmCurrentAmount.ToString ();
		GetComponent<Animator> ().SetFloat ("Count", camTake.filmCurrentAmount);
	}


}
