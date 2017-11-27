using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CloseUI : MonoBehaviour, IPointerDownHandler 
{
	public GameObject objToClose;

	public void OnPointerDown(PointerEventData eventData)
	{
		objToClose.SetActive (false);
	}
}
