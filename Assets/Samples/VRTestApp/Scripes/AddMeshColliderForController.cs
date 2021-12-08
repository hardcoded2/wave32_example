using UnityEngine;
using WVR_Log;

namespace VRTestApp
{
    public class AddMeshColliderForController : MonoBehaviour {

		private readonly GameObject[] controllers = new GameObject[4]; // devicetype is 1, 2, and 3

		void OnEnable()
        {
            WaveVR_Utils.Event.Listen(WaveVR_Utils.Event.CONTROLLER_MODEL_LOADED, ControllerLoadedHandler);
            WaveVR_Utils.Event.Listen(WaveVR_Utils.Event.ADAPTIVE_CONTROLLER_READY, AdaptiveControllerReadyHandler);
        }

        void OnDisable()
        {
            WaveVR_Utils.Event.Remove(WaveVR_Utils.Event.CONTROLLER_MODEL_LOADED, ControllerLoadedHandler);
            WaveVR_Utils.Event.Remove(WaveVR_Utils.Event.ADAPTIVE_CONTROLLER_READY, AdaptiveControllerReadyHandler);
        }

        void ShowAllChildRecursivly(Transform parent, int indent = 0)
        {
            var csb = Log.CSB;
            for (int i = 0; i < indent; i++)
            {
                csb.Append(" ");
            }
            Log.d("VRTCP", csb.Append(parent.name).Append(" isActive=").Append(parent.gameObject.activeInHierarchy).ToString());
            int count = parent.childCount;
            indent++;
            for (int i = 0; i < count; i++)
                ShowAllChildRecursivly(parent.GetChild(i), indent);
        }

        Transform FindActiveChildContainsName(Transform parent, string name)
        {
            int count = parent.childCount;
            if (parent.name.Contains(name) && parent.gameObject.activeInHierarchy)
                return parent;

            for (int i = 0; i < count; i++)
            {
                var obj = FindActiveChildContainsName(parent.GetChild(i), name);
                if (obj != null) return obj;
            }
            return null;
        }

        private void AddCollider(WaveVR_Controller.EDeviceType deviceType)
        {
            //bool isDominate = deviceType == WaveVR_Controller.EDeviceType.Dominant;
            var controller = controllers[(int)deviceType];
            if (controller == null)
                return;

            // Body is the biggest component in a controller model.
            //ShowAllChildRecursivly(controller.transform); // for debug
            Transform body = FindActiveChildContainsName(controller.transform, "__CM__Body");
            if (body == null)
                return;

            var filter = body.gameObject.GetComponent<MeshFilter>();
            if (filter == null || filter.sharedMesh == null)
                return;

            MeshCollider collider = body.gameObject.GetComponent<MeshCollider>();
            if (collider == null)
            {
                collider = body.gameObject.AddComponent<MeshCollider>();
            }

            Rigidbody rigidbody = body.gameObject.GetComponent<Rigidbody>();
            if (rigidbody == null)
            {
                rigidbody = body.gameObject.AddComponent<Rigidbody>();
                rigidbody.useGravity = false;
                rigidbody.isKinematic = true;
            }

            collider.isTrigger = false;

            controllers[(int)deviceType] = null;

            Log.d("VRTCP", "Trigger is created for " + deviceType);
        }

        public void AdaptiveControllerReadyHandler(params object[] args)
        {
            var deviceType = (WaveVR_Controller.EDeviceType)args[0];
            AddCollider(deviceType);
        }

        public void ControllerLoadedHandler(params object[] args)
        {
            var deviceType = (WaveVR_Controller.EDeviceType) args[0];
            if (deviceType == WaveVR_Controller.EDeviceType.Dominant || deviceType == WaveVR_Controller.EDeviceType.NonDominant)
            {
                var controller = (GameObject)args[1];
                if (controller == null)
                    return;

                controllers[(int) deviceType] = controller;
                AddCollider(deviceType);
            }
        }
    }
}
