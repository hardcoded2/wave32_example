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
using wvr;

namespace WaveVRGesture
{
	public class PinchStrengthText : MonoBehaviour
	{
		public WaveVR_GestureManager.EGestureHand Hand = WaveVR_GestureManager.EGestureHand.RIGHT;
		private Text m_Text = null;
		private void OnEnable()
		{
			m_Text = gameObject.GetComponent<Text>();
		}

		private WVR_HandPoseData_t handPoseData = new WVR_HandPoseData_t();
		void Update()
		{
			if (m_Text == null || WaveVR_GestureManager.Instance == null)
				return;

			bool valid_data = WaveVR_GestureManager.Instance.GetHandPoseData(ref handPoseData);
			float strength = (this.Hand == WaveVR_GestureManager.EGestureHand.RIGHT ?
					handPoseData.right.pinch.strength : handPoseData.left.pinch.strength
				);

			if (0.95f <= strength && strength <= 1) strength = 1;
			if (0.85f <= strength && strength < 0.95f) strength = 0.9f;
			if (0.75f <= strength && strength < 0.85f) strength = 0.8f;
			if (0.65f <= strength && strength < 0.75f) strength = 0.7f;
			if (0.55f <= strength && strength < 0.65f) strength = 0.6f;
			if (0.45f <= strength && strength < 0.55f) strength = 0.5f;
			if (0.35f <= strength && strength < 0.45f) strength = 0.4f;
			if (0.25f <= strength && strength < 0.35f) strength = 0.3f;
			if (0.15f <= strength && strength < 0.25f) strength = 0.2f;
			if (0.05f <= strength && strength < 0.15f) strength = 0.1f;
			if (strength < 0.05f) strength = 0;

			if (valid_data)
				m_Text.text = strength.ToString();
		}
	}
}
