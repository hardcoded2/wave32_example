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
	public class GestureStaticEventHandler : MonoBehaviour {
		private const string LOG_TAG = "GestureStaticEventHandler";
		private void DEBUG(string msg)
		{
			if (Log.EnableDebugLog)
				Log.d (LOG_TAG, this.Hand + ", " + msg, true);
		}

		private readonly string[] handGestureStrings = new string[] {
			"Invalid",	//WVR_HandGestureType_Invalid         = 0,    /**< The gesture is invalid. */
			"Unknown",	//WVR_HandGestureType_Unknown         = 1,    /**< Unknow gesture type. */
			"Fist",		//WVR_HandGestureType_Fist            = 2,    /**< Represent fist gesture. */
			"Five",		//WVR_HandGestureType_Five            = 3,    /**< Represent five gesture. */
			"OK",		//WVR_HandGestureType_OK              = 4,    /**< Represent ok gesture. */
			"Thumbup",	//WVR_HandGestureType_ThumbUp         = 5,    /**< Represent thumb up gesture. */
			"IndexUp"	//WVR_HandGestureType_IndexUp         = 6,    /**< Represent index up gesture. */
		};

		public WaveVR_GestureManager.EGestureHand Hand = WaveVR_GestureManager.EGestureHand.LEFT;
		private WVR_HandGestureType currentGesture = WVR_HandGestureType.WVR_HandGestureType_Invalid;

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
				if (this.Hand == WaveVR_GestureManager.EGestureHand.LEFT)
					WaveVR_Utils.Event.Listen (WaveVR_Utils.Event.HAND_STATIC_GESTURE_LEFT, onStaticGestureHandle);
				if (this.Hand == WaveVR_GestureManager.EGestureHand.RIGHT)
					WaveVR_Utils.Event.Listen (WaveVR_Utils.Event.HAND_STATIC_GESTURE_RIGHT, onStaticGestureHandle);
				mEnabled = true;
			}
		}

		void OnDisable()
		{
			if (mEnabled)
			{
				if (this.Hand == WaveVR_GestureManager.EGestureHand.LEFT)
					WaveVR_Utils.Event.Remove (WaveVR_Utils.Event.HAND_STATIC_GESTURE_LEFT, onStaticGestureHandle);
				if (this.Hand == WaveVR_GestureManager.EGestureHand.RIGHT)
					WaveVR_Utils.Event.Remove (WaveVR_Utils.Event.HAND_STATIC_GESTURE_RIGHT, onStaticGestureHandle);
				mEnabled = false;
			}
		}

		//private WVR_HandPoseData_t handPoseData = new WVR_HandPoseData_t();
		void Update()
		{
			if (gestureText == null || WaveVR_GestureManager.Instance == null)
				return;

			gestureText.text = this.Hand + " Gesture: " + handGestureStrings[(int)currentGesture];

			/*bool valid_data = WaveVR_GestureManager.Instance.GetHandPoseData(ref handPoseData);
			if (valid_data)
			{
				if (this.Hand == WaveVR_GestureManager.EGestureHand.RIGHT)
					gestureText.text += ", right strength: " + handPoseData.right.pinch.strength;
				if (this.Hand == WaveVR_GestureManager.EGestureHand.LEFT)
					gestureText.text += ", left strength: " + handPoseData.left.pinch.strength;
			}*/
		}

		private void onStaticGestureHandle(params object[] args)
		{
			currentGesture = (WVR_HandGestureType)args [0];
		}
	}
}
