using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using UnityEngine.PostProcessing;

[RequireComponent(typeof (Vision))]

public class CameraTake : MonoBehaviour {

	public int filmCurrentAmount;
	public int maxFilmAmount;
	public GameObject cameraFilter;
	public GameObject camVisual;
	public GameObject flash;
	public AudioClip[] cameraSounds;
	public bool canSeeGhostFlash = true;
    public GameObject cameraRedLight;
	public GameObject flashSign;
	public Vision vis;
	public float stunTime = 3;
	public float minFov = 15f;
	public float maxFov = 90f;
	public float sensitivity = 10f;
	public FrostEffect frost;


	private SkinnedMeshRenderer enemyRenderer;
	private GameObject enemy;
	// post processing effects
	private MotionBlurModel camMotionBlur;
	private DepthOfFieldModel DOF;
	private GrainModel noiseGrain;
	private ChromaticAberrationModel ChroAber;
	//
	private Animator anim;
	private FirstPersonController FPController;
	private GameObject gm;
	private bool animTime;
	[HideInInspector] public float currentTime;
	private float waitTime = 0.6f;// delay between camera coming up
	private float passedTime;

	public float shootRate = 5.3f; // camera cooldown
	[HideInInspector]public float nextShoot;


	//camera tut bit
	public bool canTakePic = false; //cannot take pic at beginning of game;
	[HideInInspector] public bool cameraUp;
	public bool cameraPressed;

	private bool cameraLightOn = false;
	//screenshot bit
	private string folderPath;
	private Texture2D screenshot;
	//private float y = 20.0f;
	private int screenshotCount;
	//Reaveal Invis Bit
	bool triggered = false;
	private bool canSeeObj = false;


	void Awake()
	{
		enemy = GameObject.FindGameObjectWithTag("Ghost");

		//maxFilmAmount = filmCurrentAmount;
		gm = GameObject.FindGameObjectWithTag("GameController");
	}

	// Use this for initialization
	void Start () 
	{
		enemyRenderer = enemy.GetComponent<GhostAI>().eRenderer;
		camMotionBlur = GetComponentInChildren<PostProcessingBehaviour> ().profile.motionBlur;
		DOF = GetComponentInChildren<PostProcessingBehaviour>().profile.depthOfField;
		ChroAber = GetComponentInChildren<PostProcessingBehaviour> ().profile.chromaticAberration;
		noiseGrain = GetComponentInChildren<PostProcessingBehaviour> ().profile.grain;
		anim = camVisual.GetComponent<Animator>();
		FPController = GetComponent<FirstPersonController>();
	}
		
	
	// Update is called once per frame
	void Update () 
	{
		revealInvisible ();
		if (canTakePic)
		{
			CameraButtonPress ();
			TakePic ();
		}
		RedCameraLight ();
    }

	void Timer(ref float curTime,float wait)
	{
		if (curTime <= wait && curTime > 0)
			curTime -=  Time.deltaTime;
		else if(curTime <= 0)
			curTime = 0;
	}
		
	void revealInvisible()
	{
		if (cameraUp && !triggered)
		{
			for(int i = 0; i < MyRegister.goList.Count; i++) 
			{
				//Debug.Log (MyRegister.goList[i].name + " " + MyRegister.goList[i].activeSelf);
				MyRegister.goList[i].GetComponent<RegisterGO> ().PictureTaken = false;
				MyRegister.goList[i].SetActive (! MyRegister.goList[i].activeInHierarchy);
			}
			triggered = true;

		} else if(!cameraUp && triggered)
		{
			triggered = false;
			foreach (GameObject go in MyRegister.goList) 
			{
				if(!go.GetComponent<RegisterGO> ().PictureTaken)
					go.SetActive (!go.activeInHierarchy);
			}
		}
	}

	void RedCameraLight()
	{
		if (Time.time < nextShoot)
		{
			cameraRedLight.GetComponent<SpriteRenderer> ().color = Color.red;
			//cameraRedLight.SetActive (false);
			flashSign.GetComponent<Image>().color = Color.red;
		}
		else
		if (Time.time > nextShoot && gm.GetComponent<StartOfGame> ().cameraLightReady)
		{
			cameraRedLight.GetComponent<SpriteRenderer> ().color = Color.green;
			if(!cameraRedLight.activeInHierarchy)
				cameraRedLight.SetActive (true);
			flashSign.GetComponent<Image>().color = Color.green;
		}
		if (cameraLightOn) 
		{
			
		} else 
		{
		}
	}
		
	void CameraButtonPress()
	{
		if(Input.GetMouseButtonDown(1))
		{
			//anim.SetTrigger("Camera"); // set camerea bool to be true on animator;
			currentTime = waitTime;
		}

		if(Input.GetMouseButton(1))
		{
			CameraUp ();
			Zoom ();
		}
		else if(Input.GetMouseButtonUp(1))
			CameraDown ();
	}

