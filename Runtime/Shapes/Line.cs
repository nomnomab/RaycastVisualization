#if UNITY_EDITOR
using UnityEngine;

namespace Nomnom.RaycastVisualization.Shapes {
    internal struct Line {
        public Vector3 from;
        public Vector3 to;

        public void Draw(Color color) {
            float drawTime = VisualPhysics.NextDrawTime;
            Debug.DrawLine(from, to, color, drawTime, true);
        }
    }
}
#endif