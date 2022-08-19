using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
	public static partial class VisualPhysics { 
		/// <summary>
		///   <para>Casts a ray, from point origin, in direction direction, of length maxDistance, against all colliders in the Scene.</para>
		/// </summary>
		/// <param name="origin">The starting point of the ray in world coordinates.</param>
		/// <param name="direction">The direction of the ray.</param>
		/// <param name="maxDistance">The max distance the ray should check for collisions.</param>
		/// <param name="layerMask">A that is used to selectively ignore Colliders when casting a ray.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <returns>
		///   <para>Returns true if the ray intersects with a Collider, otherwise false.</para>
		/// </returns>
		public static bool Raycast(
			Vector3 origin,
			Vector3 direction,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			
#if UNITY_EDITOR
			return RaycastNoHit(origin, direction, maxDistance, layerMask, queryTriggerInteraction);
#else
			return Physics.Raycast(origin, direction, maxDistance, layerMask, queryTriggerInteraction);
#endif
		}

		public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance, int layerMask) {
#if UNITY_EDITOR
			return RaycastNoHit(origin, direction, maxDistance, layerMask);
#else
			return Physics.Raycast(origin, direction, maxDistance, layerMask);
#endif
		}

		public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance) {
#if UNITY_EDITOR
			return RaycastNoHit(origin, direction, maxDistance);
#else
			return Physics.Raycast(origin, direction, maxDistance);
#endif
		}

		public static bool Raycast(Vector3 origin, Vector3 direction) {
#if UNITY_EDITOR
			return RaycastNoHit(origin, direction);
#else
			return Physics.Raycast(origin, direction);
#endif
		}

		public static bool Raycast(
			Vector3 origin,
			Vector3 direction,
			out RaycastHit hitInfo,
			float maxDistance,
			int layerMask,
			QueryTriggerInteraction queryTriggerInteraction) {
#if UNITY_EDITOR
			return RaycastWithHit(origin, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
#else
			return Physics.Raycast(origin, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
#endif
		}

		public static bool Raycast(
			Vector3 origin,
			Vector3 direction,
			out RaycastHit hitInfo,
			float maxDistance,
			int layerMask) {
#if UNITY_EDITOR
			return RaycastWithHit(origin, direction, out hitInfo, maxDistance, layerMask);
#else
			return Physics.Raycast(origin, direction, out hitInfo, maxDistance, layerMask);
#endif
		}

		public static bool Raycast(
			Vector3 origin,
			Vector3 direction,
			out RaycastHit hitInfo,
			float maxDistance) {
#if UNITY_EDITOR
			return RaycastWithHit(origin, direction, out hitInfo, maxDistance);
#else
			return Physics.Raycast(origin, direction, out hitInfo, maxDistance);
#endif
		}

		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo) {
#if UNITY_EDITOR
			return RaycastWithHit(origin, direction, out hitInfo);
#else
			return Physics.Raycast(origin, direction, out hitInfo);
#endif
		}

		/// <summary>
		///   <para>Same as above using ray.origin and ray.direction instead of origin and direction.</para>
		/// </summary>
		/// <param name="ray">The starting point and direction of the ray.</param>
		/// <param name="maxDistance">The max distance the ray should check for collisions.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a ray.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <returns>
		///   <para>Returns true when the ray intersects any collider, otherwise false.</para>
		/// </returns>
		public static bool Raycast(
			Ray ray,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {			
#if UNITY_EDITOR
			return RaycastNoHit(ray.origin, ray.direction, maxDistance, layerMask, queryTriggerInteraction);
#else
			return Physics.Raycast(ray, maxDistance, layerMask, queryTriggerInteraction);
#endif
		}

		public static bool Raycast(Ray ray, float maxDistance, int layerMask) {			
#if UNITY_EDITOR
			return RaycastNoHit(ray.origin, ray.direction, maxDistance, layerMask);
#else
			return Physics.Raycast(ray, maxDistance, layerMask);
#endif
		}

		public static bool Raycast(Ray ray, float maxDistance) {			
#if UNITY_EDITOR
			return RaycastNoHit(ray.origin, ray.direction, maxDistance);
#else
			return Physics.Raycast(ray, maxDistance);
#endif
		}

		public static bool Raycast(Ray ray) {			
#if UNITY_EDITOR
			return RaycastNoHit(ray.origin, ray.direction);
#else
			return Physics.Raycast(ray);
#endif
		}

		public static bool Raycast(
			Ray ray,
			out RaycastHit hitInfo,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction) {
#if UNITY_EDITOR
			return RaycastWithHit(ray.origin, ray.direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
#else
			return Physics.Raycast(ray, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
#endif
		}

		public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance, int layerMask) {
#if UNITY_EDITOR
			return RaycastWithHit(ray.origin, ray.direction, out hitInfo, maxDistance, layerMask);
#else
			return Physics.Raycast(ray, out hitInfo, maxDistance, layerMask);
#endif
		}

		public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance) {
#if UNITY_EDITOR
			return RaycastWithHit(ray.origin, ray.direction, out hitInfo, maxDistance);
#else
			return Physics.Raycast(ray, out hitInfo, maxDistance);
#endif
		}

		public static bool Raycast(Ray ray, out RaycastHit hitInfo) {
#if UNITY_EDITOR
			return RaycastWithHit(ray.origin, ray.direction, out hitInfo);
#else
			return Physics.Raycast(ray, out hitInfo);
#endif
		}

		internal static bool RaycastNoHit(
			Vector3 origin,
			Vector3 direction,
			[DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity,
			[DefaultValue("Physics.DefaultRaycastLayers")] int layerMask = -5,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal) {

			bool didHit = Physics.defaultPhysicsScene.Raycast(origin, direction, out RaycastHit hit, maxDistance, layerMask,
				queryTriggerInteraction);

			direction.Normalize();
			Color color = VisualUtils.GetColor(didHit);

			if (didHit) {
				VisualUtils.DrawLine(origin, hit.point, color);

				VisualUtils.DrawNormalCircle(hit.point, hit.normal, VisualUtils.GetColor(didHit));
			} else {
				float distance = didHit ? hit.distance : VisualUtils.GetMaxRayLength(maxDistance);
				VisualUtils.DrawArrow(origin, direction * distance, color);
			}

			return didHit;
		}

		internal static bool RaycastWithHit(
			Vector3 origin,
			Vector3 direction,
			out RaycastHit hit,
			[DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity,
			[DefaultValue("Physics.DefaultRaycastLayers")] int layerMask = -5,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal) {

			bool didHit = Physics.defaultPhysicsScene.Raycast(origin, direction, out hit, maxDistance, layerMask,
				queryTriggerInteraction);

			direction.Normalize();
			Color color = VisualUtils.GetColor(didHit);

			if (didHit) {
				VisualUtils.DrawLine(origin, hit.point, color);

				VisualUtils.DrawNormalCircle(hit.point, hit.normal, VisualUtils.GetColor(didHit));
			} else {
				float distance = VisualUtils.GetMaxRayLength(maxDistance);
				VisualUtils.DrawArrow(origin, direction * distance, color);
			}

			return didHit;
		}
	}
}