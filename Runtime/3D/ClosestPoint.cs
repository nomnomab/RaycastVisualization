using System.Runtime.CompilerServices;
using UnityEngine;

namespace Nomnom.RaycastVisualization {
	public static partial class VisualPhysics {
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 ClosestPoint(
			Vector3 point,
			Collider collider,
			Vector3 position,
			Quaternion rotation) {
#if UNITY_EDITOR
			Vector3 closestPoint = Physics.ClosestPoint(point, collider, position, rotation);
			Vector3 dir = closestPoint - point;

			VisualUtils.DrawArrow(point, dir, VisualUtils.GetDefaultColor());
			VisualUtils.DrawCircle(closestPoint, dir.normalized, VisualUtils.GetColor(true));

			return closestPoint;
#else
			return Physics.ClosestPoint(point, collider, position, rotation);
#endif
		}
	}
}