using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
	public static partial class VisualPhysics {
		/// <summary>
		///   <para>Computes and stores colliders touching or inside the sphere into the provided buffer.</para>
		/// </summary>
		/// <param name="position">Center of the sphere.</param>
		/// <param name="radius">Radius of the sphere.</param>
		/// <param name="results">The buffer to store the results into.</param>
		/// <param name="layerMask">A defines which layers of colliders to include in the query.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <returns>
		///   <para>Returns the amount of colliders stored into the results buffer.</para>
		/// </returns>
		public static int OverlapSphereNonAlloc(
			Vector3 position,
			float radius,
			Collider[] results,
			[DefaultValue("AllLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
#if UNITY_EDITOR
			int numberHit = Physics.defaultPhysicsScene.OverlapSphere(position, radius, results, layerMask, queryTriggerInteraction);

			bool didHit = numberHit > 0;
			
			VisualUtils.DrawSphere(position, radius, VisualUtils.GetDefaultColor());

			if (didHit) {
				Color color = VisualUtils.GetColor(didHit);

				for (int i = 0; i < numberHit; i++) {
					Collider collider = results[i];
					Vector3 colliderPos = collider.transform.position;
					Vector3 closestPoint = Physics.ClosestPoint(position, collider, colliderPos, collider.transform.rotation);
					Vector3 dir = closestPoint - position;
					VisualUtils.DrawArrow(position, dir, color);
				}
			}

			return numberHit;
#else
			return Physics.OverlapSphereNonAlloc(position, radius, results, layerMask, queryTriggerInteraction);
#endif
		}

		public static int OverlapSphereNonAlloc(
			Vector3 position,
			float radius,
			Collider[] results,
			int layerMask) {
#if UNITY_EDITOR
			return OverlapSphereNonAlloc(position, radius, results, layerMask, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.OverlapSphereNonAlloc(position, radius, results, layerMask);
#endif
		}

		public static int OverlapSphereNonAlloc(Vector3 position, float radius, Collider[] results) {
#if UNITY_EDITOR
			return OverlapSphereNonAlloc(position, radius, results, -1, QueryTriggerInteraction.UseGlobal);
#else
			return Physics.OverlapSphereNonAlloc(position, radius, results);
#endif
		}
	}
}