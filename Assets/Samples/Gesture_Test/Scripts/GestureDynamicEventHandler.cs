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
using wvr;
using UnityEngine.UI;
using WVR_Log;

namespace WaveVRGesture
{
	public class GestureDynamicEventHandler : MonoBehaviour
	{
		private const string LOG_TAG = "GestureDynamicEventHandler";
		private void DEBUG(string msg)
		{
			if (Log.EnableDebugLog)
				Log.d(LOG_TAG, msg, true);
		}

		private Text gestureText = null;
		void Start()
		{
			gestureText = gameObject.GetComponent<Text>();
		}

		private bool mEnabled = false;
		void OnEnable()
		{
			if (!mEnabled)
			{
				WaveVR_Utils.Event.Listen(WaveVR_Utils.Event.HAND_DYNAMIC_GESTURE_LEFT, onDynamicGestureHandle);
				WaveVR_Utils.Event.Listen(WaveVR_Utils.Event.HAND_DYNAMIC_GESTURE_RIGHT, onDynamicGestureHandle);
				mEnabled = true;
			}
		}

		void OnDisable()
		{
			if (mEnabled)
			{
				WaveVR_Utils.Event.Remove(WaveVR_Utils.Event.HAND_DYNAMIC_GESTURE_LEFT, onDynamicGestureHandle);
				WaveVR_Utils.Event.Remove(WaveVR_Utils.Event.HAND_DYNAMIC_GESTURE_RIGHT, onDynamicGestureHandle);
				mEnabled = false;
			}
		}

		private void onDynamicGestureHandle(params object[] args)
		{
			WVR_EventType dynamic_gesture = (WVR_EventType)args[0];
			DEBUG("onDynamicGestureHandle() " + dynamic_gesture);
			if (gestureText != null)
				gestureText.text = "Dynamic: " + dynamic_gesture.ToString();
		}
	}
}
