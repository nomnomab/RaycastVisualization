using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
	public static partial class VisualPhysics {
		/// <summary>
		///   <para>Check whether the given box overlaps with other colliders or not.</para>
		/// </summary>
		/// <param name="center">Center of the box.</param>
		/// <param name="halfExtents">Half the size of the box in each dimension.</param>
		/// <param name="orientation">Rotation of the box.</param>
		/// <param name="layermask">A that is used to selectively ignore colliders when casting a ray.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <returns>
		///   <para>True, if the box overlaps with any colliders.</para>
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckBox(
			Vector3 center,
			Vector3 halfExtents,
			[DefaultValue("Quaternion.identity")] Quaternion orientation,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
#if UNITY_EDITOR
			bool didHit = Physics.CheckBox(center, halfExtents, orientation, layerMask, queryTriggerInteraction);
			VisualUtils.DrawCube(center, halfExtents, orientation, VisualUtils.GetColor(didHit));
			return didHit;
#else
			return Physics.CheckBox(center, halfExtents, orientation, layerMask, queryTriggerInteraction);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckBox(
			Vector3 center,
			Vector3 halfExtents,
			Quaternion orientation,
			int layerMask) {
#if UNITY_EDITOR
			return CheckBox(center, halfExtents, orientation, layerMask, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.CheckBox(center, halfExtents, orientation, layerMask);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckBox(Vector3 center, Vector3 halfExtents, Quaternion orientation) {
#if UNITY_EDITOR
			return CheckBox(center, halfExtents, orientation, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.CheckBox(center, halfExtents, orientation);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckBox(Vector3 center, Vector3 halfExtents) {
#if UNITY_EDITOR
			return CheckBox(center, halfExtents, Quaternion.identity, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.CheckBox(center, halfExtents);
#endif
		}
	}
}