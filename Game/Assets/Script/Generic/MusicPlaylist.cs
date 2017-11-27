using UnityEngine;
using System.Collections;

public class MusicPlaylist : MonoBehaviour
{
	public AudioClip[] soundtrack;
	public bool startPlaylist;
	private AudioSource audios;
	
	// Use this for initialization
	void Start ()
	{
		audios = GetComponent<AudioSource>();

		if (!audios.isPlaying && startPlaylist && GetComponent<AudioSource>().isActiveAndEnabled)
		{
			audios.clip = soundtrack[Random.Range(0, soundtrack.Length)];
			audios.Play();
		}


	}
	
	// Update is called once per frame
	void Update ()
	{
		RandomSong ();
	}

	public void RandomSong ()
	{
		if(!audios.isPlaying && startPlaylist && GetComponent<AudioSource>().isActiveAndEnabled)
		{
			audios.clip = soundtrack[Random.Range(0, soundtrack.Length)];
			audios.Play();
		}
	}
}