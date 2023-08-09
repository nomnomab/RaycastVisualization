#if UNITY_EDITOR
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Nomnom.RaycastVisualization.Shapes {
    public struct Stadium {
        public static Stadium Default => new Stadium {
            radius = 0.025f
        };

        public Vector3 from;
        public Vector3 to;
        public float radius;
        public float angle;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Draw(Color color) {
            Vector3 center = (this.from + this.to) * 0.5f;
            Vector3 from = Quaternion.Euler(0, 0, angle) * (this.from - center) + center;
            Vector3 to = Quaternion.Euler(0, 0, angle) * (this.to - center) + center;

            Circle circle1 = new Circle {
                origin = from,
                radius = radius
            };

            Circle circle2 = new Circle {
                origin = to,
                radius = radius
            };

            Vector3 dir = (to - from).normalized;
            dir = Vector3.Cross(dir, Vector3.forward);

            float drawTime = VisualPhysics.NextDrawTime;
            Debug.DrawLine(from + dir * radius, to + dir * radius, color, drawTime, true);
            Debug.DrawLine(from - dir * radius, to - dir * radius, color, drawTime, true);

            circle1.Draw(color);
            circle2.Draw(color);
        }
    }
}
#endif