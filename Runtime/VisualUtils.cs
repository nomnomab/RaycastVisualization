using System;
using UnityEngine;

namespace Nomnom.RaycastVisualization {
	internal static class VisualUtils {
		private static float[] _precomputatedSin;
		private static float[] _precomputatedCos;
		private static Vector3 _lastPositionHorizontal = Vector3.zero;
		private static Vector3 _lastPositionVertical = Vector3.zero;
		private static Vector3 _lastPositionVertical2 = Vector3.zero;
		private static Vector3 _cacheHorizontal = Vector3.zero;
		private static Vector3 _cacheVertical = Vector3.zero;
		private static Vector3 _cacheVertical2 = Vector3.zero;
		
#if UNITY_EDITOR
		static VisualUtils() {
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
#endif
		
		public static float GetMaxRayLength(float distance) => Mathf.Min(distance, 10000);

		public static Color GetColor(bool value) {
#if UNITY_EDITOR
			VisualPhysicsSettingsHandler.NewCustomSettings settings = VisualPhysicsSettingsHandler.GetEditorSettings();

			return value ? settings.HitColor : settings.NoHitColor;
#endif
			return Color.white;
		}

		public static Color GetDefaultColor() {
#if UNITY_EDITOR
			VisualPhysicsSettingsHandler.NewCustomSettings settings = VisualPhysicsSettingsHandler.GetEditorSettings();

			return settings.DefaultColor;
#endif
			return Color.white;
		}

#if UNITY_EDITOR
		internal static void DrawCircle(in Vector3 center, in Vector3 upwardDirection, in Color color) {
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

				Quaternion rot = upwardDirection == Vector3.zero ? Quaternion.identity : Quaternion.LookRotation(upwardDirection);

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

		internal static void DrawNormalCircle(in Vector3 center, in Vector3 upwardDirection, in Color color, float distance = 0.025f) {
			VisualPhysicsSettingsHandler.NewCustomSettings settings = VisualPhysicsSettingsHandler.GetEditorSettings();

			Vector3 lastPosition = Vector3.zero;
			Vector3 cachePosition = Vector3.zero;

			for (int i = 0; i <= settings.CircleResolution; i++) {
				float sin = _precomputatedSin[i] * settings.CircleRadius;
				float cos = _precomputatedCos[i] * settings.CircleRadius;

				cachePosition.x = cos;
				cachePosition.y = sin;
				cachePosition.z = 0;

				Quaternion rot = upwardDirection == Vector3.zero ? Quaternion.identity : Quaternion.LookRotation(upwardDirection);
				
				cachePosition = rot * cachePosition;
				cachePosition += center + upwardDirection * settings.CircleDistance;

				if (i != 0) {
					Debug.DrawLine(lastPosition, cachePosition, color);
				}

				lastPosition.x = cachePosition.x;
				lastPosition.y = cachePosition.y;
				lastPosition.z = cachePosition.z;
			}

			DrawArrow(center, upwardDirection.normalized * distance, GetDefaultColor(), true, settings.ImpactCircleNormalArrowLength);
		}

		internal static void DrawSphere(in Vector3 center, float radius, in Color color) {
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

		internal static void DrawCapsuleNoColor(in Vector3 point1, in Vector3 point2, in Vector3 direction, in float radius,
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

		internal static void DrawCapsule(in Vector3 point1, in Vector3 point2, in Vector3 direction, float radius, float maxDistance,
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

		internal static void DrawCube(in Vector3 center, in Vector3 size, in Quaternion rotation, in Color color) {
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

		internal static void DrawArrow(
			in Vector3 pos,
			in Vector3 direction,
			in Color color,
			bool useCustomLength = false,
			float arrowHeadLength = 0.1f,
			float arrowHeadAngle = 20.0f,
			float arrowPosition = 1) {

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

		internal static void DrawLine(in Vector3 start, in Vector3 end, in Color color) {
			Debug.DrawLine(start, end, color, 0, true);
		}

		internal static void DrawRay(in Vector3 start, in Vector3 direction, in Color color) {
			Debug.DrawRay(start, direction, color, 0, true);
		}
#endif
	}
}