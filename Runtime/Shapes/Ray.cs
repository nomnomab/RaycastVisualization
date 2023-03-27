#if UNITY_EDITOR
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Nomnom.RaycastVisualization.Shapes {
    internal struct Ray {
        public Vector3 from;
        public Vector3 direction;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Draw(Color color) {
            float drawTime = VisualPhysics.NextDrawTime;
            Debug.DrawRay(from, direction, color, drawTime, true);
        }
    }
}

#endif