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
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using wvr;
using WVR_Log;

public class ControllerTipClickHandler : MonoBehaviour {
	public Text Dominant;
	public Text NonDominant;

	void OnEnable()
	{
		WaveVR_Utils.Event.Listen(WaveVR_Utils.Event.DEVICE_CONNECTED, onDeviceConnected);
	}

	void OnDisable()
	{
		WaveVR_Utils.Event.Remove(WaveVR_Utils.Event.DEVICE_CONNECTED, onDeviceConnected);
	}

	private void onDeviceConnected(params object[] args)
	{
		WVR_DeviceType _type = (WVR_DeviceType)args[0];
		Log.d("ControllerTipsTest", "onDeviceConnected type: " + _type);
		showControllerName(_type);
	}

	private void showControllerName(WVR_DeviceType _type)
	{
		string showText = "";
		if (_type == WaveVR_Controller.Input(WaveVR_Controller.EDeviceType.Dominant).DeviceType)
		{
			string str = WaveVR_Utils.GetControllerName(WaveVR_Controller.EDeviceType.Dominant);
			Log.d("ControllerTipsTest", "Dominant Controller name: " + str);

			if (str.Equals(""))
			{
				showText = "Controller can't get name!";
			}
			else
			{
				showText = str;
			}

			if (Dominant != null)
				Dominant.text = showText;
		}
		if (_type == WaveVR_Controller.Input(WaveVR_Controller.EDeviceType.NonDominant).DeviceType)
		{
			string str = WaveVR_Utils.GetControllerName(WaveVR_Controller.EDeviceType.NonDominant);
			Log.d("ControllerTipsTest", "Non-Dominant Controller name: " + str);

			if (str.Equals(""))
			{
				showText = "Controller can't get name!";
			}
			else
			{
				showText = str;
			}

			if (NonDominant != null)
				NonDominant.text = showText;
		}
	}

	public void Start()
	{
		Log.d("ControllerTipsTest", "Resume");
		showControllerName(WVR_DeviceType.WVR_DeviceType_Controller_Left);
		showControllerName(WVR_DeviceType.WVR_DeviceType_Controller_Right);
	}

	public void LoadScene2()
	{
		Log.d("ControllerTipsTest", "Controller Tips test load scene 2");

		SceneManager.LoadScene("ControllerTips_Test2");
	}

	public void OnApplicationPause(bool pause)
	{
		if (!pause)
		{
			Log.d("ControllerTipsTest", "Resume");
			showControllerName(WVR_DeviceType.WVR_DeviceType_Controller_Left);
			showControllerName(WVR_DeviceType.WVR_DeviceType_Controller_Right);
		}
	}
}
