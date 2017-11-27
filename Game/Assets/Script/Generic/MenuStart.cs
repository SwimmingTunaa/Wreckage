using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuStart : MonoBehaviour {

	public GameObject objTurnOff;
	public GameObject menuTextTurnOn;

	private bool clickedStart = false;
	private Animator anim;

	void Start () {
		anim = GetComponent<Animator>();
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && !clickedStart) 
		{
			clickedStart = true;
			objTurnOff.SetActive(false);
			menuTextTurnOn.SetActive(true);
			anim.Play("TitleMove",0);
		}
	}

	public void loadLevel(string lvlToLoad)
	{
		SceneManager.LoadScene (lvlToLoad);
	}

	public void QuitGame ()
	{
		Application.Quit ();
	}
}
