using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization.ThreeD {
	internal static class SpherecastAll {
		public static RaycastHit[] Run(
			Vector3 origin,
			float radius,
			Vector3 direction,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			RaycastHit[] hits = Physics.SphereCastAll(origin, radius, direction, maxDistance, layerMask, queryTriggerInteraction);

#if UNITY_EDITOR
			direction.Normalize();
			bool didHit = hits != null && hits.Length > 0;

			float distance = VisualUtils.GetMaxRayLength(maxDistance);
			VisualUtils.DrawArrow(origin, direction * distance, VisualUtils.GetDefaultColor());
			Color color = VisualUtils.GetColor(didHit);

			if (didHit) {
				for (int i = 0; i < hits.Length; i++) {
					ref RaycastHit hit = ref hits[i];

					VisualUtils.DrawSphere(origin + direction * hit.distance, radius, color);
				}
			} else {
				VisualUtils.DrawSphere(origin + direction * distance, radius, color);
			}
#endif

			return hits;
		}

		public static RaycastHit[] Run(
			Vector3 origin,
			float radius,
			Vector3 direction,
			float maxDistance,
			int layerMask) {
			return Run(origin, radius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] Run(
			Vector3 origin,
			float radius,
			Vector3 direction,
			float maxDistance) {
			return Run(origin, radius, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] Run(
			Vector3 origin,
			float radius,
			Vector3 direction) {
			return Run(origin, radius, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}
		
		public static RaycastHit[] Run(
			Ray ray,
			float radius,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			return Run(ray.origin, radius, ray.direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static RaycastHit[] Run(
			Ray ray,
			float radius,
			float maxDistance,
			int layerMask) {
			return Run(ray, radius, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] Run(Ray ray, float radius, float maxDistance) {
			return Run(ray, radius, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] Run(Ray ray, float radius) {
			return Run(ray, radius, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}
	}
}