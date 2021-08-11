using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization.ThreeD {
	internal static class CapsulecastAll {
		public static RaycastHit[] Run(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			RaycastHit[] hits = Physics.CapsuleCastAll(point1, point2, radius, direction, maxDistance, layerMask, queryTriggerInteraction);

#if UNITY_EDITOR
			direction.Normalize();
			bool didHit = hits != null && hits.Length != 0;

			float distance = VisualUtils.GetMaxRayLength(maxDistance);
			Vector3 origin = (point1 + point2) * 0.5f;
			VisualUtils.DrawArrow(origin, direction * distance, VisualUtils.GetDefaultColor());

			if (didHit) {
				foreach (RaycastHit hit in hits) {
					VisualUtils.DrawCapsule(point1, point2, direction, radius, maxDistance, hit, didHit);

					VisualUtils.DrawNormalCircle(hit.point, hit.normal, Color.green);
				}
			} else {
				VisualUtils.DrawCapsule(point1, point2, direction, radius, maxDistance, default, didHit);
			}
#endif

			return hits;
		}

		public static RaycastHit[] Run(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			float maxDistance,
			int layerMask) {
			return Run(point1, point2, radius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] Run(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			float maxDistance) {
			return Run(point1, point2, radius, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] Run(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction) {
			return Run(point1, point2, radius, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}
	}
}