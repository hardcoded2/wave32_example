using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using wvr;
using WVR_Log;

namespace Wave.Essence
{
	public sealed class SupportButtonABXY : MonoBehaviour
	{
		private const string LOG_TAG = "Wave.Essence.SupportButtonABXY";
		private void DEBUG(string msg)
		{
			if (Log.EnableDebugLog)
				Log.d(LOG_TAG, msg, true);
		}

		[Serializable]
		public class ButtonOption
		{
			public bool A_X = true;
			public bool B_Y = true;
		}

		[SerializeField]
		private ButtonOption m_ButtonOptionLeft = new ButtonOption();
		public ButtonOption ButtonOptionsLeft { get { return m_ButtonOptionLeft; } set { m_ButtonOptionLeft = value; } }

		[SerializeField]
		private ButtonOption m_ButtonOptionRight = new ButtonOption();
		public ButtonOption ButtonOptionsRight { get { return m_ButtonOptionRight; } set { m_ButtonOptionRight = value; } }

		private const uint inputTableSizeRight = (uint)WVR_InputId.WVR_InputId_Max;
		private WVR_InputMappingPair_t[] inputTableRight = new WVR_InputMappingPair_t[inputTableSizeRight];
		private uint inputCountRight = 0;
		private List<WVR_InputId> s_CurrentButtonsRight = new List<WVR_InputId>();
		private WVR_InputAttribute_t[] s_InputAttributesRight;

		private const uint inputTableSizeLeft = (uint)WVR_InputId.WVR_InputId_Max;
		private WVR_InputMappingPair_t[] inputTableLeft = new WVR_InputMappingPair_t[inputTableSizeLeft];
		private uint inputCountLeft = 0;
		private List<WVR_InputId> s_CurrentButtonsLeft = new List<WVR_InputId>();
		private WVR_InputAttribute_t[] s_InputAttributesLeft;

		private List<WVR_InputId> s_RequestButtons = new List<WVR_InputId>();

		#region MonoBehaviour overrides
		void OnEnable()
		{
			onRoutine = true;
			StartCoroutine(InputRequestRoutine());
		}
		private void OnDisable()
		{
			onRoutine = false;
			StopAllCoroutines();
		}
		void Update()
		{
			if (!onRoutineInitialized)
				return;

			if ((m_ButtonOptionLeft.A_X && !s_CurrentButtonsLeft.Contains(WVR_InputId.WVR_InputId_Alias1_A)) ||
				(!m_ButtonOptionLeft.A_X && s_CurrentButtonsLeft.Contains(WVR_InputId.WVR_InputId_Alias1_A)) ||
				(m_ButtonOptionLeft.B_Y && !s_CurrentButtonsLeft.Contains(WVR_InputId.WVR_InputId_Alias1_B)) ||
				(!m_ButtonOptionLeft.B_Y && s_CurrentButtonsLeft.Contains(WVR_InputId.WVR_InputId_Alias1_B)))
			{
				SetupButtonAttributes(inputTableLeft, inputCountLeft, ref s_InputAttributesLeft, m_ButtonOptionLeft);
				Interop.WVR_SetInputRequest(WVR_DeviceType.WVR_DeviceType_Controller_Left, s_InputAttributesLeft, (uint)s_InputAttributesLeft.Length);
				UpdateInputMappingTable(WVR_DeviceType.WVR_DeviceType_Controller_Left);
			}

			if ((m_ButtonOptionRight.A_X && !s_CurrentButtonsRight.Contains(WVR_InputId.WVR_InputId_Alias1_A)) ||
				(!m_ButtonOptionRight.A_X && s_CurrentButtonsRight.Contains(WVR_InputId.WVR_InputId_Alias1_A)) ||
				(m_ButtonOptionRight.B_Y && !s_CurrentButtonsRight.Contains(WVR_InputId.WVR_InputId_Alias1_B)) ||
				(!m_ButtonOptionRight.B_Y && s_CurrentButtonsRight.Contains(WVR_InputId.WVR_InputId_Alias1_B)))
			{
				SetupButtonAttributes(inputTableRight, inputCountRight, ref s_InputAttributesRight, m_ButtonOptionRight);
				Interop.WVR_SetInputRequest(WVR_DeviceType.WVR_DeviceType_Controller_Right, s_InputAttributesRight, (uint)s_InputAttributesRight.Length);
				UpdateInputMappingTable(WVR_DeviceType.WVR_DeviceType_Controller_Right);
			}
		}
		#endregion

