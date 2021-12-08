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
using UnityEngine.SceneManagement;
using WVR_Log;
using System;

public class MasterSceneManager : MonoBehaviour
{
	private static string LOG_TAG = "MasterSceneManager";
	public static Stack previouslevel;
	public static MasterSceneManager Instance;
	public static GameObject bs, hs;

	private static string[] scenes = new string[] {
		"Main",
		"CameraTexture_disableSyncPose_Test",
		"PermissionMgr_Test",
		"MotionController_Test",
		"ControllerInputMode_Test",
		"VRInputModule_Test",
		"MouseInputModule_Test",
		"InAppRecenter",
		"Battery_Test",
		"PassengerMode_Test",
		"InteractionMode_Test",
		"Resource_Test1",
		"ScreenShot_Test",
		"RenderModel_Test",
		"ControllerInstanceSence_test1",
		"ControllerTips_Test",
		"RenderMask_Test",
		"Teleport_Test",
		"Button_Test",
		"SoftwareIpd_Test",
		"DynamicResolutionScene1_Test",
		"SystemRecording_Test",
		"Overlay_Test",
		"PosePredict_Test",
		"FoveatedTest",
		"HeadTrajectory_Test",
		"FadeOut_Test",
		"AQDR_Test"
	};

	private static string[] scene_names = new string[] {
		"301 SeaOfCubes",
		"302 CameraTexture Test (Disable SyncPose)",
		"303 PermissionMgr Test",
		"304 MotionController Test",
		"305 ControllerInputMode Test",
		"306 VR InputModule Test",
		"307 Mouse InputModule Test",
		"101 InAppRecenter Test",
		"308 Battery Test",
		"102 PassengerMode Test",
		"103 InteractionMode Test",
		"309 Resource Test",
		"201 ScreenShot Test",
		"310 RenderModel Test",
		"311 ControllerInstanceMgr Test",
		"312 Controller Tips Test",
		"202 Render Mask Test",
		"313 Teleport Test",
		"314 Button Test",
		"203 SoftwareIpd Test",
		"204 DynamicResolution Test",
		"205 SystemRecording_Test",
		"206 Overlay_Test",
		"104 PosePredict Test",
		"207 Foveation Test",
		"315 Head Trajectory Test",
		"208 FadeOut Test",
		"209 AQDR_Test"
	};

	private static string[] scene_paths = new string[] {
		"Assets/Samples/SeaOfCube/scenes/Main.unity",
		"Assets/Samples/CameraTexture_disableSyncPose_Test/Scenes/CameraTexture_disableSyncPose_Test.unity",
		"Assets/Samples/PermissionMgr_Test/scenes/PermissionMgr_Test.unity",
		"Assets/Samples/MotionController_Test/Scenes/MotionController_Test.unity",
		"Assets/Samples/ControllerInputMode_Test/ControllerInputMode_Test.unity",
		"Assets/Samples_Internal/ControllerInputModule_Test/Scenes/VRInputModule_Test.unity",
		"Assets/Samples/ControllerInputModule_Test/Scenes/MouseInputModule_Test.unity",
		"Assets/Samples/InAppRecenter_Test/scene/InAppRecenter.unity",
		"Assets/Samples/Battery_Test/Scenes/Battery_Test.unity",
		"Assets/Samples_Internal/PassengerMode_Test/scenes/PassengerMode_Test.unity",
		"Assets/Samples_Internal/InteractionMode_Test/scene/InteractionMode_Test.unity",
		"Assets/Samples/Resource2_Test/Scenes/Resource_Test1.unity",
		"Assets/Samples/ScreenShot_Test/Scenes/ScreenShot_Test.unity",
		"Assets/Samples/RenderModel_Test/scenes/RenderModel_test.unity",
		"Assets/Samples/ControllerInstanceMgr_Test/scenes/ControllerInstanceSence_test1.unity",
		"Assets/Samples/ControllerTips_Test/Scenes/ControllerTips_Test.unity",
		"Assets/Samples/RenderMask_Test/Scene/RenderMask_Test.unity",
		"Assets/Samples/Teleport_Test/Scenes/Teleport_Test.unity",
		"Assets/Samples/Button_Test/Scenes/Button_Test.unity",
		"Assets/Samples_Internal/SoftwareIpd_Test/Scenes/SoftwareIpd_Test.unity",
		"Assets/Samples/DynamicResolution_Test/Scenes/DynamicResolutionScene1_Test.unity",
		"Assets/Samples_Internal/SystemRecording_Test/Scenes/SystemRecording_Test.unity",
		"Assets/Samples/Overlay_Test/Scenes/Overlay_Test.unity",
		"Assets/Samples/PosePredict_Test/Scenes/PosePredict_Test.unity",
		"Assets/Samples/FoveatedRendering_Test/Scenes/FoveatedTest.unity",
		"Assets/Samples/TrackingTrajectory_Test/Scenes/HeadTrajectory_Test.unity",
		"Assets/Samples/FadeOut_Test/Scenes/FadeOut_Test.unity",
		"Assets/Samples/AdaptiveQualityDynamicResolution_Test/Scenes/AQDR_Test.unity"
	};

