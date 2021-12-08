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
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Text))]
public class showCameraInfo : MonoBehaviour {
	private static string LOG_TAG = "CameraTextureInfo";

	private Text textField;
	private bool cameraStarted = false;
	private bool isShow = false;
	string obj = "";

	// Use this for initialization
	void Start () {
		textField = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		setCameraStarted();
		if (isShow == true)
		{
			if (cameraStarted == true)
			{
				obj = "Camera image fomat : " + WaveVR_CameraTexture.instance.getImageFormat() + "\n" + "Camera image type : "
				+ WaveVR_CameraTexture.instance.getImageType() + "\n" + "Camera image width : " + WaveVR_CameraTexture.instance.getImageWidth() + "\n" +
				"Camera image height : " + WaveVR_CameraTexture.instance.getImageHeight();
				textField.text = obj;
			}
			else
			{
				textField.text = "Camera is not started.";
			}
		}
	}

	public void ShowInfo()
	{
		if (!isShow)
		{
			if (cameraStarted == true)
			{
				string obj = "Camera image fomat : " + WaveVR_CameraTexture.instance.getImageFormat() + "\n" + "Camera image type : "
				+ WaveVR_CameraTexture.instance.getImageType() + "\n" + "Camera imege width : " + WaveVR_CameraTexture.instance.getImageWidth() + "\n" +
				"Camera imege height : " + WaveVR_CameraTexture.instance.getImageHeight();
				Log.d(LOG_TAG, " ShowInfo" + obj);
				textField.text = obj;
			}
			else
			{
				textField.text = "Camera is not started.";
			}
			isShow = true;
		}
		else
		{
			isShow = false;
			textField.text = "";
		}

	}

	private void setCameraStarted()
	{
		cameraStarted = WaveVR_CameraTexture.instance.isStarted;
	}
}
