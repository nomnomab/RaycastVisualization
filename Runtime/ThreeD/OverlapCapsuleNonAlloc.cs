using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization.ThreeD {
	internal static class OverlapCapsuleNonAlloc {
		public static int Run(
			Vector3 point0,
			Vector3 point1,
			float radius,
			Collider[] results,
			[DefaultValue("AllLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			int count = Physics.defaultPhysicsScene.OverlapCapsule(point0, point1, radius, results, layerMask, queryTriggerInteraction);
			
#if UNITY_EDITOR
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
#endif
			return count;
		}

		public static int Run(
			Vector3 point0,
			Vector3 point1,
			float radius,
			Collider[] results,
			int layerMask) {
			return Run(point0, point1, radius, results, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static int Run(
			Vector3 point0,
			Vector3 point1,
			float radius,
			Collider[] results) {
			return Run(point0, point1, radius, results, -1, QueryTriggerInteraction.UseGlobal);
		}
	}
}