// "WaveVR SDK 
// © 2017 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the WaveVR SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using AssetBundleTool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WVR_Log;

namespace AQDR
{
	public class BackContactHandler : MonoBehaviour, IColliderHandler
	{
		static readonly string TAG = "AQDRBCH";

		public GameObject BackObject = null;
		public AssetBundleLoader_LoadAll sceneLoader = null;

		public Rigidbody trickRigidbody = null;
		public Text text = null;

		private void OnEnable()
		{
			if (BackObject == null)
				enabled = false;
			if (!MasterSceneManager.Instance)
				enabled = false;
			if (enabled == false)
				return;
			if (!sceneLoader)
				Log.w(TAG, "Need sceneLoader to know if we are loading.");
			ColliderDetect.RegisterHandler(this);
		}

		void OnDisable()
		{
			ColliderDetect.UnregisterHandler(this);
		}

		void Back()
		{
			if (MasterSceneManager.Instance)
			{
				MasterSceneManager.Instance.LoadPrevious();
			}
		}

		void TrickBack()
		{
			if (trickRigidbody != null)
			{
				trickRigidbody.useGravity = Random.Range(0, 1.0f) > 0.5f;
				trickRigidbody.constraints = 0;
			}
			if (text != null)
				text.text = "Oops!";
		}

		// Why not work in SinglePass????
		IEnumerator TrickWorld()
		{
			Random.InitState((int)Time.unscaledTime);

			if (text != null)
				text.text = "Zoom out";

			if (WaveVR_Render.Instance)
			{
				// 0.5 second from 100m to 1m
				float begin = Time.unscaledTime;
				while (Time.unscaledTime - begin < 1)
				{
					var val = 99 * (Time.unscaledTime - begin) + 1;
					var scale = new Vector3(val, val, val);
					WaveVR_Render.Instance.transform.localScale = scale;
					yield return null;
				}
			}
		}

		IEnumerator DelayedBack()
		{
			var trickValue = Random.Range(0, 100.0f);
			if (trickValue > 50f)
			{
				Back();
				triggered = false;
				yield break;
			}
			else
			{
				TrickBack();
				yield return new WaitForSeconds(2.5f);
			}
			//{
			//	yield return TrickWorld();
			//}

			Back();
			triggered = false;
		}

		bool triggered = false;

		public void ProcessContactEnter(Collision collision, ContactPoint cp)
		{
			if (sceneLoader && sceneLoader.IsLoadingScenes)
			{
				Log.d(TAG, "Wait for all scenes loaded.");
				return;
			}

			if (BackObject == null || cp.thisCollider.gameObject != BackObject)
				return;

			if (!triggered)
			{
				triggered = true;
				StartCoroutine(DelayedBack());
			}
		}

		public void ProcessContactExit(Collision collision) { }
	}
}
