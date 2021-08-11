using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization.ThreeD {
	internal static class OverlapSphere {
		public static Collider[] Run(
			Vector3 position,
			float radius,
			[DefaultValue("AllLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			Collider[] colliders = Physics.OverlapSphere(position, radius, layerMask, queryTriggerInteraction);

#if UNITY_EDITOR
			bool didHit = colliders != null && colliders.Length > 0;

			VisualUtils.DrawSphere(position, radius, VisualUtils.GetDefaultColor());

			if (didHit) {
				Color color = VisualUtils.GetColor(didHit);

				foreach (Collider collider in colliders) {
					Vector3 colliderPos = collider.transform.position;
					Vector3 closestPoint = Physics.ClosestPoint(position, collider, colliderPos, collider.transform.rotation);
					Vector3 dir = closestPoint - position;
					VisualUtils.DrawArrow(position, dir, color);
				}
			}
#endif

			return colliders;
		}

		public static Collider[] Run(Vector3 position, float radius, int layerMask) {
			return Run(position, radius, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static Collider[] Run(Vector3 position, float radius) {
			return Run(position, radius, -1, QueryTriggerInteraction.UseGlobal);
		}
	}
}