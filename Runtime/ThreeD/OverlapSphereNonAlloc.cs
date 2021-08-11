using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization.ThreeD {
	internal static class OverlapSphereNonAlloc {
		public static int Run(
			Vector3 position,
			float radius,
			Collider[] results,
			[DefaultValue("AllLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			int numberHit = Physics.defaultPhysicsScene.OverlapSphere(position, radius, results, layerMask, queryTriggerInteraction);

			bool didHit = numberHit > 0;

#if UNITY_EDITOR
			VisualUtils.DrawSphere(position, radius, VisualUtils.GetDefaultColor());

			if (didHit) {
				Color color = VisualUtils.GetColor(didHit);

				for (int i = 0; i < numberHit; i++) {
					Collider collider = results[i];
					Vector3 colliderPos = collider.transform.position;
					Vector3 closestPoint = Physics.ClosestPoint(position, collider, colliderPos, collider.transform.rotation);
					Vector3 dir = closestPoint - position;
					VisualUtils.DrawArrow(position, dir, color);
				}
			}
#endif

			return numberHit;
		}

		public static int Run(
			Vector3 position,
			float radius,
			Collider[] results,
			int layerMask) {
			return Run(position, radius, results, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static int Run(Vector3 position, float radius, Collider[] results) {
			return Run(position, radius, results, -1, QueryTriggerInteraction.UseGlobal);
		}
	}
}