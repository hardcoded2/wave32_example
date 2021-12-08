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
using UnityEngine.SceneManagement;
using System;

public class CameraTextureSample : MonoBehaviour {
	public bool started = false;
	public Texture2D nativeTexture = null;
	private static string LOG_TAG = "CameraTextureSample";
	System.IntPtr textureid ;
	private MeshRenderer meshrenderer;
	private bool updated = true;
	private WaveVR_PermissionManager pmInstance = null;
	private bool permission_granted = false;

	// Use this for initialization
	void Start () {
		started = false;
		nativeTexture = new Texture2D(1280, 400);
	}

	public void startCamera()
	{
		pmInstance = WaveVR_PermissionManager.instance;
		permission_granted = pmInstance.isPermissionGranted("android.permission.CAMERA");
		if (started==false && permission_granted)
		{
			SceneManager.sceneUnloaded += OnSceneUnloaded;
			WaveVR_CameraTexture.StartCameraCompletedDelegate += startCameraeCompleted;
			WaveVR_CameraTexture.UpdateCameraCompletedDelegate += updateTextureCompleted;
			WaveVR_CameraTexture.instance.startCamera(false);

			textureid = nativeTexture.GetNativeTexturePtr();
			meshrenderer = GetComponent<MeshRenderer>();
			meshrenderer.material.mainTexture = nativeTexture;
			updated = true;
			Log.d(LOG_TAG, "startCamera");
		}
		else
		{
			Log.e(LOG_TAG, "startCamera fail, camera is already started or permissionGranted is failed");
		}
	}


	private void OnSceneUnloaded(Scene current)
	{
		Log.d(LOG_TAG, "OnSceneUnloaded and stopCamera: " + current);
		stopCamera();
	}

	public void stopCamera()
	{
		WaveVR_CameraTexture.UpdateCameraCompletedDelegate -= updateTextureCompleted;
		WaveVR_CameraTexture.StartCameraCompletedDelegate -= startCameraeCompleted;
		started = false;
		Log.d(LOG_TAG, "stopCamera");
		WaveVR_CameraTexture.instance.stopCamera();
	}

	void startCameraeCompleted(bool result)
	{
		Log.d(LOG_TAG, "startCameraeCompleted, start? " + result);
		started = result;
	}

	void updateTextureCompleted(IntPtr textureId)
	{
		Log.d(LOG_TAG, "updateTextureCompleted, textureid = " + textureId);
		meshrenderer.material.mainTexture = nativeTexture;
		updated = true;
	}

	void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			if (started)
			{
				Log.d(LOG_TAG, "Pause(" + pauseStatus + ") and stop camera");
				stopCamera();
			}
		}
	}

	void OnDestroy()
	{
		Log.d(LOG_TAG, "OnDestroy stopCamera");
		stopCamera();
	}

	// Update is called once per frame
	void Update () {
		if (started && updated)
		{
			updated = false;
			WaveVR_CameraTexture.instance.updateTexture(textureid);
		}
	}
}