	void TakePic()
	{
		if(Input.GetMouseButtonDown(0) && !flash.activeSelf && Input.GetMouseButton(1) && filmCurrentAmount > 0)
			StartCoroutine(TakePitureAction ());
		else if (Input.GetMouseButtonDown(0) && !flash.activeSelf && Input.GetMouseButton(1))
			 NoFilm();
	}

	public IEnumerator TakePitureAction()
	{
		if (Time.time > nextShoot)
		{
			cameraPressed = true;
			nextShoot = Time.time + shootRate;
			filmCurrentAmount -= 1;
			this.gameObject.GetComponent<AudioSource>().PlayOneShot(cameraSounds[0]);
			flash.SetActive(true);
			if(vis.canSee)
				enemy.GetComponent<GhostAI>().tempTime = stunTime;
			camVisual.GetComponent<AudioSource>().PlayOneShot(cameraSounds[2]);
			if (canSeeGhostFlash)
				enemyRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
			cameraFilter.SetActive (false); //hides the GUI so it's not in the frame.
			frost.enabled = false;
			yield return new WaitForEndOfFrame ();
			ScreenCapture (); // captures the picture
			cameraFilter.SetActive (true);
			frost.enabled = true;
			MakeGameObject ();
		}
		 if (flash.activeSelf)
		{
			yield return new WaitForSeconds (0.15f);
			cameraPressed = false;
			flash.SetActive(false);
			enemyRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
		}
	}

	private void MakeGameObject() // to see if invisible objects are in the view.
	{
		foreach (GameObject o in MyRegister.goList)
		{
			if(vis.InCameraView(o))
			{
				canSeeObj = vis.UpdateVisionObject (o.gameObject.transform, "Invisible");
				Debug.Log (canSeeObj);
				Debug.Log (o.name + "  hit");
				//	o.SetActive (!o.activeInHierarchy);
					o.GetComponent<RegisterGO> ().PictureTaken = true;
			}
		}
	}

	public void NoFilm()
	{
		if (filmCurrentAmount <= 0)
		{
			print("No Film Left");
			GetComponent<AudioSource>().PlayOneShot(cameraSounds[1]);
		}
	}

	public void CameraUp()
	{
		anim.SetBool("CameraDone",true);
		if (!animTime)
		{
			Timer(ref currentTime, waitTime);
			if(currentTime <= 0)
			{
				camMotionBlur.enabled = true;
				DOF.enabled = true;
				ChroAber.enabled = true;
				noiseGrain.enabled = true;
				cameraFilter.SetActive(true);
				FPController.PauseMoving (true);
				Camera.main.fieldOfView = 50;
				cameraUp = true;
				animTime = true;
			}
		}	
	}
	public IEnumerator CameraUpAuto()
	{
		anim.SetBool("CameraDone",true);
		if (!animTime)
		{
			yield return new WaitForSeconds (0.6f);
				camMotionBlur.enabled = true;
				DOF.enabled = true;
				ChroAber.enabled = true;
				noiseGrain.enabled = true;
				cameraFilter.SetActive(true);
				FPController.m_WalkSpeed = 0;
				FPController.m_RunSpeed = 0;
				FPController.m_JumpSpeed = 0;
				Camera.main.fieldOfView = 50;
				cameraUp = true;
				animTime = true;
		}	
	}

	void Zoom()
	{
		float fov = Camera.main.fieldOfView;
		fov -= Input.GetAxis ("Mouse ScrollWheel") * sensitivity;
		fov = Mathf.Clamp (fov, minFov, maxFov);

		if(Input.GetAxis("Mouse ScrollWheel") != 0)
		{
			GetComponent<AudioSource> ().PlayOneShot (cameraSounds [3]);
		}
		Camera.main.fieldOfView = fov;
	}

	public void CameraDown()
	{
		cameraUp = false;
		anim.SetBool("CameraDone",false);
		camMotionBlur.enabled = false;
		DOF.enabled = false;
		ChroAber.enabled = false;
		noiseGrain.enabled = false;
		FPController.PauseMoving (false);
		Camera.main.fieldOfView = 60;
		animTime = false;
		if(cameraFilter.activeInHierarchy)
			cameraFilter.SetActive(false);
	}

	void ScreenCapture()
	{
		screenshot = new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24, false);
		screenshot.ReadPixels(new Rect(0,0,Screen.width,Screen.height) ,0 ,0);
		screenshot.Apply ();
		Sprite tempsprite = Sprite.Create(screenshot,new Rect(0,0, Screen.width,Screen.height),transform.position);
		//gm.GetComponent<GM> ().captureList.Add (tempsprite);
		for (int i = 0; i < gm.GetComponent<Inventory> ().InventorySpaces.Length; i++) 
		{
			List<Image> inv = new List<Image> (gm.GetComponent<Inventory> ().InventorySpaces);
			//Debug.Log ("pressed");
			if (inv [i].sprite == null) 
			{
				inv [i].sprite = tempsprite;
				inv [i].color = Color.white; //make the colour white so you can see the image
				inv.RemoveAt (i);
				break;
			}
		}
	}
}
