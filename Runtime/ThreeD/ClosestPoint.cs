using UnityEngine;

namespace Nomnom.RaycastVisualization {
	public static partial class VisualPhysics {
		public static Vector3 ClosestPoint(
			Vector3 point,
			Collider collider,
			Vector3 position,
			Quaternion rotation) {
#if UNITY_EDITOR
			Vector3 closestPoint = Physics.ClosestPoint(point, collider, position, rotation);
			Vector3 dir = closestPoint - point;
			float length = dir.magnitude;
			
			dir.Normalize();
			VisualUtils.DrawArrow(point, dir * length, VisualUtils.GetDefaultColor());
			VisualUtils.DrawCircle(closestPoint, dir, VisualUtils.GetColor(true));

			return closestPoint;
#else
			return Physics.ClosestPoint(point, collider, position, rotation);
#endif
		}
	}
}