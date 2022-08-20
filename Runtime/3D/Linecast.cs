using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
	public static partial class VisualPhysics {
		/// <summary>
		///   <para>Returns true if there is any collider intersecting the line between start and end.</para>
		/// </summary>
		/// <param name="start">Start point.</param>
		/// <param name="end">End point.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a ray.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Linecast(
			Vector3 start,
			Vector3 end,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction) {
#if UNITY_EDITOR
			return Linecast(start, end, out _, layerMask, queryTriggerInteraction);
#else
			return Physics.Linecast(start, end, layerMask, queryTriggerInteraction);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Linecast(Vector3 start, Vector3 end, int layerMask) {
#if UNITY_EDITOR
			return Linecast(start, end, layerMask, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.Linecast(start, end, layerMask);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Linecast(Vector3 start, Vector3 end) {
#if UNITY_EDITOR
			return Linecast(start, end, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.Linecast(start, end);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Linecast(
			Vector3 start,
			Vector3 end,
			out RaycastHit hitInfo,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction) {
			Vector3 direction = end - start;
#if UNITY_EDITOR
			return RaycastWithHit(start, direction.normalized, out hitInfo, direction.magnitude, layerMask, queryTriggerInteraction);
#else
			return Physics.Linecast(start, end, out hitInfo, layerMask, queryTriggerInteraction);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo, int layerMask) {
#if UNITY_EDITOR
			return RaycastWithHit(start, end, out hitInfo, layerMask);
#else
			return Physics.Linecast(start, end, out hitInfo, layerMask);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo) {
#if UNITY_EDITOR
			return RaycastWithHit(start, end, out hitInfo);
#else
			return Physics.Linecast(start, end, out hitInfo);
#endif
		}
	}
}