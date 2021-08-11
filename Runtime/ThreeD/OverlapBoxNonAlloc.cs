using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization.ThreeD {
	internal static class OverlapBoxNonAlloc {
		public static int Run(
			Vector3 center,
			Vector3 halfExtents,
			Collider[] results,
			[DefaultValue("Quaternion.identity")] Quaternion orientation,
			[DefaultValue("AllLayers")] int mask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			int count = Physics.defaultPhysicsScene.OverlapBox(center, halfExtents, results, orientation, mask, queryTriggerInteraction);

#if UNITY_EDITOR
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
#endif
			return count;
		}

		public static int Run(
			Vector3 center,
			Vector3 halfExtents,
			Collider[] results,
			Quaternion orientation,
			int mask) {
			return Run(center, halfExtents, results, orientation, mask, QueryTriggerInteraction.UseGlobal);
		}

		public static int Run(
			Vector3 center,
			Vector3 halfExtents,
			Collider[] results,
			Quaternion orientation) {
			return Run(center, halfExtents, results, orientation, -1, QueryTriggerInteraction.UseGlobal);
		}

		public static int Run(Vector3 center, Vector3 halfExtents, Collider[] results) {
			return Run(center, halfExtents, results, Quaternion.identity, -1, QueryTriggerInteraction.UseGlobal);
		}
	}
}