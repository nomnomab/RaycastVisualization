#if RAYCASTVISUALIZATION_3D_PHYSICS
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
	public static partial class VisualPhysics {
		/// <summary>
		///   <para>Cast the box along the direction, and store hits in the provided buffer.</para>
		/// </summary>
		/// <param name="center">Center of the box.</param>
		/// <param name="halfExtents">Half the size of the box in each dimension.</param>
		/// <param name="direction">The direction in which to cast the box.</param>
		/// <param name="results">The buffer to store the results in.</param>
		/// <param name="orientation">Rotation of the box.</param>
		/// <param name="maxDistance">The max length of the cast.</param>
		/// <param name="layermask">A that is used to selectively ignore colliders when casting a capsule.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <param name="layerMask"></param>
		/// <returns>
		///   <para>The amount of hits stored to the results buffer.</para>
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BoxCastNonAlloc(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			RaycastHit[] results,
			[DefaultValue("Quaternion.identity")] Quaternion orientation,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
#if UNITY_EDITOR
			int count = Physics.defaultPhysicsScene.BoxCast(center, halfExtents, direction, results, orientation, maxDistance, layerMask,
				queryTriggerInteraction);

			direction.Normalize();
			bool didHit = count > 0;

			float distance = VisualUtils.GetMaxRayLength(maxDistance);
			VisualUtils.DrawArrow(center, direction * distance, VisualUtils.GetDefaultColor());
			Color color = VisualUtils.GetColor(didHit);

			if (didHit) {
				for (int i = 0; i < count; i++) {
					ref RaycastHit hit = ref results[i];
					VisualUtils.DrawCube(center + direction * hit.distance, halfExtents, orientation, color);
				}
			} else {
				VisualUtils.DrawCube(center + direction * distance, halfExtents, orientation, color);
			}

			return count;
#else
			return Physics.BoxCastNonAlloc(center, halfExtents, direction, results, orientation, maxDistance, layerMask, queryTriggerInteraction);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BoxCastNonAlloc(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			RaycastHit[] results,
			Quaternion orientation) {
#if UNITY_EDITOR
			return BoxCastNonAlloc(center, halfExtents, direction, results, orientation, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.BoxCastNonAlloc(center, halfExtents, direction, results, orientation);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BoxCastNonAlloc(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			RaycastHit[] results,
			Quaternion orientation,
			float maxDistance) {
#if UNITY_EDITOR
			return BoxCastNonAlloc(center, halfExtents, direction, results, orientation, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.BoxCastNonAlloc(center, halfExtents, direction, results, orientation);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BoxCastNonAlloc(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			RaycastHit[] results,
			Quaternion orientation,
			float maxDistance,
			int layerMask) {
#if UNITY_EDITOR
			return BoxCastNonAlloc(center, halfExtents, direction, results, orientation, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.BoxCastNonAlloc(center, halfExtents, direction, results, orientation, maxDistance, layerMask);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BoxCastNonAlloc(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			RaycastHit[] results) {
#if UNITY_EDITOR
			return BoxCastNonAlloc(center, halfExtents, direction, results, Quaternion.identity, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.BoxCastNonAlloc(center, halfExtents, direction, results);
#endif
		}
	}
}
#endif