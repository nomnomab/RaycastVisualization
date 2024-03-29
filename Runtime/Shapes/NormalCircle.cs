﻿#if UNITY_EDITOR
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Nomnom.RaycastVisualization.Shapes {
    public struct NormalCircle {
        public static NormalCircle Default => new NormalCircle {
            radius = 0.025f,
            distance = 0.025f
        };

        private static uint Iterations => VisualPhysicsSettingsHandler.GetEditorSettings().CircleResolution;
        private static float CircleDistance => VisualPhysicsSettingsHandler.GetEditorSettings().CircleDistance;
        private static float CircleLength => VisualPhysicsSettingsHandler.GetEditorSettings().ImpactCircleNormalArrowLength;

        public Vector3 origin;
        public Vector3 upDirection;
        public float radius;
        public float distance;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Draw(Color color) {
            float[] sinCache = Utils.Sin;
            float[] cosCache = Utils.Cos;

            Vector3 lastPosition = Vector3.zero;
            Vector3 cachePosition = Vector3.zero;

            uint iterations = Iterations;
            float distance = CircleDistance;

            for (int i = 0; i <= iterations; i++) {
                float sin = sinCache[i] * radius;
                float cos = cosCache[i] * radius;

                cachePosition.x = cos;
                cachePosition.y = sin;
                cachePosition.z = 0;

                Quaternion rot = upDirection == Vector3.zero || IsNan(upDirection) ? Quaternion.identity : Quaternion.LookRotation(upDirection);

                cachePosition = rot * cachePosition;
                cachePosition += origin + upDirection * distance;

                if (i != 0) {
                    float drawTime = VisualPhysics.NextDrawTime;
                    Debug.DrawLine(lastPosition, cachePosition, color, drawTime, true);
                }

                lastPosition = cachePosition;
            }

            Arrow arrow = Arrow.Default;

            arrow.origin = origin;
            arrow.direction = upDirection.normalized * this.distance;
            arrow.overrideLength = true;
            arrow.headLength = CircleLength;
            arrow.Draw(VisualUtils.GetDefaultColor());
        }

        private bool IsNan(Vector3 vector) {
            return float.IsNaN(vector.x) || float.IsNaN(vector.y) || float.IsNaN(vector.z);
        }
    }
}
#endif