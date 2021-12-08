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
using wvr;
using WVR_Log;

public class SwipeTest : MonoBehaviour {
	private const string LOG_TAG = "SwipeTest";
	public WaveVR_Controller.EDeviceType DeviceType = WaveVR_Controller.EDeviceType.Dominant;
	private void PrintDebugLog(string msg)
	{
		Log.d (LOG_TAG, this.DeviceType + " " + msg);
	}

	void OnEnable()
	{
		WaveVR_Utils.Event.Listen (WaveVR_Utils.Event.SWIPE_EVENT, OnSwipe);
	}

	void OnDisable()
	{
		WaveVR_Utils.Event.Remove (WaveVR_Utils.Event.SWIPE_EVENT, OnSwipe);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnSwipe(params object[] args)
	{
		WVR_EventType _event = (WVR_EventType)args [0];
		WVR_DeviceType _type = (WVR_DeviceType)args [1];
		PrintDebugLog ("OnSwipe() _event: " + _event + ", _type: " + _type);

		WaveVR.Device _dev = WaveVR.Instance.getDeviceByType (this.DeviceType);
		if (_dev == null)
			return;
		if (_dev.type != _type)
			return;

		switch (_event)
		{
		case WVR_EventType.WVR_EventType_LeftToRightSwipe:
			transform.Rotate (0, -180 * (10 * Time.deltaTime), 0);
			break;
		case WVR_EventType.WVR_EventType_RightToLeftSwipe:
			transform.Rotate (0, 180 * (10 * Time.deltaTime), 0);
			break;
		case WVR_EventType.WVR_EventType_DownToUpSwipe:
			transform.Rotate (180 * (10 * Time.deltaTime), 0, 0);
			break;
		case WVR_EventType.WVR_EventType_UpToDownSwipe:
			transform.Rotate (-180 * (10 * Time.deltaTime), 0, 0);
			break;
		}
	}
}
