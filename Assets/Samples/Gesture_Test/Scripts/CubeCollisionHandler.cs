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
using WVR_Log;

namespace WaveVRGesture
{
	[DisallowMultipleComponent]
	public class CubeCollisionHandler : MonoBehaviour
	{
		private const string LOG_TAG = "CubeCollisionHandler";
		private void DEBUG(string msg)
		{
			if (Log.EnableDebugLog)
				Log.d(LOG_TAG, msg, true);
		}
		private Material blueCube = null, redCube = null;

		// Use this for initialization
		void Start()
		{
			blueCube = Resources.Load("Materials/blueCube") as Material;
			if (blueCube != null)
				DEBUG("Start() Loaded BlueCube.");
			redCube = Resources.Load("Materials/RedCube") as Material;
			if (redCube != null)
				DEBUG("Start() Loaded RedCube.");
		}

		// Update is called once per frame
		void Update()
		{

		}

		void OnCollisionEnter(Collision other)
		{
			gameObject.GetComponent<MeshRenderer>().material = redCube;
		}

		void OnCollisionExit(Collision other)
		{
			gameObject.GetComponent<MeshRenderer>().material = blueCube;
		}
	}
}
