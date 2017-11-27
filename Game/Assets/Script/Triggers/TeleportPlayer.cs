using UnityEngine;
using System.Collections;

public class TeleportPlayer : MonoBehaviour 
{
	public Transform targetPos;
	public Transform targetPos2;
    public bool flipRot;
	public int triggerCount = 1;
	public bool repeat;

	private bool ChangeLocation;
    private GameObject player;
	private bool playerOverlapping = false;
    private float prevDot = 0;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
		if (playerOverlapping && (triggerCount > 0 || repeat))
		{
			if (triggerCount == 1 && targetPos2 != null)
				ChangeLocation = true;
			Transform tarPos = ChangeLocation ? targetPos2 : targetPos;
	
            float currentDot = Vector3.Dot(transform.forward, player.transform.position - transform.position);
            Debug.Log(currentDot);
            if (currentDot < 0) // only transport the player once he's moved across plane
            {              
                // transport him to the equivalent position in the other portal
				float rotDiff = -Quaternion.Angle(transform.rotation, tarPos.transform.rotation);
                rotDiff += 180;
                player.transform.Rotate(Vector3.up, rotDiff);

                Vector3 positionOffset = player.transform.position - transform.position;
               // positionOffset = Quaternion.Euler(player.transform.rotation.x, rotDiff, player.transform.rotation.y) * positionOffset;
				Vector3 newPosition = tarPos.transform.position + positionOffset;
                player.transform.position = newPosition;

                playerOverlapping = false;
            }
            prevDot = currentDot;
        }
    }


    void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Player")
        {
            playerOverlapping = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            triggerCount--;
            playerOverlapping = false;
        }
    }

}

