// "WaveVR SDK 
// © 2017 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the WaveVR SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using System.Linq;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WVR_Log;
using wvr;

public class Whiteboard : MonoBehaviour {

	private const string LOG_TAG = "Whiteboard_TAG";
	// 1 cm  = 37.7952755906 pixel
	// 50 cm = 1889.7637795276 pixel
	private int textureSize = 2000;
	private int penSize = 5;
	private int rulerLineSize = 2;
	private int isCMtoPixel = 40;

	private Texture2D texture;
	private Color[] color;

	private float drawX, drawY;

	public static bool isClearWhiteboard = false;

	void Start () {
		Renderer renderer = GetComponent<Renderer>();
		this.texture = new Texture2D(textureSize, textureSize);
		renderer.material.mainTexture = (Texture) texture;
	}
	void Update () {

		if (isClearWhiteboard == true) {

			Clear ();
			isClearWhiteboard = false;
		}
		string head_draw = "draw_w: " + drawX + ", draw_y: " + drawY;

		Log.d (LOG_TAG, head_draw, true);

		int x = (int)(drawX);
		int y = (int)(drawY);

		int stWidth = texture.width / 2;
		int stHeight = texture.height / 2;

		this.color = Enumerable.Repeat<Color>(Color.blue, penSize * penSize).ToArray<Color>();
		texture.SetPixels (x + stWidth, y + stHeight, penSize, penSize, color);

		if (TrackingTrajectory_Head.isDrawRuler == true) {
			TrackingTrajectory_Head.isDrawRuler = false;

			int ruler_x = 0;
			int ruler_y = 0;
			for ( ; ruler_x < texture.width ; ++ruler_x, ++ruler_y) {
				SetColor (Color.black);

				// 1 cm for each
				if (ruler_x % isCMtoPixel == 0) {
					int ruler_line = 20;
					for ( ; ruler_line > 0 ; --ruler_line ) {
						texture.SetPixels (ruler_x, (texture.height / 2) + ruler_line, rulerLineSize, rulerLineSize, color);
						texture.SetPixels (ruler_x, (texture.height / 2) - ruler_line, rulerLineSize, rulerLineSize, color);
					}
				}

				// 5 cm for each
				if (ruler_x % (isCMtoPixel * 5) == 0) {
					int ruler_line = 60 ;
					for ( ; ruler_line > 0; --ruler_line ) {
						texture.SetPixels (ruler_x, (texture.height / 2) + ruler_line, rulerLineSize, rulerLineSize, color);
						texture.SetPixels (ruler_x, (texture.height / 2) - ruler_line, rulerLineSize, rulerLineSize, color);
					}
				}

				texture.SetPixels (ruler_x, texture.height / 2, 2, 2, color);

				// 1 cm for each
				if (ruler_y % isCMtoPixel == 0) {
					int ruler_line = 20;
					for ( ; ruler_line > 0 ; --ruler_line ) {
						texture.SetPixels ((texture.width / 2) + ruler_line, ruler_y, rulerLineSize, rulerLineSize, color);
						texture.SetPixels ((texture.width / 2) - ruler_line, ruler_y, rulerLineSize, rulerLineSize, color);
					}
				}

				// 5 cm for each
				if (ruler_y % (isCMtoPixel * 5) == 0) {
					int ruler_line = 60;
					for ( ; ruler_line > 0 ; --ruler_line ) {
						texture.SetPixels ((texture.width / 2) + ruler_line, ruler_y, rulerLineSize, rulerLineSize, color);
						texture.SetPixels ((texture.width / 2) - ruler_line, ruler_y, rulerLineSize, rulerLineSize, color);
					}
				}

				texture.SetPixels (texture.width / 2, ruler_y, rulerLineSize, rulerLineSize, color);
			}
		}
		texture.Apply ();
	}

	private void Clear()
	{
		Log.d (LOG_TAG, "Whiteboard to clear", true);

		int x, y;
		for (x = 0; x < texture.width; x++)
		{
			for (y = 0; y < texture.height; y++)
			{
				texture.SetPixel(x, y, Color.white);
			}
		}
		texture.Apply();
	}

	public void SetHeadTrackingTrajectoryPosition(float x, float y) {

		// 1 cm ~= 40 pixel
		this.drawX = x * 40;
		this.drawY = y * 40;
	}

	public void SetColor(Color color) {
		this.color = Enumerable.Repeat<Color>(color, penSize * penSize).ToArray<Color>();
	}
}
