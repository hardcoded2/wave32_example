// "WaveVR SDK 
// © 2017 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the WaveVR SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using wvr;
using WVR_Log;

public class HalfUVSquare_seeThrough: MonoBehaviour {
	public WaveVR_Controller.EDeviceType DeviceType = WaveVR_Controller.EDeviceType.Dominant;
	public Material material;
	private bool autoStart = false;

	private Mesh meshL = null;
	private Mesh meshR = null;
	private Mesh meshB = null;

	private bool updated = false;
	private bool started = false;
	private Texture2D nativeTexture = null;
	private System.IntPtr textureid;
	private MeshRenderer meshrenderer;
	private static string LOG_TAG = "HalfUVSquare_seeThrough";
	private WVR_PoseState_t cPose;

	// Use this for initialization
	void Start()
	{
		Log.d(LOG_TAG, "Start");
		nativeTexture = new Texture2D(1280, 400); // width/height of Focus
		autoStart = false;
		StartCoroutine(wait());
	}

	IEnumerator wait()
	{
		yield return new WaitForSeconds(2);
		startTest();
	}

	public void startCamera()
	{
		Log.d(LOG_TAG, "startCamera");
		if (started == false)
		{
			Log.d(LOG_TAG, "startCamera in fact");
			WaveVR_CameraTexture.StartCameraCompletedDelegate += onStartCameraCompleted;
			WaveVR_CameraTexture.UpdateCameraCompletedDelegate += updateTextureCompleted;
			WaveVR_CameraTexture.instance.startCamera(true);

			textureid = nativeTexture.GetNativeTexturePtr();
			meshrenderer = GetComponent<MeshRenderer>();
			meshrenderer.material.mainTexture = nativeTexture;
			updated = started;		
		}
		else
		{
			Log.e(LOG_TAG, "startCamera fail, camera is already started or permissionGranted is failed");
		}
	}

	public void stopCamera()
	{
		Log.d(LOG_TAG, "stopCamera");
		WaveVR_CameraTexture.StartCameraCompletedDelegate -= onStartCameraCompleted;
		WaveVR_CameraTexture.UpdateCameraCompletedDelegate -= updateTextureCompleted;

		WaveVR_CameraTexture.instance.stopCamera();
		started = false;
		updated = false;
	}

	void onStartCameraCompleted(bool result)
	{
		Log.d(LOG_TAG, "onStartCameraCompleted, result = " + result);
		started = result;
	}

	void updateTextureCompleted(IntPtr textureId)
	{
		bool isgetFramePose;
		isgetFramePose = WaveVR_CameraTexture.instance.getFramePose(ref cPose);

		Log.d(LOG_TAG, "updateTextureCompleted, timeStamp: " + cPose.PoseTimestamp_ns);
		updated = true;
	}

	void LateUpdate()
	{
#if UNITY_EDITOR
		if (Application.isEditor)
		{
			return;
		}
#endif

		if (updated && started)
		{
			updated = false;
			WaveVR_CameraTexture.instance.updateTexture(textureid);
		}
	}

	private float getImageRatio()
	{
		if (WaveVR_CameraTexture.instance.isStarted)
		{
			WVR_CameraImageType imgType = WaveVR_CameraTexture.instance.getImageType();
			uint width = WaveVR_CameraTexture.instance.getImageWidth();
			uint height = WaveVR_CameraTexture.instance.getImageHeight();
			Log.d(LOG_TAG, "getImageRatio type = " + imgType + ", width= " + width + ", height= " + height);
			if (imgType == WVR_CameraImageType.WVR_CameraImageType_SingleEye)
			{
				return ((float)height / (float)width);
			}
			else if (imgType == WVR_CameraImageType.WVR_CameraImageType_DualEye)
			{
				return ((float)height / ((float)width / 2));
			}
		}

		return 1.0f;
	}

