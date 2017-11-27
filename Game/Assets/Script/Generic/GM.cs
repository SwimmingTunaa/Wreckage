using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.Characters.FirstPerson;

public class GM : MonoBehaviour
{
	public bool lockCursor;
	public List<int> keynumber; 
	public Sprite[] numbers;
	public GameObject[] numberSpawnPoints;
	public Text keypadNumbers;
	public List<Sprite> captureList;
	public static bool globalPlayerDead;
	public GameObject player;
	public GameObject handCamera;
	public bool UIopen;
	public Transform ghostRespawnPos;


	[HideInInspector] public static bool showMouse;
	static public Vector3 lastCheckpointLoc;

	private Quaternion lastRot;
	private GameObject ghost;
	private NumberID[] numbersInGame;

	public static Vector3 lastPlayerSighting;
	// Use this for initialization
	void Awake()
	{
		lastCheckpointLoc = player.transform.position;
		lastRot = player.transform.rotation;
		randommiseCode ();
	}
	void Start () 
	{
		ghost = GameObject.FindGameObjectWithTag("Ghost");
		if (lockCursor)
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
    }
	
	// Update is called once per frame
	void Update ()
	{
		//print (Cursor.lockState);
	
		if (!ClickControl.firstClick)
			keypadNumbers.text = "****";
		else
			keypadNumbers.text = ClickControl.playerCode;
		Debug.DrawRay (lastPlayerSighting, Vector3.up * 5, Color.red);
	}

	public void loadLevel (string name)
	{
		SceneManager.LoadScene (name);
		Time.timeScale = 1f;
	}
				
	public void HideMouse()
	{	
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

	}

	public void ShowMouse()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	public void PausePlayer()
	{
		player.GetComponent<FirstPersonController> ().enabled = false;
		player.GetComponent<CameraTake> ().enabled = false;
	}

	public void ResumePlayer ()
	{
		player.GetComponent<FirstPersonController> ().enabled = true;
		player.GetComponent<CameraTake> ().enabled = true;
	}



	public IEnumerator RestartCheckpoint()
	{
		Color alphaColor = new Color (255, 255, 255, 0);

		numbersInGame = GameObject.FindObjectsOfType<NumberID> ();
		foreach(NumberID no in numbersInGame)
		{
			no.GetComponent<SpriteRenderer> ().sprite = null;
		}

		randommiseCode ();
		if(GetComponent<Inventory>().inventoryBag.activeSelf)
			GetComponent<Inventory>().inventoryBag.SetActive (false);
		for (int i = 0; i < GetComponent<Inventory>().InventorySpaces.Length; i++) 
		{
			GetComponent<Inventory>().InventorySpaces[i].color = alphaColor;
			GetComponent<Inventory>().InventorySpaces [i].sprite = null;
		}
		lockCursor = true;
		Cursor.visible = true;
		player.GetComponent<PlayerHealth> ().health = 1;
		globalPlayerDead = false;
		player.transform.rotation = lastRot;
		player.transform.position = lastCheckpointLoc;
		GetComponent<ScreenFade> ().FadeToClear ();
		player.GetComponentInChildren<Animator> ().SetBool ("Dead", false);		
		player.GetComponentInChildren<Animator>().Play("laying",0);
		yield return new WaitForSeconds (3.5f);
		player.GetComponent<FirstPersonController> ().enabled = true;
		handCamera.SetActive(true);

		ghost.GetComponent<UnityEngine.AI.NavMeshAgent> ().Warp (ghostRespawnPos.position);
	}

	public  void randommiseCode()
	{
		ClickControl.correctCode = "";
		List<GameObject> freeSpawnPoints = new List<GameObject> (numberSpawnPoints);
		//intial number
		Sprite startSprite = numbers[Random.Range(0, numbers.Length)];
		numberSpawnPoints[numberSpawnPoints.Length -1].GetComponent<SpriteRenderer>().sprite = startSprite;
		numberSpawnPoints[numberSpawnPoints.Length -1].GetComponent<NumberID>().ID = int.Parse(	numberSpawnPoints[numberSpawnPoints.Length -1].GetComponent<SpriteRenderer>().sprite.name);
		ClickControl.correctCode += numberSpawnPoints[numberSpawnPoints.Length - 1].GetComponent<NumberID> ().ID.ToString ();
        for (int i = 0; i <= 2; i++) 
		{
			int index = Random.Range (0, freeSpawnPoints.Count - 1);
			Transform pos = freeSpawnPoints [index].transform;
            Sprite tempObj = numbers[Random.Range(0, numbers.Length)];
            pos.GetComponent<SpriteRenderer>().sprite = tempObj;
            pos.GetComponent<NumberID>().ID = int.Parse(pos.GetComponent<SpriteRenderer>().sprite.name);
            ClickControl.correctCode += pos.GetComponent<NumberID>().ID.ToString(); // randomise the code for keypad
			//print(  ClickControl.correctCode);
            freeSpawnPoints.RemoveAt(index);
        }
		Debug.Log(ClickControl.correctCode);
	}
}
