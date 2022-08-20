using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
	public static partial class VisualPhysics {
		/// <summary>
		///   <para>Computes and stores colliders touching or inside the sphere.</para>
		/// </summary>
		/// <param name="position">Center of the sphere.</param>
		/// <param name="radius">Radius of the sphere.</param>
		/// <param name="layerMask">A defines which layers of colliders to include in the query.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <returns>
		///   <para>Returns an array with all colliders touching or inside the sphere.</para>
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider[] OverlapSphere(
			Vector3 position,
			float radius,
			[DefaultValue("AllLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
#if UNITY_EDITOR
			Collider[] colliders = Physics.OverlapSphere(position, radius, layerMask, queryTriggerInteraction);
			
			bool didHit = colliders != null && colliders.Length > 0;

			VisualUtils.DrawSphere(position, radius, VisualUtils.GetDefaultColor());

			if (didHit) {
				Color color = VisualUtils.GetColor(didHit);

				foreach (Collider collider in colliders) {
					Vector3 colliderPos = collider.transform.position;
					Vector3 closestPoint = Physics.ClosestPoint(position, collider, colliderPos, collider.transform.rotation);
					Vector3 dir = closestPoint - position;
					VisualUtils.DrawArrow(position, dir, color);
				}
			}

			return colliders;
#else
			return Physics.OverlapSphere(position, radius, layerMask, queryTriggerInteraction);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider[] OverlapSphere(Vector3 position, float radius, int layerMask) {
#if UNITY_EDITOR
			return OverlapSphere(position, radius, layerMask, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.OverlapSphere(position, radius, layerMask);
#endif
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider[] OverlapSphere(Vector3 position, float radius) {
#if UNITY_EDITOR
			return OverlapSphere(position, radius, -1, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.OverlapSphere(position, radius);
#endif
		}
	}
}