	private Mesh CreateMesh(WVR_Eye eye)
	{
		if (eye == WVR_Eye.WVR_Eye_Both)
			return CreateMeshBoth();
		// ab
		// cd
		List<Vector3> vertices = new List<Vector3>();

		Log.d(LOG_TAG, "CreateMesh eye = " + eye);

		const float scale = 0.8f;
		float cameraOffset = 0.044f * 2f;
		float x = 1f * scale + cameraOffset;
		float y = 0.625f;

		vertices.Add(new Vector3(-x, y, 0.5f)); // a
		vertices.Add(new Vector3(x, y, 0.5f)); // b
		vertices.Add(new Vector3(-x, -y, 0.5f)); // c
		vertices.Add(new Vector3(x, -y, 0.5f)); // d

		List<Vector2> uvsL = new List<Vector2>();

		uvsL.Add(new Vector2(0, 0));
		uvsL.Add(new Vector2(0.5f, 0));
		uvsL.Add(new Vector2(0, 1));
		uvsL.Add(new Vector2(0.5f, 1));

		List<Vector2> uvsR = new List<Vector2>();

		uvsR.Add(new Vector2(0.5f, 0));
		uvsR.Add(new Vector2(1, 0));
		uvsR.Add(new Vector2(0.5f, 1));
		uvsR.Add(new Vector2(1, 1));

		List<int> indices = new List<int>();
		indices.Add(0);
		indices.Add(2);
		indices.Add(1);

		indices.Add(1);
		indices.Add(2);
		indices.Add(3);

		Mesh mesh = new Mesh();
		mesh.vertices = vertices.ToArray();
		if (eye == WVR_Eye.WVR_Eye_Left)
			mesh.SetUVs(0, uvsL);
		else if (eye == WVR_Eye.WVR_Eye_Right)
			mesh.SetUVs(0, uvsR);

		mesh.SetIndices(indices.ToArray(), MeshTopology.Triangles, 0);

		return mesh;
	}

	private Mesh CreateMeshBoth() {
		Log.d(LOG_TAG, "CreateMesh both eye");
		// ab
		// cd
		List<Vector3> vertices = new List<Vector3>();

		float x = 1f;
		float y = 0.625f;

		vertices.Add(new Vector3(-x -1 , y, 0.5f)); // a
		vertices.Add(new Vector3(-x +1, y, 0.5f)); // b
		vertices.Add(new Vector3(-x -1, -y, 0.5f)); // c
		vertices.Add(new Vector3(-x +1, -y, 0.5f)); // d

		vertices.Add(new Vector3(x - 1, y, 0.5f)); // a
		vertices.Add(new Vector3(x + 1, y, 0.5f)); // b
		vertices.Add(new Vector3(x - 1, -y, 0.5f)); // c
		vertices.Add(new Vector3(x + 1, -y, 0.5f)); // d

		List<Vector2> uvs = new List<Vector2>();
		uvs.Add(new Vector2(0, 0));
		uvs.Add(new Vector2(0.5f, 0));
		uvs.Add(new Vector2(0, 1));
		uvs.Add(new Vector2(0.5f, 1));

		uvs.Add(new Vector2(0.5f, 0));
		uvs.Add(new Vector2(1, 0));
		uvs.Add(new Vector2(0.5f, 1));
		uvs.Add(new Vector2(1, 1));

		List<int> indices = new List<int>();
		indices.Add(0);
		indices.Add(2);
		indices.Add(1);

		indices.Add(1);
		indices.Add(2);
		indices.Add(3);

		indices.Add(0 + 4);
		indices.Add(2 + 4);
		indices.Add(1 + 4);

		indices.Add(1 + 4);
		indices.Add(2 + 4);
		indices.Add(3 + 4);

		Mesh mesh = new Mesh();
		mesh.vertices = vertices.ToArray();
		mesh.SetUVs(0, uvs);

		mesh.SetIndices(indices.ToArray(), MeshTopology.Triangles, 0);

		return mesh;
	}

	void OnEnable()
	{
		Log.d(LOG_TAG, "OnEnable");
		WaveVR_Utils.Event.Listen(WaveVR_Utils.Event.SWIPE_EVENT, OnSwipe);
	}

