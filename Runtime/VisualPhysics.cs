using UnityEngine;

namespace Nomnom.RaycastVisualization {
	public static partial class VisualPhysics {
        internal static float NextDrawTime;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnLoad() {
            NextDrawTime = 0f;
        }
    }
}