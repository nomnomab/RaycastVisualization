using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization.ThreeD {
	internal static class Linecast {
		public static bool Run(
			Vector3 start,
			Vector3 end,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			return Run(start, end, out _, layerMask, queryTriggerInteraction);
		}

		public static bool Run(Vector3 start, Vector3 end, int layerMask) {
			return Run(start, end, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static bool Run(Vector3 start, Vector3 end) {
			return Run(start, end, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static bool Run(
			Vector3 start,
			Vector3 end,
			out RaycastHit hitInfo,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			Vector3 direction = end - start;
			return Raycast.RaycastWithHit(start, direction.normalized, out hitInfo, direction.magnitude, layerMask, queryTriggerInteraction);
		}

		public static bool Run(Vector3 start, Vector3 end, out RaycastHit hitInfo, int layerMask) {
			return Raycast.RaycastWithHit(start, end, out hitInfo, layerMask);
		}

		public static bool Run(Vector3 start, Vector3 end, out RaycastHit hitInfo) {
			return Raycast.RaycastWithHit(start, end, out hitInfo);
		}
	}
}