	private static int scene_idx = 0;

	private void Awake()
	{
		if (Instance == null)
		{
			DontDestroyOnLoad(this);
			Instance = this;
			previouslevel = new Stack();
			bs = GameObject.Find("BackButton");
			if (bs != null)
			{
				DontDestroyOnLoad(bs);
				bs.SetActive(false);
			}
			hs = GameObject.Find("HelpButton");
			if (hs != null)
			{
				DontDestroyOnLoad(hs);
				hs.SetActive(false);
			}
		}
		else
		{
			previouslevel.Clear();
			if (bs != null)
				bs.SetActive (false);
			if (hs != null)
				hs.SetActive (false);
			GameObject dd = GameObject.Find("BackButton");
			if (dd != null)
				dd.SetActive (false);
			dd = GameObject.Find("HelpButton");
			if (dd != null)
				dd.SetActive (false);
		}

		GameObject ts = GameObject.Find("SceneText");
		if (ts != null)
		{
			Text sceneText = ts.GetComponent<Text>();
			if (sceneText != null)
			{
				sceneText.text = scene_names[scene_idx];
			}
		}

		GameObject ts2 = GameObject.Find("SceneText2");
		if (ts2 != null)
		{
			Text sceneText = ts2.GetComponent<Text>();
			if (sceneText != null)
			{
				sceneText.text = scene_names[scene_idx];
			}
		}

		{
			GameObject vrstring = GameObject.Find("UnityBuildTime");
			if (vrstring != null)
			{
				Text sceneText = vrstring.GetComponent<Text>();
				if (sceneText != null)
				{
					sceneText.text = "Unity version: " + Application.unityVersion;
				}
			}
		}

#if UNITY_EDITOR
		if (Application.isEditor)
			return;
#endif

		try
		{
			AndroidJavaClass jc = new AndroidJavaClass("com.htc.vr.BuildConfig");
			String buildtime = jc.GetStatic<String>("VR_VERSION");
			GameObject vrstring = GameObject.Find("VRBuildTime");
			if (vrstring != null)
			{
				Text sceneText = vrstring.GetComponent<Text>();
				if (sceneText != null)
				{
					sceneText.text = "VR Client AAR: " + buildtime;
				}
			}
		}
		catch (Exception e)
		{
			Log.e(LOG_TAG, e.Message);
		}

		try
		{
			AndroidJavaClass jc = new AndroidJavaClass("com.htc.vr.permission.client.BuildConfig");
			String buildtime = jc.GetStatic<String>("AAR_BUILDTIME");
			GameObject vrstring = GameObject.Find("PermissionBuildTime");
			if (vrstring != null)
			{
				Text sceneText = vrstring.GetComponent<Text>();
				if (sceneText != null)
				{
					sceneText.text = "Permission AAR: " + buildtime;
				}
			}
		}
		catch (Exception e)
		{
			Log.e(LOG_TAG, e.Message);
		}

		try
		{
			AndroidJavaClass jc = new AndroidJavaClass("com.htc.vr.ime.client.BuildConfig");
			String buildtime = jc.GetStatic<String>("AAR_BUILDTIME");
			GameObject vrstring = GameObject.Find("IMEBuildTime");
			if (vrstring != null)
			{
				Text sceneText = vrstring.GetComponent<Text>();
				if (sceneText != null)
				{
					sceneText.text = "IME AAR: " + buildtime;
				}
			}
		}
		catch (Exception e)
		{
			Log.e(LOG_TAG, e.Message);
		}

		try
		{
			AndroidJavaClass jc = new AndroidJavaClass("com.htc.vr.unity.pidemo.BuildConfig");
			String buildtime = jc.GetStatic<String>("AAR_BUILDTIME");
			GameObject vrstring = GameObject.Find("PiDemoBuildTime");
			if (vrstring != null)
			{
				Text sceneText = vrstring.GetComponent<Text>();
				if (sceneText != null)
				{
					sceneText.text = "PiDemo AAR: " + buildtime;
				}
			}
		}
		catch (Exception e)
		{
			Log.e(LOG_TAG, e.Message);
		}

		try
		{
			AndroidJavaClass jc = new AndroidJavaClass("com.htc.vr.unity.resdemo.BuildConfig");
			String buildtime = jc.GetStatic<String>("AAR_BUILDTIME");
			GameObject vrstring = GameObject.Find("ResDemoBuildTime");
			if (vrstring != null)
			{
				Text sceneText = vrstring.GetComponent<Text>();
				if (sceneText != null)
				{
					sceneText.text = "ResDemo AAR: " + buildtime;
				}
			}
		}
		catch (Exception e)
		{
			Log.e(LOG_TAG, e.Message);
		}

		try
		{
			AndroidJavaClass jc = new AndroidJavaClass("com.htc.vr.unity.resindicator.BuildConfig");
			String buildtime = jc.GetStatic<String>("AAR_BUILDTIME");
			GameObject vrstring = GameObject.Find("ResIndicatorBuildTime");
			if (vrstring != null)
			{
				Text sceneText = vrstring.GetComponent<Text>();
				if (sceneText != null)
				{
					sceneText.text = "ResIndicator AAR: " + buildtime;
				}
			}
		}
		catch (Exception e)
		{
			Log.e(LOG_TAG, e.Message);
		}

		try
		{
			AndroidJavaClass jc = new AndroidJavaClass("com.htc.vr.unity.BuildConfig");
			String buildtime = jc.GetStatic<String>("AAR_BUILDTIME");
			GameObject vrstring = GameObject.Find("UnityPluginBuildTime");
			if (vrstring != null)
			{
				Text sceneText = vrstring.GetComponent<Text>();
				if (sceneText != null)
				{
					sceneText.text = "Unity Plugin AAR: " + buildtime;
				}
			}
		}
		catch (Exception e)
		{
			Log.e(LOG_TAG, e.Message);
		}
	}

