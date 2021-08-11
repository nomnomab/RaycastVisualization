using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization.ThreeD {
	internal static class SpherecastNonAlloc {
		public static int Run(
			Vector3 origin,
			float radius,
			Vector3 direction,
			RaycastHit[] results,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			int count = Physics.defaultPhysicsScene.SphereCast(origin, radius, direction, results, maxDistance, layerMask,
				queryTriggerInteraction);

#if UNITY_EDITOR
			direction.Normalize();
			bool didHit = count > 0;

			float distance = VisualUtils.GetMaxRayLength(maxDistance);
			VisualUtils.DrawArrow(origin, direction * distance, VisualUtils.GetDefaultColor());
			Color color = VisualUtils.GetColor(didHit);

			if (didHit) {
				for (int i = 0; i < count; i++) {
					ref RaycastHit hit = ref results[i];

					VisualUtils.DrawSphere(origin + direction * hit.distance, radius, color);
				}
			} else {
				VisualUtils.DrawSphere(origin + direction * distance, radius, color);
			}
#endif

			return count;
		}

		public static int Run(
			Vector3 origin,
			float radius,
			Vector3 direction,
			RaycastHit[] results,
			float maxDistance,
			int layerMask) {
			return Run(origin, radius, direction, results, maxDistance, layerMask,
				QueryTriggerInteraction.UseGlobal);
		}

		public static int Run(
			Vector3 origin,
			float radius,
			Vector3 direction,
			RaycastHit[] results,
			float maxDistance) {
			return Run(origin, radius, direction, results, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static int Run(
			Vector3 origin,
			float radius,
			Vector3 direction,
			RaycastHit[] results) {
			return Run(origin, radius, direction, results, float.PositiveInfinity, -5,
				QueryTriggerInteraction.UseGlobal);
		}

		/// <summary>
		///   <para>Cast sphere along the direction and store the results into buffer.</para>
		/// </summary>
		/// <param name="ray">The starting point and direction of the ray into which the sphere sweep is cast.</param>
		/// <param name="radius">The radius of the sphere.</param>
		/// <param name="results">The buffer to save the results to.</param>
		/// <param name="maxDistance">The max length of the sweep.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a sphere.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <returns>
		///   <para>The amount of hits stored into the results buffer.</para>
		/// </returns>
		public static int Run(
			Ray ray,
			float radius,
			RaycastHit[] results,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			return Run(ray.origin, radius, ray.direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static int Run(
			Ray ray,
			float radius,
			RaycastHit[] results,
			float maxDistance,
			int layerMask) {
			return Run(ray, radius, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static int Run(
			Ray ray,
			float radius,
			RaycastHit[] results,
			float maxDistance) {
			return Run(ray, radius, results, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static int Run(Ray ray, float radius, RaycastHit[] results) {
			return Run(ray, radius, results, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}
	}
}