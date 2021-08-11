using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization.ThreeD {
	internal static class CheckCapsule {
		public static bool Run(
			Vector3 start,
			Vector3 end,
			float radius,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			bool didHit = Physics.CheckCapsule(start, end, radius, layerMask, queryTriggerInteraction);
#if UNITY_EDITOR
			VisualUtils.DrawCapsule(start, end, Vector3.zero, radius, 0, default, didHit);
#endif
			return didHit;
		}

		public static bool Run(Vector3 start, Vector3 end, float radius, int layerMask) {
			return Run(start, end, radius, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static bool Run(Vector3 start, Vector3 end, float radius) {
			return Run(start, end, radius, -5, QueryTriggerInteraction.UseGlobal);
		}
	}
}