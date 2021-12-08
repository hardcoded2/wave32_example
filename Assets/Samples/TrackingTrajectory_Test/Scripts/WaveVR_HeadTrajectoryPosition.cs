using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

[RequireComponent(typeof(Text))]
public class WaveVR_HeadTrajectoryPosition : MonoBehaviour {
	private StringBuilder sbText = new StringBuilder(511, 511);
	private Text headPositionText = null;
	private WaveVR.Device hmdDevice = null;

	void Start()
	{
		headPositionText = GetComponent<Text> ();
		hmdDevice = WaveVR.Instance.getDeviceByType (WaveVR_Controller.EDeviceType.Head);
	}

	void Update () {
		if (headPositionText != null)
		{
			#if NET_2_0 || NET_2_0_SUBSET
			sbText.Length = 0;
			#else
			sbText.Clear();
			#endif

			sbText.Append ("Head Position (")
				.Append (hmdDevice.rigidTransform.pos.x)
				.Append (", ")
				.Append (hmdDevice.rigidTransform.pos.y)
				.Append (", ")
				.Append (hmdDevice.rigidTransform.pos.z)
				.Append (")");

			headPositionText.text = sbText.ToString ();
		}
	}
}
