using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Ladder : MonoBehaviour {

	public float speed;
	public FirstPersonController fpController;
	public GameObject camVisual;

	public bool canPass;
	private bool _climbing;

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			Ray ray = new Ray(other.transform.position + (-other.transform.up *0.1f), -other.transform.up *0.05f);
			RaycastHit hit;
		//	Debug.DrawRay(other.transform.position + (-other.transform.up *0.1f), -other.transform.up  -other.transform.up *0.1f,Color.green);
			if (Physics.Raycast (ray, out hit, 0.5f) && canPass)
			{
				if (hit.collider.tag == "Walls" && _climbing)
					Exit ();
			}
			else
			{
				camVisual.SetActive (!false);
				fpController.PauseMoving (true);
				fpController.m_StickToGroundForce = 0;
				fpController.m_GravityMultiplier = 0;
				fpController.m_MouseLook.clampHorizontalRotation = true;
				if (Input.GetKey (KeyCode.W))
				{
					_climbing = true;
					fpController.isClimbing = _climbing;
					fpController.m_MoveDir.y = speed;
					canPass = false;
				}
				else
					fpController.m_MoveDir.y = 0;

				if (Input.GetKey (KeyCode.S) && _climbing && !fpController.m_CharacterController.isGrounded)
				{
					
					fpController.m_MoveDir.y = -speed;
					canPass = true;
				}
				else
					if (Input.GetKey (KeyCode.S) && _climbing && fpController.m_CharacterController.isGrounded)
						other.transform.Translate (-Vector3.forward * Time.deltaTime * 2);
			}
		}
	}


	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (Input.GetKey (KeyCode.W))
				other.transform.Translate (Vector3.forward * Time.deltaTime * 8);
			Exit ();
		}
	}

	void Exit()
	{
		canPass = false;
		_climbing = false;
		fpController.m_MouseLook.clampHorizontalRotation = false;
		fpController.m_StickToGroundForce = 10;
		fpController.m_GravityMultiplier = 2;
		fpController.PauseMoving (false);
		camVisual.SetActive (true);
	}
}

