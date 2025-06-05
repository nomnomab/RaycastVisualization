#if RAYCASTVISUALIZATION_3D_PHYSICS
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
	public static partial class VisualPhysics {
		/// <summary>
		///   <para>Returns true if there are any colliders overlapping the sphere defined by position and radius in world coordinates.</para>
		/// </summary>
		/// <param name="position">Center of the sphere.</param>
		/// <param name="radius">Radius of the sphere.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a capsule.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckSphere(
			Vector3 position,
			float radius,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
#if UNITY_EDITOR
			bool didHit = Physics.CheckSphere(position, radius, layerMask, queryTriggerInteraction);
			VisualUtils.DrawSphere(position, radius, VisualUtils.GetColor(didHit));

			return didHit;
#else
			return Physics.CheckSphere(position, radius, layerMask, queryTriggerInteraction);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckSphere(Vector3 position, float radius, int layerMask) {
#if UNITY_EDITOR
			return CheckSphere(position, radius, layerMask, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.CheckSphere(position, radius, layerMask);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckSphere(Vector3 position, float radius) {
#if UNITY_EDITOR
			return CheckSphere(position, radius, -5, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.CheckSphere(position, radius);
#endif
		}
	}
}
#endif