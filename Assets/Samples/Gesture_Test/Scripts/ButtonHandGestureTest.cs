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
using UnityEngine.UI;
using WVR_Log;

namespace WaveVRGesture {
	public class ButtonHandGestureTest : MonoBehaviour {
		private Button btn = null;
		private Text btnText = null;

		// Use this for initialization
		void Start () {
			btn = gameObject.GetComponent<Button> ();
			btnText = gameObject.GetComponentInChildren<Text> ();
		}

		WaveVR_Utils.HandGestureStatus hand_gesture_status = WaveVR_Utils.HandGestureStatus.NOT_START;
		void Update () {
			hand_gesture_status = WaveVR_GestureManager.Instance.GetHandGestureStatus ();
			if (btnText != null && btn != null)
			{
				if (hand_gesture_status == WaveVR_Utils.HandGestureStatus.AVAILABLE)
				{
					btn.interactable = true;
					btnText.text = "Disable Hand Gesture";
				} else if (
					hand_gesture_status == WaveVR_Utils.HandGestureStatus.NOT_START ||
					hand_gesture_status == WaveVR_Utils.HandGestureStatus.START_FAILURE)
				{
					btn.interactable = true;
					btnText.text = "Enable Hand Gesture";
				}
				else
				{
					btn.interactable = false;
					btnText.text = "Processing Gesture";
				}
			}
		}

		void OnEnable()
		{
			WaveVR_Utils.Event.Listen (WaveVR_Utils.Event.HAND_GESTURE_STATUS, OnGestureStatus);
		}

		void OnDisable()
		{
			WaveVR_Utils.Event.Remove (WaveVR_Utils.Event.HAND_GESTURE_STATUS, OnGestureStatus);
		}

		private void OnGestureStatus(params object[] args)
		{
			WaveVR_Utils.HandGestureStatus status = (WaveVR_Utils.HandGestureStatus)args [0];
			Log.d ("ButtonHandGestureTest", "Hand gesture status: " + status, true);
		}

		public void EnableHandGesture()
		{
			if (hand_gesture_status == WaveVR_Utils.HandGestureStatus.AVAILABLE)
				WaveVR_GestureManager.Instance.EnableHandGesture = false;
			else
				WaveVR_GestureManager.Instance.EnableHandGesture = true;
		}
	}
}
