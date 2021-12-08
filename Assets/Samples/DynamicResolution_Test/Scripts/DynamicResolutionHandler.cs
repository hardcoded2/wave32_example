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
using UnityEngine.UI;

public class DynamicResolutionHandler : MonoBehaviour
{

	public Text textField;
	// Use this for initialization
	void Start() { }

	// Update is called once per frame
	void Update() { }

	public void LoadMultiPassScene()
	{
		Log.d("DynamicResolutionTest", "DynamicResolution test load multipass scene");
		SceneManager.LoadScene("DynamicResolutionScene2_Test");
	}

	public void LoadSinglePassScene()
	{
		Log.d("DynamicResolutionTest", "DynamicResolution test load multipass scene");
		SceneManager.LoadScene("DynamicResolutionScene1_Test");
	}

	public void SetDefaultValue()//1
	{
		SetDynamicResolutionValue(1);
		printScaleInfo(1);
	}

	public void SetMediumValue()
	{
		SetDynamicResolutionValue(0.5f);
		printScaleInfo(0.5f);
	}

	public void SetLowValue()
	{
		SetDynamicResolutionValue(0.3f);
		printScaleInfo(0.3f);
	}

	private void SetDynamicResolutionValue(float value)
	{
		WaveVR_Render.Instance.SetResolutionScale(value);
	}

	public void printScaleInfo(float value)
	{
		string str = string.Empty;
		str = "Scale : " + value.ToString();
		this.textField.text = str;
	}
}
