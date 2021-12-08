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
using UnityEngine.Video;
using WVR_Log;

[RequireComponent(typeof(VideoPlayer))]
public class VideoLoader : MonoBehaviour
{
	string videoRootPath;
	VideoPlayer player;

	private void Awake()
	{
		videoRootPath = Path.Combine(Application.persistentDataPath, "Videos");
		Log.i("VideoLoader", "VideoRootPath is " + videoRootPath);

		if (!Directory.Exists(videoRootPath))
			Directory.CreateDirectory(videoRootPath);
	}

	// Start is called before the first frame update
	IEnumerator Start()
	{
		player = GetComponent<VideoPlayer>();
		if (player == null)
			yield break;

		string[] files;
		while (true)
		{
			files = Directory.GetFiles(videoRootPath);
			if (files.Length == 0)
			{
				Log.w("VideoLoader", "No video found");
				yield return new WaitForSeconds(1);
				continue;
			}

			for (int i = 0; i < files.Length; i++)
			{
				try
				{
					Log.i("VideoLoader", "Try load file: " + files[0]);
					player.url = files[0];
					player.sendFrameReadyEvents = true;
					player.frameReady += OnFrameReady;
					player.Play();
				}
				catch (Exception )
				{
					continue;
				}
				break;
			}
			break;

		}
	}

	private int frameCount = 0;	

	public void OnFrameReady(VideoPlayer source, long frameIdx) {
		frameCount++;
	}


	private float startTime = 0;
	private void Update()
	{
		if (Time.unscaledTime - startTime > 1)
		{
			Log.i("VideoLoader", "framerate=" + frameCount);
			frameCount = 0;
			startTime = Time.unscaledTime;
		}
	}
}
