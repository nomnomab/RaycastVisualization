using UnityEngine;

namespace Nomnom.RaycastVisualization.ThreeD {
	internal static class ClosestPoint {
		public static Vector3 Run(
			Vector3 point,
			Collider collider,
			Vector3 position,
			Quaternion rotation) {
			Vector3 closestPoint = Physics.ClosestPoint(point, collider, position, rotation);
			Vector3 dir = closestPoint - point;
			float length = dir.magnitude;

#if UNITY_EDITOR
			dir.Normalize();
			VisualUtils.DrawArrow(point, dir * length, VisualUtils.GetDefaultColor());
			VisualUtils.DrawCircle(closestPoint, dir, VisualUtils.GetColor(true));
#endif

			return closestPoint;
		}
	}
}