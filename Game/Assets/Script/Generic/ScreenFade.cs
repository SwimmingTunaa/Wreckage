using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ScreenFade : MonoBehaviour
{
	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
	public bool doLoadlevel;
	public GameObject black;
	
	private bool sceneStarting = true;      // Whether or not the scene is still fading in.

	void Awake ()
	{
		if(!black.activeSelf)
			black.SetActive (true);
	}
	void Update ()
	{
		// If the scene is starting...
		if(sceneStarting)
			// ... call the StartScene function.
			StartScene();
	}
	
	
	public void FadeToClear ()
	{
		// Lerp the colour of the texture between itself and transparent.
		black.GetComponent<Image>().color = Color.Lerp(black.GetComponent<Image>().color, Color.clear, fadeSpeed * Time.deltaTime);
	}
	
	
	public void FadeToBlack (float speed)
	{

		black.GetComponent<Image>().enabled = true;
		// Lerp the colour of the texture between itself and black.
		black.GetComponent<Image>().color = Color.Lerp(black.GetComponent<Image>().color, Color.black, speed * Time.deltaTime);
	}
	
	
	void StartScene ()
	{
		
		// Fade the texture to clear.
		FadeToClear();
		
		// If the texture is almost clear...
		if(black.GetComponent<Image>().color.a <= 0.05f)
		{
			// ... set the colour to clear and disable the GUITexture.
			black.GetComponent<Image>().color = Color.clear;
			black.GetComponent<Image>().enabled = false;
			
			// The scene is no longer starting.
			sceneStarting = false;
		}
	}
	
	
	public void EndScene ()
	{
		// Make sure the texture is enabled.
		black.GetComponent<Image>().enabled = true;
		
		// Start fading towards black.
		FadeToBlack(fadeSpeed);
		
		// If the screen is almost black...
		if(black.GetComponent<Image>().color.a > 0.95f && doLoadlevel)
			// ... reload the level.
			SceneManager.LoadScene (1);
	}
}