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
	public class ButtonHandTrackingTest : MonoBehaviour {
		private Button btn = null;
		private Text btnText = null;

		// Use this for initialization
		void Start () {
			btn = gameObject.GetComponent<Button> ();
			btnText = gameObject.GetComponentInChildren<Text> ();
		}

		WaveVR_Utils.HandTrackingStatus hand_tracking_status = WaveVR_Utils.HandTrackingStatus.NOT_START;
		void Update () {
			hand_tracking_status = WaveVR_GestureManager.Instance.GetHandTrackingStatus ();
			if (btnText != null && btn != null)
			{
				if (hand_tracking_status == WaveVR_Utils.HandTrackingStatus.AVAILABLE)
				{
					btn.interactable = true;
					btnText.text = "Disable Hand Tracking";
				} else if (
					hand_tracking_status == WaveVR_Utils.HandTrackingStatus.NOT_START ||
					hand_tracking_status == WaveVR_Utils.HandTrackingStatus.START_FAILURE)
				{
					btn.interactable = true;
					btnText.text = "Enable Hand Tracking";
				}
				else
				{
					btn.interactable = false;
					btnText.text = "Processing Tracking";
				}
			}
		}

		void OnEnable()
		{
			WaveVR_Utils.Event.Listen (WaveVR_Utils.Event.HAND_TRACKING_STATUS, OnTrackingStatus);
		}

		void OnDisable()
		{
			WaveVR_Utils.Event.Remove (WaveVR_Utils.Event.HAND_TRACKING_STATUS, OnTrackingStatus);
		}

		private void OnTrackingStatus(params object[] args)
		{
			WaveVR_Utils.HandTrackingStatus status = (WaveVR_Utils.HandTrackingStatus)args [0];
			Log.d ("ButtonHandTrackingTest", "Hand tracking status: " + status, true);
		}

		public void EnableHandTracking()
		{
			if (hand_tracking_status == WaveVR_Utils.HandTrackingStatus.AVAILABLE)
				WaveVR_GestureManager.Instance.EnableHandTracking = false;
			else
				WaveVR_GestureManager.Instance.EnableHandTracking = true;
		}
	}
}