		#region Input Request
		private bool onRoutine = true, onRoutineInitialized = false;
		IEnumerator InputRequestRoutine()
		{
			while (onRoutine)
			{
				yield return new WaitForSeconds(3); // Checks the available button every 3s.

				UpdateInputMappingTable(WVR_DeviceType.WVR_DeviceType_Controller_Left);
				UpdateInputMappingTable(WVR_DeviceType.WVR_DeviceType_Controller_Right);
				onRoutineInitialized = true;
			}
		}
		private void UpdateInputMappingTable(WVR_DeviceType device)
		{
			if (device == WVR_DeviceType.WVR_DeviceType_Controller_Left)
			{
				inputCountLeft = Interop.WVR_GetInputMappingTable(WVR_DeviceType.WVR_DeviceType_Controller_Left, inputTableLeft, inputTableSizeLeft);
				if (inputCountLeft > 0)
				{
					s_CurrentButtonsLeft.Clear();
					for (int i = 0; i < (int)inputCountLeft; i++)
					{
						s_CurrentButtonsLeft.Add(inputTableLeft[i].destination.id);
						DEBUG("Left button " + inputTableLeft[i].source.id + " is mapping to " + inputTableLeft[i].destination.id);
					}
				}
			}
			if (device == WVR_DeviceType.WVR_DeviceType_Controller_Right)
			{
				inputCountRight = Interop.WVR_GetInputMappingTable(WVR_DeviceType.WVR_DeviceType_Controller_Right, inputTableRight, inputTableSizeRight);
				if (inputCountRight > 0)
				{
					s_CurrentButtonsRight.Clear();
					for (int i = 0; i < (int)inputCountRight; i++)
					{
						s_CurrentButtonsRight.Add(inputTableRight[i].destination.id);
						DEBUG("Right button " + inputTableRight[i].source.id + " is mapping to " + inputTableRight[i].destination.id);
					}
				}
			}
		}
		private void SetupButtonAttributes(WVR_InputMappingPair_t[] inputTable, uint inputTableCount, ref WVR_InputAttribute_t[] inputAttributes, ButtonOption buttonOption)
		{
			s_RequestButtons.Clear();
			for (int i = 0; i < (int)inputTableCount; i++)
			{
				if (inputTable[i].destination.id == WVR_InputId.WVR_InputId_Alias1_A ||
					inputTable[i].destination.id == WVR_InputId.WVR_InputId_Alias1_B)
					continue;

				s_RequestButtons.Add(inputTable[i].destination.id);
			}
			if (buttonOption.A_X)
				s_RequestButtons.Add(WVR_InputId.WVR_InputId_Alias1_A);
			if (buttonOption.B_Y)
				s_RequestButtons.Add(WVR_InputId.WVR_InputId_Alias1_B);

			inputAttributes = new WVR_InputAttribute_t[s_RequestButtons.Count];
			for (int i = 0; i < inputAttributes.Length; i++)
			{
				switch (s_RequestButtons[i])
				{
					case WVR_InputId.WVR_InputId_Alias1_Menu:
					case WVR_InputId.WVR_InputId_Alias1_Grip:
					case WVR_InputId.WVR_InputId_Alias1_DPad_Left:
					case WVR_InputId.WVR_InputId_Alias1_DPad_Up:
					case WVR_InputId.WVR_InputId_Alias1_DPad_Right:
					case WVR_InputId.WVR_InputId_Alias1_DPad_Down:
					case WVR_InputId.WVR_InputId_Alias1_Volume_Up:
					case WVR_InputId.WVR_InputId_Alias1_Volume_Down:
					case WVR_InputId.WVR_InputId_Alias1_Bumper:
					case WVR_InputId.WVR_InputId_Alias1_Back:
					case WVR_InputId.WVR_InputId_Alias1_Enter:
					case WVR_InputId.WVR_InputId_Alias1_A:
					case WVR_InputId.WVR_InputId_Alias1_B:
						inputAttributes[i].id = s_RequestButtons[i];
						inputAttributes[i].capability = (uint)WVR_InputType.WVR_InputType_Button;
						inputAttributes[i].axis_type = WVR_AnalogType.WVR_AnalogType_None;
						break;
					case WVR_InputId.WVR_InputId_Alias1_Touchpad:
					case WVR_InputId.WVR_InputId_Alias1_Thumbstick:
						inputAttributes[i].id = s_RequestButtons[i];
						inputAttributes[i].capability = (uint)(WVR_InputType.WVR_InputType_Button | WVR_InputType.WVR_InputType_Touch | WVR_InputType.WVR_InputType_Analog);
						inputAttributes[i].axis_type = WVR_AnalogType.WVR_AnalogType_2D;
						break;
					case WVR_InputId.WVR_InputId_Alias1_Trigger:
						inputAttributes[i].id = s_RequestButtons[i];
						inputAttributes[i].capability = (uint)(WVR_InputType.WVR_InputType_Button | WVR_InputType.WVR_InputType_Touch | WVR_InputType.WVR_InputType_Analog);
						inputAttributes[i].axis_type = WVR_AnalogType.WVR_AnalogType_1D;
						break;
					default:
						break;
				}
			}
		}
		#endregion
	}
}
