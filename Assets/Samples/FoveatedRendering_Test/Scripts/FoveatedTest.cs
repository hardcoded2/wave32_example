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
using wvr.render;
using UnityEngine.UI;
using wvr;
using WaveVR_Log;
using wvr.sample.foveated;


public class FoveatedTest : MonoBehaviour
{
	WaveVR_FoveatedRendering foveated;
	//private static string LOG_TAG = "FoveatedTest";
	public Text StatustextField;
	private static FoveatedTest mInstance;
	private float FOVLarge = 57;
	private float FOVMiddle = 38;
	private float FOVSmall = 19;
	public GameObject ObjectFar;
	public GameObject ObjectNear;
	private bool changeStatus = true;
	//float time;

	public void FoveationIsDisable()
	{
		if (this.foveated.enabled)
		{
			this.foveated.FoveationMode = WVR_FoveationMode.Disable;
			this.foveated.enabled = false;
			changeStatus = true;
		}
	}

	public void FoveationIsEnable()
	{
		if (this.foveated.FoveationMode == WVR_FoveationMode.Default)
		{
			this.foveated.enabled = false;
			changeStatus = true;
		}
		if (!this.foveated.enabled)
		{
			this.foveated.FoveationMode = WVR_FoveationMode.Enable;
			this.foveated.enabled = true;
			changeStatus = true;
		}
	}

	public void LeftClearVisionFOVHigh()
	{
		if (this.foveated.FoveationMode == WVR_FoveationMode.Default)
		{
			this.foveated.enabled = false;
			this.foveated.FoveationMode = WVR_FoveationMode.Enable;
			this.foveated.enabled = true;
			changeStatus = true;
		}
		if (this.foveated.enabled)
		{
			if (this.foveated.LeftClearVisionFOV != this.FOVLarge)
			{
				this.foveated.LeftClearVisionFOV = this.FOVLarge;
				changeStatus = true;
			}
		}
	}

	public void LeftClearVisionFOVLow()
	{
		if (this.foveated.FoveationMode == WVR_FoveationMode.Default)
		{
			this.foveated.enabled = false;
			this.foveated.FoveationMode = WVR_FoveationMode.Enable;
			this.foveated.enabled = true;
			changeStatus = true;
		}
		if (this.foveated.enabled)
		{
			if (this.foveated.LeftClearVisionFOV != this.FOVSmall)
			{
				this.foveated.LeftClearVisionFOV = this.FOVSmall;
				changeStatus = true;
			}
		}
	}

	public void LeftClearVisionFOVMiddle()
	{
		if (this.foveated.FoveationMode == WVR_FoveationMode.Default)
		{
			this.foveated.enabled = false;
			this.foveated.FoveationMode = WVR_FoveationMode.Enable;
			this.foveated.enabled = true;
			changeStatus = true;
		}
		if (this.foveated.enabled)
		{
			if (this.foveated.LeftClearVisionFOV != this.FOVMiddle)
			{
				this.foveated.LeftClearVisionFOV = this.FOVMiddle;
				changeStatus = true;
			}
		}
	}

	public void LeftEyePeripheralQualityHigh()
	{
		if (this.foveated.FoveationMode == WVR_FoveationMode.Default)
		{
			this.foveated.enabled = false;
			this.foveated.FoveationMode = WVR_FoveationMode.Enable;
			this.foveated.enabled = true;
			changeStatus = true;
		}
		if (this.foveated.enabled)
		{
			if (this.foveated.LeftPeripheralQuality != WVR_PeripheralQuality.High)
			{
				this.foveated.LeftPeripheralQuality = WVR_PeripheralQuality.High;
				changeStatus = true;
			}
		}
	}

	public void LeftEyePeripheralQualityLow()
	{
		if (this.foveated.FoveationMode == WVR_FoveationMode.Default)
		{
			this.foveated.enabled = false;
			this.foveated.FoveationMode = WVR_FoveationMode.Enable;
			this.foveated.enabled = true;
			changeStatus = true;
		}
		if (this.foveated.enabled)
		{
			if (this.foveated.LeftPeripheralQuality != WVR_PeripheralQuality.Low)
			{
				this.foveated.LeftPeripheralQuality = WVR_PeripheralQuality.Low;
				changeStatus = true;
			}
		}
	}

	public void LeftEyePeripheralQualityMiddle()
	{
		if (this.foveated.FoveationMode == WVR_FoveationMode.Default)
		{
			this.foveated.enabled = false;
			this.foveated.FoveationMode = WVR_FoveationMode.Enable;
			this.foveated.enabled = true;
			changeStatus = true;
		}
		if (this.foveated.enabled)
		{
			if (this.foveated.LeftPeripheralQuality != WVR_PeripheralQuality.Middle)
			{
				this.foveated.LeftPeripheralQuality = WVR_PeripheralQuality.Middle;
				changeStatus = true;
			}
		}
	}

