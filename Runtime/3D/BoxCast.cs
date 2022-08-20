using System.Runtime.CompilerServices;
using Nomnom.RaycastVisualization.Shapes;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
	public static partial class VisualPhysics {
		/// <summary>
		///   <para>Casts the box along a ray and returns detailed information on what was hit.</para>
		/// </summary>
		/// <param name="center">Center of the box.</param>
		/// <param name="halfExtents">Half the size of the box in each dimension.</param>
		/// <param name="direction">The direction in which to cast the box.</param>
		/// <param name="orientation">Rotation of the box.</param>
		/// <param name="maxDistance">The max length of the cast.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a capsule.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <returns>
		///   <para>True, if any intersections were found.</para>
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BoxCast(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			[DefaultValue("Quaternion.identity")] Quaternion orientation,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
#if UNITY_EDITOR
			return BoxCast(center, halfExtents, direction, out _, orientation, maxDistance, layerMask, queryTriggerInteraction);
#else
			return Physics.BoxCast(center, halfExtents, direction, orientation, maxDistance, layerMask, queryTriggerInteraction);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BoxCast(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			Quaternion orientation,
			float maxDistance,
			int layerMask) {
#if UNITY_EDITOR
			return BoxCast(center, halfExtents, direction, orientation, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.BoxCast(center, halfExtents, direction, orientation, maxDistance, layerMask);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BoxCast(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			Quaternion orientation,
			float maxDistance) {
#if UNITY_EDITOR
			return BoxCast(center, halfExtents, direction, orientation, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.BoxCast(center, halfExtents, direction, orientation, maxDistance);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BoxCast(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			Quaternion orientation) {
#if UNITY_EDITOR
			return BoxCast(center, halfExtents, direction, orientation, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.BoxCast(center, halfExtents, direction, orientation);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction) {
#if UNITY_EDITOR
			return BoxCast(center, halfExtents, direction, Quaternion.identity, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.BoxCast(center, halfExtents, direction);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BoxCast(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			out RaycastHit hit,
			[DefaultValue("Quaternion.identity")] Quaternion orientation,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
#if UNITY_EDITOR
			bool didHit = Physics.defaultPhysicsScene.BoxCast(center, halfExtents, direction, out hit, orientation, maxDistance, layerMask,
				queryTriggerInteraction);
			
			direction.Normalize();
			Color color = VisualUtils.GetColor(didHit);
			float distance = VisualUtils.GetMaxRayLength(maxDistance);
			
			Arrow arrow = Arrow.Default;
			Cube cube = new Cube {
				origin = center + direction * (didHit ? hit.distance : distance),
				rotation = orientation,
				size = halfExtents
			};
			
			arrow.origin = center;
			arrow.direction = direction * distance;
			arrow.Draw(VisualUtils.GetDefaultColor());
			
			cube.Draw(color);
			
			if (didHit) {
				NormalCircle circle = NormalCircle.Default;

				circle.origin = hit.point;
				circle.upDirection = hit.normal;
				circle.Draw(color);
			}

			return didHit;
#else
			return Physics.BoxCast(center, halfExtents, direction, out hit, orientation, maxDistance, layerMask, queryTriggerInteraction);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BoxCast(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			out RaycastHit hit,
			Quaternion orientation,
			float maxDistance,
			int layerMask) {
#if UNITY_EDITOR
			return BoxCast(center, halfExtents, direction, out hit, orientation, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.BoxCast(center, halfExtents, direction, out hit, orientation, maxDistance, layerMask);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BoxCast(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			out RaycastHit hit,
			Quaternion orientation,
			float maxDistance) {
#if UNITY_EDITOR
			return BoxCast(center, halfExtents, direction, out hit, orientation, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.BoxCast(center, halfExtents, direction, out hit, orientation, maxDistance);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BoxCast(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			out RaycastHit hit,
			Quaternion orientation) {
#if UNITY_EDITOR
			return BoxCast(center, halfExtents, direction, out hit, orientation, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.BoxCast(center, halfExtents, direction, out hit, orientation);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BoxCast(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			out RaycastHit hit) {
#if UNITY_EDITOR
			return BoxCast(center, halfExtents, direction, out hit, Quaternion.identity, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.BoxCast(center, halfExtents, direction, out hit);
#endif
		}
	}
}