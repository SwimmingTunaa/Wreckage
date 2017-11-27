using UnityEngine;
using System.Collections;

public class Stun : MonoBehaviour {

	public float stunTime = 3;
	public GameObject ghostVisual;
	public  GameObject interact;

	[HideInInspector] public UnityEngine.AI.NavMeshAgent navEnemy;
	[HideInInspector] public GhostMovement gMove;

	private float startSpeed;
	//private GameObject player;
	private float currentTime;

	// Use this for initialization
	void Start()
	{
		navEnemy = GameObject.FindGameObjectWithTag("Ghost").GetComponent<UnityEngine.AI.NavMeshAgent>();
		gMove = GameObject.FindGameObjectWithTag("Ghost").GetComponent<GhostMovement>();
		//player = GameObject.FindGameObjectWithTag ("Player");
	}

	/*void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Ghost") )
		{
			
			//RaycastHit hit;
			//Vector3 direction =  ghostVisual.transform.parent.position - player.transform.position;	
			if (gMove.currentTime <= 0)
			{
				navEnemy.speed = 0;
				gMove.currentTime = stunTime;
				//gMove.ghostHide = false;
				navEnemy.GetComponent<AudioSource> ().Stop ();
				navEnemy.GetComponent<AudioSource> ().PlayOneShot (gMove.audios [0]);
			}
		}
	}*/
}
