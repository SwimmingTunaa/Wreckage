using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {

	
	public Camera m_Camera;
	public bool amActive =false;
	public bool autoInit =false;
	public bool rotateZ;

	GameObject myContainer;	
	Vector3 camRot;
	
	void Awake(){
		if (autoInit == true){
			m_Camera = Camera.main;
			amActive = true;
		}
		
		myContainer = new GameObject();
		myContainer.name = "GRP_"+transform.gameObject.name;
		myContainer.transform.position = transform.position;
		transform.parent = myContainer.transform;
	}
	
	
	void Update(){
		if (rotateZ)
		{
			camRot = m_Camera.transform.rotation.eulerAngles;
			camRot.z = 0;
			camRot.y = 0;
		}
		else 
		{
			camRot = m_Camera.transform.rotation.eulerAngles;
			camRot.x = 0;
			camRot.z = 0;
		}

		if(amActive==true){
			myContainer.transform.LookAt(myContainer.transform.position + Quaternion.Euler(camRot) * Vector3.back, m_Camera.transform.rotation * Vector3.up);
		}	
	}
}
