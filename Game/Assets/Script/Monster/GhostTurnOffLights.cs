using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTurnOffLights : MonoBehaviour {
	
	public bool enable;

	private Color colour;

	void OnTriggerEnter(Collider other)
	{
		if (enable &&other.gameObject.tag == "Light")
		{
			if(other.GetComponent<Light> () != null)
				other.GetComponent<Light> ().enabled = false;

			if (other.GetComponent<MeshRenderer> () != null)
			{
				colour = other.GetComponent<MeshRenderer> ().material.color;
				other.GetComponent<MeshRenderer> ().material.SetColor ("_EmissionColor", Color.black);
			}
			
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (enable && other.gameObject.tag == "Light")
		{
			if (other.GetComponent<Light> () != null)
			{
				Light l = other.gameObject.GetComponent<Light> ();

				if (!l.isActiveAndEnabled)
				{
					l.enabled = true;
				}
			}

			if (other.GetComponent<MeshRenderer> () != null)
				other.GetComponent<MeshRenderer> ().material.SetColor ("_EmissionColor", colour);
		}
	}
}
