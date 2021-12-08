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

public class ScreenShotHandle : MonoBehaviour {
	private static string LOG_TAG = "ScreenShotHandle";
	private void DEBUG(string msg)
	{
		if (Log.EnableDebugLog)
			Log.d (LOG_TAG, msg, true);
	}

	public Text ScreenshotText = null;
	public Text PermissionText = null;

	private WaveVR_PermissionManager pmInstance = null;
	private bool permission_granted = false;

	void OnEnable()
	{
		pmInstance = WaveVR_PermissionManager.instance;
		permission_granted = pmInstance.isPermissionGranted ("android.permission.WRITE_EXTERNAL_STORAGE");

		if (this.PermissionText != null)
		{
			if (permission_granted)
				PermissionText.text = "Permission\n android.permission.EXTERNAL_STORAGE \nis granted.";
			else
				PermissionText.text = "Permission\n android.permission.EXTERNAL_STORAGE \nis NOT granted.";
		}
	}

	public void onDefaultClick()
	{
		DEBUG ("onDefaultClick() permission_granted: " + permission_granted);
		if (this.ScreenshotText != null)
			this.ScreenshotText.text = "";

		if (permission_granted)
		{
			WaveVR_Screenshot.requestScreenshot (WVR_ScreenshotMode.WVR_ScreenshotMode_Default, "Unity_Default");

			if (this.ScreenshotText != null)
				this.ScreenshotText.text = "Captured a DEFAULT screenshot.";
		} else
		{
			if (this.ScreenshotText != null)
				this.ScreenshotText.text = "CANNOT capture a DEFAULT screenshot.";
		}
	}

	public void onDistortedClick()
	{
		DEBUG ("onDistortedClick() permission_granted: " + permission_granted);
		if (this.ScreenshotText != null)
			this.ScreenshotText.text = "";

		if (permission_granted)
		{
			WaveVR_Screenshot.requestScreenshot (WVR_ScreenshotMode.WVR_ScreenshotMode_Distorted, "Unity_Distorted");

			if (this.ScreenshotText != null)
				this.ScreenshotText.text = "Captured a DISTORED screenshot.";
		} else
		{
			if (this.ScreenshotText != null)
				this.ScreenshotText.text = "CANNOT capture a DISTORED screenshot.";
		}
	}

	public void onRawClick()
	{
		DEBUG ("onRawClick() permission_granted: " + permission_granted);
		if (this.ScreenshotText != null)
			this.ScreenshotText.text = "";

		if (permission_granted)
		{
			WaveVR_Screenshot.requestScreenshot(WVR_ScreenshotMode.WVR_ScreenshotMode_Raw, "Unity_Raw");

			if (this.ScreenshotText != null)
				this.ScreenshotText.text = "Captured a RAW screenshot.";
		}
		else
		{
			if (this.ScreenshotText != null)
				this.ScreenshotText.text = "CANNOT capture a RAW screenshot.";
		}
	}
}
