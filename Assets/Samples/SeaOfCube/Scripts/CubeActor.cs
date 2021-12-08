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

public class CubeActor : MonoBehaviour {
	public WaveVR_Controller.EDeviceType Type = WaveVR_Controller.EDeviceType.Dominant;
	public AudioSource AudioData;

	// Use this for initialization
	void Start () {
		
	}

	bool _size = false;
	Vector3 scale_big = new Vector3(1.5f, 1.5f, 1.5f), scale_small = new Vector3(1, 1, 1);
	Vector2 axis = Vector2.zero;
	void Update () {
		if (WaveVR_Controller.Input (this.Type).GetPressDown (WVR_InputId.WVR_InputId_Alias1_Touchpad))
		{
			_size = !_size;
			this.AudioData.Play (0);
			if (_size)
				transform.localScale = scale_big;
			else
				transform.localScale = scale_small;
		}
		if (WaveVR_Controller.Input (this.Type).GetTouchDown (WVR_InputId.WVR_InputId_Alias1_Touchpad))
		{
			axis = WaveVR_Controller.Input (this.Type).GetAxis (WVR_InputId.WVR_InputId_Alias1_Touchpad);
			transform.Rotate (axis.y * 30, axis.x * 30, 0);
		}
	}
}
