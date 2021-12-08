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

public class Controller : MonoBehaviour {
	private static string LOG_TAG = "Controller";
	public WVR_DeviceType device = WVR_DeviceType.WVR_DeviceType_HMD;
	public GameObject ControlledObject;

	// Update is called once per frame
	void Update () {
		if (WaveVR_Controller.Input(device).GetPressDown(WVR_InputId.WVR_InputId_Alias1_Trigger))
		{
			Log.d(LOG_TAG, "button " + WVR_InputId.WVR_InputId_Alias1_Trigger + " press down");

			ControlledObject.SetActive(true);
		}
	}
}
