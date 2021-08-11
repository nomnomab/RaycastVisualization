using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization.ThreeD {
	internal static class BoxCastNonAlloc {
		public static int Run(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			RaycastHit[] results,
			[DefaultValue("Quaternion.identity")] Quaternion orientation,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			int count = Physics.defaultPhysicsScene.BoxCast(center, halfExtents, direction, results, orientation, maxDistance, layerMask,
				queryTriggerInteraction);

#if UNITY_EDITOR
			direction.Normalize();
			bool didHit = count > 0;

			float distance = VisualUtils.GetMaxRayLength(maxDistance);
			VisualUtils.DrawArrow(center, direction * distance, VisualUtils.GetDefaultColor());
			Color color = VisualUtils.GetColor(didHit);

			if (didHit) {
				for (int i = 0; i < count; i++) {
					ref RaycastHit hit = ref results[i];
					VisualUtils.DrawCube(center + direction * hit.distance, halfExtents, orientation, color);
				}
			} else {
				VisualUtils.DrawCube(center + direction * distance, halfExtents, orientation, color);
			}
#endif

			return count;
		}

		public static int Run(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			RaycastHit[] results,
			Quaternion orientation) {
			return Run(center, halfExtents, direction, results, orientation, float.PositiveInfinity, -5,
				QueryTriggerInteraction.UseGlobal);
		}

		public static int Run(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			RaycastHit[] results,
			Quaternion orientation,
			float maxDistance) {
			return Run(center, halfExtents, direction, results, orientation, maxDistance, -5,
				QueryTriggerInteraction.UseGlobal);
		}

		public static int Run(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			RaycastHit[] results,
			Quaternion orientation,
			float maxDistance,
			int layerMask) {
			return Run(center, halfExtents, direction, results, orientation, maxDistance, layerMask,
				QueryTriggerInteraction.UseGlobal);
		}

		public static int Run(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			RaycastHit[] results) {
			return Run(center, halfExtents, direction, results, Quaternion.identity, float.PositiveInfinity, -5,
				QueryTriggerInteraction.UseGlobal);
		}
	}
}