﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScreamState : StateMachineBehaviour {

	public AudioClip clip;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		animator.GetComponentInParent<GhostAI> ().StopMove (animator.GetComponentInParent<NavMeshAgent>());
		animator.GetComponentInParent<AudioSource> ().PlayOneShot (clip);
		animator.GetComponentInParent<Vision> ().targetObject.GetComponent<CameraShake> ().shakeDuration = 2f;
		CameraShake.shakeActive = true;
		if (stateInfo.length > 0.5f)
			animator.GetComponentInParent<Vision> ().targetObject.GetComponent<CameraShake> ().Shake();

	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	//OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		animator.GetComponentInParent<GhostAI> ().StartMove (animator.GetComponentInParent<NavMeshAgent>(), 2);
		CameraShake.shakeActive = false;
		animator.GetComponentInParent<GhostAI> ().playAnim = false;
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
