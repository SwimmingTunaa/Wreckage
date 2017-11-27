using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEngine.PostProcessing;

public class PlayerHealth : MonoBehaviour {

	public int health = 2;
	public AudioClip[] hitAudios;
	public AudioClip deathAudio;
	public float healthRecoverTime;
	public GameObject handCam;
	public GameObject Ghost;
	public bool chillEffIsOn = false;
	public GameObject cameraGallery;

	private int startHealth;
	private float currentTimer;
	[HideInInspector] public static bool playerDead;
	private ChromaticAberrationModel Vignette;
	private GameObject gm;

	bool triggered = false;
	
	void Start()
	{
		startHealth = health;
		Vignette = GetComponentInChildren<PostProcessingBehaviour>().profile.chromaticAberration;
		gm = GameObject.FindGameObjectWithTag("GameController");
	}
		
	void Update()
	{
		HealthRegenTimer();
		StartCoroutine(Death());

	}

	IEnumerator Death()
	{
		if (health <= 0)
		{
			gm.GetComponent<ScreenFade> ().FadeToBlack (0.4f);
			if (!triggered)
			{
				if (!GM.globalPlayerDead)
				{
					GetComponent<AudioSource> ().PlayOneShot (deathAudio);
					GetComponentInChildren<Animator> ().SetBool ("Dead", true);		
					GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = false;
					Ghost.SetActive (true);
				}
		
				if (Vignette.enabled)
					Vignette.enabled = false;
				handCam.SetActive (false);
				gm.GetComponent<GM> ().ShowMouse ();
				GM.globalPlayerDead = true;
				triggered = true;
				yield return new WaitForSeconds (4f);
				cameraGallery.SetActive (true);

			}
		}
	}

	void HealthRegenTimer() //timer before health recovers.
	{
		if(currentTimer <= healthRecoverTime && currentTimer > 0)
		{
			currentTimer -= Time.deltaTime;
		}
		else if(currentTimer <= 0 && health > 0)
		{
			currentTimer = 0;
			health = startHealth;
		//	Vignette.enabled  = false;
		}
	}

	public void TakeDamage(int damage)
	{	
		health -= damage;
		GetComponent<AudioSource>().PlayOneShot(hitAudios[Random.Range(0,hitAudios.Length)]);
		Vignette.enabled  = true;
		GetComponent<CameraShake>().shakeDuration = 0.75f; 
		currentTimer = healthRecoverTime;
	}
}
