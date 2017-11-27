using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLightColour : MonoBehaviour {

	public Color colourStart;
	public Color colourEnd;
	public List<Light> lt;
	public float duration = 5f;

	public bool triggered;
	private float time;

	void Start()
	{
		//colourStart = new Color (255/255, 240/255, 208/255); 
	}

	void Update()
	{
		if (triggered)
		{
			ChangeColour();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			triggered = true;
		}
	}
		
	void ChangeColour()
	{
		foreach(Light l in lt)
		{
			if (l.color != colourEnd)
				l.color = Color.Lerp (colourStart, colourEnd, time += Time.deltaTime / duration);
		}
	}
}
