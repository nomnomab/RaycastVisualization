using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
	public static partial class VisualPhysics {
		/// <summary>
		///   <para>Cast a ray through the Scene and store the hits into the buffer.</para>
		/// </summary>
		/// <param name="ray">The starting point and direction of the ray.</param>
		/// <param name="results">The buffer to store the hits into.</param>
		/// <param name="maxDistance">The max distance the rayhit is allowed to be from the start of the ray.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a ray.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <returns>
		///   <para>The amount of hits stored into the results buffer.</para>
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int RaycastNonAlloc(
			Ray ray,
			RaycastHit[] results,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
#if UNITY_EDITOR
			return RaycastNonAlloc(ray.origin, ray.direction, results, maxDistance, layerMask, queryTriggerInteraction);
#else
			return Physics.RaycastNonAlloc(ray, results, maxDistance, layerMask, queryTriggerInteraction);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int RaycastNonAlloc(
			Ray ray,
			RaycastHit[] results,
			float maxDistance,
			int layerMask) {
#if UNITY_EDITOR
			return RaycastNonAlloc(ray, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.RaycastNonAlloc(ray, results, maxDistance, layerMask);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int RaycastNonAlloc(Ray ray, RaycastHit[] results, float maxDistance) {
#if UNITY_EDITOR
			return RaycastNonAlloc(ray, results, maxDistance, Physics.DefaultRaycastLayers, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.RaycastNonAlloc(ray, results, maxDistance);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int RaycastNonAlloc(Ray ray, RaycastHit[] results) {
#if UNITY_EDITOR
			return RaycastNonAlloc(ray, results, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.RaycastNonAlloc(ray, results);
#endif
		}
		
		/// <summary>
		///   <para>Cast a ray through the Scene and store the hits into the buffer.</para>
		/// </summary>
		/// <param name="origin">The starting point and direction of the ray.</param>
		/// <param name="results">The buffer to store the hits into.</param>
		/// <param name="direction">The direction of the ray.</param>
		/// <param name="maxDistance">The max distance the rayhit is allowed to be from the start of the ray.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a ray.</param>
		/// <returns>
		///   <para>The amount of hits stored into the results buffer.</para>
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int RaycastNonAlloc(
			Vector3 origin,
			Vector3 direction,
			RaycastHit[] results,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
#if UNITY_EDITOR
			int hitCount = Physics.defaultPhysicsScene.Raycast(origin, direction, results, maxDistance, layerMask, queryTriggerInteraction);

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

			return hitCount;
#else
			return Physics.RaycastNonAlloc(origin, direction, results, maxDistance, layerMask, queryTriggerInteraction);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int RaycastNonAlloc(
			Vector3 origin,
			Vector3 direction,
			RaycastHit[] results,
			float maxDistance,
			int layerMask) {
#if UNITY_EDITOR
			return RaycastNonAlloc(origin, direction, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.RaycastNonAlloc(origin, direction, results, maxDistance, layerMask);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int RaycastNonAlloc(
			Vector3 origin,
			Vector3 direction,
			RaycastHit[] results,
			float maxDistance) {
#if UNITY_EDITOR
			return RaycastNonAlloc(origin, direction, results, maxDistance, Physics.DefaultRaycastLayers, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.RaycastNonAlloc(origin, direction, results, maxDistance);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results) {
#if UNITY_EDITOR
			return RaycastNonAlloc(origin, direction, results, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.RaycastNonAlloc(origin, direction, results);
#endif
		}
	}
}