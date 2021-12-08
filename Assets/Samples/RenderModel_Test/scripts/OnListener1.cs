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

public class OnListener1 : MonoBehaviour {
	void OnEnable()
	{
		WaveVR_Utils.Event.Listen(WaveVR_Utils.Event.DS_ASSETS_NOT_FOUND, onAssetNotFound);
	}

	void OnDisable()
	{
		WaveVR_Utils.Event.Remove(WaveVR_Utils.Event.DS_ASSETS_NOT_FOUND, onAssetNotFound);
	}

	// Use this for initialization
	void Start () {
		GameObject.Find("Asset").GetComponent<Text>().text = "";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void onAssetNotFound(params object[] args)
	{
		GameObject.Find("Asset").GetComponent<Text>().text = "Controller model asset is not found in DS.";
	}
}
