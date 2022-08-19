using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
	public static partial class VisualPhysics {
		/// <summary>
		///   <para>Cast sphere along the direction and store the results into buffer.</para>
		/// </summary>
		/// <param name="origin">The center of the sphere at the start of the sweep.</param>
		/// <param name="radius">The radius of the sphere.</param>
		/// <param name="direction">The direction in which to sweep the sphere.</param>
		/// <param name="results">The buffer to save the hits into.</param>
		/// <param name="maxDistance">The max length of the sweep.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a sphere.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <returns>
		///   <para>The amount of hits stored into the results buffer.</para>
		/// </returns>
		public static int SphereCastNonAlloc(
			Vector3 origin,
			float radius,
			Vector3 direction,
			RaycastHit[] results,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
#if UNITY_EDITOR
			int count = Physics.defaultPhysicsScene.SphereCast(origin, radius, direction, results, maxDistance, layerMask,
				queryTriggerInteraction);
			
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

			return count;
#else
			return Physics.SphereCastNonAlloc(origin, radius, direction, results, maxDistance, layerMask, queryTriggerInteraction);
#endif
		}

		public static int SphereCastNonAlloc(
			Vector3 origin,
			float radius,
			Vector3 direction,
			RaycastHit[] results,
			float maxDistance,
			int layerMask) {
#if UNITY_EDITOR
			return SphereCastNonAlloc(origin, radius, direction, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.SphereCastNonAlloc(origin, radius, direction, results, maxDistance, layerMask);
#endif
		}

		public static int SphereCastNonAlloc(
			Vector3 origin,
			float radius,
			Vector3 direction,
			RaycastHit[] results,
			float maxDistance) {
#if UNITY_EDITOR
			return SphereCastNonAlloc(origin, radius, direction, results, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.SphereCastNonAlloc(origin, radius, direction, results, maxDistance);
#endif
		}

		public static int SphereCastNonAlloc(
			Vector3 origin,
			float radius,
			Vector3 direction,
			RaycastHit[] results) {
#if UNITY_EDITOR
			return SphereCastNonAlloc(origin, radius, direction, results, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.SphereCastNonAlloc(origin, radius, direction, results);
#endif
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
		public static int SphereCastNonAlloc(
			Ray ray,
			float radius,
			RaycastHit[] results,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
#if UNITY_EDITOR
			return SphereCastNonAlloc(ray.origin, radius, ray.direction, results, maxDistance, layerMask, queryTriggerInteraction);
#else
			return Physics.SphereCastNonAlloc(ray, radius, results, maxDistance, layerMask, queryTriggerInteraction);
#endif
		}

		public static int SphereCastNonAlloc(
			Ray ray,
			float radius,
			RaycastHit[] results,
			float maxDistance,
			int layerMask) {
#if UNITY_EDITOR
			return SphereCastNonAlloc(ray, radius, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.SphereCastNonAlloc(ray, radius, results, maxDistance, layerMask);
#endif
		}

		public static int SphereCastNonAlloc(
			Ray ray,
			float radius,
			RaycastHit[] results,
			float maxDistance) {
#if UNITY_EDITOR
			return SphereCastNonAlloc(ray, radius, results, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.SphereCastNonAlloc(ray, radius, results, maxDistance);
#endif
		}

		public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results) {
#if UNITY_EDITOR
			return SphereCastNonAlloc(ray, radius, results, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.SphereCastNonAlloc(ray, radius, results);
#endif
		}
	}
}