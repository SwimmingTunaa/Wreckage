using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.AI;

public class GhostAI : GhostMovement {

	public SkinnedMeshRenderer eRenderer;
	public GameObject targetObject;


	public int attackDmg = 1;
	public float attackRange = 1f;

	public float minRange, maxRange;
	public float wanderTime;
	public float wanderRadius;

	public bool enemyInvisible;
	public AudioClip[] footsteps;
	public GameObject monster;
	public AudioClip[] audioClips;
	public AudioSource music;


	private Animator anim;
	private bool updatePos;
	private float startSpeed;
	[HideInInspector] public bool checkOutLoc;
	[HideInInspector] public UnityEngine.AI.NavMeshAgent navAgent;
	[HideInInspector] public bool isAttacking;
	[HideInInspector] public bool playAnim = true;
	[HideInInspector] public bool ghostVisible;
	[HideInInspector] public float tempTime; // stun timer;
	bool audioPlayed = false;
	PlayerHealth playerHp;
	float dist;

	// Use this for initialization
	void Start () 
	{
		navAgent = GetComponent<NavMeshAgent> ();
		anim = GameObject.FindGameObjectWithTag("GhostVisual").GetComponent<Animator> (); 
	
		startSpeed = navAgent.speed;
		playerHp = targetObject.GetComponent<PlayerHealth> (); 
		//InvokeRepeating ("Footsteps", 0, 1.2f)
	}

	void Update()
	{
		
		startRandomPos (minRange, maxRange, targetObject);
		/*Movement ();
		Stun ();
		StartCoroutine (Attack ());*/
		if (ghostVisible || targetObject.GetComponent<CameraTake> ().cameraUp)
			eRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
		else
			eRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
		anim.SetFloat ("Vspeed", navAgent.velocity.magnitude);
		UpdatePlayerPos (transform);
	}

	void LateUpdate()
	{
		dist = Vector3.Distance (gameObject.transform.position, playerHp.transform.position);
		//Debug.DrawRay (transform.position, transform.forward * 20, Color.green);
		anim.SetFloat ("Distance", dist);

	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player" && other.GetComponent<CameraTake> ().cameraUp && other.GetComponent<CameraTake> ().cameraPressed)
		{
			if (CaclulatePathLength (transform.position) < GetComponent<SphereCollider> ().radius)
			{
				checkOutLoc = true;
			}
		}
	}

	public void Seek()
	{
		MoveToTargetObject (GM.lastPlayerSighting, startSpeed + 1, navAgent);
	}

	public void Movement()
	{
		if (playAnim)
		{
			Scream ();
			playAnim = false;
		}
		if (!playAnim)
		{
			MoveToTargetObject (GM.lastPlayerSighting, startSpeed + 2f, navAgent);
			updatePos = true;
		}
	}

	public void Predict() // predict movement after losing sight of player
	{
		navAgent.speed = startSpeed + 2;
		StartCoroutine (UpdatePlayerPosDelayed (1));
	}

	public void Wander()
	{
		navAgent.speed = startSpeed;
		Wander (wanderTime, wanderRadius, navAgent);		
	}

	public void Scream()
	{
			anim.SetTrigger ("Scream");
	}

	IEnumerator UpdatePlayerPosDelayed(float delayTime)
	{
		yield return new WaitForSeconds (delayTime);
		MoveToTargetObject (UpdatePlayerPos (playerHp.gameObject.transform),startSpeed + 2f, navAgent);
	}

	public void Stun()
	{
		checkOutLoc = true;
		GM.lastPlayerSighting = targetObject.transform.position;
		tempTime -= Time.deltaTime;
		StopMove(navAgent);
		anim.SetBool ("IsDamaged", true);
		if (!audioPlayed) 
		{
			GetComponent<AudioSource> ().Stop ();
			print ("stunned_played");
			audioPlayed = true;
			GetComponent<AudioSource> ().PlayOneShot (audioClips [0]);
		}
	}

	public void RecoverFromStun() 
	{
		audioPlayed = false;
		anim.SetBool ("IsDamaged", false);
		StartMove (navAgent, startSpeed);
	}

	public IEnumerator Attack ()
	{
		// checks if stunned, how close and if player is alive before attacking
			print("atacking");	
			isAttacking = true;
			playerHp.TakeDamage (attackDmg);
			StopMove (navAgent);
			GetComponent<AudioSource> ().PlayOneShot (audioClips [1]);
			anim.SetTrigger ("Attack");
			playerHp.GetComponentInChildren<SmoothLookAt> ().enable = true; // make player turn to face monster
			//lookAt.enabled = true;
			ghostVisible = true;
			music.Stop ();
			yield return new WaitForSeconds (2f);
			playerHp.GetComponentInChildren<SmoothLookAt> ().enable = false;
			navAgent.Warp(playerHp.gameObject.transform.transform.position + gameObject.transform.forward *0.5f );
			playerHp.GetComponentInChildren<Animator> ().Play ("Fall");

	}
		
	public float CaclulatePathLength(Vector3 targetPosition)
	{
		NavMeshPath path = new NavMeshPath ();

		if (navAgent.enabled)
			navAgent.CalculatePath (targetPosition, path);

		Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];// +2 to include enemy and player position

		allWayPoints [0] = transform.position;
		allWayPoints [allWayPoints.Length - 1] = targetPosition;

		for (int i = 0; i < path.corners.Length; i++)
		{
			allWayPoints [i + 1] = path.corners [i];
		}

		float pathLength = 0f;

		for (int i = 0; i < allWayPoints.Length - 1; i++)
		{
			pathLength += Vector3.Distance (allWayPoints [i], allWayPoints[i+1]);
		}
		return pathLength;
	}
}
