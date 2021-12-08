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

public class angle : MonoBehaviour {
	public GameObject target;
	// private float fAngle = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null)
		{
			/*
			Vector3 _targetForward = target.transform.rotation * Vector3.forward;
			fAngle = Vector3.Angle (_targetForward, Vector3.right);
			Debug.Log (fAngle);
			*/
			Debug.Log ("rotation " + target.transform.rotation.eulerAngles.x + ", " + target.transform.rotation.eulerAngles.y + ", " + target.transform.rotation.eulerAngles.z);
		}
	}
}
