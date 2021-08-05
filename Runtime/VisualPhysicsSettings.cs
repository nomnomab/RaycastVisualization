#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Nomnom.RaycastVisualization {
	public class VisualPhysicsSettingsHandler {
		private const string _hitColorKey = "customSettings.HitColor";
		private const string _noHitColorKey = "customSettings.NoHitColor";
		private const string _defaultColorKey = "customSettings.DefaultColor";

		private static NewCustomSettings _cachedSettings;

		public class NewCustomSettings {
			public Color32 HitColor = Color.green;
			public Color32 NoHitColor = Color.red;
			public Color32 DefaultColor = Color.white;
		}

		public static NewCustomSettings GetEditorSettings() {
			return _cachedSettings ??= new NewCustomSettings {
				HitColor = HexToColor(EditorPrefs.GetString(_hitColorKey, ColorToHex(Color.green))),
				NoHitColor = HexToColor(EditorPrefs.GetString(_noHitColorKey, ColorToHex(Color.red))),
				DefaultColor = HexToColor(EditorPrefs.GetString(_defaultColorKey, ColorToHex(Color.white))),
			};
		}

		public static void SetEditorSettings(NewCustomSettings settings) {
			EditorPrefs.SetString(_hitColorKey, ColorToHex(settings.HitColor));
			EditorPrefs.SetString(_noHitColorKey, ColorToHex(settings.NoHitColor));
			EditorPrefs.SetString(_defaultColorKey, ColorToHex(settings.DefaultColor));
		}

		private static string ColorToHex(Color32 color) {
			return $"{color.r:X2}{color.g:X2}{color.b:X2}{color.a:X2}";
		}

		private static Color32 HexToColor(string hex) {
			hex = hex
				.Replace("0x", string.Empty)
				.Replace("#", string.Empty);

			byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
			byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
			byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
			byte a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);

			return new Color32(r, g, b, a);
		}
	}

	internal class SettingsGUIContent {
		private static readonly GUIContent _hitColorLabel = new GUIContent("Hit Color", "The color used when a raycast hits a collider");
		private static readonly GUIContent _noHitColorLabel = new GUIContent("No Hit Color", "The color used when a raycast does not hit a collider");
		private static readonly GUIContent _defaultColorLabel = new GUIContent("Default Color", "The color used for anything that doesn't depend on a collision");

		public static void DrawSettings(VisualPhysicsSettingsHandler.NewCustomSettings settings) {
			EditorGUI.indentLevel++;
			
			EditorGUILayout.HelpBox("Upon color changes in the Editor, the scene needs to repaint or an object has to be moved to refresh the colors.", MessageType.Info);
			settings.HitColor = EditorGUILayout.ColorField(_hitColorLabel, settings.HitColor);
			settings.NoHitColor = EditorGUILayout.ColorField(_noHitColorLabel, settings.NoHitColor);
			settings.DefaultColor = EditorGUILayout.ColorField(_defaultColorLabel, settings.DefaultColor);

			EditorGUI.indentLevel--;
		}
	}

	internal static class NewCustomSettingsProvider {
		[SettingsProvider]
		public static SettingsProvider CreateSettingsProvider() {
			SettingsProvider provider = new SettingsProvider("Preferences/Raycast Visualization", SettingsScope.User) {
				label = "Raycast Visualization",
				guiHandler = ctx => {
					VisualPhysicsSettingsHandler.NewCustomSettings settings = VisualPhysicsSettingsHandler.GetEditorSettings();
					
					EditorGUI.BeginChangeCheck();
					SettingsGUIContent.DrawSettings(settings);

					if (EditorGUI.EndChangeCheck()) {
						VisualPhysicsSettingsHandler.SetEditorSettings(settings);
					}
				},
				keywords = new HashSet<string>(new [] { "Raycast", "Visualization" })
			};

			return provider;
		}
	}
}
#endif