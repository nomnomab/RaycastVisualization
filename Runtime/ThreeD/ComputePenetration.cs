using UnityEngine;

namespace Nomnom.RaycastVisualization.ThreeD {
	internal static class ComputePenetration {
		public static bool Run(
			Collider colliderA,
			Vector3 positionA,
			Quaternion rotationA,
			Collider colliderB,
			Vector3 positionB,
			Quaternion rotationB,
			out Vector3 direction,
			out float distance) {
			bool isPenetrating = Physics.ComputePenetration(colliderA, positionA, rotationA, colliderB, positionB, rotationB, out direction,
				out distance);

#if UNITY_EDITOR
			if (!isPenetrating) {
				return isPenetrating;
			}

			Vector3 nearestPoint = Physics.ClosestPoint(positionA, colliderB, positionB, rotationB);
			VisualUtils.DrawNormalCircle(nearestPoint, -direction.normalized, Color.green, distance);
#endif

			return isPenetrating;
		}
	}
}