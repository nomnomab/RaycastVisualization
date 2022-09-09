using System;
using UnityEngine;

namespace Nomnom.RaycastVisualization.Shapes {
  internal static class Utils {
#if UNITY_EDITOR
    public static float[] Sin;
    public static float[] Cos;
    
    [UnityEditor.InitializeOnLoadMethod]
    private static void OnLoad() {
      uint iterations = VisualPhysicsSettingsHandler.GetEditorSettings().CircleResolution;
      float radiansDelta = 2 * Mathf.PI / iterations;
			
      Sin ??= new float[iterations + 1];
      Cos ??= new float[iterations + 1];

      if (Sin.Length != iterations + 1) {
        Array.Resize(ref Sin, (int)iterations + 1);
      }
			
      if (Cos.Length != iterations + 1) {
        Array.Resize(ref Cos, (int)iterations + 1);
      }

      for (int i = 0; i <= iterations; i++) {
        float d = radiansDelta * i;
        Sin[i] = Mathf.Sin(d);
        Cos[i] = Mathf.Cos(d);
      }
    }
#endif
    
    public static Color GetColor(bool value) {
#if UNITY_EDITOR
      VisualPhysicsSettingsHandler.NewCustomSettings settings = VisualPhysicsSettingsHandler.GetEditorSettings();

      return value ? settings.HitColor : settings.NoHitColor;
#endif
      return Color.white;
    }

    public static Color GetDefaultColor() {
#if UNITY_EDITOR
      VisualPhysicsSettingsHandler.NewCustomSettings settings = VisualPhysicsSettingsHandler.GetEditorSettings();

      return settings.DefaultColor;
#endif
      return Color.white;
    }
    
    public static float GetMaxRayLength(float distance) => Mathf.Min(distance, 10000);
  }
}