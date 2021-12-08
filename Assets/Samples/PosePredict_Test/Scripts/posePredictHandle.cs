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
using UnityEngine.UI;
using wvr;
using WVR_Log;

public class posePredictHandle : MonoBehaviour
{
	//private static string LOG_TAG = "posePredictHandle";
	private bool HMD_posePredictisEnable = false;
	private bool Controller_posePredictisEnable = false;
	public Text textField;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void setHMDposePredictedEnable()
	{
		Interop.WVR_SetPosePredictEnabled(WVR_DeviceType.WVR_DeviceType_HMD, true, true);
		HMD_posePredictisEnable = true;
		this.description();
	}

	public void setHMDposePredictedDisable()
	{
		Interop.WVR_SetPosePredictEnabled(WVR_DeviceType.WVR_DeviceType_HMD, false, false);
		HMD_posePredictisEnable = false;
		this.description();
	}

	public void setControllerposePredictedEnable()
	{
		Interop.WVR_SetPosePredictEnabled(WVR_DeviceType.WVR_DeviceType_Controller_Right, true, true);
		Controller_posePredictisEnable = true;
		this.description();
	}

	public void setControllerposePredictedDisable()
	{
		Interop.WVR_SetPosePredictEnabled(WVR_DeviceType.WVR_DeviceType_Controller_Right, false, false);
		Controller_posePredictisEnable = false;
		this.description();
	}

	public void description()
	{
		this.textField.text = "HMD PosePredict enable status is : " + HMD_posePredictisEnable.ToString() + "\n Controller PosePredict enable status is : " + Controller_posePredictisEnable.ToString();
	}
	private void OnApplicationPause(bool pause)
	{
		if (pause == true)
		{
			Interop.WVR_SetPosePredictEnabled(WVR_DeviceType.WVR_DeviceType_HMD, false, false);
			Interop.WVR_SetPosePredictEnabled(WVR_DeviceType.WVR_DeviceType_Controller_Right, false, false);
			HMD_posePredictisEnable = false;
			Controller_posePredictisEnable = false;
		}
	}

	private void OnApplicationQuit()
	{
		Interop.WVR_SetPosePredictEnabled(WVR_DeviceType.WVR_DeviceType_HMD, false, false);
		Interop.WVR_SetPosePredictEnabled(WVR_DeviceType.WVR_DeviceType_Controller_Right, false, false);
		HMD_posePredictisEnable = false;
		Controller_posePredictisEnable = false;
	}
}
