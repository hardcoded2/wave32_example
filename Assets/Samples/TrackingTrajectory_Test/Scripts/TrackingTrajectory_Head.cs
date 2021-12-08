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
using WaveVR_Log;
using UnityEngine.UI;
using wvr;
using UnityEngine.SceneManagement;

public class TrackingTrajectory_Head : MonoBehaviour {

	private const string LOG_TAG = "WaveVR_TrackingTrajectory_Head";

	public static int isTrackingTrajectoryId = 0;

	public GameObject HeadTrackingTrajectoryCanvas = null;

	public static bool isTrackingTrajectory = false;
	public static bool isDrawRuler = false;

	private void UpdateIsTrackingTrajectory()
	{
		if ( true == WaveVR_Controller.Input(WaveVR_Controller.EDeviceType.Dominant).GetPressDown(WVR_InputId.WVR_InputId_Alias1_Trigger ))
		{
			Whiteboard.isClearWhiteboard = true;
			isTrackingTrajectory = true;
			isDrawRuler = true;
		}
	}

	void Start () {
	}

	private const int Yaw 	= 	1;
	private const int Pitch = 	2;
	private const int Roll	=	3;

	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR && UNITY_ANDROID
		if (!WaveVR.EnableSimulator) {
#endif
			UpdateIsTrackingTrajectory ();

			Text[] textObj =  HeadTrackingTrajectoryCanvas.GetComponentsInChildren<Text> ();

			for (int index = 0 ; index < textObj.Length ; ++index) {

				if (textObj [index].gameObject.name.Equals ("Head_Position_X")) {

					string position_x = "X: "
						+ (WaveVR.Instance.getDeviceByType (WaveVR_Controller.EDeviceType.Head).rigidTransform.pos.x * 100 ).ToString();

					textObj [index].text = position_x;
				}

				if (textObj [index].gameObject.name.Equals ("Head_Position_Y")) {

					string position_y = "Y: "
						+ (WaveVR.Instance.getDeviceByType (WaveVR_Controller.EDeviceType.Head).rigidTransform.pos.y * 100 ).ToString();
					textObj [index].text = position_y;
				}
				if (textObj [index].gameObject.name.Equals ("Head_Position_Z")) {

					string position_z = "Z: "
						+ (WaveVR.Instance.getDeviceByType (WaveVR_Controller.EDeviceType.Head).rigidTransform.pos.z * 100 * -1).ToString();
					textObj [index].text = position_z;
				}

				if (textObj [index].gameObject.name.Equals ("TrackingTrajectoryPositionType")) {

					textObj [index].text = "Tracking Trajector: Type";

					switch (isTrackingTrajectoryId) {

					case Yaw:
						textObj [index].text = "(Yellow) Current Mode : Yaw  < x-axis: X, y-axis: Z >";
						break;
					case Pitch:
						textObj [index].text = "(Green)Current Mode : Pitch < x-axis: Z, y-axis: Y >";
						break;
					case Roll:
						textObj [index].text = "(Blue)Current Mode : Roll < x-axis:X, y-axis: Y >";
						break;
					default:
						break;
					}
				}

				if (textObj [index].gameObject.name.Equals ("TrackingTrajectoryInstruction_1")) {

					textObj [index].text = "Draw Tracking Trajectory <Trigger: Draw> <Gray Ball: Stop>";
				}

				if (textObj [index].gameObject.name.Equals ("RuntimeVersion")) {

					textObj [index].text = "Runtime support version of 2.0.32";
				}
			}
#if UNITY_EDITOR && UNITY_ANDROID
		} // if(WaveVR.EnableSimulator
#endif
	}
}
