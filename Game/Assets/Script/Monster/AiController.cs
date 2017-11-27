using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum states{
	Chase,
	Predict,
	Seek,
	Attack,
	Wander,
	Stunned
}

public class AiController : MonoBehaviour {

	public states state;

	private bool updatePos;
	private GhostAI ai;
	private Vision vis;

	void Start () 
	{
		vis = GetComponent<Vision>();
		ai = GetComponent<GhostAI> ();
	}


	void Update () 
	{
		if (!GM.globalPlayerDead)
		{
			if (!vis.canSee && ai.checkOutLoc && state != states.Stunned)
				state = states.Seek;
			else
				if (ai.checkOutLoc && ai.navAgent.remainingDistance <= 1.5f) // if ai reaches destination, continue to wander.
				{
					ai.playAnim = true;
					ai.checkOutLoc = false;
				}
			
			if (vis.canSee && ai.navAgent.remainingDistance <= ai.attackRange && state != states.Stunned)
				state = states.Attack;
			
			if (((vis.canSee && ai.navAgent.remainingDistance >= ai.attackRange)) && state != states.Stunned)
				state = states.Chase;
			else
				if (!vis.canSee && state != states.Stunned && updatePos )
					state = states.Predict;
			
			if (!vis.canSee && state != states.Stunned &&  (state == states.Seek || state == states.Chase || state == states.Predict) && ai.navAgent.remainingDistance < 1.5f)
				state = states.Wander;
			
			if (ai.tempTime > 0)
				state = states.Stunned;
			else 
				if(ai.tempTime <= 0 && state == states.Stunned)
				{
					ai.RecoverFromStun ();
					state = states.Wander;
				}
		}
	}

	void FixedUpdate()
	{
		switch (state)
		{
			case states.Chase:
				updatePos = true;
				ai.checkOutLoc = false;
				ai.Movement ();
				break;

			case states.Predict:
				ai.navAgent.speed = 4;
				updatePos = false;
				if (updatePos)
				{
					ai.Predict ();
				}
				break;

			case states.Attack:
				if(!ai.isAttacking)
				StartCoroutine (ai.Attack ()); 
				break;

			case states.Seek:
				ai.Seek ();
				break;

			case states.Wander:
				ai.Wander ();
				break;

			case states.Stunned:
				ai.Stun ();
				break;

			default:
				ai.Wander ();
				break;
		}
	}
}

