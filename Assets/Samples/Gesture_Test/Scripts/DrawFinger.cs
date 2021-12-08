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

namespace WaveVRGesture {
	public class DrawFinger : MonoBehaviour {
		public GameObject TargetBone = null;
		public float FingerWidth = 0.001f;
		public Color FingerColor = Color.red;

		private LineRenderer finger = null;

		private Vector3 startPos = Vector3.zero;
		private Vector3 endPos = Vector3.zero;

		// Use this for initialization
		void Start () {
			if (this.TargetBone != null)
			{
				finger = this.gameObject.AddComponent<LineRenderer> ();
#if UNITY_2019_1_OR_NEWER
				finger.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
#else
				finger.material = new Material (Shader.Find ("Particles/Additive"));
#endif

#if UNITY_5_6_OR_NEWER
				finger.positionCount = 2;
#else
				finger.SetVertexCount (2);
#endif
			}
		}

		// Update is called once per frame
		void Update () {
			if (finger == null)
				return;

			finger.enabled = this.TargetBone.GetComponent<WaveVR_BonePose> ().Valid;

			startPos = transform.position;
			endPos = this.TargetBone.transform.position;

			finger.startColor = this.FingerColor;
			finger.endColor = this.FingerColor;
			finger.startWidth = this.FingerWidth;
			finger.endWidth = this.FingerWidth;
			finger.SetPosition(0, startPos);
			finger.SetPosition(1, endPos);
		}
	}
}
