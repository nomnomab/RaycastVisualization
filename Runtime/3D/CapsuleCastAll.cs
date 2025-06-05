#if RAYCASTVISUALIZATION_3D_PHYSICS
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
	public static partial class VisualPhysics {
		/// <summary>
		///   <para>Like Physics.CapsuleCast, but this function will return all hits the capsule sweep intersects.</para>
		/// </summary>
		/// <param name="point1">The center of the sphere at the start of the capsule.</param>
		/// <param name="point2">The center of the sphere at the end of the capsule.</param>
		/// <param name="radius">The radius of the capsule.</param>
		/// <param name="direction">The direction into which to sweep the capsule.</param>
		/// <param name="maxDistance">The max length of the sweep.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a capsule.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <returns>
		///   <para>An array of all colliders hit in the sweep.</para>
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] CapsuleCastAll(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
#if UNITY_EDITOR
			RaycastHit[] hits = Physics.CapsuleCastAll(point1, point2, radius, direction, maxDistance, layerMask, queryTriggerInteraction);

			direction.Normalize();
			bool didHit = hits != null && hits.Length != 0;

			float distance = VisualUtils.GetMaxRayLength(maxDistance);
			Vector3 origin = (point1 + point2) * 0.5f;
			VisualUtils.DrawArrow(origin, direction * distance, VisualUtils.GetDefaultColor());

			if (didHit) {
				foreach (RaycastHit hit in hits) {
					VisualUtils.DrawCapsule(point1, point2, direction, radius, maxDistance, hit, didHit);

					VisualUtils.DrawNormalCircle(hit.point, hit.normal, Color.green);
				}
			} else {
				VisualUtils.DrawCapsule(point1, point2, direction, radius, maxDistance, default, didHit);
			}

			return hits;
#else
			return Physics.CapsuleCastAll(point1, point2, radius, direction, maxDistance, layerMask, queryTriggerInteraction);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] CapsuleCastAll(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			float maxDistance,
			int layerMask) {
#if UNITY_EDITOR
			return CapsuleCastAll(point1, point2, radius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.CapsuleCastAll(point1, point2, radius, direction, maxDistance, layerMask);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] CapsuleCastAll(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			float maxDistance) {
#if UNITY_EDITOR
			return CapsuleCastAll(point1, point2, radius, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.CapsuleCastAll(point1, point2, radius, direction, maxDistance);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] CapsuleCastAll(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction) {
#if UNITY_EDITOR
			return CapsuleCastAll(point1, point2, radius, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.CapsuleCastAll(point1, point2, radius, direction);
#endif
		}
	}
}
#endif