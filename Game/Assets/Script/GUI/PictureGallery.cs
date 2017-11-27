using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PictureGallery : MonoBehaviour {

	public GM gm;
	public Image image;

	private int index = 0;
	private bool firstPic;

	void LateUpdate () 
	{
		if (gm.captureList.Count >= 1 && !firstPic) 
		{
			image.sprite = gm.captureList [0];
			firstPic = true;
		}
	}
	
	public 	void NextPicture()
	{
		if ((index + 1) < gm.captureList.Count)
		{
			index++;
			image.sprite = gm.captureList [index];
		}
	}

	public void PreviousPicture()
	{
		if ((index - 1) >= 0)
		{
			index--;
			image.sprite = gm.captureList [index];
		}

	}
}
