using UnityEngine;
using System.Collections;

public class GhostStartFollow : MonoBehaviour {

	// Use this for initialization

	public GameObject postionToMove;
	public AudioSource ghostAudio;
	public GameObject[] turnOffThis;
	public AudioClip audioToPlay;
	public AudioSource source;
	public float setSpeed = 2;

	private GameObject ghost;
	public GameObject ghostVisual;

	void Start () {
		ghost = GameObject.FindGameObjectWithTag ("Ghost");
	//	ghostVisual =  GameObject.FindGameObjectWithTag ("GhostVisual");
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			other.GetComponent <PlayerHealth>().chillEffIsOn = true;

			if(ghostAudio !=null)
				ghostAudio.enabled = true;
			if (postionToMove != null)
				ghost.GetComponent<UnityEngine.AI.NavMeshAgent> ().Warp (postionToMove.transform.position);
			ghost.SetActive (true);
			ghost.GetComponent<GhostAI> ().enemyInvisible = true;
			if(!ghostVisual.GetComponent<Animator> ().enabled)
				ghostVisual.GetComponent<Animator> ().enabled = true;
			for (int i = 0; i < turnOffThis.Length; i++)
				turnOffThis[i].SetActive (false);
			if (audioToPlay != null) 
			{
				source.clip = audioToPlay;
				source.Play ();
			}
			Destroy (gameObject);

		}
	}
}
