#if RAYCASTVISUALIZATION_3D_PHYSICS
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
	public static partial class VisualPhysics {
		/// <summary>
		///   <para>See Also: Raycast.</para>
		/// </summary>
		/// <param name="origin">The starting point of the ray in world coordinates.</param>
		/// <param name="direction">The direction of the ray.</param>
		/// <param name="maxDistance">The max distance the rayhit is allowed to be from the start of the ray.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a ray.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] RaycastAll(
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] RaycastAll(
			Vector3 origin,
			Vector3 direction,
			float maxDistance,
			int layerMask) {
			return RaycastAll(origin, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] RaycastAll(
			Vector3 origin,
			Vector3 direction,
			float maxDistance) {
			return RaycastAll(origin, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction) {
			return RaycastAll(origin, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
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
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] RaycastAll(
			Ray ray,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			return RaycastAll(ray.origin, ray.direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] RaycastAll(Ray ray, float maxDistance, int layerMask) {
			return RaycastAll(ray.origin, ray.direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] RaycastAll(Ray ray, float maxDistance) {
			return RaycastAll(ray.origin, ray.direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] RaycastAll(Ray ray) {
			return RaycastAll(ray.origin, ray.direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}
	}
}
#endif