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
using WVR_Log;
using wvr;

public class Go_Event_TrackingTrajectory_3 : MonoBehaviour,
IPointerUpHandler,
IPointerEnterHandler,
IPointerExitHandler,
IPointerDownHandler,
IBeginDragHandler,
IDragHandler,
IEndDragHandler,
IDropHandler,
IPointerHoverHandler
{
	private const string LOG_TAG = "WaveVR_TrackingTrajectory_Roll";

	WaveVR_Controller.EDeviceType curFocusControllerType = WaveVR_Controller.EDeviceType.Head;

	public bool isControllerFocus_R;
	public bool isControllerFocus_L;

	private GameObject m_RightController;
	private GameObject m_LeftController;

	// Use this for initialization
	void Start () {
		GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.blue);
	}

	// Update is called once per frame
	void Update () {
		if (isControllerFocus_R || isControllerFocus_L)
		{
			if (WaveVR_Controller.Input(curFocusControllerType).GetPressUp(WVR_InputId.WVR_InputId_Alias1_Touchpad) ||
				WaveVR_Controller.Input(curFocusControllerType).GetPressDown(WVR_InputId.WVR_InputId_Alias1_Trigger))
			{
				Log.d(LOG_TAG, "Tracking Trajectory: <Roll> Oz, Rotation around the front-to-back axis is called.", true);
				TrackingTrajectory_Head.isTrackingTrajectoryId = 3;
			}
		}
	}

	public void OnPointerEnter (PointerEventData eventData)
	{
		WaveVR_Controller.EDeviceType type = eventData.enterEventCamera.gameObject.GetComponent<WaveVR_PoseTrackerManager>().Type;
		GameObject target=eventData.enterEventCamera.gameObject;
		if (target.GetComponent<WaveVR_PoseTrackerManager>())
		{
			if (type == WaveVR_Controller.EDeviceType.Dominant)
			{
				m_RightController = target;
				isControllerFocus_R = true;
			}
			else if (type == WaveVR_Controller.EDeviceType.NonDominant)
			{
				m_LeftController = target;
				isControllerFocus_L = true;
			}

			// Right-Hand mode
			if (!WaveVR_Controller.IsLeftHanded)
			{
				if(isControllerFocus_R)
				{
					GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
				}
				else if (isControllerFocus_L)
				{
					GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
				}
			}

			// Left-Hand mode
			else if (WaveVR_Controller.IsLeftHanded)
			{
				if(isControllerFocus_R)
				{
					GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
				}
				else if (isControllerFocus_L)
				{
					GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
				}
			}
		}
		curFocusControllerType = type;
	}

	public void OnPointerExit (PointerEventData eventData)
	{
		RaycastHit hit;
		if(m_RightController && isControllerFocus_R){ //R_Controller Leave
			Vector3 fwd_R = m_RightController.transform.TransformDirection(Vector3.forward);
			if (!Physics.Raycast(m_RightController.transform.position, fwd_R, out hit))
			{

				isControllerFocus_R = false;
				if (isControllerFocus_L)
				{
					curFocusControllerType = WaveVR_Controller.EDeviceType.NonDominant;
					GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
					return;
				}
			}
		}

		if(m_LeftController && isControllerFocus_L){ //L_Controller Leave
			Vector3 fwd_L = m_LeftController.transform.TransformDirection(Vector3.forward);
			if (!Physics.Raycast(m_LeftController.transform.position, fwd_L, out hit))
			{
				isControllerFocus_L = false;
				if (isControllerFocus_R)
				{
					curFocusControllerType = WaveVR_Controller.EDeviceType.Dominant;
					GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
					return;
				}
			}
		}
		curFocusControllerType = WaveVR_Controller.EDeviceType.Head;

		GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.blue);
	}

	public void OnPointerUp(PointerEventData eventData){
		//Debug.Log("OnPointerUp");
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		//Debug.Log("OnPointerDown");

	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		//Debug.Log("OnBeginDrag");
	}

	public void OnDrag(PointerEventData eventData)
	{
		//Debug.Log("OnDrag");
	}


	public void OnEndDrag(PointerEventData eventData)
	{
		//Debug.Log("OnEndDrag");
	}

	public void OnDrop(PointerEventData eventData)
	{
		//Debug.Log("OnDrop");
	}

	public void OnPointerHover (PointerEventData eventData)
	{
		// Debug.Log("OnPointerHover: "+eventData.enterEventCamera.gameObject);
	}
}
