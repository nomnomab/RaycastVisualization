using UnityEngine;

namespace Nomnom.RaycastVisualization {
	public static partial class VisualPhysics {
		public static bool ComputePenetration(
			Collider colliderA,
			Vector3 positionA,
			Quaternion rotationA,
			Collider colliderB,
			Vector3 positionB,
			Quaternion rotationB,
			out Vector3 direction,
			out float distance) {
#if UNITY_EDITOR
			bool isPenetrating = Physics.ComputePenetration(colliderA, positionA, rotationA, colliderB, positionB, rotationB, out direction,
				out distance);
			
			if (!isPenetrating) {
				return isPenetrating;
			}

			Vector3 nearestPoint = Physics.ClosestPoint(positionA, colliderB, positionB, rotationB);
			VisualUtils.DrawNormalCircle(nearestPoint, -direction.normalized, Color.green, distance);

			return isPenetrating;
#else
			return Physics.ComputePenetration(colliderA, positionA, rotationA, colliderB, positionB, rotationB, out direction, out distance);
#endif
		}
	}
}