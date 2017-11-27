using UnityEngine;
using System.Collections;

public class GhostMovement : MonoBehaviour {
	
	[HideInInspector] public Vector3 point;
	[HideInInspector] public Vector3 pointAttack;
	private float timer;
	private float timerTwo;
	private Vector3 tempPos; 



	public Vector3 UpdatePlayerPos(Transform targetTransform)
	{
		return targetTransform.position;
	}

	public void Wander(float wanderTime, float wanderRadius, UnityEngine.AI.NavMeshAgent agent)
	{
		timer += Time.deltaTime;
		if (timer >= wanderTime || agent.remainingDistance <= 2)
		{
			Vector3 newPos =  RandomNavSphere(transform.position, wanderRadius);
			timer = 0;
			tempPos = newPos;
			agent.SetDestination(tempPos);
			//Debug.DrawRay (tempPos, Vector3.up * 100, Color.magenta, wanderTime);
		}
	}

	public IEnumerator WanderRandom(float wanderTime, float wanderRadius, float minRadius, float changeTime, UnityEngine.AI.NavMeshAgent agent, GameObject targetObject)
	{
		timer += Time.deltaTime;
		timerTwo += Time.deltaTime;

		print (timerTwo);
		if (timer >= wanderTime || agent.remainingDistance <= 2)
		{
			Vector3 newPos =  RandomNavSphere(transform.position, wanderRadius);
			timer = 0;
			tempPos = newPos;
			agent.SetDestination(tempPos);
			//Debug.DrawRay (tempPos, Vector3.up * 100, Color.cyan, wanderTime);
			if (timerTwo >= changeTime) 
			{
				startRandomPos (wanderRadius, minRadius, targetObject);
				//ghostVisual.GetComponent<Animator> ().SetTrigger ("Dissapear");
		
				yield return new WaitForSeconds (1);
				agent.Warp (point);
				timerTwo = 0;
			}
		}
	}

	public static Vector3 RandomNavSphere(Vector3 origin, float dist) {
		Vector3 randDirection = Random.insideUnitSphere * dist;
		randDirection += origin;
		UnityEngine.AI.NavMeshHit navHit;
	
		UnityEngine.AI.NavMesh.SamplePosition (randDirection, out navHit, dist, UnityEngine.AI.NavMesh.AllAreas);
		return navHit.position;
	}


	public void Footsteps(bool screenShake, AudioClip[] footStepsAudio, GameObject targetObject, UnityEngine.AI.NavMeshAgent agent)
	{
		if (agent.velocity.magnitude >= 0.2)
		{
			agent.GetComponent<AudioSource> ().PlayOneShot (footStepsAudio [Random.Range (0, footStepsAudio.Length)]);

			float dist = Vector3.Distance (agent.transform.position, targetObject.transform.position);
			CameraShake camShake = targetObject.GetComponent<CameraShake>();

			if (dist <= 15 && screenShake)
			{ 
				camShake.enabled = true;
				camShake.shakeAmount = Mathf.Clamp (1 / dist, 0.01f, 0.3f);
				camShake.shakeDuration = Mathf.Clamp (1 / dist, 0.25f, 0.5f);
			}
		}
	}

	public void MoveToTargetObject(Vector3 targetPos, float speed, UnityEngine.AI.NavMeshAgent agent)
	{
		agent.destination = targetPos;
		agent.speed = speed;
	}

	public void StopMove(UnityEngine.AI.NavMeshAgent agent)
	{
		agent.speed = 0;
		agent.isStopped = true;
	}

	public void StartMove (UnityEngine.AI.NavMeshAgent agent, float startSpeed)
	{
		agent.speed = startSpeed;
		agent.isStopped = false;
	}

	public void startRandomPos(float maxRange, float minRange, GameObject targetObject)
	{
		if(RandomPosition(minRange,minRange,out point, targetObject))
		{

			Debug.DrawRay (point, Vector3.up * 100, Color.blue, 1);
		}

		if(RandomPosition(minRange/2,minRange/2,out pointAttack, targetObject))
		{

			Debug.DrawRay (pointAttack, Vector3.up * 100, Color.green, 1);
		}
	}
	public bool RandomPosition(float maxRange, float minRange, out Vector3 result, GameObject targetObject)
	{
		for (int i = 0; i < 30; i++)
		{
			Vector3 randomDir;
			UnityEngine.AI.NavMeshHit hit;
			randomDir = (targetObject.transform.position) + Random.insideUnitSphere * maxRange; 
			if (UnityEngine.AI.NavMesh.SamplePosition (randomDir, out hit, maxRange, UnityEngine.AI.NavMesh.AllAreas)) 
			{
				float dis = Vector3.Distance(hit.position,targetObject.transform.position);
				if (dis > minRange) 
				{
					result = hit.position;
					return true;
				}
			}
		}
		result = targetObject.transform.position + Vector3.forward * 4;
		return false;
	}
}