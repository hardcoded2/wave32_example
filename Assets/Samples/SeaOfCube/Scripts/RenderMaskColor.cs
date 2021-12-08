﻿// "WaveVR SDK 
// © 2017 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the WaveVR SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using System.Collections;
using UnityEngine;

// A debug tool for test render mask
[RequireComponent(typeof(WaveVR_RenderMask))]
public class RenderMaskColor : MonoBehaviour
{
	public Color32 SinglePassColor = Color.green;
	public Color32 MutiPassColor = Color.red;

	IEnumerator Start() {
		while (WaveVR_Render.Instance == null)
		{
			yield return null;
		}

		var mask = GetComponent<WaveVR_RenderMask>();
		if (mask != null)
		{
			if (WaveVR_Render.Instance.IsSinglePass)
				mask.SetMaskColor(SinglePassColor);
			else
				mask.SetMaskColor(MutiPassColor);
		}
	}
}