using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization.ThreeD {
	internal static class CheckBox {
		public static bool Run(
			Vector3 center,
			Vector3 halfExtents,
			[DefaultValue("Quaternion.identity")] Quaternion orientation,
			[DefaultValue("DefaultRaycastLayers")] int layermask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			bool didHit = Physics.CheckBox(center, halfExtents, orientation, layermask, queryTriggerInteraction);
#if UNITY_EDITOR
			VisualUtils.DrawCube(center, halfExtents, orientation, VisualUtils.GetColor(didHit));
#endif
			return didHit;
		}

		public static bool Run(
			Vector3 center,
			Vector3 halfExtents,
			Quaternion orientation,
			int layerMask) {
			return Run(center, halfExtents, orientation, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static bool Run(Vector3 center, Vector3 halfExtents, Quaternion orientation) {
			return Run(center, halfExtents, orientation, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static bool Run(Vector3 center, Vector3 halfExtents) {
			return Run(center, halfExtents, Quaternion.identity, -5, QueryTriggerInteraction.UseGlobal);
		}
	}
}