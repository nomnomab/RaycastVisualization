using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization.ThreeD {
	internal static class CheckSphere {
		public static bool Run(
			Vector3 position,
			float radius,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			bool didHit = Physics.CheckSphere(position, radius, layerMask, queryTriggerInteraction);
#if UNITY_EDITOR
			VisualUtils.DrawSphere(position, radius, VisualUtils.GetColor(didHit));
#endif
			return didHit;
		}

		public static bool Run(Vector3 position, float radius, int layerMask) {
			return Run(position, radius, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static bool Run(Vector3 position, float radius) {
			return Run(position, radius, -5, QueryTriggerInteraction.UseGlobal);
		}
	}
}