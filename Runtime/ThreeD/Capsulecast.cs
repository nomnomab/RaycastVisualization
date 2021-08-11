using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization.ThreeD {
	internal static class Capsulecast {
		public static bool Run(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			return Run(point1, point2, radius, direction, out _, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static bool Run(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			float maxDistance,
			int layerMask) {
			return Run(point1, point2, radius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static bool Run(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			float maxDistance) {
			return Run(point1, point2, radius, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static bool Run(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction) {
			return Run(point1, point2, radius, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static bool Run(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			out RaycastHit hit,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			bool didHit = Physics.defaultPhysicsScene.CapsuleCast(point1, point2, radius, direction, out hit, maxDistance, layerMask,
				queryTriggerInteraction);

#if UNITY_EDITOR
			direction.Normalize();
			float distance = VisualUtils.GetMaxRayLength(maxDistance);
			VisualUtils.DrawArrow((point1 + point2) * 0.5f, direction * distance, VisualUtils.GetDefaultColor());

			VisualUtils.DrawCapsule(point1, point2, direction, radius, maxDistance, hit, didHit);

			if (didHit) {
				VisualUtils.DrawNormalCircle(hit.point, hit.normal, VisualUtils.GetColor(didHit));
			}
#endif

			return didHit;
		}

		public static bool Run(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			out RaycastHit hit,
			float maxDistance,
			int layerMask) {
			return Run(point1, point2, radius, direction, out hit, maxDistance, layerMask,
				QueryTriggerInteraction.UseGlobal);
		}

		public static bool Run(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			out RaycastHit hit,
			float maxDistance) {
			return Run(point1, point2, radius, direction, out hit, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static bool Run(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			out RaycastHit hit) {
			return Run(point1, point2, radius, direction, out hit, float.PositiveInfinity, -5,
				QueryTriggerInteraction.UseGlobal);
		}
	}
}