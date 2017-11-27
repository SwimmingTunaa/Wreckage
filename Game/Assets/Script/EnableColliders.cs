using UnityEngine;
using System.Collections;

public class EnableColliders : MonoBehaviour {

	public bool enable;
	public MeshCollider[] mCollider;

	private bool triggered;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player" && !triggered) 
		{
			foreach (MeshCollider m in mCollider) 
			{
				m.enabled = enable;
			}
			triggered = true;
		}
	}
}
