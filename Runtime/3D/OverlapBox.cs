using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
	public static partial class VisualPhysics {
		/// <summary>
		///   <para>Find all colliders touching or inside of the given box.</para>
		/// </summary>
		/// <param name="center">Center of the box.</param>
		/// <param name="halfExtents">Half of the size of the box in each dimension.</param>
		/// <param name="orientation">Rotation of the box.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a ray.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <returns>
		///   <para>Colliders that overlap with the given box.</para>
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider[] OverlapBox(
			Vector3 center,
			Vector3 halfExtents,
			[DefaultValue("Quaternion.identity")] Quaternion orientation,
			[DefaultValue("AllLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
#if UNITY_EDITOR
			Collider[] colliders = Physics.OverlapBox(center, halfExtents, orientation, layerMask, queryTriggerInteraction);

			bool didHit = colliders != null && colliders.Length > 0;
			
			VisualUtils.DrawCube(center, halfExtents, orientation, VisualUtils.GetDefaultColor());

			if (didHit) {
				Color color = VisualUtils.GetColor(didHit);

				foreach (Collider collider in colliders) {
					Vector3 colliderPos = collider.transform.position;
					Vector3 closestPoint = Physics.ClosestPoint(center, collider, colliderPos, collider.transform.rotation);
					Vector3 dir = closestPoint - center;
					VisualUtils.DrawArrow(center, dir, color);
				}
			}
			
			return colliders;
#else
			return Physics.OverlapBox(center, halfExtents, orientation, layerMask, queryTriggerInteraction);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider[] OverlapBox(
			Vector3 center,
			Vector3 halfExtents,
			Quaternion orientation,
			int layerMask) {
#if UNITY_EDITOR
			return OverlapBox(center, halfExtents, orientation, layerMask, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.OverlapBox(center, halfExtents, orientation, layerMask);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider[] OverlapBox(
			Vector3 center,
			Vector3 halfExtents,
			Quaternion orientation) {
#if UNITY_EDITOR
			return OverlapBox(center, halfExtents, orientation, -1, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.OverlapBox(center, halfExtents, orientation);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents) {
#if UNITY_EDITOR
			return OverlapBox(center, halfExtents, Quaternion.identity, -1, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.OverlapBox(center, halfExtents);
#endif
		}
	}
}