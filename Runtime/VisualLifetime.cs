using System;

namespace Nomnom.RaycastVisualization {
    public readonly struct VisualLifetime: IDisposable {
        private readonly float _drawTime;
        
        private VisualLifetime(float drawTime, float newDrawTime) {
            _drawTime = drawTime;
            VisualPhysics.NextDrawTime = newDrawTime;
        }
        
        public static VisualLifetime Create(float seconds) {
            return new VisualLifetime(VisualPhysics.NextDrawTime, seconds);
        }

        public void Dispose() {
            VisualPhysics.NextDrawTime = _drawTime;
        }
    }
}