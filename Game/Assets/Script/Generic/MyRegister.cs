using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyRegister : MonoBehaviour {

	public static List<GameObject> register = new List<GameObject> ();
	public static List<GameObject> goList = register;

	void Start()
	{
		Debug.Log (register.Count);
	}

	public static void Register (GameObject go)
	{
		register.Add (go);
	}

	public static void Unregister (GameObject go)
	{
		register.Remove (go);
	}

	public static GameObject Find(string name)
	{
		return register.Find (x => x.name == name);
	}
}
