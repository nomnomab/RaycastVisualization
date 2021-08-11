using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization.ThreeD {
	internal static class CapsuleCastNonAlloc {
		public static int Run(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			RaycastHit[] results,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			int count = Physics.defaultPhysicsScene.CapsuleCast(point1, point2, radius, direction, results, maxDistance, layerMask,
				queryTriggerInteraction);

#if UNITY_EDITOR
			direction.Normalize();
			bool didHit = count > 0;
			float distance = VisualUtils.GetMaxRayLength(maxDistance);
			Vector3 origin = (point1 + point2) * 0.5f;

			VisualUtils.DrawArrow(origin, direction * distance, VisualUtils.GetDefaultColor());

			if (didHit) {
				for (int i = 0; i < count; i++) {
					ref RaycastHit hit = ref results[i];
					VisualUtils.DrawCapsule(point1, point2, direction, radius, maxDistance, hit, didHit);
					VisualUtils.DrawNormalCircle(hit.point, hit.normal, VisualUtils.GetColor(didHit));
				}
			} else {
				VisualUtils.DrawCapsule(point1, point2, direction, radius, maxDistance, default, didHit);
			}
#endif

			return count;
		}

		public static int Run(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			RaycastHit[] results,
			float maxDistance,
			int layerMask) {
			return Run(point1, point2, radius, direction, results, maxDistance, layerMask,
				QueryTriggerInteraction.UseGlobal);
		}

		public static int Run(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			RaycastHit[] results,
			float maxDistance) {
			return Run(point1, point2, radius, direction, results, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static int Run(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			RaycastHit[] results) {
			return Run(point1, point2, radius, direction, results, float.PositiveInfinity, -5,
				QueryTriggerInteraction.UseGlobal);
		}
	}
}