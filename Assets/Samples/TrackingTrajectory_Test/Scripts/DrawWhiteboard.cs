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
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WVR_Log;
using wvr;
using System.IO;

public class DrawWhiteboard : MonoBehaviour {

	public GameObject head;
	public Whiteboard whiteboard;

	private float posX, posY, posZ;
	private const string LOG_TAG = "TRACKING_TRAJECTORY_WHITEBOARD_TAG";

	void Start () {

		if (null != GameObject.Find ("Whiteboard")) {
			this.whiteboard = GameObject.Find("Whiteboard").GetComponent<Whiteboard>();
		}
	}

	private const int Yaw 	=	1;
	private const int Pitch =	2;
	private const int Roll	=	3;

	void Update () {

		// let 150 dpi, 1 pixel = 0.02 cm
#if UNITY_EDITOR && UNITY_ANDROID
		if (!WaveVR.EnableSimulator) {
#endif
			posX = (WaveVR.Instance.getDeviceByType (WaveVR_Controller.EDeviceType.Head).rigidTransform.pos.x * 100);
			posY = (WaveVR.Instance.getDeviceByType (WaveVR_Controller.EDeviceType.Head).rigidTransform.pos.y * 100);
			posZ = (WaveVR.Instance.getDeviceByType (WaveVR_Controller.EDeviceType.Head).rigidTransform.pos.z * 100 * -1);

#if UNITY_EDITOR && UNITY_ANDROID
			} //if (!WaveVR.EnableSimulator)
#endif

		string head_draw = " x: " + posX + " y: " + posY + " z: " + posZ;

		Log.d (LOG_TAG, head_draw, true);

		if (TrackingTrajectory_Head.isTrackingTrajectory == true) {

			whiteboard.SetColor (Color.blue);

			int typeId = TrackingTrajectory_Head.isTrackingTrajectoryId;

			switch (typeId) {
			case Yaw:
					Log.d (LOG_TAG, "Draw Yaw on the whiteboard.", true);
					whiteboard.SetHeadTrackingTrajectoryPosition (-posX, -posZ);
					break;
			case Pitch:
					Log.d (LOG_TAG, "Draw Pitch on the whiteboard.", true);
					whiteboard.SetHeadTrackingTrajectoryPosition (-posZ, -posY);
					break;
			case Roll:
					Log.d (LOG_TAG, "Draw Roll on the whiteboard.", true);
					whiteboard.SetHeadTrackingTrajectoryPosition (-posX, -posY);
					break;
				default:
					break;
			}
		} else {
			;
		}
	}
}
