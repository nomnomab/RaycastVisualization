#if UNITY_EDITOR
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Nomnom.RaycastVisualization.Shapes {
    internal struct Range {
        public Vector3 from;
        public Vector3 to;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Draw(Color color) {
            float drawTime = VisualPhysics.NextDrawTime;
            Debug.DrawLine(from, to, color, drawTime, true);

            NormalCircle min = NormalCircle.Default;
            min.origin = from;
            min.upDirection = Vector3.forward;

            NormalCircle max = NormalCircle.Default;
            max.origin = to;
            max.upDirection = Vector3.back;

            min.Draw(color);
            max.Draw(color);
        }
    }
}
#endif