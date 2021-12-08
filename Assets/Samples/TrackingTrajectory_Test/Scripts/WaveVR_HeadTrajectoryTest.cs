using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WVR_Log;
using UnityEngine.UI;
using System.Linq;
using wvr;
using System.Text;

[RequireComponent(typeof(Image))]
public class WaveVR_HeadTrajectoryTest : MonoBehaviour {
	private const string LOG_TAG = "WaveVR_HeadTrajectoryTest";
	private void DEBUG(string msg)
	{
		if (Log.EnableDebugLog)
			Log.d (LOG_TAG, msg, true);
	}

	public enum TrajectoryMode
	{
		YAW = 0,
		PITCH = 1,
		ROLL = 2
	}

	private WaveVR.Device hmdDevice = null;
	private Texture2D mTexture = null;
	private const int textureWidth = 1000, textureHeight = 1000;
	private Color[] mColors;
	void OnEnable()
	{
		hmdDevice = WaveVR.Instance.getDeviceByType (WaveVR_Controller.EDeviceType.Head);
		if (mTexture == null)
			mTexture = new Texture2D (textureWidth, textureHeight);
		if (mTexture != null)
			GetComponent<Image> ().material.mainTexture = mTexture;
	}

	void OnDisable()
	{
		ClearImage ();
		mTexture = null;
		GetComponent<Image> ().material.mainTexture = null;
	}

	private bool mPainting = false;
	private float curPixelX = 0, prePixelX = 0, curPixelY = 0, prePixelY = 0;
	private float posOffsetX = 0, posOffsetY = 0, posOffsetZ = 0;
	private TrajectoryMode mTrajectoryMode = TrajectoryMode.YAW;
	private bool toSetPixel = false;
	private int applyTextureCount = 0;
	void Update () {
		if (WaveVR_Controller.Input (WaveVR_Controller.EDeviceType.Dominant).GetPressDown (WVR_InputId.WVR_InputId_Alias1_Trigger))
		{
			mPainting = !mPainting;
			if (mPainting)
			{
				ClearImage ();
				PaintScaleOnImage (Color.black);
				mColors = Enumerable.Repeat<Color> (Color.red, penSize * penSize).ToArray<Color> ();
				posOffsetX = -(hmdDevice.rigidTransform.pos.x);
				posOffsetY = -(hmdDevice.rigidTransform.pos.y);
				posOffsetZ = -(hmdDevice.rigidTransform.pos.z);
			}	
		}

		if (!mPainting)
			return;

		switch (mTrajectoryMode)
		{
		case TrajectoryMode.YAW:
			curPixelX = (mTexture.width / 2)
				+ ((hmdDevice.rigidTransform.pos.x + posOffsetX) * 1000);
			curPixelY = (mTexture.height / 2)
				+ ((hmdDevice.rigidTransform.pos.z + posOffsetZ) * 1000);
			break;
		case TrajectoryMode.PITCH:
			curPixelX = (mTexture.height / 2)
				+ ((hmdDevice.rigidTransform.pos.z + posOffsetZ) * 1000);
			curPixelY = (mTexture.width / 2)
				+ ((hmdDevice.rigidTransform.pos.y + posOffsetY) * 1000);
			break;
		case TrajectoryMode.ROLL:
			curPixelX = (mTexture.width / 2)
				+ ((hmdDevice.rigidTransform.pos.x + posOffsetX) * 1000);
			curPixelY = (mTexture.height / 2)
				+ ((hmdDevice.rigidTransform.pos.y + posOffsetY) * 1000);
			break;
		default:
			break;
		}

		curPixelX = Mathf.Clamp (curPixelX, 0, mTexture.width - 2);
		if (prePixelX != curPixelX)
		{
			prePixelX = curPixelX;
			toSetPixel = true;
		}
		curPixelY = Mathf.Clamp (curPixelY, 0, mTexture.height - 2);
		if (prePixelY != curPixelY)
		{
			prePixelY = curPixelY;
			toSetPixel = true;
		}

		if (toSetPixel)
		{
			//DEBUG ("curPixelX: " + curPixelX + ", curPixelY: " + curPixelY);
			mTexture.SetPixels ((int)curPixelX, (int)curPixelY, rulerLineSize, rulerLineSize, mColors);
			toSetPixel = false;
		}

		applyTextureCount++;
		applyTextureCount %= 10;	// Apply texture every 10 frames.
		if (applyTextureCount == 0)
			mTexture.Apply ();
	}