	void OnDisable()
	{
		Log.d(LOG_TAG, "OnDisable");
		WaveVR_Utils.Event.Remove(WaveVR_Utils.Event.SWIPE_EVENT, OnSwipe);

		stopTest();
	}

	private void startTest()
	{
		Log.d(LOG_TAG, "startTest");
		startCamera();
		if (started)
		{
			Camera.onPostRender += MyPostRender;

			meshL = CreateMesh(WVR_Eye.WVR_Eye_Left);
			meshR = CreateMesh(WVR_Eye.WVR_Eye_Right);
			meshB = CreateMesh(WVR_Eye.WVR_Eye_Both);
		} else
		{
			Log.d(LOG_TAG, "startCamera fail");
		}
	}

	private void stopTest()
	{
		Log.d(LOG_TAG, "stopTest");
		stopCamera();

		Camera.onPostRender -= MyPostRender;

		Destroy(meshL);
		meshL = null;
		Destroy(meshR);
		meshL = null;
		Destroy(meshB);
		meshL = null;
	}

	void OnApplicationPause(bool pauseStatus)
	{
		Log.d(LOG_TAG, "OnApplicationPause, pause=" + pauseStatus);
		if (pauseStatus)
		{
			if (started)
			{
				stopTest();
				autoStart = true;
			}
		} else
		{
			if (autoStart)
			{
				StartCoroutine(wait());
				autoStart = false;
			}
		}
	}

	private void OnSwipe(params object[] args)
	{
		WVR_EventType _event = (WVR_EventType)args[0];
		WVR_DeviceType _type = (WVR_DeviceType)args[1];
		Log.d(LOG_TAG, "OnSwipe() _event: " + _event + ", _type: " + _type);

		WaveVR.Device _dev = WaveVR.Instance.getDeviceByType(this.DeviceType);
		if (_dev == null)
			return;
		if (_dev.type != _type)
			return;

		switch (_event)
		{
			case WVR_EventType.WVR_EventType_LeftToRightSwipe:
				startTest();
				break;
			case WVR_EventType.WVR_EventType_RightToLeftSwipe:
				stopTest();
				break;
			case WVR_EventType.WVR_EventType_DownToUpSwipe:
				quitGame();
				break;
			case WVR_EventType.WVR_EventType_UpToDownSwipe:
				break;
		}
	}

	public void MyPostRender(Camera cam) {

		material.mainTexture = nativeTexture;
		bool isgetFramePose;
		isgetFramePose = WaveVR_CameraTexture.instance.getFramePose(ref cPose);

		Log.d(LOG_TAG, "MyPostRender, timeStamp = " + cPose.PoseTimestamp_ns);

		if (cam.gameObject == WaveVR_Render.Instance.lefteye.gameObject)
		{
			material.SetPass(0);
			if (meshL != null)
			{
				Log.d(LOG_TAG, "SetPass(0)L, timeStamp = " + cPose.PoseTimestamp_ns);
				Graphics.DrawMeshNow(meshL, new Vector3(0, 0, 0), Quaternion.identity);
			}
		}
		if (cam.gameObject == WaveVR_Render.Instance.righteye.gameObject)
		{
			material.SetPass(0);
			if (meshR != null)
			{
				Log.d(LOG_TAG, "SetPass(0)R, timeStamp = " + cPose.PoseTimestamp_ns);
				Graphics.DrawMeshNow(meshR, new Vector3(0, 0, 0), Quaternion.identity);
			}
		}
		if (cam.gameObject == WaveVR_Render.Instance.botheyes.gameObject)
		{
			material.SetPass(1);

			if (meshB != null)
			{
				Log.d(LOG_TAG, "SetPass(1), timeStamp = " + cPose.PoseTimestamp_ns);
				Graphics.DrawMeshNow(meshB, new Vector3(0, 0, 0), Quaternion.identity);
			}
		}
	}

	public void quitGame()
	{
		Log.d(LOG_TAG, "quitGame");
		Application.Quit();
	}
}
