using System;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
	public static class VisualPhysics {
		private static float[] _precomputatedSin;
		private static float[] _precomputatedCos;

		private static Vector3 _lastPositionHorizontal = Vector3.zero;
		private static Vector3 _lastPositionVertical = Vector3.zero;
		private static Vector3 _lastPositionVertical2 = Vector3.zero;
		private static Vector3 _cacheHorizontal = Vector3.zero;
		private static Vector3 _cacheVertical = Vector3.zero;
		private static Vector3 _cacheVertical2 = Vector3.zero;

		static VisualPhysics() {
			RecalculateComputations();
		}

		public static void RecalculateComputations() {
			uint iterations = VisualPhysicsSettingsHandler.GetEditorSettings().CircleResolution;
			float radiansDelta = 2 * Mathf.PI / iterations;
			
			_precomputatedSin ??= new float[iterations + 1];
			_precomputatedCos ??= new float[iterations + 1];

			if (_precomputatedSin.Length != iterations + 1) {
				Array.Resize(ref _precomputatedSin, (int)iterations + 1);
			}
			
			if (_precomputatedCos.Length != iterations + 1) {
				Array.Resize(ref _precomputatedCos, (int)iterations + 1);
			}

			for (int i = 0; i <= iterations; i++) {
				float d = radiansDelta * i;
				_precomputatedSin[i] = Mathf.Sin(d);
				_precomputatedCos[i] = Mathf.Cos(d);
			}
		}

		// Raycasts

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

			return RaycastNoHit(origin, direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance, int layerMask) {
			return RaycastNoHit(origin, direction, maxDistance, layerMask);
		}

		public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance) {
			return RaycastNoHit(origin, direction, maxDistance);
		}

		public static bool Raycast(Vector3 origin, Vector3 direction) {
			return RaycastNoHit(origin, direction);
		}

		public static bool Raycast(
			Vector3 origin,
			Vector3 direction,
			out RaycastHit hitInfo,
			float maxDistance,
			int layerMask,
			QueryTriggerInteraction queryTriggerInteraction) {
			return RaycastWithHit(origin, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static bool Raycast(
			Vector3 origin,
			Vector3 direction,
			out RaycastHit hitInfo,
			float maxDistance,
			int layerMask) {
			return RaycastWithHit(origin, direction, out hitInfo, maxDistance, layerMask);
		}

		public static bool Raycast(
			Vector3 origin,
			Vector3 direction,
			out RaycastHit hitInfo,
			float maxDistance) {
			return RaycastWithHit(origin, direction, out hitInfo, maxDistance);
		}

		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo) {
			return RaycastWithHit(origin, direction, out hitInfo);
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
			return RaycastNoHit(ray.origin, ray.direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static bool Raycast(Ray ray, float maxDistance, int layerMask) {
			return RaycastNoHit(ray.origin, ray.direction, maxDistance, layerMask);
		}

		public static bool Raycast(Ray ray, float maxDistance) {
			return RaycastNoHit(ray.origin, ray.direction, maxDistance);
		}

		public static bool Raycast(Ray ray) {
			return RaycastNoHit(ray.origin, ray.direction);
		}

		public static bool Raycast(
			Ray ray,
			out RaycastHit hitInfo,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			return RaycastWithHit(ray.origin, ray.direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance, int layerMask) {
			return RaycastWithHit(ray.origin,
				ray.direction, out hitInfo, maxDistance, layerMask);
		}

		public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance) {
			return RaycastWithHit(ray.origin, ray.direction, out hitInfo, maxDistance);
		}

		public static bool Raycast(Ray ray, out RaycastHit hitInfo) {
			return RaycastWithHit(ray.origin, ray.direction, out hitInfo);
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
			return Linecast(start, end, out _, layerMask, queryTriggerInteraction);
		}

		public static bool Linecast(Vector3 start, Vector3 end, int layerMask) {
			return Linecast(start, end, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static bool Linecast(Vector3 start, Vector3 end) {
			return Linecast(start, end, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static bool Linecast(
			Vector3 start,
			Vector3 end,
			out RaycastHit hitInfo,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			Vector3 direction = end - start;
			return RaycastWithHit(start, direction.normalized, out hitInfo, direction.magnitude, layerMask, queryTriggerInteraction);
		}

		public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo, int layerMask) {
			return RaycastWithHit(start, end, out hitInfo, layerMask);
		}

		public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo) {
			return RaycastWithHit(start, end, out hitInfo);
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

			return CapsuleCast(point1, point2, radius, direction, out _, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static bool CapsuleCast(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			float maxDistance,
			int layerMask) {
			return CapsuleCast(point1, point2, radius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static bool CapsuleCast(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			float maxDistance) {
			return CapsuleCast(point1, point2, radius, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static bool CapsuleCast(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction) {
			return CapsuleCast(point1, point2, radius, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
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
			direction.Normalize();

			bool didHit = Physics.defaultPhysicsScene.CapsuleCast(point1, point2, radius, direction, out hit, maxDistance, layerMask,
				queryTriggerInteraction);

#if UNITY_EDITOR
			float distance = GetMaxRayLength(maxDistance);
			DrawArrow((point1 + point2) * 0.5f, direction * distance, GetDefaultColor());

			DrawCapsule(point1, point2, direction, radius, maxDistance, hit, didHit);

			if (didHit) {
				DrawNormalCircle(hit.point, hit.normal, GetColor(didHit));
			}
#endif

			return didHit;
		}

		public static bool CapsuleCast(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			out RaycastHit hit,
			float maxDistance,
			int layerMask) {
			return CapsuleCast(point1, point2, radius, direction, out hit, maxDistance, layerMask,
				QueryTriggerInteraction.UseGlobal);
		}

		public static bool CapsuleCast(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			out RaycastHit hit,
			float maxDistance) {
			return CapsuleCast(point1, point2, radius, direction, out hit, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static bool CapsuleCast(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			out RaycastHit hit) {
			return CapsuleCast(point1, point2, radius, direction, out hit, float.PositiveInfinity, -5,
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
			direction.Normalize();

			bool didHit = Physics.defaultPhysicsScene.SphereCast(origin, radius, direction, out hit, maxDistance, layerMask,
				queryTriggerInteraction);

#if UNITY_EDITOR
			Color color = GetColor(didHit);
			float distance = GetMaxRayLength(maxDistance);
			DrawArrow(origin, direction * distance, GetDefaultColor());

			if (didHit) {
				DrawNormalCircle(hit.point, hit.normal, color);
				DrawSphere(origin + direction * hit.distance, radius, color);
			} else {
				DrawSphere(origin + direction * distance, radius, color);
			}
#endif

			return didHit;
		}

		public static bool SphereCast(
			Vector3 origin,
			float radius,
			Vector3 direction,
			out RaycastHit hit,
			float maxDistance,
			int layerMask) {
			return SphereCast(origin, radius, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static bool SphereCast(
			Vector3 origin,
			float radius,
			Vector3 direction,
			out RaycastHit hit,
			float maxDistance) {
			return SphereCast(origin, radius, direction, out hit, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static bool SphereCast(
			Vector3 origin,
			float radius,
			Vector3 direction,
			out RaycastHit hit) {
			return SphereCast(origin, radius, direction, out hit, float.PositiveInfinity, -5,
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
			return SphereCast(ray.origin, radius, ray.direction, out RaycastHit _, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static bool SphereCast(Ray ray, float radius, float maxDistance, int layerMask) {
			return SphereCast(ray, radius, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static bool SphereCast(Ray ray, float radius, float maxDistance) {
			return SphereCast(ray, radius, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static bool SphereCast(Ray ray, float radius) {
			return SphereCast(ray, radius, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static bool SphereCast(
			Ray ray,
			float radius,
			out RaycastHit hitInfo,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {
			return SphereCast(ray.origin, radius, ray.direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static bool SphereCast(
			Ray ray,
			float radius,
			out RaycastHit hitInfo,
			float maxDistance,
			int layerMask) {
			return SphereCast(ray, radius, out hitInfo, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static bool SphereCast(
			Ray ray,
			float radius,
			out RaycastHit hitInfo,
			float maxDistance) {
			return SphereCast(ray, radius, out hitInfo, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo) {
			return SphereCast(ray, radius, out hitInfo, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
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
			direction.Normalize();

			RaycastHit[] hits = Physics.SphereCastAll(origin, radius, direction, maxDistance, layerMask, queryTriggerInteraction);

#if UNITY_EDITOR
			bool didHit = hits != null && hits.Length > 0;

			float distance = GetMaxRayLength(maxDistance);
			DrawArrow(origin, direction * distance, GetDefaultColor());
			Color color = GetColor(didHit);

			if (didHit) {
				for (int i = 0; i < hits.Length; i++) {
					ref RaycastHit hit = ref hits[i];

					DrawSphere(origin + direction * hit.distance, radius, color);
				}
			} else {
				DrawSphere(origin + direction * distance, radius, color);
			}
#endif

			return hits;
		}

		public static RaycastHit[] SphereCastAll(
			Vector3 origin,
			float radius,
			Vector3 direction,
			float maxDistance,
			int layerMask) {
			return SphereCastAll(origin, radius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] SphereCastAll(
			Vector3 origin,
			float radius,
			Vector3 direction,
			float maxDistance) {
			return SphereCastAll(origin, radius, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] SphereCastAll(
			Vector3 origin,
			float radius,
			Vector3 direction) {
			return SphereCastAll(origin, radius, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
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
			return SphereCastAll(ray.origin, radius, ray.direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static RaycastHit[] SphereCastAll(
			Ray ray,
			float radius,
			float maxDistance,
			int layerMask) {
			return SphereCastAll(ray, radius, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] SphereCastAll(Ray ray, float radius, float maxDistance) {
			return SphereCastAll(ray, radius, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] SphereCastAll(Ray ray, float radius) {
			return SphereCastAll(ray, radius, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
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

			return BoxCast(center, halfExtents, direction, out _, orientation, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static bool BoxCast(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			Quaternion orientation,
			float maxDistance,
			int layerMask) {
			return BoxCast(center, halfExtents, direction, orientation, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static bool BoxCast(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			Quaternion orientation,
			float maxDistance) {
			return BoxCast(center, halfExtents, direction, orientation, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static bool BoxCast(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			Quaternion orientation) {
			return BoxCast(center, halfExtents, direction, orientation, float.PositiveInfinity, -5,
				QueryTriggerInteraction.UseGlobal);
		}

		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction) {
			return BoxCast(center, halfExtents,
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
			direction.Normalize();

			bool didHit = Physics.defaultPhysicsScene.BoxCast(center, halfExtents, direction, out hit, orientation, maxDistance, layerMask,
				queryTriggerInteraction);
			
#if UNITY_EDITOR

			Color color = GetColor(didHit);
			float distance = GetMaxRayLength(maxDistance);
			DrawArrow(center, direction * distance, GetDefaultColor());

			if (didHit) {
				DrawCube(center + direction * hit.distance, halfExtents, orientation, color);
				DrawNormalCircle(hit.point, hit.normal, color);
			} else {
				DrawCube(center + direction * distance, halfExtents, orientation, color);
			}
#endif

			return didHit;
		}

		public static bool BoxCast(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			out RaycastHit hit,
			Quaternion orientation,
			float maxDistance,
			int layerMask) {
			return BoxCast(center, halfExtents, direction, out hit, orientation, maxDistance, layerMask,
				QueryTriggerInteraction.UseGlobal);
		}

		public static bool BoxCast(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			out RaycastHit hit,
			Quaternion orientation,
			float maxDistance) {
			return BoxCast(center, halfExtents, direction, out hit, orientation, maxDistance, -5,
				QueryTriggerInteraction.UseGlobal);
		}

		public static bool BoxCast(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			out RaycastHit hit,
			Quaternion orientation) {
			return BoxCast(center, halfExtents, direction, out hit, orientation, float.PositiveInfinity, -5,
				QueryTriggerInteraction.UseGlobal);
		}

		public static bool BoxCast(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			out RaycastHit hit) {
			return BoxCast(center, halfExtents, direction, out hit, Quaternion.identity, float.PositiveInfinity, -5,
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
		/// <param name="layerMask"></param>
		public static RaycastHit[] RaycastAll(
			Vector3 origin,
			Vector3 direction,
			[DefaultValue("Mathf.Infinity")] float maxDistance,
			[DefaultValue("DefaultRaycastLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			direction.Normalize();

			RaycastHit[] hits = Physics.RaycastAll(origin, direction, maxDistance, layerMask, queryTriggerInteraction);

#if UNITY_EDITOR
			bool didHit = hits != null && hits.Length != 0;
			Color color = GetColor(didHit);

			float distance = GetMaxRayLength(maxDistance);
			DrawArrow(origin, direction * distance, GetDefaultColor());

			if (!didHit) {
				return hits;
			}

			for (int i = 0; i < hits.Length; i++) {
				ref RaycastHit hit = ref hits[i];
				DrawNormalCircle(hit.point, hit.normal, color);
			}
#endif

			return hits;
		}

		public static RaycastHit[] RaycastAll(
			Vector3 origin,
			Vector3 direction,
			float maxDistance,
			int layerMask) {
			return RaycastAll(origin, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] RaycastAll(
			Vector3 origin,
			Vector3 direction,
			float maxDistance) {
			return RaycastAll(origin, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction) {
			return RaycastAll(origin, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
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
			return RaycastAll(ray.origin, ray.direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static RaycastHit[] RaycastAll(Ray ray, float maxDistance, int layerMask) {
			return RaycastAll(ray.origin, ray.direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] RaycastAll(Ray ray, float maxDistance) {
			return RaycastAll(ray.origin, ray.direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] RaycastAll(Ray ray) {
			return RaycastAll(ray.origin, ray.direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
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

			return RaycastNonAlloc(ray.origin, ray.direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static int RaycastNonAlloc(
			Ray ray,
			RaycastHit[] results,
			float maxDistance,
			int layerMask) {
			return RaycastNonAlloc(ray, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static int RaycastNonAlloc(Ray ray, RaycastHit[] results, float maxDistance) {
			return RaycastNonAlloc(ray, results, maxDistance, Physics.DefaultRaycastLayers, QueryTriggerInteraction.UseGlobal);
		}

		public static int RaycastNonAlloc(Ray ray, RaycastHit[] results) {
			return RaycastNonAlloc(ray, results, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.UseGlobal);
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

			direction.Normalize();

			int hitCount = Physics.defaultPhysicsScene.Raycast(origin, direction, results, maxDistance, layerMask, queryTriggerInteraction);

#if UNITY_EDITOR
			bool didHit = hitCount > 0;
			Color color = GetColor(didHit);

			float distance = GetMaxRayLength(maxDistance);
			DrawArrow(origin, direction * distance, GetDefaultColor());

			if (!didHit) {
				return hitCount;
			}

			for (int i = 0; i < hitCount; i++) {
				ref RaycastHit hit = ref results[i];

				DrawNormalCircle(hit.point, hit.normal, color);
			}
#endif

			return hitCount;
		}

		public static int RaycastNonAlloc(
			Vector3 origin,
			Vector3 direction,
			RaycastHit[] results,
			float maxDistance,
			int layerMask) {
			return RaycastNonAlloc(origin, direction, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static int RaycastNonAlloc(
			Vector3 origin,
			Vector3 direction,
			RaycastHit[] results,
			float maxDistance) {
			return RaycastNonAlloc(origin, direction, results, maxDistance, Physics.DefaultRaycastLayers,
				QueryTriggerInteraction.UseGlobal);
		}

		public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results) {
			return RaycastNonAlloc(origin, direction, results, Mathf.Infinity, Physics.DefaultRaycastLayers,
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

			direction.Normalize();

			RaycastHit[] hits = Physics.CapsuleCastAll(point1, point2, radius, direction, maxDistance, layerMask, queryTriggerInteraction);

#if UNITY_EDITOR
			bool didHit = hits != null && hits.Length != 0;

			float distance = GetMaxRayLength(maxDistance);
			Vector3 origin = (point1 + point2) * 0.5f;
			DrawArrow(origin, direction * distance, GetDefaultColor());

			if (didHit) {
				foreach (RaycastHit hit in hits) {
					DrawCapsule(point1, point2, direction, radius, maxDistance, hit, didHit);

					DrawNormalCircle(hit.point, hit.normal, Color.green);
				}
			} else {
				DrawCapsule(point1, point2, direction, radius, maxDistance, default, didHit);
			}
#endif

			return hits;
		}

		public static RaycastHit[] CapsuleCastAll(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			float maxDistance,
			int layerMask) {
			return CapsuleCastAll(point1, point2, radius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] CapsuleCastAll(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			float maxDistance) {
			return CapsuleCastAll(point1, point2, radius, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] CapsuleCastAll(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction) {
			return CapsuleCastAll(point1, point2, radius, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static Collider[] OverlapCapsule(
			Vector3 point0,
			Vector3 point1,
			float radius,
			int layerMask) {

			return OverlapCapsule(point0, point1, radius, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static Collider[] OverlapCapsule(Vector3 point0, Vector3 point1, float radius) {
			return OverlapCapsule(point0, point1, radius, -1, QueryTriggerInteraction.UseGlobal);
		}

		public static Collider[] OverlapCapsule(
			Vector3 point0,
			Vector3 point1,
			float radius,
			[DefaultValue("AllLayers")] int layerMask,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction) {

			Collider[] colliders = Physics.OverlapCapsule(point0, point1, radius, layerMask, queryTriggerInteraction);

#if UNITY_EDITOR
			bool didHit = colliders != null && colliders.Length > 0;

			DrawCapsuleNoColor(point0, point1, Vector3.zero, radius, 0, default, didHit);

			if (didHit) {
				Vector3 center = (point0 + point1) * 0.5f;
				Color color = GetColor(didHit);

				foreach (Collider collider in colliders) {
					Vector3 colliderPos = collider.transform.position;
					Vector3 closestPoint = Physics.ClosestPoint(center, collider, colliderPos, collider.transform.rotation);
					Vector3 dir = closestPoint - center;
					DrawArrow(center, dir, color);
				}
			}
#endif

			return colliders;
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

			Collider[] colliders = Physics.OverlapSphere(position, radius, layerMask, queryTriggerInteraction);

#if UNITY_EDITOR
			bool didHit = colliders != null && colliders.Length > 0;

			DrawSphere(position, radius, GetDefaultColor());

			if (didHit) {
				Color color = GetColor(didHit);

				foreach (Collider collider in colliders) {
					Vector3 colliderPos = collider.transform.position;
					Vector3 closestPoint = Physics.ClosestPoint(position, collider, colliderPos, collider.transform.rotation);
					Vector3 dir = closestPoint - position;
					DrawArrow(position, dir, color);
				}
			}
#endif

			return colliders;
		}

		public static Collider[] OverlapSphere(Vector3 position, float radius, int layerMask) {
			return OverlapSphere(position, radius, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static Collider[] OverlapSphere(Vector3 position, float radius) {
			return OverlapSphere(position, radius, -1, QueryTriggerInteraction.UseGlobal);
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
			bool isPenetrating = Physics.ComputePenetration(colliderA, positionA, rotationA, colliderB, positionB, rotationB, out direction,
				out distance);

#if UNITY_EDITOR
			if (!isPenetrating) {
				return isPenetrating;
			}

			Vector3 nearestPoint = Physics.ClosestPoint(positionA, colliderB, positionB, rotationB);
			DrawNormalCircle(nearestPoint, -direction.normalized, Color.green, distance);
#endif

			return isPenetrating;
		}

		public static Vector3 ClosestPoint(
			Vector3 point,
			Collider collider,
			Vector3 position,
			Quaternion rotation) {
			Vector3 closestPoint = Physics.ClosestPoint(point, collider, position, rotation);
			Vector3 dir = closestPoint - point;
			float length = dir.magnitude;
			dir.Normalize();
			
#if UNITY_EDITOR
			DrawArrow(point, dir * length, GetDefaultColor());
			DrawCircle(closestPoint, dir, GetColor(true));
#endif

			return closestPoint;
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
			int numberHit = Physics.defaultPhysicsScene.OverlapSphere(position, radius, results, layerMask, queryTriggerInteraction);

			bool didHit = numberHit > 0;

#if UNITY_EDITOR
			DrawSphere(position, radius, GetDefaultColor());

			if (didHit) {
				Color color = GetColor(didHit);

				for (int i = 0; i < numberHit; i++) {
					Collider collider = results[i];
					Vector3 colliderPos = collider.transform.position;
					Vector3 closestPoint = Physics.ClosestPoint(position, collider, colliderPos, collider.transform.rotation);
					Vector3 dir = closestPoint - position;
					DrawArrow(position, dir, color);
				}
			}
#endif

			return numberHit;
		}

		public static int OverlapSphereNonAlloc(
			Vector3 position,
			float radius,
			Collider[] results,
			int layerMask) {
			return OverlapSphereNonAlloc(position, radius, results, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static int OverlapSphereNonAlloc(Vector3 position, float radius, Collider[] results) {
			return OverlapSphereNonAlloc(position, radius, results, -1, QueryTriggerInteraction.UseGlobal);
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

			bool didHit = Physics.CheckSphere(position, radius, layerMask, queryTriggerInteraction);
#if UNITY_EDITOR
			DrawSphere(position, radius, GetColor(didHit));
#endif
			return didHit;
		}

		public static bool CheckSphere(Vector3 position, float radius, int layerMask) {
			return CheckSphere(position, radius, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static bool CheckSphere(Vector3 position, float radius) {
			return CheckSphere(position, radius, -5, QueryTriggerInteraction.UseGlobal);
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
			int count = Physics.defaultPhysicsScene.CapsuleCast(point1, point2, radius, direction, results, maxDistance, layerMask,
				queryTriggerInteraction);

			direction.Normalize();

#if UNITY_EDITOR
			bool didHit = count > 0;
			float distance = GetMaxRayLength(maxDistance);
			Vector3 origin = (point1 + point2) * 0.5f;

			DrawArrow(origin, direction * distance, GetDefaultColor());

			if (didHit) {
				for (int i = 0; i < count; i++) {
					ref RaycastHit hit = ref results[i];
					DrawCapsule(point1, point2, direction, radius, maxDistance, hit, didHit);
					DrawNormalCircle(hit.point, hit.normal, GetColor(didHit));
				}
			} else {
				DrawCapsule(point1, point2, direction, radius, maxDistance, default, didHit);
			}
#endif

			return count;
		}

		public static int CapsuleCastNonAlloc(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			RaycastHit[] results,
			float maxDistance,
			int layerMask) {
			return CapsuleCastNonAlloc(point1, point2, radius, direction, results, maxDistance, layerMask,
				QueryTriggerInteraction.UseGlobal);
		}

		public static int CapsuleCastNonAlloc(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			RaycastHit[] results,
			float maxDistance) {
			return CapsuleCastNonAlloc(point1, point2, radius, direction, results, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static int CapsuleCastNonAlloc(
			Vector3 point1,
			Vector3 point2,
			float radius,
			Vector3 direction,
			RaycastHit[] results) {
			return CapsuleCastNonAlloc(point1, point2, radius, direction, results, float.PositiveInfinity, -5,
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

			direction.Normalize();

			int count = Physics.defaultPhysicsScene.SphereCast(origin, radius, direction, results, maxDistance, layerMask,
				queryTriggerInteraction);

#if UNITY_EDITOR
			bool didHit = count > 0;

			float distance = GetMaxRayLength(maxDistance);
			DrawArrow(origin, direction * distance, GetDefaultColor());
			Color color = GetColor(didHit);

			if (didHit) {
				for (int i = 0; i < count; i++) {
					ref RaycastHit hit = ref results[i];

					DrawSphere(origin + direction * hit.distance, radius, color);
				}
			} else {
				DrawSphere(origin + direction * distance, radius, color);
			}
#endif

			return count;
		}

		public static int SphereCastNonAlloc(
			Vector3 origin,
			float radius,
			Vector3 direction,
			RaycastHit[] results,
			float maxDistance,
			int layerMask) {
			return SphereCastNonAlloc(origin, radius, direction, results, maxDistance, layerMask,
				QueryTriggerInteraction.UseGlobal);
		}

		public static int SphereCastNonAlloc(
			Vector3 origin,
			float radius,
			Vector3 direction,
			RaycastHit[] results,
			float maxDistance) {
			return SphereCastNonAlloc(origin, radius, direction, results, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static int SphereCastNonAlloc(
			Vector3 origin,
			float radius,
			Vector3 direction,
			RaycastHit[] results) {
			return SphereCastNonAlloc(origin, radius, direction, results, float.PositiveInfinity, -5,
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
			return SphereCastNonAlloc(ray.origin, radius, ray.direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		public static int SphereCastNonAlloc(
			Ray ray,
			float radius,
			RaycastHit[] results,
			float maxDistance,
			int layerMask) {
			return SphereCastNonAlloc(ray, radius, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static int SphereCastNonAlloc(
			Ray ray,
			float radius,
			RaycastHit[] results,
			float maxDistance) {
			return SphereCastNonAlloc(ray, radius, results, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results) {
			return SphereCastNonAlloc(ray, radius, results, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
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
			bool didHit = Physics.CheckCapsule(start, end, radius, layerMask, queryTriggerInteraction);
#if UNITY_EDITOR
			DrawCapsule(start, end, Vector3.zero, radius, 0, default, didHit);
#endif
			return didHit;
		}

		public static bool CheckCapsule(Vector3 start, Vector3 end, float radius, int layerMask) {
			return CheckCapsule(start, end, radius, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static bool CheckCapsule(Vector3 start, Vector3 end, float radius) {
			return CheckCapsule(start, end, radius, -5, QueryTriggerInteraction.UseGlobal);
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

			bool didHit = Physics.CheckBox(center, halfExtents, orientation, layermask, queryTriggerInteraction);
#if UNITY_EDITOR
			DrawCube(center, halfExtents, orientation, GetColor(didHit));
#endif
			return didHit;
		}

		public static bool CheckBox(
			Vector3 center,
			Vector3 halfExtents,
			Quaternion orientation,
			int layerMask) {
			return CheckBox(center, halfExtents, orientation, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static bool CheckBox(Vector3 center, Vector3 halfExtents, Quaternion orientation) {
			return CheckBox(center, halfExtents, orientation, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static bool CheckBox(Vector3 center, Vector3 halfExtents) {
			return CheckBox(center, halfExtents, Quaternion.identity, -5, QueryTriggerInteraction.UseGlobal);
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
			Collider[] colliders = Physics.OverlapBox(center, halfExtents, orientation, layerMask, queryTriggerInteraction);

			bool didHit = colliders != null && colliders.Length > 0;

#if UNITY_EDITOR
			DrawCube(center, halfExtents, orientation, GetDefaultColor());

			if (didHit) {
				Color color = GetColor(didHit);

				foreach (Collider collider in colliders) {
					Vector3 colliderPos = collider.transform.position;
					Vector3 closestPoint = Physics.ClosestPoint(center, collider, colliderPos, collider.transform.rotation);
					Vector3 dir = closestPoint - center;
					DrawArrow(center, dir, color);
				}
			}
#endif

			return colliders;
		}

		public static Collider[] OverlapBox(
			Vector3 center,
			Vector3 halfExtents,
			Quaternion orientation,
			int layerMask) {
			return OverlapBox(center, halfExtents, orientation, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static Collider[] OverlapBox(
			Vector3 center,
			Vector3 halfExtents,
			Quaternion orientation) {
			return OverlapBox(center, halfExtents, orientation, -1, QueryTriggerInteraction.UseGlobal);
		}

		public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents) {
			return OverlapBox(center, halfExtents, Quaternion.identity, -1, QueryTriggerInteraction.UseGlobal);
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
			int count = Physics.defaultPhysicsScene.OverlapBox(center, halfExtents, results, orientation, mask, queryTriggerInteraction);

#if UNITY_EDITOR
			bool didHit = count > 0;

			DrawCube(center, halfExtents, orientation, GetDefaultColor());

			if (didHit) {
				Color color = GetColor(didHit);

				for (int i = 0; i < count; i++) {
					Collider collider = results[i];
					Vector3 colliderPos = collider.transform.position;
					Vector3 closestPoint = Physics.ClosestPoint(center, collider, colliderPos, collider.transform.rotation);
					Vector3 dir = closestPoint - center;
					DrawArrow(center, dir, color);
				}
			}
#endif

			return count;
		}

		public static int OverlapBoxNonAlloc(
			Vector3 center,
			Vector3 halfExtents,
			Collider[] results,
			Quaternion orientation,
			int mask) {
			return OverlapBoxNonAlloc(center, halfExtents, results, orientation, mask, QueryTriggerInteraction.UseGlobal);
		}

		public static int OverlapBoxNonAlloc(
			Vector3 center,
			Vector3 halfExtents,
			Collider[] results,
			Quaternion orientation) {
			return OverlapBoxNonAlloc(center, halfExtents, results, orientation, -1, QueryTriggerInteraction.UseGlobal);
		}

		public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results) {
			return OverlapBoxNonAlloc(center, halfExtents, results, Quaternion.identity, -1, QueryTriggerInteraction.UseGlobal);
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
			direction.Normalize();

			int count = Physics.defaultPhysicsScene.BoxCast(center, halfExtents, direction, results, orientation, maxDistance, layerMask,
				queryTriggerInteraction);

#if UNITY_EDITOR
			bool didHit = count > 0;

			float distance = GetMaxRayLength(maxDistance);
			DrawArrow(center, direction * distance, GetDefaultColor());
			Color color = GetColor(didHit);

			if (didHit) {
				for (int i = 0; i < count; i++) {
					ref RaycastHit hit = ref results[i];
					DrawCube(center + direction * hit.distance, halfExtents, orientation, color);
				}
			} else {
				DrawCube(center + direction * distance, halfExtents, orientation, color);
			}
#endif

			return count;
		}

		public static int BoxCastNonAlloc(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			RaycastHit[] results,
			Quaternion orientation) {
			return BoxCastNonAlloc(center, halfExtents, direction, results, orientation, float.PositiveInfinity, -5,
				QueryTriggerInteraction.UseGlobal);
		}

		public static int BoxCastNonAlloc(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			RaycastHit[] results,
			Quaternion orientation,
			float maxDistance) {
			return BoxCastNonAlloc(center, halfExtents, direction, results, orientation, maxDistance, -5,
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
			return BoxCastNonAlloc(center, halfExtents, direction, results, orientation, maxDistance, layerMask,
				QueryTriggerInteraction.UseGlobal);
		}

		public static int BoxCastNonAlloc(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			RaycastHit[] results) {
			return BoxCastNonAlloc(center, halfExtents, direction, results, Quaternion.identity, float.PositiveInfinity, -5,
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
			direction.Normalize();

			RaycastHit[] hits = Physics.BoxCastAll(center, halfExtents, direction, orientation, maxDistance, layerMask,
				queryTriggerInteraction);

#if UNITY_EDITOR
			bool didHit = hits != null && hits.Length > 0;

			float distance = GetMaxRayLength(maxDistance);
			DrawArrow(center, direction * distance, GetDefaultColor());
			Color color = GetColor(didHit);

			if (didHit) {
				for (int i = 0; i < hits.Length; i++) {
					ref RaycastHit hit = ref hits[i];

					DrawCube(center + direction * hit.distance, halfExtents, orientation, color);
				}
			} else {
				DrawCube(center + direction * distance, halfExtents, orientation, color);
			}
#endif

			return hits;
		}

		public static RaycastHit[] BoxCastAll(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			Quaternion orientation,
			float maxDistance,
			int layerMask) {
			return BoxCastAll(center, halfExtents, direction, orientation, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] BoxCastAll(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			Quaternion orientation,
			float maxDistance) {
			return BoxCastAll(center, halfExtents, direction, orientation, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] BoxCastAll(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction,
			Quaternion orientation) {
			return BoxCastAll(center, halfExtents, direction, orientation, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		public static RaycastHit[] BoxCastAll(
			Vector3 center,
			Vector3 halfExtents,
			Vector3 direction) {
			return BoxCastAll(center, halfExtents, direction, Quaternion.identity, float.PositiveInfinity, -5,
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
			int count = Physics.defaultPhysicsScene.OverlapCapsule(point0, point1, radius, results, layerMask, queryTriggerInteraction);
			
#if UNITY_EDITOR
			bool didHit = count > 0;

			DrawCapsuleNoColor(point0, point1, Vector3.zero, radius, 0, default, didHit);

			if (didHit) {
				Vector3 center = (point0 + point1) * 0.5f;
				Color color = GetColor(didHit);

				for (int i = 0; i < count; i++) {
					Collider collider = results[i];
					Vector3 colliderPos = collider.transform.position;
					Vector3 closestPoint = Physics.ClosestPoint(center, collider, colliderPos, collider.transform.rotation);
					Vector3 dir = closestPoint - center;
					DrawArrow(center, dir, color);
				}
			}
#endif
			
			return count;
		}

		public static int OverlapCapsuleNonAlloc(
			Vector3 point0,
			Vector3 point1,
			float radius,
			Collider[] results,
			int layerMask) {
			return OverlapCapsuleNonAlloc(point0, point1, radius, results, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		public static int OverlapCapsuleNonAlloc(
			Vector3 point0,
			Vector3 point1,
			float radius,
			Collider[] results) {
			return OverlapCapsuleNonAlloc(point0, point1, radius, results, -1, QueryTriggerInteraction.UseGlobal);
		}

		// other

		private static float GetMaxRayLength(float distance) => Mathf.Min(distance, 10000);

		private static Color GetColor(bool value) {
#if UNITY_EDITOR
			VisualPhysicsSettingsHandler.NewCustomSettings settings = VisualPhysicsSettingsHandler.GetEditorSettings();

			return value ? settings.HitColor : settings.NoHitColor;
#endif
			return Color.white;
		}

		private static Color GetDefaultColor() {
#if UNITY_EDITOR
			VisualPhysicsSettingsHandler.NewCustomSettings settings = VisualPhysicsSettingsHandler.GetEditorSettings();

			return settings.DefaultColor;
#endif
			return Color.white;
		}

		private static bool RaycastNoHit(
			Vector3 origin,
			Vector3 direction,
			[DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity,
			[DefaultValue("Physics.DefaultRaycastLayers")]
			int layerMask = -5,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal) {

			direction.Normalize();

			bool didHit = Physics.defaultPhysicsScene.Raycast(origin, direction, out RaycastHit hit, maxDistance, layerMask,
				queryTriggerInteraction);

#if UNITY_EDITOR
			Color color = GetColor(didHit);

			if (didHit) {
				DrawLine(origin, hit.point, color);

				DrawNormalCircle(hit.point, hit.normal, GetColor(didHit));
			} else {
				float distance = didHit ? hit.distance : GetMaxRayLength(maxDistance);
				DrawArrow(origin, direction * distance, color);
			}
#endif

			return didHit;
		}

		private static bool RaycastWithHit(
			Vector3 origin,
			Vector3 direction,
			out RaycastHit hit,
			[DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity,
			[DefaultValue("Physics.DefaultRaycastLayers")]
			int layerMask = -5,
			[DefaultValue("QueryTriggerInteraction.UseGlobal")]
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal) {

			direction.Normalize();

			bool didHit = Physics.defaultPhysicsScene.Raycast(origin, direction, out hit, maxDistance, layerMask,
				queryTriggerInteraction);

#if UNITY_EDITOR
			Color color = GetColor(didHit);

			if (didHit) {
				DrawLine(origin, hit.point, color);

				DrawNormalCircle(hit.point, hit.normal, GetColor(didHit));
			} else {
				float distance = GetMaxRayLength(maxDistance);
				DrawArrow(origin, direction * distance, color);
			}
#endif

			return didHit;
		}

#if UNITY_EDITOR
		private static void DrawCircle(in Vector3 center, in Vector3 upwardDirection, in Color color) {
			const float RADIUS = 0.025f;
			
			uint iterations = VisualPhysicsSettingsHandler.GetEditorSettings().CircleResolution;

			Vector3 lastPosition = Vector3.zero;
			Vector3 cachePosition = Vector3.zero;

			for (int i = 0; i <= iterations; i++) {
				float sin = _precomputatedSin[i] * RADIUS;
				float cos = _precomputatedCos[i] * RADIUS;

				cachePosition.x = cos;
				cachePosition.y = sin;
				cachePosition.z = 0;

				Quaternion rot = Quaternion.LookRotation(upwardDirection);
				cachePosition = rot * cachePosition;
				cachePosition += center;

				if (i != 0) {
					Debug.DrawLine(lastPosition, cachePosition, color);
				}

				lastPosition.x = cachePosition.x;
				lastPosition.y = cachePosition.y;
				lastPosition.z = cachePosition.z;
			}
		}

		private static void DrawNormalCircle(in Vector3 center, in Vector3 upwardDirection, in Color color, float distance = 0.025f) {
			const float RADIUS = 0.05f;

			VisualPhysicsSettingsHandler.NewCustomSettings settings = VisualPhysicsSettingsHandler.GetEditorSettings();

			Vector3 lastPosition = Vector3.zero;
			Vector3 cachePosition = Vector3.zero;

			for (int i = 0; i <= settings.CircleResolution; i++) {
				float sin = _precomputatedSin[i] * RADIUS;
				float cos = _precomputatedCos[i] * RADIUS;

				cachePosition.x = cos;
				cachePosition.y = sin;
				cachePosition.z = 0;

				Quaternion rot = Quaternion.LookRotation(upwardDirection);
				cachePosition = rot * cachePosition;
				cachePosition += center;

				if (i != 0) {
					Debug.DrawLine(lastPosition, cachePosition, color);
				}

				lastPosition.x = cachePosition.x;
				lastPosition.y = cachePosition.y;
				lastPosition.z = cachePosition.z;
			}

			DrawArrow(center, upwardDirection.normalized * distance, GetDefaultColor(), true, settings.ImpactCircleNormalArrowLength);
		}

		private static void DrawSphere(in Vector3 center, in float radius, in Color color) {
			uint iterations = VisualPhysicsSettingsHandler.GetEditorSettings().CircleResolution;
			
			for (int i = 0; i <= iterations; i++) {
				float sin = _precomputatedSin[i] * radius;
				float cos = _precomputatedCos[i] * radius;

				_cacheHorizontal.x = center.x + cos;
				_cacheHorizontal.y = center.y + sin;
				_cacheHorizontal.z = center.z;

				_cacheVertical.x = center.x + cos;
				_cacheVertical.y = center.y;
				_cacheVertical.z = center.z + sin;

				_cacheVertical2.x = center.x;
				_cacheVertical2.y = center.y + cos;
				_cacheVertical2.z = center.z + sin;

				if (i != 0) {
					DrawLine(_lastPositionHorizontal, _cacheHorizontal, color);
					DrawLine(_lastPositionVertical, _cacheVertical, color);
					DrawLine(_lastPositionVertical2, _cacheVertical2, color);
				}

				_lastPositionHorizontal.x = _cacheHorizontal.x;
				_lastPositionHorizontal.y = _cacheHorizontal.y;
				_lastPositionHorizontal.z = _cacheHorizontal.z;

				_lastPositionVertical.x = _cacheVertical.x;
				_lastPositionVertical.y = _cacheVertical.y;
				_lastPositionVertical.z = _cacheVertical.z;

				_lastPositionVertical2.x = _cacheVertical2.x;
				_lastPositionVertical2.y = _cacheVertical2.y;
				_lastPositionVertical2.z = _cacheVertical2.z;
			}
		}

		private static void DrawCapsuleNoColor(in Vector3 point1, in Vector3 point2, in Vector3 direction, in float radius,
			in float maxDistance,
			in RaycastHit hit, in bool didHit) {
			Color color = GetDefaultColor();
			
			if (didHit) {
				Vector3 dir = direction * hit.distance;
				Vector3 point1Pos = point1 + dir;
				Vector3 point2Pos = point2 + dir;

				DrawLine(point1Pos + Vector3.forward * radius, point2Pos + Vector3.forward * radius, color);
				DrawLine(point1Pos + Vector3.back * radius, point2Pos + Vector3.back * radius, color);
				DrawLine(point1Pos + Vector3.right * radius, point2Pos + Vector3.right * radius, color);
				DrawLine(point1Pos + Vector3.left * radius, point2Pos + Vector3.left * radius, color);

				DrawSphere(point1Pos, radius, color);
				DrawSphere(point2Pos, radius, color);
			} else {
				float rayDistance = GetMaxRayLength(maxDistance);
				Vector3 dir = direction * rayDistance;
				Vector3 point1Pos = point1 + dir;
				Vector3 point2Pos = point2 + dir;

				DrawLine(point1Pos + Vector3.forward * radius, point2Pos + Vector3.forward * radius, color);
				DrawLine(point1Pos + Vector3.back * radius, point2Pos + Vector3.back * radius, color);
				DrawLine(point1Pos + Vector3.right * radius, point2Pos + Vector3.right * radius, color);
				DrawLine(point1Pos + Vector3.left * radius, point2Pos + Vector3.left * radius, color);
				DrawSphere(point1Pos, radius, color);
				DrawSphere(point2Pos, radius, color);
			}
		}

		private static void DrawCapsule(in Vector3 point1, in Vector3 point2, in Vector3 direction, in float radius, in float maxDistance,
			in RaycastHit hit, in bool didHit) {
			Color color = GetColor(didHit);

			if (didHit) {
				Vector3 dir = direction * hit.distance;
				Vector3 point1Pos = point1 + dir;
				Vector3 point2Pos = point2 + dir;

				DrawLine(point1Pos + Vector3.forward * radius, point2Pos + Vector3.forward * radius, color);
				DrawLine(point1Pos + Vector3.back * radius, point2Pos + Vector3.back * radius, color);
				DrawLine(point1Pos + Vector3.right * radius, point2Pos + Vector3.right * radius, color);
				DrawLine(point1Pos + Vector3.left * radius, point2Pos + Vector3.left * radius, color);

				DrawSphere(point1Pos, radius, color);
				DrawSphere(point2Pos, radius, color);
			} else {
				float rayDistance = GetMaxRayLength(maxDistance);
				Vector3 dir = direction * rayDistance;
				Vector3 point1Pos = point1 + dir;
				Vector3 point2Pos = point2 + dir;

				DrawLine(point1Pos + Vector3.forward * radius, point2Pos + Vector3.forward * radius, color);
				DrawLine(point1Pos + Vector3.back * radius, point2Pos + Vector3.back * radius, color);
				DrawLine(point1Pos + Vector3.right * radius, point2Pos + Vector3.right * radius, color);
				DrawLine(point1Pos + Vector3.left * radius, point2Pos + Vector3.left * radius, color);
				DrawSphere(point1Pos, radius, color);
				DrawSphere(point2Pos, radius, color);
			}
		}

		private static void DrawCube(in Vector3 center, in Vector3 size, in Quaternion rotation, Color color) {
			Matrix4x4 matrix4X4 = Matrix4x4.TRS(center, rotation, size);

			Vector3 blF = matrix4X4.MultiplyPoint(new Vector3(-1, -1, 1));
			Vector3 brF = matrix4X4.MultiplyPoint(new Vector3(1, -1, 1));
			Vector3 tlF = matrix4X4.MultiplyPoint(new Vector3(-1, 1, 1));
			Vector3 trF = matrix4X4.MultiplyPoint(new Vector3(1, 1, 1));

			Vector3 blB = matrix4X4.MultiplyPoint(new Vector3(-1, -1, -1));
			Vector3 brB = matrix4X4.MultiplyPoint(new Vector3(1, -1, -1));
			Vector3 tlB = matrix4X4.MultiplyPoint(new Vector3(-1, 1, -1));
			Vector3 trB = matrix4X4.MultiplyPoint(new Vector3(1, 1, -1));

			DrawLine(blF, brF, color);
			DrawLine(brF, trF, color);
			DrawLine(trF, tlF, color);
			DrawLine(tlF, blF, color);

			DrawLine(blB, brB, color);
			DrawLine(brB, trB, color);
			DrawLine(trB, tlB, color);
			DrawLine(tlB, blB, color);

			DrawLine(blB, blF, color);
			DrawLine(brB, brF, color);
			DrawLine(trB, trF, color);
			DrawLine(tlB, tlF, color);
		}

		private static void DrawArrow(
			in Vector3 pos,
			in Vector3 direction,
			in Color color,
			in bool useCustomLength = false,
			float arrowHeadLength = 0.1f,
			in float arrowHeadAngle = 20.0f,
			in float arrowPosition = 1) {

			if (!useCustomLength) {
				arrowHeadLength = VisualPhysicsSettingsHandler.GetEditorSettings().RegularArrowLength;
			}
			
			Quaternion rot = direction == Vector3.zero ? Quaternion.identity : Quaternion.LookRotation(direction);
			Vector3 backDir = Vector3.back * arrowHeadLength;
			Vector3 right = rot * Quaternion.Euler(arrowHeadAngle, 0, 0) * backDir;
			Vector3 left = rot * Quaternion.Euler(-arrowHeadAngle, 0, 0) * backDir;
			Vector3 up = rot * Quaternion.Euler(0, arrowHeadAngle, 0) * backDir;
			Vector3 down = rot * Quaternion.Euler(0, -arrowHeadAngle, 0) * backDir;

			Vector3 arrowTip = pos + direction * arrowPosition;

			DrawRay(pos, direction, color);
			DrawRay(arrowTip, right, color);
			DrawRay(arrowTip, left, color);
			DrawRay(arrowTip, up, color);
			DrawRay(arrowTip, down, color);
		}

		private static void DrawLine(in Vector3 start, in Vector3 end, in Color color) {
			Debug.DrawLine(start, end, color, 0, true);
		}

		private static void DrawRay(in Vector3 start, in Vector3 direction, in Color color) {
			Debug.DrawRay(start, direction, color, 0, true);
		}
#endif
	}
}