	public void ChangeToNext()
	{
		int tmp = scene_idx;
		while (true)
		{
			scene_idx++;

			if (scene_idx >= scenes.Length)
				scene_idx = 0;
			if (tmp == scene_idx) // Avoid infinite loop
				break;
			string scene_path = scene_paths[scene_idx];
			if (SceneUtility.GetBuildIndexByScenePath(scene_path) == -1)
			{
				Log.i(LOG_TAG, "ChangeToNext: Scene path not existed : " + scene_path);
				continue;
			}
			break;
		}

		GameObject ts = GameObject.Find("SceneText");
		if (ts != null)
		{
			Text sceneText = ts.GetComponent<Text>();
			if (sceneText != null)
			{
				sceneText.text = scene_names[scene_idx];
			}
		}

		GameObject ts2 = GameObject.Find("SceneText2");
		if (ts2 != null)
		{
			Text sceneText = ts2.GetComponent<Text>();
			if (sceneText != null)
			{
				sceneText.text = scene_names[scene_idx];
			}
		}
	}

	public void ChangeToPrevious()
	{
		int tmp = scene_idx;
		while (true)
		{
			scene_idx--;

			if (scene_idx < 0)
				scene_idx = scenes.Length - 1;
			if (tmp == scene_idx) // Avoid infinite loop
				break;
			string scene_path = scene_paths[scene_idx];
			if (SceneUtility.GetBuildIndexByScenePath(scene_path) == -1)
			{
				Log.i(LOG_TAG, "ChangeToPrevious: Scene path not existed : " + scene_path);
				continue;
			}
			break;
		}

		GameObject ts = GameObject.Find("SceneText");
		if (ts != null)
		{
			Text sceneText = ts.GetComponent<Text>();
			if (sceneText != null)
			{
				sceneText.text = scene_names[scene_idx];
			}
		}

		GameObject ts2 = GameObject.Find("SceneText2");
		if (ts2 != null)
		{
			Text sceneText = ts2.GetComponent<Text>();
			if (sceneText != null)
			{
				sceneText.text = scene_names[scene_idx];
			}
		}
	}

	public void LoadPrevious()
	{
		if (previouslevel.Count > 0)
		{
			string scene_name = previouslevel.Pop().ToString();
			if (previouslevel.Count != 0)
			{
				hs.SetActive (true);
			}
			SceneManager.LoadScene(scene_name);
		}
	}

	public void LoadScene()
	{
		string scene = scenes[scene_idx];
		string scene_path = scene_paths[scene_idx];
		bs.SetActive (true);
		LoadNext(scene, scene_path);
	}

	public void loadHelpScene()
	{
		string help_scene = SceneManager.GetActiveScene().name + "_Help";
		LoadNext(help_scene, "");
	}

	private void LoadNext(string scene, string scene_path)
	{
		previouslevel.Push(SceneManager.GetActiveScene().name);
		if (scene_path.Length > 6)
		{
			scene_path = scene_path.Remove(scene_path.Length - 6);
			scene_path += "_Help.unity";
			if (SceneUtility.GetBuildIndexByScenePath(scene_path) != -1)
			{
				hs.SetActive (true);
			}
			else
			{
				hs.SetActive (false);
			}
		}
		else
		{
			hs.SetActive (false);
		}
		SceneManager.LoadScene(scene);
	}

	public void ChooseQuit()
	{
		Application.Quit();
	}
}
