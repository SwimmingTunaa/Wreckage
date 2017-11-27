using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObject : MonoBehaviour {

	public bool enableHighlight = true;
	public Color colour; // highlight colour
	public float colourIntensity = 1f;
	public float frequency; 


	private Material mat;
	private Color startColour;

	void Start()
	{
		mat = GetComponent<Renderer> ().material;
		startColour = mat.color;
	}
	// Update is called once per frame
	void Update ()
	{
		HighlightColour ();
	}

	void HighlightColour()
	{
		if(enableHighlight)
			mat.SetColor("_EmissionColor", Color.Lerp(Color.black, colour * colourIntensity, Mathf.Sin(Time.time * frequency)));
	}

	public void StopHighlight()
	{
		if(!enableHighlight)
			mat.color = startColour;
	}

}
