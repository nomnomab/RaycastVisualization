using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization.ThreeD {
	internal static class OverlapBox {
		public static Collider[] Run(
			Vector3 center,
			Vector3 halfExtents,
			[DefaultValue("Quaternion.identity")] Quaternion orientation,
			[DefaultValue("AllLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			Collider[] colliders = Physics.OverlapBox(center, halfExtents, orientation, layerMask, queryTriggerInteraction);

			bool didHit = colliders != null && colliders.Length > 0;

#if UNITY_EDITOR
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
#endif

			return colliders;
		}

		public static Collider[] Run(
			Vector3 center,
			Vector3 halfExtents,
			Quaternion orientation,
			int layerMask) {
			return Run(center, halfExtents, orientation, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static Collider[] Run(
			Vector3 center,
			Vector3 halfExtents,
			Quaternion orientation) {
			return Run(center, halfExtents, orientation, -1, QueryTriggerInteraction.UseGlobal);
		}

		public static Collider[] Run(Vector3 center, Vector3 halfExtents) {
			return Run(center, halfExtents, Quaternion.identity, -1, QueryTriggerInteraction.UseGlobal);
		}
	}
}