#if UNITY_EDITOR
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Nomnom.RaycastVisualization.Shapes {
    internal struct Sphere {
        private static uint Iterations => VisualPhysicsSettingsHandler.GetEditorSettings().CircleResolution;

        public Vector3 origin;
        public float radius;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Draw(Color color) {
            float[] sinCache = Utils.Sin;
            float[] cosCache = Utils.Cos;

            Vector3 lastPositionHorizontal = default;
            Vector3 lastPositionVertical = default;
            Vector3 lastPositionVertical2 = default;

            uint iterations = Iterations;
            for (int i = 0; i <= iterations; i++) {
                float sin = sinCache[i] * radius;
                float cos = cosCache[i] * radius;
                Vector3 horizontal = new Vector3(origin.x + cos, origin.y + sin, origin.z);
                Vector3 vertical = new Vector3(origin.x + cos, origin.y, origin.z + sin);
                Vector3 vertical2 = new Vector3(origin.x, origin.y + cos, origin.z + sin);

                if (i != 0) {
                    float drawTime = VisualPhysics.NextDrawTime;
                    Debug.DrawLine(lastPositionHorizontal, horizontal, color, drawTime, true);
                    Debug.DrawLine(lastPositionVertical, vertical, color, drawTime, true);
                    Debug.DrawLine(lastPositionVertical2, vertical2, color, drawTime, true);
                }

                lastPositionHorizontal = horizontal;
                lastPositionVertical = vertical;
                lastPositionVertical2 = vertical2;
            }
        }
    }
}
#endif