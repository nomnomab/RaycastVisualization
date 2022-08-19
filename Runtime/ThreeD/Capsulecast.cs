using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
	public static partial class VisualPhysics {
		/// <summary>
		///   <para>Casts a capsule against all colliders in the Scene and returns detailed information on what was hit.</para>
		/// </summary>
		/// <param name="point1">The center of the sphere at the start of the capsule.</param>
		/// <param name="point2">The center of the sphere at the end of the capsule.</param>
		/// <param name="radius">The radius of the capsule.</param>
		/// <param name="direction">The direction into which to sweep the capsule.</param>
		/// <param name="maxDistance">The max length of the sweep.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a capsule.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <returns>
		///   <para>True when the capsule sweep intersects any collider, otherwise false.</para>
		/// </returns>
		public static bool CapsuleCast(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction) {
#if UNITY_EDITOR
			return CapsuleCast(point1, point2, radius, direction, out _, maxDistance, layerMask, queryTriggerInteraction);
#else
			return Physics.CapsuleCast(point1, point2, radius, direction, maxDistance, layerMask, queryTriggerInteraction);
#endif
		}

		public static bool CapsuleCast(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			float maxDistance,
			int layerMask) {
#if UNITY_EDITOR
			return CapsuleCast(point1, point2, radius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.CapsuleCast(point1, point2, radius, direction, maxDistance, layerMask);
#endif
		}

		public static bool CapsuleCast(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			float maxDistance) {
#if UNITY_EDITOR
			return CapsuleCast(point1, point2, radius, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.CapsuleCast(point1, point2, radius, direction, maxDistance);
#endif
		}

		public static bool CapsuleCast(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction) {
#if UNITY_EDITOR
			return CapsuleCast(point1, point2, radius, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.CapsuleCast(point1, point2, radius, direction);
#endif
		}

		public static bool CapsuleCast(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			out RaycastHit hit,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

#if UNITY_EDITOR
			bool didHit = Physics.defaultPhysicsScene.CapsuleCast(point1, point2, radius, direction, out hit, maxDistance, layerMask,
				queryTriggerInteraction);
			
			direction.Normalize();
			float distance = VisualUtils.GetMaxRayLength(maxDistance);
			VisualUtils.DrawArrow((point1 + point2) * 0.5f, direction * distance, VisualUtils.GetDefaultColor());

			VisualUtils.DrawCapsule(point1, point2, direction, radius, maxDistance, hit, didHit);

			if (didHit) {
				VisualUtils.DrawNormalCircle(hit.point, hit.normal, VisualUtils.GetColor(didHit));
			}

			return didHit;		
#else
			return Physics.CapsuleCast(point1, point2, radius, direction, out hit, maxDistance, layerMask, queryTriggerInteraction);
#endif
		}

		public static bool CapsuleCast(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			out RaycastHit hit,
			float maxDistance,
			int layerMask) {
#if UNITY_EDITOR
			return CapsuleCast(point1, point2, radius, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.CapsuleCast(point1, point2, radius, direction, out hit, maxDistance, layerMask);
#endif
		}

		public static bool CapsuleCast(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			out RaycastHit hit,
			float maxDistance) {
#if UNITY_EDITOR
			return CapsuleCast(point1, point2, radius, direction, out hit, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.CapsuleCast(point1, point2, radius, direction, out hit, maxDistance);
#endif
		}

		public static bool CapsuleCast(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			out RaycastHit hit) {
#if UNITY_EDITOR
			return CapsuleCast(point1, point2, radius, direction, out hit, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.CapsuleCast(point1, point2, radius, direction, out hit);
#endif
		}
	}
}