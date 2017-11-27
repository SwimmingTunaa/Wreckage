using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour {

	public float duration;
	public bool active;

	[Header("CHANGE BLEND MATERIAL")]
	public Renderer rend;
	private float blendSpeed;

	[Header("CHANGE EMISSION COLOUR")]
	public bool On;
	public Color colourStart;
	public Color colourEnd;
	public Renderer mat;

	private bool triggered;

	// Use this for initialization
	void Start () 
	{
		if (GetComponent<Renderer>() != null)
		{
			rend.sharedMaterial.shader = rend.GetComponent<Renderer> ().sharedMaterial.shader;
			rend.sharedMaterial.SetFloat ("_Blend", 0);
			mat.sharedMaterial.color = colourStart;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (triggered || active)
		{
			ChangeTextureBlend ();
			if (On)
				ChangeEmissionColour ();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			triggered = true;
		}
	}

	void ChangeTextureBlend()
	{
		if (rend != null && rend.sharedMaterial.GetFloat ("_Blend") <= 1)
			rend.sharedMaterial.SetFloat ("_Blend", Mathf.Lerp(0, 1, blendSpeed += Time.deltaTime/duration));
		else
			if (triggered && rend.sharedMaterial.GetFloat ("_Blend") >= 1)
				this.gameObject.SetActive(false);
	}

	void ChangeEmissionColour()
	{
		mat.sharedMaterial.SetColor ("_EmissionColor", Color.Lerp(colourStart , colourEnd, blendSpeed += Time.deltaTime/duration));
	}
}
