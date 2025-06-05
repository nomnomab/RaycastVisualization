#if RAYCASTVISUALIZATION_3D_PHYSICS
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
	public static partial class VisualPhysics {
		/// <summary>
		///   <para>Like Physics.SphereCast, but this function will return all hits the sphere sweep intersects.</para>
		/// </summary>
		/// <param name="origin">The center of the sphere at the start of the sweep.</param>
		/// <param name="radius">The radius of the sphere.</param>
		/// <param name="direction">The direction in which to sweep the sphere.</param>
		/// <param name="maxDistance">The max length of the sweep.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a sphere.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <returns>
		///   <para>An array of all colliders hit in the sweep.</para>
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] SphereCastAll(
			Vector3 origin,
			float radius,
			Vector3 direction,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
#if UNITY_EDITOR
			RaycastHit[] hits = Physics.SphereCastAll(origin, radius, direction, maxDistance, layerMask, queryTriggerInteraction);
			
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

			return hits;
#else
			return Physics.SphereCastAll(origin, radius, direction, maxDistance, layerMask, queryTriggerInteraction);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] SphereCastAll(
			Vector3 origin,
			float radius,
			Vector3 direction,
			float maxDistance,
			int layerMask) {
#if UNITY_EDITOR
			return SphereCastAll(origin, radius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.SphereCastAll(origin, radius, direction, maxDistance, layerMask);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] SphereCastAll(
			Vector3 origin,
			float radius,
			Vector3 direction,
			float maxDistance) {
#if UNITY_EDITOR
			return SphereCastAll(origin, radius, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.SphereCastAll(origin, radius, direction, maxDistance);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] SphereCastAll(
			Vector3 origin,
			float radius,
			Vector3 direction) {
#if UNITY_EDITOR
			return SphereCastAll(origin, radius, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.SphereCastAll(origin, radius, direction);
#endif
		}
		
		/// <summary>
		///   <para>Like Physics.SphereCast, but this function will return all hits the sphere sweep intersects.</para>
		/// </summary>
		/// <param name="ray">The starting point and direction of the ray into which the sphere sweep is cast.</param>
		/// <param name="radius">The radius of the sphere.</param>
		/// <param name="maxDistance">The max length of the sweep.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a sphere.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] SphereCastAll(
			Ray ray,
			float radius,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
#if UNITY_EDITOR
			return SphereCastAll(ray.origin, radius, ray.direction, maxDistance, layerMask, queryTriggerInteraction);
#else
			return Physics.SphereCastAll(ray, radius, maxDistance, layerMask, queryTriggerInteraction);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] SphereCastAll(
			Ray ray,
			float radius,
			float maxDistance,
			int layerMask) {
#if UNITY_EDITOR
			return SphereCastAll(ray, radius, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.SphereCastAll(ray, radius, maxDistance, layerMask);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] SphereCastAll(Ray ray, float radius, float maxDistance) {
#if UNITY_EDITOR
			return SphereCastAll(ray, radius, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.SphereCastAll(ray, radius, maxDistance);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] SphereCastAll(Ray ray, float radius) {
#if UNITY_EDITOR
			return SphereCastAll(ray, radius, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.SphereCastAll(ray, radius);
#endif
		}
	}
}
#endif