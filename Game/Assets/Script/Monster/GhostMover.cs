using UnityEngine;
using System.Collections;

public class GhostMover : MonoBehaviour {

	public GameObject moveTo;
	public GameObject enemy;
	public GameObject objToActivate;
	public bool showGhost;
	public bool disableGhost;
	public float startDelay;
	public float returnDelay;


	// Use this for initialization
	IEnumerator OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			yield return new WaitForSeconds (startDelay);
			if (enemy != null)
			{
				enemy.GetComponent<UnityEngine.AI.NavMeshAgent> ().Warp (moveTo.transform.position);
				if (showGhost)
				{
					enemy.GetComponent<GhostAI> ().enemyInvisible = !showGhost;
					yield return new WaitForSeconds (returnDelay);
				}
				else
					enemy.GetComponent<GhostAI> ().enemyInvisible = !showGhost;
			}

			if (objToActivate != null)
				objToActivate.SetActive (true);
			
			/*	if (disableGhost) 
			{
				enemy.GetComponent<GhostAI> ().startSpeed = 0;
			}

		}*/

			//	gameObject.SetActive (false);
		}
	}
}
