using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization.ThreeD {
	internal static class Boxcast {
		public static bool Run(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			[DefaultValue("Quaternion.identity")] Quaternion orientation,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			return Run(center, halfExtents, direction, out _, orientation, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static bool Run(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			Quaternion orientation,
			float maxDistance,
			int layerMask) {
			return Run(center, halfExtents, direction, orientation, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static bool Run(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			Quaternion orientation,
			float maxDistance) {
			return Run(center, halfExtents, direction, orientation, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static bool Run(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			Quaternion orientation) {
			return Run(center, halfExtents, direction, orientation, float.PositiveInfinity, -5,
				QueryTriggerInteraction.UseGlobal);
		}

		public static bool Run(Vector3 center, Vector3 halfExtents, Vector3 direction) {
			return Run(center, halfExtents,
				direction, Quaternion.identity, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static bool Run(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			out RaycastHit hit,
			[DefaultValue("Quaternion.identity")] Quaternion orientation,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			bool didHit = Physics.defaultPhysicsScene.BoxCast(center, halfExtents, direction, out hit, orientation, maxDistance, layerMask,
				queryTriggerInteraction);

#if UNITY_EDITOR
			direction.Normalize();
			Color color = VisualUtils.GetColor(didHit);
			float distance = VisualUtils.GetMaxRayLength(maxDistance);
			VisualUtils.DrawArrow(center, direction * distance, VisualUtils.GetDefaultColor());

			if (didHit) {
				VisualUtils.DrawCube(center + direction * hit.distance, halfExtents, orientation, color);
				VisualUtils.DrawNormalCircle(hit.point, hit.normal, color);
			} else {
				VisualUtils.DrawCube(center + direction * distance, halfExtents, orientation, color);
			}
#endif

			return didHit;
		}

		public static bool Run(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			out RaycastHit hit,
			Quaternion orientation,
			float maxDistance,
			int layerMask) {
			return Run(center, halfExtents, direction, out hit, orientation, maxDistance, layerMask,
				QueryTriggerInteraction.UseGlobal);
		}

		public static bool Run(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			out RaycastHit hit,
			Quaternion orientation,
			float maxDistance) {
			return Run(center, halfExtents, direction, out hit, orientation, maxDistance, -5,
				QueryTriggerInteraction.UseGlobal);
		}

		public static bool Run(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			out RaycastHit hit,
			Quaternion orientation) {
			return Run(center, halfExtents, direction, out hit, orientation, float.PositiveInfinity, -5,
				QueryTriggerInteraction.UseGlobal);
		}

		public static bool Run(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			out RaycastHit hit) {
			return Run(center, halfExtents, direction, out hit, Quaternion.identity, float.PositiveInfinity, -5,
				QueryTriggerInteraction.UseGlobal);
		}
	}
}