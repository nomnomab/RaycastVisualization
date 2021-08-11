using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization.ThreeD {
	internal static class RaycastNonAlloc {
		public static int Run(
			Ray ray,
			RaycastHit[] results,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			return Run(ray.origin, ray.direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static int Run(
			Ray ray,
			RaycastHit[] results,
			float maxDistance,
			int layerMask) {
			return Run(ray, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static int Run(Ray ray, RaycastHit[] results, float maxDistance) {
			return Run(ray, results, maxDistance, Physics.DefaultRaycastLayers, QueryTriggerInteraction.UseGlobal);
		}

		public static int Run(Ray ray, RaycastHit[] results) {
			return Run(ray, results, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.UseGlobal);
		}
		
		public static int Run(
			Vector3 origin,
			Vector3 direction,
			RaycastHit[] results,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			int hitCount = Physics.defaultPhysicsScene.Raycast(origin, direction, results, maxDistance, layerMask, queryTriggerInteraction);

#if UNITY_EDITOR
			direction.Normalize();
			bool didHit = hitCount > 0;
			Color color = VisualUtils.GetColor(didHit);

			float distance = VisualUtils.GetMaxRayLength(maxDistance);
			VisualUtils.DrawArrow(origin, direction * distance, VisualUtils.GetDefaultColor());

			if (!didHit) {
				return hitCount;
			}

			for (int i = 0; i < hitCount; i++) {
				ref RaycastHit hit = ref results[i];

				VisualUtils.DrawNormalCircle(hit.point, hit.normal, color);
			}
#endif

			return hitCount;
		}

		public static int Run(
			Vector3 origin,
			Vector3 direction,
			RaycastHit[] results,
			float maxDistance,
			int layerMask) {
			return Run(origin, direction, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static int Run(
			Vector3 origin,
			Vector3 direction,
			RaycastHit[] results,
			float maxDistance) {
			return Run(origin, direction, results, maxDistance, Physics.DefaultRaycastLayers,
				QueryTriggerInteraction.UseGlobal);
		}

		public static int Run(Vector3 origin, Vector3 direction, RaycastHit[] results) {
			return Run(origin, direction, results, Mathf.Infinity, Physics.DefaultRaycastLayers,
				QueryTriggerInteraction.UseGlobal);
		}
	}
}