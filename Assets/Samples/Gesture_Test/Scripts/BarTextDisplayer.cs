// "Wave SDK 
// © 2017 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the Wave SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WVR_Log;

namespace WaveVRGesture
{
	[DisallowMultipleComponent]
	public class BarTextDisplayer : MonoBehaviour,
		IPointerEnterHandler,
		IPointerExitHandler,
		IPointerDownHandler,
		IBeginDragHandler,
		IDragHandler,
		IEndDragHandler,
		IDropHandler,
		IPointerClickHandler,
		IPointerUpHandler
	{
		private const string LOG_TAG = "BarTextDisplayer";
		private void DEBUG(string msg)
		{
			Log.d(LOG_TAG, gameObject.name + " " + msg, true);
		}
		public Text m_Text = null;
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (m_Text != null)
				m_Text.text = "Enter";
			DEBUG("OnPointerEnter");
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (m_Text != null)
				m_Text.text = "Exit";
			DEBUG("OnPointerExit");
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (m_Text != null)
				m_Text.text = "Down";
			DEBUG("OnPointerDown");
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			if (m_Text != null)
				m_Text.text = "Begin Drag";
			DEBUG("OnBeginDrag");
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (m_Text != null)
				m_Text.text = "Dragging";
			DEBUG("OnDrag");
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			if (m_Text != null)
				m_Text.text = "EndDrag";
			DEBUG("OnEndDrag");
		}

		public void OnDrop(PointerEventData eventData)
		{
			if (m_Text != null)
				m_Text.text = "Drop";
			DEBUG("OnDrop");
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (m_Text != null)
				m_Text.text = "Up";
			DEBUG("OnPointerUp");
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (m_Text != null)
				m_Text.text = "Click";
			DEBUG("OnPointerClick");
		}

		private GameObject eventSystemObject = null;
		void Start()
		{
			SetEventSystem();
		}

		private void SetEventSystem()
		{
			if (EventSystem.current == null)
			{
				EventSystem event_system = FindObjectOfType<EventSystem>();
				if (event_system != null)
				{
					eventSystemObject = event_system.gameObject;
					DEBUG("SetEventSystem() Found current EventSystem: " + eventSystemObject.name);
				}
			}
			else
			{
				eventSystemObject = EventSystem.current.gameObject;
				DEBUG("SetEventSystem() Found current EventSystem: " + eventSystemObject.name);
			}
		}
	}
}
