using UnityEngine;
using System.Collections;

public class RegisterGO : MonoBehaviour {

	public bool PictureTaken;
	public bool disable;
	// Use this for initialization
	void Awake ()
	{
		MyRegister.Register (this.gameObject);
		if (disable)
			this.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void OnDestroy () 
	{
		MyRegister.Unregister (this.gameObject);
	}
}
