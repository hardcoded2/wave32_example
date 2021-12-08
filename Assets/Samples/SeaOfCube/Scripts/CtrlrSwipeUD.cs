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

public class CtrlrSwipeUD : MonoBehaviour
{
	void OnEvent(params object[] args)
	{
		var _event = (WVR_EventType)args[0];
		Log.d("CtrlrSwipeUD", "OnEvent() _event = " + _event);

		switch (_event)
		{
			case WVR_EventType.WVR_EventType_DownToUpSwipe:
				transform.Rotate(30, 0, 0);
				break;
			case WVR_EventType.WVR_EventType_UpToDownSwipe:
				transform.Rotate(-30, 0, 0);
				break;
		}
	}

	void OnEnable()
	{
		WaveVR_Utils.Event.Listen(WaveVR_Utils.Event.SWIPE_EVENT, OnEvent);
	}

	void OnDisable()
	{
		WaveVR_Utils.Event.Remove(WaveVR_Utils.Event.SWIPE_EVENT, OnEvent);
	}
}