	private int penSize = 5;
	private int pixelToCmScale = 10;	// 10 pixels = 1cm
	private int rulerLineSize = 2;
	private void PaintScaleOnImage(Color scale_color)
	{
		if (mTexture == null)
			return;

		mColors = Enumerable.Repeat<Color>(scale_color, penSize * penSize).ToArray<Color>();
		DEBUG ("PaintScale() paint color " + scale_color);

		int ruler_x = 0, ruler_y = 0;
		for ( ; ruler_x < mTexture.width-1 ; ++ruler_x, ++ruler_y) {
			// 1 * n cm horizontal calibration
			if (ruler_x % pixelToCmScale == 0) {
				int ruler_line = 20;
				for ( ; ruler_line > 0 ; --ruler_line ) {
					mTexture.SetPixels (ruler_x, (mTexture.height / 2) + ruler_line, rulerLineSize, rulerLineSize, mColors);
					mTexture.SetPixels (ruler_x, (mTexture.height / 2) - ruler_line, rulerLineSize, rulerLineSize, mColors);
				}
			}

			// 5 * n cm horizontal calibration
			if (ruler_x % (pixelToCmScale * 5) == 0) {
				int ruler_line = 60 ;
				for ( ; ruler_line > 0; --ruler_line ) {
					mTexture.SetPixels (ruler_x, (mTexture.height / 2) + ruler_line, rulerLineSize, rulerLineSize, mColors);
					mTexture.SetPixels (ruler_x, (mTexture.height / 2) - ruler_line, rulerLineSize, rulerLineSize, mColors);
				}
			}

			// Horizontal line
			mTexture.SetPixels (ruler_x, mTexture.height / 2, 2, 2, mColors);

			// 1 * n cm vertical calibration
			if (ruler_y % pixelToCmScale == 0) {
				int ruler_line = 20;
				for ( ; ruler_line > 0 ; --ruler_line ) {
					mTexture.SetPixels ((mTexture.width / 2) + ruler_line, ruler_y, rulerLineSize, rulerLineSize, mColors);
					mTexture.SetPixels ((mTexture.width / 2) - ruler_line, ruler_y, rulerLineSize, rulerLineSize, mColors);
				}
			}

			// 5 * n cm vertical calibration
			if (ruler_y % (pixelToCmScale * 5) == 0) {
				int ruler_line = 60;
				for ( ; ruler_line > 0 ; --ruler_line ) {
					mTexture.SetPixels ((mTexture.width / 2) + ruler_line, ruler_y, rulerLineSize, rulerLineSize, mColors);
					mTexture.SetPixels ((mTexture.width / 2) - ruler_line, ruler_y, rulerLineSize, rulerLineSize, mColors);
				}
			}

			// Vertical line
			mTexture.SetPixels (mTexture.width / 2, ruler_y, rulerLineSize, rulerLineSize, mColors);
		}
		mTexture.Apply ();
	}

	private void ClearImage()
	{
		if (mTexture == null)
			return;

		DEBUG ("ClearImage()");

		for (int x = 0; x < mTexture.width - 1; x++)
		{
			for (int y = 0; y < mTexture.height - 1; y++)
			{
				mTexture.SetPixel (x, y, Color.white);
			}
		}
		mTexture.Apply ();
	}

	public void SetTrajectoryMode(int mode)
	{
		mTrajectoryMode = (TrajectoryMode)mode;
		DEBUG ("SetTrajectoryMode() " + mTrajectoryMode);
	}
}
