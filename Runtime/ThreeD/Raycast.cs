using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization.ThreeD {
	internal static class Raycast { 
		public static bool Run(
			Vector3 origin,
			Vector3 direction,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			return RaycastNoHit(origin, direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static bool Run(Vector3 origin, Vector3 direction, float maxDistance, int layerMask) {
			return RaycastNoHit(origin, direction, maxDistance, layerMask);
		}

		public static bool Run(Vector3 origin, Vector3 direction, float maxDistance) {
			return RaycastNoHit(origin, direction, maxDistance);
		}

		public static bool Run(Vector3 origin, Vector3 direction) {
			return RaycastNoHit(origin, direction);
		}

		public static bool Run(
			Vector3 origin,
			Vector3 direction,
			out RaycastHit hitInfo,
			float maxDistance,
			int layerMask,
			QueryTriggerInteraction queryTriggerInteraction) {
			return RaycastWithHit(origin, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static bool Run(
			Vector3 origin,
			Vector3 direction,
			out RaycastHit hitInfo,
			float maxDistance,
			int layerMask) {
			return RaycastWithHit(origin, direction, out hitInfo, maxDistance, layerMask);
		}

		public static bool Run(
			Vector3 origin,
			Vector3 direction,
			out RaycastHit hitInfo,
			float maxDistance) {
			return RaycastWithHit(origin, direction, out hitInfo, maxDistance);
		}

		public static bool Run(Vector3 origin, Vector3 direction, out RaycastHit hitInfo) {
			return RaycastWithHit(origin, direction, out hitInfo);
		}

		public static bool Run(
			Ray ray,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			return RaycastNoHit(ray.origin, ray.direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static bool Run(Ray ray, float maxDistance, int layerMask) {
			return RaycastNoHit(ray.origin, ray.direction, maxDistance, layerMask);
		}

		public static bool Run(Ray ray, float maxDistance) {
			return RaycastNoHit(ray.origin, ray.direction, maxDistance);
		}

		public static bool Run(Ray ray) {
			return RaycastNoHit(ray.origin, ray.direction);
		}

		public static bool Run(
			Ray ray,
			out RaycastHit hitInfo,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			return RaycastWithHit(ray.origin, ray.direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static bool Run(Ray ray, out RaycastHit hitInfo, float maxDistance, int layerMask) {
			return RaycastWithHit(ray.origin,
				ray.direction, out hitInfo, maxDistance, layerMask);
		}

		public static bool Run(Ray ray, out RaycastHit hitInfo, float maxDistance) {
			return RaycastWithHit(ray.origin, ray.direction, out hitInfo, maxDistance);
		}

		public static bool Run(Ray ray, out RaycastHit hitInfo) {
			return RaycastWithHit(ray.origin, ray.direction, out hitInfo);
		}

		public static bool RaycastNoHit(
			Vector3 origin,
			Vector3 direction,
			[DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity,
			[DefaultValue("Physics.DefaultRaycastLayers")]
			int layerMask = -5,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal) {

			bool didHit = Physics.defaultPhysicsScene.Raycast(origin, direction, out RaycastHit hit, maxDistance, layerMask,
				queryTriggerInteraction);

#if UNITY_EDITOR
			direction.Normalize();
			Color color = VisualUtils.GetColor(didHit);

			if (didHit) {
				VisualUtils.DrawLine(origin, hit.point, color);

				VisualUtils.DrawNormalCircle(hit.point, hit.normal, VisualUtils.GetColor(didHit));
			} else {
				float distance = didHit ? hit.distance : VisualUtils.GetMaxRayLength(maxDistance);
				VisualUtils.DrawArrow(origin, direction * distance, color);
			}
#endif

			return didHit;
		}

		public static bool RaycastWithHit(
			Vector3 origin,
			Vector3 direction,
			out RaycastHit hit,
			[DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity,
			[DefaultValue("Physics.DefaultRaycastLayers")]
			int layerMask = -5,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal) {

			bool didHit = Physics.defaultPhysicsScene.Raycast(origin, direction, out hit, maxDistance, layerMask,
				queryTriggerInteraction);

#if UNITY_EDITOR
			direction.Normalize();
			Color color = VisualUtils.GetColor(didHit);

			if (didHit) {
				VisualUtils.DrawLine(origin, hit.point, color);

				VisualUtils.DrawNormalCircle(hit.point, hit.normal, VisualUtils.GetColor(didHit));
			} else {
				float distance = VisualUtils.GetMaxRayLength(maxDistance);
				VisualUtils.DrawArrow(origin, direction * distance, color);
			}
#endif

			return didHit;
		}
	}
}