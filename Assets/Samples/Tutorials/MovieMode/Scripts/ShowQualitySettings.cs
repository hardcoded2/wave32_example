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
using wvr.render;
using wvr;

public class ShowQualitySettings : MonoBehaviour {

	private Text textField;
	public WaveVR_DynamicResolution dynRes;
	public WaveVR_AdaptiveQuality aq;

	void Awake()
	{
		textField = GetComponent<Text>();
	}

	void Start()
	{
		UpdateText();
	}

	void OnApplicationPause(bool pause)
	{
		UpdateText();
	}

	float time = 0;
	void Update()
	{
		time += Time.unscaledDeltaTime;
		if (time > 0.5f)
		{
			time = 0;
			UpdateText();
		}
	}

	void UpdateText()
	{
		if (dynRes != null && aq != null)
			textField.text = "aq=" + (aq.enabled ? "Enable" : "Disable") + " ResScale=" + dynRes.CurrentScale;
	}
}
