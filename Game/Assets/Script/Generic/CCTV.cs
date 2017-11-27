using UnityEngine;
using System.Collections;

public class CCTV : MonoBehaviour {

	public Camera[] cams;
	public RenderTexture CCTVtexture;

	bool triggered = false;

	void Update () {
		if(!triggered)
			StartCoroutine (FootageChange ());
	}

	IEnumerator FootageChange()
	{
		for (int i = 0; i < cams.Length; i++) 
		{
			triggered = true;
			cams [i].gameObject.SetActive (true);
			if ((i - 1) >= 0) 
			{
				cams [i - 1].targetTexture = null;
				cams [i - 1].gameObject.SetActive (false);
			}
			cams [i].targetTexture = CCTVtexture;
			RenderTexture.active = CCTVtexture;
			cams [i].Render ();
			yield return new WaitForSeconds (5);
			RenderTexture.active = null;
			cams [i].targetTexture = null;
			if (i == cams.Length - 1) 
			{
				i = 0;
				triggered = false;
			}
		}
	}
}
