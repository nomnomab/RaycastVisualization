#if UNITY_EDITOR
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Nomnom.RaycastVisualization.Shapes {
  internal struct Arrow {
    public static Arrow Default => new Arrow {
      headLength = 0.1f,
      headAngle = 20f,
      position = 1
    };
    
    public Vector3 origin;
    public Vector3 direction;
    public float headLength;
    public float headAngle;
    public float position;
    public bool overrideLength;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Draw(Color color) {
      float headLength = this.headLength;
      
      if (!overrideLength) {
        headLength = VisualPhysicsSettingsHandler.GetEditorSettings().RegularArrowLength;
      }

      Quaternion rot = direction == Vector3.zero ? Quaternion.identity : Quaternion.LookRotation(direction);
      Vector3 backDir = Vector3.back * headLength;
      Vector3 right = rot * Quaternion.Euler(headAngle, 0, 0) * backDir;
      Vector3 left = rot * Quaternion.Euler(-headAngle, 0, 0) * backDir;
      Vector3 up = rot * Quaternion.Euler(0, headAngle, 0) * backDir;
      Vector3 down = rot * Quaternion.Euler(0, -headAngle, 0) * backDir;

      Vector3 arrowTip = origin + direction * position;

      Debug.DrawRay(origin, direction, color, 0, true);
      Debug.DrawRay(arrowTip, right, color, 0, true);
      Debug.DrawRay(arrowTip, left, color, 0, true);
      Debug.DrawRay(arrowTip, up, color, 0, true);
      Debug.DrawRay(arrowTip, down, color, 0, true);
    }
  }
}
#endif