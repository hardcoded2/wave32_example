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
using wvr;
using WVR_Log;

public class ClickHandler2 : MonoBehaviour {

	public void LoadScene1()
	{
		Log.d("RenderModelTest", "Render model load scene 1");

		SceneManager.LoadScene("RenderModel_test");
	}
}
