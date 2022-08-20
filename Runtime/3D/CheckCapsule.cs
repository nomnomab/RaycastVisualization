using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
	public static partial class VisualPhysics {
		/// <summary>
		///   <para>Checks if any colliders overlap a capsule-shaped volume in world space.</para>
		/// </summary>
		/// <param name="start">The center of the sphere at the start of the capsule.</param>
		/// <param name="end">The center of the sphere at the end of the capsule.</param>
		/// <param name="radius">The radius of the capsule.</param>
		/// <param name="layermask">A that is used to selectively ignore colliders when casting a capsule.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <param name="layerMask"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckCapsule(
			Vector3 start,
			Vector3 end,
			float radius,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
#if UNITY_EDITOR
			bool didHit = Physics.CheckCapsule(start, end, radius, layerMask, queryTriggerInteraction);
			VisualUtils.DrawCapsule(start, end, Vector3.zero, radius, 0, default, didHit);
			return didHit;
#else
			return Physics.CheckCapsule(start, end, radius, layerMask, queryTriggerInteraction);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckCapsule(Vector3 start, Vector3 end, float radius, int layerMask) {
#if UNITY_EDITOR
			return CheckCapsule(start, end, radius, layerMask, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.CheckCapsule(start, end, radius, layerMask);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckCapsule(Vector3 start, Vector3 end, float radius) {
#if UNITY_EDITOR
			return CheckCapsule(start, end, radius, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.CheckCapsule(start, end, radius);
#endif
		}
	}
}