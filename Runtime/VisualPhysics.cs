using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
	public static class VisualPhysics {
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

			return ThreeD.Raycast.Run(origin, direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance, int layerMask) {
			return ThreeD.Raycast.Run(origin, direction, maxDistance, layerMask);
		}

		public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance) {
			return ThreeD.Raycast.Run(origin, direction, maxDistance);
		}

		public static bool Raycast(Vector3 origin, Vector3 direction) {
			return ThreeD.Raycast.Run(origin, direction);
		}

		public static bool Raycast(
			Vector3 origin,
			Vector3 direction,
			out RaycastHit hitInfo,
			float maxDistance,
			int layerMask,
			QueryTriggerInteraction queryTriggerInteraction) {
			return ThreeD.Raycast.Run(origin, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static bool Raycast(
			Vector3 origin,
			Vector3 direction,
			out RaycastHit hitInfo,
			float maxDistance,
			int layerMask) {
			return ThreeD.Raycast.Run(origin, direction, out hitInfo, maxDistance, layerMask);
		}

		public static bool Raycast(
			Vector3 origin,
			Vector3 direction,
			out RaycastHit hitInfo,
			float maxDistance) {
			return ThreeD.Raycast.Run(origin, direction, out hitInfo, maxDistance);
		}

		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo) {
			return ThreeD.Raycast.Run(origin, direction, out hitInfo);
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
			return ThreeD.Raycast.Run(ray.origin, ray.direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static bool Raycast(Ray ray, float maxDistance, int layerMask) {
			return ThreeD.Raycast.Run(ray.origin, ray.direction, maxDistance, layerMask);
		}

		public static bool Raycast(Ray ray, float maxDistance) {
			return ThreeD.Raycast.Run(ray.origin, ray.direction, maxDistance);
		}

		public static bool Raycast(Ray ray) {
			return ThreeD.Raycast.Run(ray.origin, ray.direction);
		}

		public static bool Raycast(
			Ray ray,
			out RaycastHit hitInfo,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			return ThreeD.Raycast.Run(ray.origin, ray.direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance, int layerMask) {
			return ThreeD.Raycast.Run(ray.origin,
				ray.direction, out hitInfo, maxDistance, layerMask);
		}

		public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance) {
			return ThreeD.Raycast.Run(ray.origin, ray.direction, out hitInfo, maxDistance);
		}

		public static bool Raycast(Ray ray, out RaycastHit hitInfo) {
			return ThreeD.Raycast.Run(ray.origin, ray.direction, out hitInfo);
		}

		// Linecast

		/// <summary>
		///   <para>Returns true if there is any collider intersecting the line between start and end.</para>
		/// </summary>
		/// <param name="start">Start point.</param>
		/// <param name="end">End point.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a ray.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		public static bool Linecast(
			Vector3 start,
			Vector3 end,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			return ThreeD.Linecast.Run(start, end, out _, layerMask, queryTriggerInteraction);
		}

		public static bool Linecast(Vector3 start, Vector3 end, int layerMask) {
			return ThreeD.Linecast.Run(start, end, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static bool Linecast(Vector3 start, Vector3 end) {
			return ThreeD.Linecast.Run(start, end, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static bool Linecast(
			Vector3 start,
			Vector3 end,
			out RaycastHit hitInfo,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			return ThreeD.Linecast.Run(start, end, out hitInfo, layerMask, queryTriggerInteraction);
		}

		public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo, int layerMask) {
			return ThreeD.Linecast.Run(start, end, out hitInfo, layerMask);
		}

		public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo) {
			return ThreeD.Linecast.Run(start, end, out hitInfo);
		}

		// CapsuleCast

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
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			return ThreeD.Capsulecast.Run(point1, point2, radius, direction, out _, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static bool CapsuleCast(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			float maxDistance,
			int layerMask) {
			return ThreeD.Capsulecast.Run(point1, point2, radius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static bool CapsuleCast(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			float maxDistance) {
			return ThreeD.Capsulecast.Run(point1, point2, radius, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static bool CapsuleCast(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction) {
			return ThreeD.Capsulecast.Run(point1, point2, radius, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
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

			return ThreeD.Capsulecast.Run(point1, point2, radius, direction, out hit, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static bool CapsuleCast(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			out RaycastHit hit,
			float maxDistance,
			int layerMask) {
			return ThreeD.Capsulecast.Run(point1, point2, radius, direction, out hit, maxDistance, layerMask,
				QueryTriggerInteraction.UseGlobal);
		}

		public static bool CapsuleCast(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			out RaycastHit hit,
			float maxDistance) {
			return ThreeD.Capsulecast.Run(point1, point2, radius, direction, out hit, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static bool CapsuleCast(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			out RaycastHit hit) {
			return ThreeD.Capsulecast.Run(point1, point2, radius, direction, out hit, float.PositiveInfinity, -5,
				QueryTriggerInteraction.UseGlobal);
		}

		// SphereCast

		public static bool SphereCast(
			Vector3 origin,
			float radius,
			Vector3 direction,
			out RaycastHit hit,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			return ThreeD.Spherecast.Run(origin, radius, direction, out hit, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static bool SphereCast(
			Vector3 origin,
			float radius,
			Vector3 direction,
			out RaycastHit hit,
			float maxDistance,
			int layerMask) {
			return ThreeD.Spherecast.Run(origin, radius, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static bool SphereCast(
			Vector3 origin,
			float radius,
			Vector3 direction,
			out RaycastHit hit,
			float maxDistance) {
			return ThreeD.Spherecast.Run(origin, radius, direction, out hit, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static bool SphereCast(
			Vector3 origin,
			float radius,
			Vector3 direction,
			out RaycastHit hit) {
			return ThreeD.Spherecast.Run(origin, radius, direction, out hit, float.PositiveInfinity, -5,
				QueryTriggerInteraction.UseGlobal);
		}

		/// <summary>
		///   <para>Casts a sphere along a ray and returns detailed information on what was hit.</para>
		/// </summary>
		/// <param name="ray">The starting point and direction of the ray into which the sphere sweep is cast.</param>
		/// <param name="radius">The radius of the sphere.</param>
		/// <param name="maxDistance">The max length of the cast.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a capsule.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <returns>
		///   <para>True when the sphere sweep intersects any collider, otherwise false.</para>
		/// </returns>
		public static bool SphereCast(
			Ray ray,
			float radius,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			return ThreeD.Spherecast.Run(ray.origin, radius, ray.direction, out RaycastHit _, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static bool SphereCast(Ray ray, float radius, float maxDistance, int layerMask) {
			return ThreeD.Spherecast.Run(ray, radius, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static bool SphereCast(Ray ray, float radius, float maxDistance) {
			return ThreeD.Spherecast.Run(ray, radius, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static bool SphereCast(Ray ray, float radius) {
			return ThreeD.Spherecast.Run(ray, radius, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static bool SphereCast(
			Ray ray,
			float radius,
			out RaycastHit hitInfo,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			return ThreeD.Spherecast.Run(ray.origin, radius, ray.direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static bool SphereCast(
			Ray ray,
			float radius,
			out RaycastHit hitInfo,
			float maxDistance,
			int layerMask) {
			return ThreeD.Spherecast.Run(ray, radius, out hitInfo, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static bool SphereCast(
			Ray ray,
			float radius,
			out RaycastHit hitInfo,
			float maxDistance) {
			return ThreeD.Spherecast.Run(ray, radius, out hitInfo, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo) {
			return ThreeD.Spherecast.Run(ray, radius, out hitInfo, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		/// <summary>
		///   <para>Like Physics.SphereCast, but this function will return all hits the sphere sweep intersects.</para>
		/// </summary>
		/// <param name="origin">The center of the sphere at the start of the sweep.</param>
		/// <param name="radius">The radius of the sphere.</param>
		/// <param name="direction">The direction in which to sweep the sphere.</param>
		/// <param name="maxDistance">The max length of the sweep.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a sphere.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <returns>
		///   <para>An array of all colliders hit in the sweep.</para>
		/// </returns>
		public static RaycastHit[] SphereCastAll(
			Vector3 origin,
			float radius,
			Vector3 direction,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			return ThreeD.SpherecastAll.Run(origin, radius, direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static RaycastHit[] SphereCastAll(
			Vector3 origin,
			float radius,
			Vector3 direction,
			float maxDistance,
			int layerMask) {
			return ThreeD.SpherecastAll.Run(origin, radius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] SphereCastAll(
			Vector3 origin,
			float radius,
			Vector3 direction,
			float maxDistance) {
			return ThreeD.SpherecastAll.Run(origin, radius, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] SphereCastAll(
			Vector3 origin,
			float radius,
			Vector3 direction) {
			return ThreeD.SpherecastAll.Run(origin, radius, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		/// <summary>
		///   <para>Like Physics.SphereCast, but this function will return all hits the sphere sweep intersects.</para>
		/// </summary>
		/// <param name="ray">The starting point and direction of the ray into which the sphere sweep is cast.</param>
		/// <param name="radius">The radius of the sphere.</param>
		/// <param name="maxDistance">The max length of the sweep.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a sphere.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		public static RaycastHit[] SphereCastAll(
			Ray ray,
			float radius,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			return ThreeD.SpherecastAll.Run(ray.origin, radius, ray.direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static RaycastHit[] SphereCastAll(
			Ray ray,
			float radius,
			float maxDistance,
			int layerMask) {
			return ThreeD.SpherecastAll.Run(ray, radius, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] SphereCastAll(Ray ray, float radius, float maxDistance) {
			return ThreeD.SpherecastAll.Run(ray, radius, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] SphereCastAll(Ray ray, float radius) {
			return ThreeD.SpherecastAll.Run(ray, radius, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// BoxCasts

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
		public static bool BoxCast(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			[DefaultValue("Quaternion.identity")] Quaternion orientation,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			return ThreeD.Boxcast.Run(center, halfExtents, direction, out _, orientation, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static bool BoxCast(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			Quaternion orientation,
			float maxDistance,
			int layerMask) {
			return ThreeD.Boxcast.Run(center, halfExtents, direction, orientation, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static bool BoxCast(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			Quaternion orientation,
			float maxDistance) {
			return ThreeD.Boxcast.Run(center, halfExtents, direction, orientation, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static bool BoxCast(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			Quaternion orientation) {
			return ThreeD.Boxcast.Run(center, halfExtents, direction, orientation, float.PositiveInfinity, -5,
				QueryTriggerInteraction.UseGlobal);
		}

		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction) {
			return ThreeD.Boxcast.Run(center, halfExtents,
				direction, Quaternion.identity, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

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

			return ThreeD.Boxcast.Run(center, halfExtents, direction, out hit, orientation, maxDistance, layerMask,
				queryTriggerInteraction);
		}

		public static bool BoxCast(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			out RaycastHit hit,
			Quaternion orientation,
			float maxDistance,
			int layerMask) {
			return ThreeD.Boxcast.Run(center, halfExtents, direction, out hit, orientation, maxDistance, layerMask,
				QueryTriggerInteraction.UseGlobal);
		}

		public static bool BoxCast(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			out RaycastHit hit,
			Quaternion orientation,
			float maxDistance) {
			return ThreeD.Boxcast.Run(center, halfExtents, direction, out hit, orientation, maxDistance, -5,
				QueryTriggerInteraction.UseGlobal);
		}

		public static bool BoxCast(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			out RaycastHit hit,
			Quaternion orientation) {
			return ThreeD.Boxcast.Run(center, halfExtents, direction, out hit, orientation, float.PositiveInfinity, -5,
				QueryTriggerInteraction.UseGlobal);
		}

		public static bool BoxCast(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			out RaycastHit hit) {
			return ThreeD.Boxcast.Run(center, halfExtents, direction, out hit, Quaternion.identity, float.PositiveInfinity, -5,
				QueryTriggerInteraction.UseGlobal);
		}

		// Array methods

		/// <summary>
		///   <para>See Also: Raycast.</para>
		/// </summary>
		/// <param name="origin">The starting point of the ray in world coordinates.</param>
		/// <param name="direction">The direction of the ray.</param>
		/// <param name="maxDistance">The max distance the rayhit is allowed to be from the start of the ray.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a ray.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		public static RaycastHit[] RaycastAll(
			Vector3 origin,
			Vector3 direction,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			return ThreeD.RaycastAll.Run(origin, direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static RaycastHit[] RaycastAll(
			Vector3 origin,
			Vector3 direction,
			float maxDistance,
			int layerMask) {
			return ThreeD.RaycastAll.Run(origin, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] RaycastAll(
			Vector3 origin,
			Vector3 direction,
			float maxDistance) {
			return ThreeD.RaycastAll.Run(origin, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction) {
			return ThreeD.RaycastAll.Run(origin, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		/// <summary>
		///   <para>Casts a ray through the Scene and returns all hits. Note that order of the results is undefined.</para>
		/// </summary>
		/// <param name="ray">The starting point and direction of the ray.</param>
		/// <param name="maxDistance">The max distance the rayhit is allowed to be from the start of the ray.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a ray.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <returns>
		///   <para>An array of RaycastHit objects. Note that the order of the results is undefined.</para>
		/// </returns>
		public static RaycastHit[] RaycastAll(
			Ray ray,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			return ThreeD.RaycastAll.Run(ray.origin, ray.direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static RaycastHit[] RaycastAll(Ray ray, float maxDistance, int layerMask) {
			return ThreeD.RaycastAll.Run(ray.origin, ray.direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] RaycastAll(Ray ray, float maxDistance) {
			return ThreeD.RaycastAll.Run(ray.origin, ray.direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] RaycastAll(Ray ray) {
			return ThreeD.RaycastAll.Run(ray.origin, ray.direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		/// <summary>
		///   <para>Cast a ray through the Scene and store the hits into the buffer.</para>
		/// </summary>
		/// <param name="ray">The starting point and direction of the ray.</param>
		/// <param name="results">The buffer to store the hits into.</param>
		/// <param name="maxDistance">The max distance the rayhit is allowed to be from the start of the ray.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a ray.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <returns>
		///   <para>The amount of hits stored into the results buffer.</para>
		/// </returns>
		public static int RaycastNonAlloc(
			Ray ray,
			RaycastHit[] results,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			return ThreeD.RaycastNonAlloc.Run(ray.origin, ray.direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static int RaycastNonAlloc(
			Ray ray,
			RaycastHit[] results,
			float maxDistance,
			int layerMask) {
			return ThreeD.RaycastNonAlloc.Run(ray, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static int RaycastNonAlloc(Ray ray, RaycastHit[] results, float maxDistance) {
			return ThreeD.RaycastNonAlloc.Run(ray, results, maxDistance, Physics.DefaultRaycastLayers, QueryTriggerInteraction.UseGlobal);
		}

		public static int RaycastNonAlloc(Ray ray, RaycastHit[] results) {
			return ThreeD.RaycastNonAlloc.Run(ray, results, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.UseGlobal);
		}

		/// <summary>
		///   <para>Cast a ray through the Scene and store the hits into the buffer.</para>
		/// </summary>
		/// <param name="origin">The starting point and direction of the ray.</param>
		/// <param name="results">The buffer to store the hits into.</param>
		/// <param name="direction">The direction of the ray.</param>
		/// <param name="maxDistance">The max distance the rayhit is allowed to be from the start of the ray.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a ray.</param>
		/// <returns>
		///   <para>The amount of hits stored into the results buffer.</para>
		/// </returns>
		public static int RaycastNonAlloc(
			Vector3 origin,
			Vector3 direction,
			RaycastHit[] results,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			return ThreeD.RaycastNonAlloc.Run(origin, direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static int RaycastNonAlloc(
			Vector3 origin,
			Vector3 direction,
			RaycastHit[] results,
			float maxDistance,
			int layerMask) {
			return ThreeD.RaycastNonAlloc.Run(origin, direction, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static int RaycastNonAlloc(
			Vector3 origin,
			Vector3 direction,
			RaycastHit[] results,
			float maxDistance) {
			return ThreeD.RaycastNonAlloc.Run(origin, direction, results, maxDistance, Physics.DefaultRaycastLayers,
				QueryTriggerInteraction.UseGlobal);
		}

		public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results) {
			return ThreeD.RaycastNonAlloc.Run(origin, direction, results, Mathf.Infinity, Physics.DefaultRaycastLayers,
				QueryTriggerInteraction.UseGlobal);
		}

		/// <summary>
		///   <para>Like Physics.CapsuleCast, but this function will return all hits the capsule sweep intersects.</para>
		/// </summary>
		/// <param name="point1">The center of the sphere at the start of the capsule.</param>
		/// <param name="point2">The center of the sphere at the end of the capsule.</param>
		/// <param name="radius">The radius of the capsule.</param>
		/// <param name="direction">The direction into which to sweep the capsule.</param>
		/// <param name="maxDistance">The max length of the sweep.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a capsule.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <returns>
		///   <para>An array of all colliders hit in the sweep.</para>
		/// </returns>
		public static RaycastHit[] CapsuleCastAll(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			return ThreeD.CapsulecastAll.Run(point1, point2, radius, direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static RaycastHit[] CapsuleCastAll(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			float maxDistance,
			int layerMask) {
			return ThreeD.CapsulecastAll.Run(point1, point2, radius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] CapsuleCastAll(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			float maxDistance) {
			return ThreeD.CapsulecastAll.Run(point1, point2, radius, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] CapsuleCastAll(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction) {
			return ThreeD.CapsulecastAll.Run(point1, point2, radius, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static Collider[] OverlapCapsule(
			Vector3 point0,
			Vector3 point1,
			float radius,
			int layerMask) {

			return ThreeD.OverlapCapsule.Run(point0, point1, radius, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static Collider[] OverlapCapsule(Vector3 point0, Vector3 point1, float radius) {
			return ThreeD.OverlapCapsule.Run(point0, point1, radius, -1, QueryTriggerInteraction.UseGlobal);
		}

		public static Collider[] OverlapCapsule(
			Vector3 point0,
			Vector3 point1,
			float radius,
			[DefaultValue("AllLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			return ThreeD.OverlapCapsule.Run(point0, point1, radius, layerMask, queryTriggerInteraction);
		}

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
		public static Collider[] OverlapSphere(
			Vector3 position,
			float radius,
			[DefaultValue("AllLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			return ThreeD.OverlapSphere.Run(position, radius, layerMask, queryTriggerInteraction);
		}

		public static Collider[] OverlapSphere(Vector3 position, float radius, int layerMask) {
			return ThreeD.OverlapSphere.Run(position, radius, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static Collider[] OverlapSphere(Vector3 position, float radius) {
			return ThreeD.OverlapSphere.Run(position, radius, -1, QueryTriggerInteraction.UseGlobal);
		}

		public static bool ComputePenetration(
			Collider colliderA,
			Vector3 positionA,
			Quaternion rotationA,
			Collider colliderB,
			Vector3 positionB,
			Quaternion rotationB,
			out Vector3 direction,
			out float distance) { 
			return ThreeD.ComputePenetration.Run(colliderA, positionA, rotationA, colliderB, positionB, rotationB, out direction,
				out distance);
		}

		public static Vector3 ClosestPoint(
			Vector3 point,
			Collider collider,
			Vector3 position,
			Quaternion rotation) {
			return ThreeD.ClosestPoint.Run(point, collider, position, rotation);
		}

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
			return ThreeD.OverlapSphereNonAlloc.Run(position, radius, results, layerMask, queryTriggerInteraction);
		}

		public static int OverlapSphereNonAlloc(
			Vector3 position,
			float radius,
			Collider[] results,
			int layerMask) {
			return ThreeD.OverlapSphereNonAlloc.Run(position, radius, results, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static int OverlapSphereNonAlloc(Vector3 position, float radius, Collider[] results) {
			return ThreeD.OverlapSphereNonAlloc.Run(position, radius, results, -1, QueryTriggerInteraction.UseGlobal);
		}

		/// <summary>
		///   <para>Returns true if there are any colliders overlapping the sphere defined by position and radius in world coordinates.</para>
		/// </summary>
		/// <param name="position">Center of the sphere.</param>
		/// <param name="radius">Radius of the sphere.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a capsule.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		public static bool CheckSphere(
			Vector3 position,
			float radius,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			return ThreeD.CheckSphere.Run(position, radius, layerMask, queryTriggerInteraction);
		}

		public static bool CheckSphere(Vector3 position, float radius, int layerMask) {
			return ThreeD.CheckSphere.Run(position, radius, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static bool CheckSphere(Vector3 position, float radius) {
			return ThreeD.CheckSphere.Run(position, radius, -5, QueryTriggerInteraction.UseGlobal);
		}

		/// <summary>
		///   <para>Casts a capsule against all colliders in the Scene and returns detailed information on what was hit into the buffer.</para>
		/// </summary>
		/// <param name="point1">The center of the sphere at the start of the capsule.</param>
		/// <param name="point2">The center of the sphere at the end of the capsule.</param>
		/// <param name="radius">The radius of the capsule.</param>
		/// <param name="direction">The direction into which to sweep the capsule.</param>
		/// <param name="results">The buffer to store the hits into.</param>
		/// <param name="maxDistance">The max length of the sweep.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a capsule.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <returns>
		///   <para>The amount of hits stored into the buffer.</para>
		/// </returns>
		public static int CapsuleCastNonAlloc(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			RaycastHit[] results,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			return ThreeD.CapsuleCastNonAlloc.Run(point1, point2, radius, direction, results, maxDistance, layerMask,
				queryTriggerInteraction);
		}

		public static int CapsuleCastNonAlloc(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			RaycastHit[] results,
			float maxDistance,
			int layerMask) {
			return ThreeD.CapsuleCastNonAlloc.Run(point1, point2, radius, direction, results, maxDistance, layerMask,
				QueryTriggerInteraction.UseGlobal);
		}

		public static int CapsuleCastNonAlloc(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			RaycastHit[] results,
			float maxDistance) {
			return ThreeD.CapsuleCastNonAlloc.Run(point1, point2, radius, direction, results, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static int CapsuleCastNonAlloc(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			RaycastHit[] results) {
			return ThreeD.CapsuleCastNonAlloc.Run(point1, point2, radius, direction, results, float.PositiveInfinity, -5,
				QueryTriggerInteraction.UseGlobal);
		}

		/// <summary>
		///   <para>Cast sphere along the direction and store the results into buffer.</para>
		/// </summary>
		/// <param name="origin">The center of the sphere at the start of the sweep.</param>
		/// <param name="radius">The radius of the sphere.</param>
		/// <param name="direction">The direction in which to sweep the sphere.</param>
		/// <param name="results">The buffer to save the hits into.</param>
		/// <param name="maxDistance">The max length of the sweep.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a sphere.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <returns>
		///   <para>The amount of hits stored into the results buffer.</para>
		/// </returns>
		public static int SphereCastNonAlloc(
			Vector3 origin,
			float radius,
			Vector3 direction,
			RaycastHit[] results,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			return ThreeD.SpherecastNonAlloc.Run(origin, radius, direction, results, maxDistance, layerMask,
				queryTriggerInteraction);
		}

		public static int SphereCastNonAlloc(
			Vector3 origin,
			float radius,
			Vector3 direction,
			RaycastHit[] results,
			float maxDistance,
			int layerMask) {
			return ThreeD.SpherecastNonAlloc.Run(origin, radius, direction, results, maxDistance, layerMask,
				QueryTriggerInteraction.UseGlobal);
		}

		public static int SphereCastNonAlloc(
			Vector3 origin,
			float radius,
			Vector3 direction,
			RaycastHit[] results,
			float maxDistance) {
			return ThreeD.SpherecastNonAlloc.Run(origin, radius, direction, results, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static int SphereCastNonAlloc(
			Vector3 origin,
			float radius,
			Vector3 direction,
			RaycastHit[] results) {
			return ThreeD.SpherecastNonAlloc.Run(origin, radius, direction, results, float.PositiveInfinity, -5,
				QueryTriggerInteraction.UseGlobal);
		}

		/// <summary>
		///   <para>Cast sphere along the direction and store the results into buffer.</para>
		/// </summary>
		/// <param name="ray">The starting point and direction of the ray into which the sphere sweep is cast.</param>
		/// <param name="radius">The radius of the sphere.</param>
		/// <param name="results">The buffer to save the results to.</param>
		/// <param name="maxDistance">The max length of the sweep.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a sphere.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <returns>
		///   <para>The amount of hits stored into the results buffer.</para>
		/// </returns>
		public static int SphereCastNonAlloc(
			Ray ray,
			float radius,
			RaycastHit[] results,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			return ThreeD.SpherecastNonAlloc.Run(ray.origin, radius, ray.direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static int SphereCastNonAlloc(
			Ray ray,
			float radius,
			RaycastHit[] results,
			float maxDistance,
			int layerMask) {
			return ThreeD.SpherecastNonAlloc.Run(ray, radius, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static int SphereCastNonAlloc(
			Ray ray,
			float radius,
			RaycastHit[] results,
			float maxDistance) {
			return ThreeD.SpherecastNonAlloc.Run(ray, radius, results, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results) {
			return ThreeD.SpherecastNonAlloc.Run(ray, radius, results, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		/// <summary>
		///   <para>Checks if any colliders overlap a capsule-shaped volume in world space.</para>
		/// </summary>
		/// <param name="start">The center of the sphere at the start of the capsule.</param>
		/// <param name="end">The center of the sphere at the end of the capsule.</param>
		/// <param name="radius">The radius of the capsule.</param>
		/// <param name="layermask">A that is used to selectively ignore colliders when casting a capsule.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <param name="layerMask"></param>
		public static bool CheckCapsule(
			Vector3 start,
			Vector3 end,
			float radius,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			return ThreeD.CheckCapsule.Run(start, end, radius, layerMask, queryTriggerInteraction);
		}

		public static bool CheckCapsule(Vector3 start, Vector3 end, float radius, int layerMask) {
			return ThreeD.CheckCapsule.Run(start, end, radius, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static bool CheckCapsule(Vector3 start, Vector3 end, float radius) {
			return ThreeD.CheckCapsule.Run(start, end, radius, -5, QueryTriggerInteraction.UseGlobal);
		}

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
		public static bool CheckBox(
			Vector3 center,
			Vector3 halfExtents,
			[DefaultValue("Quaternion.identity")] Quaternion orientation,
			[DefaultValue("DefaultRaycastLayers")] int layermask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			return ThreeD.CheckBox.Run(center, halfExtents, orientation, layermask, queryTriggerInteraction);
		}

		public static bool CheckBox(
			Vector3 center,
			Vector3 halfExtents,
			Quaternion orientation,
			int layerMask) {
			return ThreeD.CheckBox.Run(center, halfExtents, orientation, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static bool CheckBox(Vector3 center, Vector3 halfExtents, Quaternion orientation) {
			return ThreeD.CheckBox.Run(center, halfExtents, orientation, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static bool CheckBox(Vector3 center, Vector3 halfExtents) {
			return ThreeD.CheckBox.Run(center, halfExtents, Quaternion.identity, -5, QueryTriggerInteraction.UseGlobal);
		}

		/// <summary>
		///   <para>Find all colliders touching or inside of the given box.</para>
		/// </summary>
		/// <param name="center">Center of the box.</param>
		/// <param name="halfExtents">Half of the size of the box in each dimension.</param>
		/// <param name="orientation">Rotation of the box.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a ray.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <returns>
		///   <para>Colliders that overlap with the given box.</para>
		/// </returns>
		public static Collider[] OverlapBox(
			Vector3 center,
			Vector3 halfExtents,
			[DefaultValue("Quaternion.identity")] Quaternion orientation,
			[DefaultValue("AllLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			return ThreeD.OverlapBox.Run(center, halfExtents, orientation, layerMask, queryTriggerInteraction);
		}

		public static Collider[] OverlapBox(
			Vector3 center,
			Vector3 halfExtents,
			Quaternion orientation,
			int layerMask) {
			return ThreeD.OverlapBox.Run(center, halfExtents, orientation, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static Collider[] OverlapBox(
			Vector3 center,
			Vector3 halfExtents,
			Quaternion orientation) {
			return ThreeD.OverlapBox.Run(center, halfExtents, orientation, -1, QueryTriggerInteraction.UseGlobal);
		}

		public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents) {
			return ThreeD.OverlapBox.Run(center, halfExtents, Quaternion.identity, -1, QueryTriggerInteraction.UseGlobal);
		}

		/// <summary>
		///   <para>Find all colliders touching or inside of the given box, and store them into the buffer.</para>
		/// </summary>
		/// <param name="center">Center of the box.</param>
		/// <param name="halfExtents">Half of the size of the box in each dimension.</param>
		/// <param name="results">The buffer to store the results in.</param>
		/// <param name="orientation">Rotation of the box.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a ray.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <param name="mask"></param>
		/// <returns>
		///   <para>The amount of colliders stored in results.</para>
		/// </returns>
		public static int OverlapBoxNonAlloc(
			Vector3 center,
			Vector3 halfExtents,
			Collider[] results,
			[DefaultValue("Quaternion.identity")] Quaternion orientation,
			[DefaultValue("AllLayers")] int mask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			return ThreeD.OverlapBoxNonAlloc.Run(center, halfExtents, results, orientation, mask, queryTriggerInteraction);
		}

		public static int OverlapBoxNonAlloc(
			Vector3 center,
			Vector3 halfExtents,
			Collider[] results,
			Quaternion orientation,
			int mask) {
			return ThreeD.OverlapBoxNonAlloc.Run(center, halfExtents, results, orientation, mask, QueryTriggerInteraction.UseGlobal);
		}

		public static int OverlapBoxNonAlloc(
			Vector3 center,
			Vector3 halfExtents,
			Collider[] results,
			Quaternion orientation) {
			return ThreeD.OverlapBoxNonAlloc.Run(center, halfExtents, results, orientation, -1, QueryTriggerInteraction.UseGlobal);
		}

		public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results) {
			return ThreeD.OverlapBoxNonAlloc.Run(center, halfExtents, results, Quaternion.identity, -1, QueryTriggerInteraction.UseGlobal);
		}

		/// <summary>
		///   <para>Cast the box along the direction, and store hits in the provided buffer.</para>
		/// </summary>
		/// <param name="center">Center of the box.</param>
		/// <param name="halfExtents">Half the size of the box in each dimension.</param>
		/// <param name="direction">The direction in which to cast the box.</param>
		/// <param name="results">The buffer to store the results in.</param>
		/// <param name="orientation">Rotation of the box.</param>
		/// <param name="maxDistance">The max length of the cast.</param>
		/// <param name="layermask">A that is used to selectively ignore colliders when casting a capsule.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <param name="layerMask"></param>
		/// <returns>
		///   <para>The amount of hits stored to the results buffer.</para>
		/// </returns>
		public static int BoxCastNonAlloc(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			RaycastHit[] results,
			[DefaultValue("Quaternion.identity")] Quaternion orientation,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			return ThreeD.BoxCastNonAlloc.Run(center, halfExtents, direction, results, orientation, maxDistance, layerMask,
				queryTriggerInteraction);
		}

		public static int BoxCastNonAlloc(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			RaycastHit[] results,
			Quaternion orientation) {
			return ThreeD.BoxCastNonAlloc.Run(center, halfExtents, direction, results, orientation, float.PositiveInfinity, -5,
				QueryTriggerInteraction.UseGlobal);
		}

		public static int BoxCastNonAlloc(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			RaycastHit[] results,
			Quaternion orientation,
			float maxDistance) {
			return ThreeD.BoxCastNonAlloc.Run(center, halfExtents, direction, results, orientation, maxDistance, -5,
				QueryTriggerInteraction.UseGlobal);
		}

		public static int BoxCastNonAlloc(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			RaycastHit[] results,
			Quaternion orientation,
			float maxDistance,
			int layerMask) {
			return ThreeD.BoxCastNonAlloc.Run(center, halfExtents, direction, results, orientation, maxDistance, layerMask,
				QueryTriggerInteraction.UseGlobal);
		}

		public static int BoxCastNonAlloc(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			RaycastHit[] results) {
			return ThreeD.BoxCastNonAlloc.Run(center, halfExtents, direction, results, Quaternion.identity, float.PositiveInfinity, -5,
				QueryTriggerInteraction.UseGlobal);
		}

		/// <summary>
		///   <para>Like Physics.BoxCast, but returns all hits.</para>
		/// </summary>
		/// <param name="center">Center of the box.</param>
		/// <param name="halfExtents">Half the size of the box in each dimension.</param>
		/// <param name="direction">The direction in which to cast the box.</param>
		/// <param name="orientation">Rotation of the box.</param>
		/// <param name="maxDistance">The max length of the cast.</param>
		/// <param name="layermask">A that is used to selectively ignore colliders when casting a capsule.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <param name="layerMask"></param>
		/// <returns>
		///   <para>All colliders that were hit.</para>
		/// </returns>
		public static RaycastHit[] BoxCastAll(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			[DefaultValue("Quaternion.identity")] Quaternion orientation,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			return ThreeD.BoxcastAll.Run(center, halfExtents, direction, orientation, maxDistance, layerMask,
				queryTriggerInteraction);
		}

		public static RaycastHit[] BoxCastAll(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			Quaternion orientation,
			float maxDistance,
			int layerMask) {
			return ThreeD.BoxcastAll.Run(center, halfExtents, direction, orientation, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] BoxCastAll(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			Quaternion orientation,
			float maxDistance) {
			return ThreeD.BoxcastAll.Run(center, halfExtents, direction, orientation, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] BoxCastAll(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			Quaternion orientation) {
			return ThreeD.BoxcastAll.Run(center, halfExtents, direction, orientation, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] BoxCastAll(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction) {
			return ThreeD.BoxcastAll.Run(center, halfExtents, direction, Quaternion.identity, float.PositiveInfinity, -5,
				QueryTriggerInteraction.UseGlobal);
		}

		/// <summary>
		///   <para>Check the given capsule against the physics world and return all overlapping colliders in the user-provided buffer.</para>
		/// </summary>
		/// <param name="point0">The center of the sphere at the start of the capsule.</param>
		/// <param name="point1">The center of the sphere at the end of the capsule.</param>
		/// <param name="radius">The radius of the capsule.</param>
		/// <param name="results">The buffer to store the results into.</param>
		/// <param name="layerMask">A that is used to selectively ignore colliders when casting a capsule.</param>
		/// <param name="queryTriggerInteraction">Specifies whether this query should hit Triggers.</param>
		/// <returns>
		///   <para>The amount of entries written to the buffer.</para>
		/// </returns>
		public static int OverlapCapsuleNonAlloc(
			Vector3 point0,
			Vector3 point1,
			float radius,
			Collider[] results,
			[DefaultValue("AllLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			return ThreeD.OverlapCapsuleNonAlloc.Run(point0, point1, radius, results, layerMask, queryTriggerInteraction);
		}

		public static int OverlapCapsuleNonAlloc(
			Vector3 point0,
			Vector3 point1,
			float radius,
			Collider[] results,
			int layerMask) {
			return ThreeD.OverlapCapsuleNonAlloc.Run(point0, point1, radius, results, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static int OverlapCapsuleNonAlloc(
			Vector3 point0,
			Vector3 point1,
			float radius,
			Collider[] results) {
			return ThreeD.OverlapCapsuleNonAlloc.Run(point0, point1, radius, results, -1, QueryTriggerInteraction.UseGlobal);
		}
	}
}