	public void RightClearVisionFOVHigh()
	{
		if (this.foveated.FoveationMode == WVR_FoveationMode.Default)
		{
			this.foveated.enabled = false;
			this.foveated.FoveationMode = WVR_FoveationMode.Enable;
			this.foveated.enabled = true;
			changeStatus = true;
		}
		if (this.foveated.enabled)
		{
			if (this.foveated.RightClearVisionFOV != this.FOVLarge)
			{
				this.foveated.RightClearVisionFOV = this.FOVLarge;
				changeStatus = true;
			}
		}
	}

	public void RightClearVisionFOVLow()
	{
		if (this.foveated.FoveationMode == WVR_FoveationMode.Default)
		{
			this.foveated.enabled = false;
			this.foveated.FoveationMode = WVR_FoveationMode.Enable;
			this.foveated.enabled = true;
			changeStatus = true;
		}
		if (this.foveated.enabled)
		{
			if (this.foveated.RightClearVisionFOV != this.FOVSmall)
			{
				this.foveated.RightClearVisionFOV = this.FOVSmall;
				changeStatus = true;
			}
		}
	}

	public void RightClearVisionFOVMiddle()
	{
		if (this.foveated.FoveationMode == WVR_FoveationMode.Default)
		{
			this.foveated.enabled = false;
			this.foveated.FoveationMode = WVR_FoveationMode.Enable;
			this.foveated.enabled = true;
			changeStatus = true;
		}
		if (this.foveated.enabled)
		{
			if (this.foveated.RightClearVisionFOV != this.FOVMiddle)
			{
				this.foveated.RightClearVisionFOV = this.FOVMiddle;
				changeStatus = true;
			}
		}
	}

	public void RightEyePeripheralQualityHigh()
	{
		if (this.foveated.FoveationMode == WVR_FoveationMode.Default)
		{
			this.foveated.enabled = false;
			this.foveated.FoveationMode = WVR_FoveationMode.Enable;
			this.foveated.enabled = true;
			changeStatus = true;
		}
		if (this.foveated.enabled)
		{
			if (this.foveated.RightPeripheralQuality != WVR_PeripheralQuality.High)
			{
				this.foveated.RightPeripheralQuality = WVR_PeripheralQuality.High;
				changeStatus = true;
			}
		}
	}

	public void RightEyePeripheralQualityLow()
	{
		if (this.foveated.FoveationMode == WVR_FoveationMode.Default)
		{
			this.foveated.enabled = false;
			this.foveated.FoveationMode = WVR_FoveationMode.Enable;
			this.foveated.enabled = true;
			changeStatus = true;
		}
		if (this.foveated.enabled)
		{
			if (this.foveated.RightPeripheralQuality != WVR_PeripheralQuality.Low)
			{
				this.foveated.RightPeripheralQuality = WVR_PeripheralQuality.Low;
				changeStatus = true;
			}
		}
	}

	public void RightEyePeripheralQualityMedium()
	{
		if (this.foveated.FoveationMode == WVR_FoveationMode.Default)
		{
			this.foveated.enabled = false;
			this.foveated.FoveationMode = WVR_FoveationMode.Enable;
			this.foveated.enabled = true;
			changeStatus = true;
		}
		if (this.foveated.enabled)
		{
			if (this.foveated.RightPeripheralQuality != WVR_PeripheralQuality.Middle)
			{
				this.foveated.RightPeripheralQuality = WVR_PeripheralQuality.Middle;
				changeStatus = true;
			}
		}
	}

	private void printFoveationInfo()
	{
		string str = string.Empty;
		if (!this.foveated.enabled)
		{
			str = "Foveation enable : " + this.foveated.enabled.ToString();
		}
		else
		{
			string[] textArray1 = new string[10];
			textArray1[0] = "Foveation enable : ";
			textArray1[1] = this.foveated.enabled.ToString();
			textArray1[2] = "\n LeftClearVisionFOV : ";
			textArray1[3] = this.foveated.LeftClearVisionFOV.ToString();
			textArray1[4] = "\n RightClearVisionFOV : ";
			textArray1[5] = this.foveated.RightClearVisionFOV.ToString();
			textArray1[6] = "\n LeftPeripheralQuality : ";
			textArray1[7] = this.foveated.LeftPeripheralQuality.ToString();
			textArray1[8] = "\n RightPeripheralQuality : ";
			textArray1[9] = this.foveated.RightPeripheralQuality.ToString();
			str = string.Concat(textArray1);
			//Log.d(LOG_TAG, "foveation_type_text: " + str, false);
		}
		this.StatustextField.text = str;
	}

	void Start()
	{
		if (this.foveated == null)
		{
			this.foveated = new WaveVR_FoveatedRendering();
			if (!this.foveated)
			{
				return;
			}
		}
		this.StatustextField = GetComponent<Text>();
	}

	// Update is called once per frame
	void Update()
	{
		if (this.foveated == null)
		{
			this.foveated = WaveVR_FoveatedRendering.Instance;
			if (!this.foveated)
			{
				return;
			}
		}
		if (changeStatus == true)
		{
			this.printFoveationInfo();
			changeStatus = false;
		}
	}
}
