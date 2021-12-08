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
using WVR_Log;

public class DropdownTest : MonoBehaviour
{
	private static string LOG_TAG = "DropdownTest";
	public Dropdown m_DropDown = null;
	public Text m_DropDownText = null;
	private string[] textStrings = new string[] { "aaa", "bbb", "ccc" };
	private Color m_Color = new Color(26, 7, 253, 255);

	void Start()
	{
		// clear all option item
		m_DropDown.options.Clear();

		// fill the dropdown menu OptionData
		foreach (string c in textStrings)
		{
			m_DropDown.options.Add(new Dropdown.OptionData() { text = c });
		}
		// this swith from 1 to 0 is only to refresh the visual menu
		m_DropDown.value = 1;
		m_DropDown.value = 0;
	}

	void Update()
	{
		if (m_DropDownText == null)
			return;

		m_DropDownText.text = textStrings[m_DropDown.value];

		Canvas dropdown_canvas = m_DropDown.gameObject.GetComponentInChildren<Canvas>();
		Button[] buttons = m_DropDown.gameObject.GetComponentsInChildren<Button>();
		if (dropdown_canvas != null)
		{
			dropdown_canvas.gameObject.tag = "EventCanvas";
			foreach (Button _btn in buttons)
			{
				Log.d(LOG_TAG, "set button " + _btn.name + " color.");
				ColorBlock _cb = _btn.colors;
				_cb.normalColor = this.m_Color;
				_btn.colors = _cb;
			}
		}
	}

	public void ChangeColor()
	{
		Image img = gameObject.GetComponent<Image>();
		img.color = img.color == Color.yellow ? Color.green : Color.yellow;
	}
}
