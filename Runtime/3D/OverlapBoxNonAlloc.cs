#if RAYCASTVISUALIZATION_3D_PHYSICS
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
	public static partial class VisualPhysics {
		/// <summary>
		///   <para>Find all colliders touching or inside of the given box, and store them into the buffer.</para>
		/// </summary>
		/// <param name="center">Center of the box.</param>
		/// <param name="halfExtents">Half of the size of the box in each dimension.</param>
		/// <param name="results">The buffer to store the results in.</param>
		/// <param name="orientation">Rotation of the box.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a ray.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <param name="mask"></param>
		/// <returns>
		///   <para>The amount of colliders stored in results.</para>
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapBoxNonAlloc(
			Vector3 center,
			Vector3 halfExtents,
			Collider[] results,
			[DefaultValue("Quaternion.identity")] Quaternion orientation,
			[DefaultValue("AllLayers")] int mask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
#if UNITY_EDITOR
			int count = Physics.defaultPhysicsScene.OverlapBox(center, halfExtents, results, orientation, mask, queryTriggerInteraction);
			
			bool didHit = count > 0;

			VisualUtils.DrawCube(center, halfExtents, orientation, VisualUtils.GetDefaultColor());

			if (didHit) {
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
			return Physics.OverlapBoxNonAlloc(center, halfExtents, results, orientation, mask, queryTriggerInteraction);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapBoxNonAlloc(
			Vector3 center,
			Vector3 halfExtents,
			Collider[] results,
			Quaternion orientation,
			int mask) {
#if UNITY_EDITOR
			return OverlapBoxNonAlloc(center, halfExtents, results, orientation, mask, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.OverlapBoxNonAlloc(center, halfExtents, results, orientation, mask);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapBoxNonAlloc(
			Vector3 center,
			Vector3 halfExtents,
			Collider[] results,
			Quaternion orientation) {
#if UNITY_EDITOR
			return OverlapBoxNonAlloc(center, halfExtents, results, orientation, -1, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.OverlapBoxNonAlloc(center, halfExtents, results, orientation);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results) {
#if UNITY_EDITOR
			return OverlapBoxNonAlloc(center, halfExtents, results, Quaternion.identity, -1, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.OverlapBoxNonAlloc(center, halfExtents, results);
#endif
		}
	}
}
#endif