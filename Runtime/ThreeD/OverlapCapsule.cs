using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization.ThreeD {
	internal static class OverlapCapsule {
		public static Collider[] Run(
			Vector3 point0,
			Vector3 point1,
			float radius,
			int layerMask) {

			return Run(point0, point1, radius, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static Collider[] Run(Vector3 point0, Vector3 point1, float radius) {
			return Run(point0, point1, radius, -1, QueryTriggerInteraction.UseGlobal);
		}

		public static Collider[] Run(
			Vector3 point0,
			Vector3 point1,
			float radius,
			[DefaultValue("AllLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			Collider[] colliders = Physics.OverlapCapsule(point0, point1, radius, layerMask, queryTriggerInteraction);

#if UNITY_EDITOR
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
#endif
			return colliders;
		}
	}
}