// "WaveVR SDK 
// © 2017 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the WaveVR SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using wvr;
using WVR_Log;

public class testeventhandler : MonoBehaviour,
IPointerEnterHandler,
IPointerExitHandler,
IPointerDownHandler,
IBeginDragHandler,
IDragHandler,
IEndDragHandler,
IDropHandler,
IPointerClickHandler,
IPointerUpHandler
{
	private const string LOG_TAG = "testeventhandler";
	private void DEBUG (string msg)
	{
		Log.d (LOG_TAG, gameObject.name + " " + msg, true);
	}
	public bool UnityMode = false;
	public Text text;
	public void OnPointerEnter (PointerEventData eventData)
	{
		text.text = "Enter";
		DEBUG ("OnPointerEnter");
	}

	public void OnPointerExit (PointerEventData eventData)
	{
		text.text = "Exit";
		DEBUG ("OnPointerExit");
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		text.text = "Down";
		DEBUG ("OnPointerDown");
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		text.text = "Begin Drag";
		DEBUG ("OnBeginDrag");
	}

	public void OnDrag (PointerEventData eventData)
	{
		text.text = "Dragging";
		DEBUG ("OnDrag");
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		text.text = "EndDrag";
		DEBUG ("OnEndDrag");
	}

	public void OnDrop (PointerEventData eventData)
	{
		text.text = "Drop";
		DEBUG ("OnDrop");
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		text.text = "Up";
		DEBUG ("OnPointerUp");
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		text.text = "Click";
		DEBUG ("OnPointerClick");
	}

	private GameObject eventSystem = null;
	// Use this for initialization
	void Start ()
	{
		DEBUG ("Start()");
		setEventSystem ();
	}

	WaveVR_ControllerInputModule cim = null;
	// Update is called once per frame
	void Update ()
	{
		if (WaveVR_InputModuleManager.Instance != null && this.eventSystem == null)
		{
			setEventSystem ();
		}

		if (this.eventSystem != null)
		{
			if (cim == null)
				cim = this.eventSystem.GetComponent<WaveVR_ControllerInputModule> ();
			if (cim != null)
				cim.UnityMode = this.UnityMode;
		}
	}

	private void setEventSystem()
	{
		if (EventSystem.current == null)
		{
			EventSystem _es = FindObjectOfType<EventSystem> ();
			if (_es != null)
			{
				this.eventSystem = _es.gameObject;
				DEBUG ("setEventSystem() find current EventSystem: " + eventSystem.name);
			}
		} else
		{
			this.eventSystem = EventSystem.current.gameObject;
			DEBUG ("setEventSystem() find current EventSystem: " + eventSystem.name);
		}
	}
}
