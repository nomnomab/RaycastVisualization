using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization.ThreeD {
	internal static class RaycastAll {
		public static RaycastHit[] Run(
			Vector3 origin,
			Vector3 direction,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			RaycastHit[] hits = Physics.RaycastAll(origin, direction, maxDistance, layerMask, queryTriggerInteraction);

#if UNITY_EDITOR
			direction.Normalize();
			bool didHit = hits != null && hits.Length != 0;
			Color color = VisualUtils.GetColor(didHit);

			float distance = VisualUtils.GetMaxRayLength(maxDistance);
			VisualUtils.DrawArrow(origin, direction * distance, VisualUtils.GetDefaultColor());

			if (!didHit) {
				return hits;
			}

			for (int i = 0; i < hits.Length; i++) {
				ref RaycastHit hit = ref hits[i];
				VisualUtils.DrawNormalCircle(hit.point, hit.normal, color);
			}
#endif

			return hits;
		}

		public static RaycastHit[] Run(
			Vector3 origin,
			Vector3 direction,
			float maxDistance,
			int layerMask) {
			return Run(origin, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] Run(
			Vector3 origin,
			Vector3 direction,
			float maxDistance) {
			return Run(origin, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] Run(Vector3 origin, Vector3 direction) {
			return Run(origin, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		/// <summary>
		///   <para>Casts a ray through the Scene and returns all hits. Note that order of the results is undefined.</para>
		/// </summary>
		/// <param name="ray">The starting point and direction of the ray.</param>
		/// <param name="maxDistance">The max distance the rayhit is allowed to be from the start of the ray.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a ray.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <returns>
		///   <para>An array of RaycastHit objects. Note that the order of the results is undefined.</para>
		/// </returns>
		public static RaycastHit[] Run(
			Ray ray,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			return Run(ray.origin, ray.direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static RaycastHit[] Run(Ray ray, float maxDistance, int layerMask) {
			return Run(ray.origin, ray.direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] Run(Ray ray, float maxDistance) {
			return Run(ray.origin, ray.direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] Run(Ray ray) {
			return Run(ray.origin, ray.direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}
	}
}