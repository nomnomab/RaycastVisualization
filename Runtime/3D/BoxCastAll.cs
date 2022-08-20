using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
	public static partial class VisualPhysics {
		/// <summary>
		///   <para>Like Physics.BoxCast, but returns all hits.</para>
		/// </summary>
		/// <param name="center">Center of the box.</param>
		/// <param name="halfExtents">Half the size of the box in each dimension.</param>
		/// <param name="direction">The direction in which to cast the box.</param>
		/// <param name="orientation">Rotation of the box.</param>
		/// <param name="maxDistance">The max length of the cast.</param>
		/// <param name="layermask">A that is used to selectively ignore colliders when casting a capsule.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <param name="layerMask"></param>
		/// <returns>
		///   <para>All colliders that were hit.</para>
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] BoxCastAll(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			[DefaultValue("Quaternion.identity")] Quaternion orientation,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
#if UNITY_EDITOR
			RaycastHit[] hits = Physics.BoxCastAll(center, halfExtents, direction, orientation, maxDistance, layerMask,
				queryTriggerInteraction);
			
			direction.Normalize();
			bool didHit = hits != null && hits.Length > 0;

			float distance = VisualUtils.GetMaxRayLength(maxDistance);
			VisualUtils.DrawArrow(center, direction * distance, VisualUtils.GetDefaultColor());
			Color color = VisualUtils.GetColor(didHit);

			if (didHit) {
				for (int i = 0; i < hits.Length; i++) {
					ref RaycastHit hit = ref hits[i];

					VisualUtils.DrawCube(center + direction * hit.distance, halfExtents, orientation, color);
				}
			} else {
				VisualUtils.DrawCube(center + direction * distance, halfExtents, orientation, color);
			}
			
			return hits;
#else
			return Physics.BoxCastAll(center, halfExtents, direction, orientation, maxDistance, layerMask, queryTriggerInteraction);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] BoxCastAll(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			Quaternion orientation,
			float maxDistance,
			int layerMask) {
#if UNITY_EDITOR
			return BoxCastAll(center, halfExtents, direction, orientation, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.BoxCastAll(center, halfExtents, direction, orientation, maxDistance, layerMask);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] BoxCastAll(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			Quaternion orientation,
			float maxDistance) {
#if UNITY_EDITOR
			return BoxCastAll(center, halfExtents, direction, orientation, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.BoxCastAll(center, halfExtents, direction, orientation, maxDistance);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] BoxCastAll(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			Quaternion orientation) {
#if UNITY_EDITOR
			return BoxCastAll(center, halfExtents, direction, orientation, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.BoxCastAll(center, halfExtents, direction, orientation);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] BoxCastAll(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction) {
#if UNITY_EDITOR
			return BoxCastAll(center, halfExtents, direction, Quaternion.identity, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.BoxCastAll(center, halfExtents, direction);
#endif
		}
	}
}