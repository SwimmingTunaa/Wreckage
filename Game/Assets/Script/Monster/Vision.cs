using UnityEngine;
using System.Collections;

public class Vision : MonoBehaviour {

	public Transform targetObject;
	public float fieldOfViewAngle = 135f;
	public Vector3 personalLastSighting;
	public float dectectDistance = 20;
	public bool canSee;
	public bool isAi;

	private Vector3 previousSighting;
	private Vector3 resetPos = new Vector3 (1000, 1000, 1000);


	void Awake()
	{
		previousSighting = resetPos;
		personalLastSighting = resetPos;
	}
	// Update is called once per frame
	void Update () 
	{
		UpdateVision (targetObject, isAi);
		if (GM.lastPlayerSighting != previousSighting && isAi) 
			personalLastSighting = GM.lastPlayerSighting;
	
	}

	public bool InCameraView(GameObject obj)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes (Camera.main);
		if (obj.GetComponent<Collider>() !=null && GeometryUtility.TestPlanesAABB (planes, obj.GetComponent<Collider> ().bounds))
			return true;
		else
			return false;
	}

	public void UpdateVision(Transform targetPos, bool AI)
	{
		float dist = Vector3.Distance (transform.position, targetPos.position);
//		print (dist);
		if (dist <= dectectDistance) 
		{
			Vector3 direction = targetPos.position - transform.position;
			float angle = Vector3.Angle (direction, transform.forward);

			if (angle < fieldOfViewAngle * 0.5f) 
			{
				
				// If the angle between forward and where the player is, is less than half the angle of view...
				RaycastHit hit;
				// ... and if a raycast towards the player hits something...
				if (Physics.Raycast (transform.position, direction, out hit)) 
				{
				//	Debug.Log (hit.collider.gameObject.name);
					Debug.DrawRay (transform.position, direction, Color.cyan);
					// ... and if the raycast hits the player...
					if (hit.collider.tag == targetPos.tag) 
					{
						if (AI)
						{
							Debug.Log ("inFoV");
							canSee = true;
							GetComponentInParent<GhostAI> ().ghostVisible = true;
							GM.lastPlayerSighting = targetObject.transform.position; 
						}
						if (!AI)
						{
							//Debug.Log("hit");
							canSee = InCameraView (targetPos.gameObject);
							//Debug.Log( InCameraView (targetPos.gameObject));
							//Debug.Log(targetPos.gameObject.name);
						}
					}
					else
						canSee = false;
				}
			}
		} else
			{
			canSee = false;
			if( AI && GetComponentInParent<GhostAI> ().ghostVisible && GetComponentInParent<GhostAI> () != null )
				GetComponentInParent<GhostAI> ().ghostVisible = false;
			}
	}

	public bool UpdateVisionObject(Transform targetPos, string tagName)
	{
		float dist = Vector3.Distance (transform.position, targetPos.position);
		//		print (dist);
		if (dist <= dectectDistance)
		{
			Vector3 direction = targetPos.position - transform.position;


				RaycastHit hit;
				// ... and if a raycast towards the player hits something...
				if (Physics.Raycast (transform.position, direction, out hit))
				{
					Debug.Log (hit.collider.name + "ray  hit");
					Debug.DrawRay (transform.position, direction, Color.cyan);
					// ... and if the raycast hits the player...
					if (hit.collider.tag == targetPos.tag)
						return true;
					else
						return false;
				}
			else
				return false;
		}
		else
			return false;
	}
}


