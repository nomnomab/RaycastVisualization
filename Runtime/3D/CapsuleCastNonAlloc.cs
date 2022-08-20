using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
	public static partial class VisualPhysics {
		/// <summary>
		///   <para>Casts a capsule against all colliders in the Scene and returns detailed information on what was hit into the buffer.</para>
		/// </summary>
		/// <param name="point1">The center of the sphere at the start of the capsule.</param>
		/// <param name="point2">The center of the sphere at the end of the capsule.</param>
		/// <param name="radius">The radius of the capsule.</param>
		/// <param name="direction">The direction into which to sweep the capsule.</param>
		/// <param name="results">The buffer to store the hits into.</param>
		/// <param name="maxDistance">The max length of the sweep.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a capsule.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <returns>
		///   <para>The amount of hits stored into the buffer.</para>
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CapsuleCastNonAlloc(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			RaycastHit[] results,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
#if UNITY_EDITOR
			int count = Physics.defaultPhysicsScene.CapsuleCast(point1, point2, radius, direction, results, maxDistance, layerMask,
				queryTriggerInteraction);
			
			direction.Normalize();
			bool didHit = count > 0;
			float distance = VisualUtils.GetMaxRayLength(maxDistance);
			Vector3 origin = (point1 + point2) * 0.5f;

			VisualUtils.DrawArrow(origin, direction * distance, VisualUtils.GetDefaultColor());

			if (didHit) {
				for (int i = 0; i < count; i++) {
					ref RaycastHit hit = ref results[i];
					VisualUtils.DrawCapsule(point1, point2, direction, radius, maxDistance, hit, didHit);
					VisualUtils.DrawNormalCircle(hit.point, hit.normal, VisualUtils.GetColor(didHit));
				}
			} else {
				VisualUtils.DrawCapsule(point1, point2, direction, radius, maxDistance, default, didHit);
			}

			return count;
#else
			return Physics.CapsuleCastNonAlloc(point1, point2, radius, direction, results, maxDistance, layerMask, queryTriggerInteraction);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CapsuleCastNonAlloc(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			RaycastHit[] results,
			float maxDistance,
			int layerMask) {
#if UNITY_EDITOR
			return CapsuleCastNonAlloc(point1, point2, radius, direction, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.CapsuleCastNonAlloc(point1, point2, radius, direction, results, maxDistance, layerMask);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CapsuleCastNonAlloc(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			RaycastHit[] results,
			float maxDistance) {
#if UNITY_EDITOR
			return CapsuleCastNonAlloc(point1, point2, radius, direction, results, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.CapsuleCastNonAlloc(point1, point2, radius, direction, results, maxDistance);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CapsuleCastNonAlloc(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			RaycastHit[] results) {
#if UNITY_EDITOR
			return CapsuleCastNonAlloc(point1, point2, radius, direction, results, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.CapsuleCastNonAlloc(point1, point2, radius, direction, results);
#endif
		}
	}
}