#if UNITY_EDITOR
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Nomnom.RaycastVisualization.Shapes {
    public struct Circle {
        public static Circle Default => new Circle {
            radius = 0.025f
        };

        private static uint Iterations => VisualPhysicsSettingsHandler.GetEditorSettings().CircleResolution;

        public Vector3 origin;
        public Vector3 upDirection;
        public float radius;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Draw(Color color) {
            float[] sinCache = Utils.Sin;
            float[] cosCache = Utils.Cos;

            Vector3 lastPosition = Vector3.zero;
            Vector3 cachePosition = Vector3.zero;

            uint iterations = Iterations;
            for (int i = 0; i <= iterations; i++) {
                float sin = sinCache[i] * radius;
                float cos = cosCache[i] * radius;

                cachePosition.x = cos;
                cachePosition.y = sin;
                cachePosition.z = 0;

                Quaternion rot = upDirection == Vector3.zero || VisualUtils.IsNan(upDirection) ? Quaternion.identity : Quaternion.LookRotation(upDirection);

                cachePosition = rot * cachePosition;
                cachePosition += origin;

                if (i != 0) {
                    float drawTime = VisualPhysics.NextDrawTime;
                    Debug.DrawLine(lastPosition, cachePosition, color, drawTime, true);
                }

                lastPosition = cachePosition;
            }
        }
    }
}
#endif