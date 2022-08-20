using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
	public static partial class VisualPhysics {
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SphereCast(
			Vector3 origin,
			float radius,
			Vector3 direction,
			out RaycastHit hit,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction) {

#if UNITY_EDITOR
			bool didHit = Physics.defaultPhysicsScene.SphereCast(origin, radius, direction, out hit, maxDistance, layerMask,
				queryTriggerInteraction);
			
			direction.Normalize();
			Color color = VisualUtils.GetColor(didHit);
			float distance = VisualUtils.GetMaxRayLength(maxDistance);
			VisualUtils.DrawArrow(origin, direction * distance, VisualUtils.GetDefaultColor());

			if (didHit) {
				VisualUtils.DrawNormalCircle(hit.point, hit.normal, color);
				VisualUtils.DrawSphere(origin + direction * hit.distance, radius, color);
			} else {
				VisualUtils.DrawSphere(origin + direction * distance, radius, color);
			}

			return didHit;
#else
			return Physics.SphereCast(origin, radius, direction, out hit, maxDistance, layerMask, queryTriggerInteraction);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SphereCast(
			Vector3 origin,
			float radius,
			Vector3 direction,
			out RaycastHit hit,
			float maxDistance,
			int layerMask) {
#if UNITY_EDITOR
			return SphereCast(origin, radius, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.SphereCast(origin, radius, direction, out hit, maxDistance, layerMask);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SphereCast(
			Vector3 origin,
			float radius,
			Vector3 direction,
			out RaycastHit hit,
			float maxDistance) {
#if UNITY_EDITOR
			return SphereCast(origin, radius, direction, out hit, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.SphereCast(origin, radius, direction, out hit, maxDistance);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SphereCast(
			Vector3 origin,
			float radius,
			Vector3 direction,
			out RaycastHit hit) {
#if UNITY_EDITOR
			return SphereCast(origin, radius, direction, out hit, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.SphereCast(origin, radius, direction, out hit);
#endif
		}

		/// <summary>
		///   <para>Casts a sphere along a ray and returns detailed information on what was hit.</para>
		/// </summary>
		/// <param name="ray">The starting point and direction of the ray into which the sphere sweep is cast.</param>
		/// <param name="radius">The radius of the sphere.</param>
		/// <param name="maxDistance">The max length of the cast.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a capsule.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <returns>
		///   <para>True when the sphere sweep intersects any collider, otherwise false.</para>
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SphereCast(
			Ray ray,
			float radius,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
#if UNITY_EDITOR
			return SphereCast(ray.origin, radius, ray.direction, out RaycastHit _, maxDistance, layerMask, queryTriggerInteraction);
#else
			return Physics.SphereCast(ray, radius, maxDistance, layerMask, queryTriggerInteraction);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SphereCast(Ray ray, float radius, float maxDistance, int layerMask) {
#if UNITY_EDITOR
			return SphereCast(ray, radius, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.SphereCast(ray, radius, maxDistance, layerMask);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SphereCast(Ray ray, float radius, float maxDistance) {
#if UNITY_EDITOR
			return SphereCast(ray, radius, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.SphereCast(ray, radius, maxDistance);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SphereCast(Ray ray, float radius) {
#if UNITY_EDITOR
			return SphereCast(ray, radius, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.SphereCast(ray, radius);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SphereCast(
			Ray ray,
			float radius,
			out RaycastHit hitInfo,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
#if UNITY_EDITOR
			return SphereCast(ray.origin, radius, ray.direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
#else
			return Physics.SphereCast(ray, radius, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SphereCast(
			Ray ray,
			float radius,
			out RaycastHit hitInfo,
			float maxDistance,
			int layerMask) {
#if UNITY_EDITOR
			return SphereCast(ray, radius, out hitInfo, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.SphereCast(ray, radius, out hitInfo, maxDistance, layerMask);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SphereCast(
			Ray ray,
			float radius,
			out RaycastHit hitInfo,
			float maxDistance) {
#if UNITY_EDITOR
			return SphereCast(ray, radius, out hitInfo, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.SphereCast(ray, radius, out hitInfo, maxDistance);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo) {
#if UNITY_EDITOR
			return SphereCast(ray, radius, out hitInfo, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.SphereCast(ray, radius, out hitInfo);
#endif
		}
	}
}