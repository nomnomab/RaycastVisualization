using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
	public static partial class VisualPhysics {
		/// <summary>
		///   <para>Check the given capsule against the physics world and return all overlapping colliders in the user-provided buffer.</para>
		/// </summary>
		/// <param name="point0">The center of the sphere at the start of the capsule.</param>
		/// <param name="point1">The center of the sphere at the end of the capsule.</param>
		/// <param name="radius">The radius of the capsule.</param>
		/// <param name="results">The buffer to store the results into.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a capsule.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <returns>
		///   <para>The amount of entries written to the buffer.</para>
		/// </returns>
		public static int OverlapCapsuleNonAlloc(
			Vector3 point0,
			Vector3 point1,
			float radius,
			Collider[] results,
			[DefaultValue("AllLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
#if UNITY_EDITOR
			int count = Physics.defaultPhysicsScene.OverlapCapsule(point0, point1, radius, results, layerMask, queryTriggerInteraction);
			
			bool didHit = count > 0;

			VisualUtils.DrawCapsuleNoColor(point0, point1, Vector3.zero, radius, 0, default, didHit);

			if (didHit) {
				Vector3 center = (point0 + point1) * 0.5f;
				Color color = VisualUtils.GetColor(didHit);

				for (int i = 0; i < count; i++) {
					Collider collider = results[i];
					Vector3 colliderPos = collider.transform.position;
					Vector3 closestPoint = Physics.ClosestPoint(center, collider, colliderPos, collider.transform.rotation);
					Vector3 dir = closestPoint - center;
					VisualUtils.DrawArrow(center, dir, color);
				}
			}
			
			return count;
#else
			return Physics.OverlapCapsuleNonAlloc(point0, point1, radius, results, layerMask, queryTriggerInteraction);
#endif
		}

		public static int OverlapCapsuleNonAlloc(
			Vector3 point0,
			Vector3 point1,
			float radius,
			Collider[] results,
			int layerMask) {
#if UNITY_EDITOR
			return OverlapCapsuleNonAlloc(point0, point1, radius, results, layerMask, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.OverlapCapsuleNonAlloc(point0, point1, radius, results, layerMask);
#endif
		}

		public static int OverlapCapsuleNonAlloc(
			Vector3 point0,
			Vector3 point1,
			float radius,
			Collider[] results) {
#if UNITY_EDITOR
			return OverlapCapsuleNonAlloc(point0, point1, radius, results, -1, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.OverlapCapsuleNonAlloc(point0, point1, radius, results);
#endif
		}
	}
}