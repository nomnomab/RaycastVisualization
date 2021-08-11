using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization.ThreeD {
	internal static class BoxcastAll {
		public static RaycastHit[] Run(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			[DefaultValue("Quaternion.identity")] Quaternion orientation,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			RaycastHit[] hits = Physics.BoxCastAll(center, halfExtents, direction, orientation, maxDistance, layerMask,
				queryTriggerInteraction);

#if UNITY_EDITOR
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
#endif

			return hits;
		}

		public static RaycastHit[] Run(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			Quaternion orientation,
			float maxDistance,
			int layerMask) {
			return Run(center, halfExtents, direction, orientation, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] Run(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			Quaternion orientation,
			float maxDistance) {
			return Run(center, halfExtents, direction, orientation, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] Run(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			Quaternion orientation) {
			return Run(center, halfExtents, direction, orientation, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] Run(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction) {
			return Run(center, halfExtents, direction, Quaternion.identity, float.PositiveInfinity, -5,
				QueryTriggerInteraction.UseGlobal);
		}
	}
}