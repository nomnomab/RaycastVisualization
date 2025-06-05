#if RAYCASTVISUALIZATION_3D_PHYSICS
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
	public static partial class VisualPhysics {
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider[] OverlapCapsule(
			Vector3 point0,
			Vector3 point1,
			float radius,
			[DefaultValue("AllLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
#if UNITY_EDITOR
			Collider[] colliders = Physics.OverlapCapsule(point0, point1, radius, layerMask, queryTriggerInteraction);

			bool didHit = colliders != null && colliders.Length > 0;

			VisualUtils.DrawCapsuleNoColor(point0, point1, Vector3.zero, radius, 0, default, didHit);

			if (didHit) {
				Vector3 center = (point0 + point1) * 0.5f;
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
			return Physics.OverlapCapsule(point0, point1, radius, layerMask, queryTriggerInteraction);
#endif
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider[] OverlapCapsule(
			Vector3 point0,
			Vector3 point1,
			float radius,
			int layerMask) {
#if UNITY_EDITOR
			return OverlapCapsule(point0, point1, radius, layerMask, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.OverlapCapsule(point0, point1, radius, layerMask);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider[] OverlapCapsule(Vector3 point0, Vector3 point1, float radius) {
#if UNITY_EDITOR
			return OverlapCapsule(point0, point1, radius, -1, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.OverlapCapsule(point0, point1, radius);
#endif
		}
	}
}
#endif