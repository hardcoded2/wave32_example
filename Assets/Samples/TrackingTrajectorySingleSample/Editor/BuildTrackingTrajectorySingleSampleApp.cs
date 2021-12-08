// "WaveVR SDK
// © 2017 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the WaveVR SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.IO;

public class BuildTrackingTrajectorySingleSampleApp
{
	private static string _destinationPath;
	private static void CustomizedCommandLine()
	{
		Dictionary<string, Action<string>> cmdActions = new Dictionary<string, Action<string>>
		{
			{
				"-destinationPath", delegate(string argument)
				{
					_destinationPath = argument;
				}
			}
		};

		Action<string> actionCache;
		string[] cmdArguments = Environment.GetCommandLineArgs();

		for (int count = 0; count < cmdArguments.Length; count++)
		{
			if (cmdActions.ContainsKey(cmdArguments[count]))
			{
				actionCache = cmdActions[cmdArguments[count]];
				actionCache(cmdArguments[count + 1]);
			}
		}

		if (string.IsNullOrEmpty(_destinationPath))
		{
			_destinationPath = Path.GetDirectoryName(Application.dataPath);
		}
	}

	private static void GeneralSettings()
	{
		PlayerSettings.Android.bundleVersionCode = 1;
		PlayerSettings.bundleVersion = "2.0.0";
		PlayerSettings.companyName = "HTC Corp.";
		PlayerSettings.defaultInterfaceOrientation = UIOrientation.LandscapeLeft;
		PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel22;
	}

	static void BuildApkAll()
	{
		CustomizedCommandLine();
		BuildTrackingTrajectorApk(_destinationPath + "/", false, false, true, true, true);
		BuildTrackingTrajectorApk(_destinationPath + "/armv7", false, false);
		BuildTrackingTrajectorApk(_destinationPath + "/arm64", false, false, true, false, true);
	}

	[UnityEditor.MenuItem("WaveVR/Build Android APK/Build TrackingTrajector.apk/32+64bit")]
	static void BuildTrackingTrajectorApk()
	{
		BuildTrackingTrajectorApk(null, false, false, true, true, true);
	}

	[UnityEditor.MenuItem("WaveVR/Build Android APK/Build TrackingTrajector.apk/32bit")]
	static void BuildTrackingTrajectorApk32()
	{
		BuildTrackingTrajectorApk("armv7", false, false);
	}

	[UnityEditor.MenuItem("WaveVR/Build Android APK/Build TrackingTrajector.apk/64bit")]
	static void BuildTrackingTrajectorApk64()
	{
		BuildTrackingTrajectorApk("arm64", false, false, true, false, true);
	}

	[UnityEditor.MenuItem("WaveVR/Build Android APK/Build+Dev+Run TrackingTrajector.apk/32+64bit")]
	static void BuildTrackingTrajectorDevAndRunApk()
	{
		BuildTrackingTrajectorApk(null, true, true, true, true, true);
	}

	[UnityEditor.MenuItem("WaveVR/Build Android APK/Build+Dev+Run TrackingTrajector.apk/32bit")]
	static void BuildTrackingTrajectorDevAndRunApk32()
	{
		BuildTrackingTrajectorApk("armv7", true, true);
	}

	[UnityEditor.MenuItem("WaveVR/Build Android APK/Build+Dev+Run TrackingTrajector.apk/64bit")]
	static void BuildTrackingTrajectorDevAndRunApk64()
	{
		BuildTrackingTrajectorApk("arm64", true, true, true, false, true);
	}


	public static void BuildTrackingTrajectorApk(string destPath, bool run, bool development, bool isIL2CPP = false, bool isSupport32 = true, bool isSupport64 = false)
	{
		string[] levels = {
			"Assets/Samples/TrackingTrajectory_Test/Scenes/TrackingTrajectory_Test.unity"
		};

		ApplyTrackingTrajectorApkInner(destPath, run, development, levels, isIL2CPP, isSupport32, isSupport64);
	}

	static void ApplyTrackingTrajectorPlayerSettings(bool isIL2CPP = false, bool isSupport32 = true, bool isSupport64 = false)
	{
		Debug.Log("ApplyTrackingTrajectorPlayerSettings");

		GeneralSettings();

		if (!isSupport32 && !isSupport64)
			isSupport32 = true;

		PlayerSettings.productName = "unity_trackingtrajector";

		#if UNITY_5_6_OR_NEWER
		PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, "com.vrm.unity.trackingtrajector");
		#else
		PlayerSettings.bundleIdentifier = "com.vrm.unity.VRTestApp";
		#endif
		Texture2D icon = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Samples/VRTestApp/Textures/test.png", typeof(Texture2D));
		if (icon == null)
			Debug.LogError("Fail to read app icon");

		Texture2D[] group = { icon, icon, icon, icon, icon, icon };

		PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Android, group);
		PlayerSettings.gpuSkinning = false;
		#if UNITY_2017_2_OR_NEWER
		PlayerSettings.SetMobileMTRendering(BuildTargetGroup.Android, true);
		#else
		PlayerSettings.mobileMTRendering = true;
		#endif
		PlayerSettings.graphicsJobs = true;

		// This can help check the Settings by text editor
		EditorSettings.serializationMode = SerializationMode.ForceText;

		// Enable VR support and singlepass
		WaveVR_Settings.SetVirtualRealitySupported(BuildTargetGroup.Android, true);
		var list = WaveVR_Settings.GetVirtualRealitySDKs(BuildTargetGroup.Android);
		if (!ArrayUtility.Contains<string>(list, WaveVR_Settings.WVRSinglePassDeviceName))
		{
			ArrayUtility.Insert<string>(ref list, 0, WaveVR_Settings.WVRSinglePassDeviceName);
		}
		WaveVR_Settings.SetVirtualRealitySDKs(BuildTargetGroup.Android, list);
		PlayerSettings.stereoRenderingPath = StereoRenderingPath.SinglePass;
		var symbols = WaveVR_Settings.GetDefineSymbols(BuildTargetGroup.Android);
		WaveVR_Settings.SetSinglePassDefine(BuildTargetGroup.Android, true, symbols);

		// Force use GLES31
		PlayerSettings.SetUseDefaultGraphicsAPIs(BuildTarget.Android, false);
		UnityEngine.Rendering.GraphicsDeviceType[] apis = { UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3 };
		PlayerSettings.SetGraphicsAPIs(BuildTarget.Android, apis);
		PlayerSettings.openGLRequireES31 = true;
		PlayerSettings.openGLRequireES31AEP = true;

		if (isIL2CPP)
			PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
		else
			PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.Mono2x);
		if (isSupport32)
		{
			if (isSupport64)
				PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7 | AndroidArchitecture.ARM64;
			else
				PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7;
		}
		else
		{
			PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
		}

		PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel25;
		PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel26;

		AssetDatabase.SaveAssets();
	}

	private static void ApplyTrackingTrajectorApkInner(string destPath, bool run, bool development, string[] levels, bool isIL2CPP = false, bool isSupport32 = true, bool isSupport64 = false)
	{
		var apkName = "unity_trackingtrajector.apk";
		ApplyTrackingTrajectorPlayerSettings(isIL2CPP, isSupport32, isSupport64);

		string outputFilePath = string.IsNullOrEmpty(destPath) ? apkName
			: destPath + "/" + apkName;
		BuildPipeline.BuildPlayer(levels, outputFilePath, BuildTarget.Android, (run ? BuildOptions.AutoRunPlayer : BuildOptions.None) | (development ? BuildOptions.Development : BuildOptions.None));
	}
}
