#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Nomnom.RaycastVisualization {
	public class VisualPhysicsSettingsHandler {
		private const string _hitColorKey = "customSettings.HitColor";
		private const string _noHitColorKey = "customSettings.NoHitColor";
		private const string _defaultColorKey = "customSettings.DefaultColor";
		private const string _circleResolutionKey = "customSettings.CircleResolution";
		private const string _circleRadiusKey = "customSettings.CircleRadius";
		private const string _impactCircleNormalArrowLengthKey = "customSettings.ImpactCircleNormalArrowLength";
		private const string _regularArrowLengthKey = "customSettings.RegularArrowLength";
		private const string _circleDistanceKey = "customSettings.CircleDistance";

		private static NewCustomSettings _cachedSettings;

		public class NewCustomSettings {
			public Color32 HitColor = Color.green;
			public Color32 NoHitColor = Color.red;
			public Color32 DefaultColor = Color.white;
			public uint CircleResolution = 24;
			public float ImpactCircleNormalArrowLength = 0.0075f;
			public float RegularArrowLength = 0.1f;
			public float CircleRadius = 0.025f;
			public float CircleDistance = 0.0001f;
		}

		public static NewCustomSettings GetEditorSettings() {
			return _cachedSettings ??= new NewCustomSettings {
				HitColor = HexToColor(EditorPrefs.GetString(_hitColorKey, ColorToHex(Color.green))),
				NoHitColor = HexToColor(EditorPrefs.GetString(_noHitColorKey, ColorToHex(Color.red))),
				DefaultColor = HexToColor(EditorPrefs.GetString(_defaultColorKey, ColorToHex(Color.white))),
				CircleResolution = (uint)EditorPrefs.GetInt(_circleResolutionKey, 24),
				CircleRadius = EditorPrefs.GetFloat(_circleRadiusKey, 0.025f),
				CircleDistance = EditorPrefs.GetFloat(_circleDistanceKey, 0.0001f),
				ImpactCircleNormalArrowLength = EditorPrefs.GetFloat(_impactCircleNormalArrowLengthKey, 0.0075f),
				RegularArrowLength = EditorPrefs.GetFloat(_regularArrowLengthKey, 0.1f),
			};
		}

		public static void SetEditorSettings(NewCustomSettings settings) {
			EditorPrefs.SetString(_hitColorKey, ColorToHex(settings.HitColor));
			EditorPrefs.SetString(_noHitColorKey, ColorToHex(settings.NoHitColor));
			EditorPrefs.SetString(_defaultColorKey, ColorToHex(settings.DefaultColor));
			EditorPrefs.SetInt(_circleResolutionKey, (int)settings.CircleResolution);
			EditorPrefs.SetFloat(_circleRadiusKey, settings.CircleRadius);
			EditorPrefs.SetFloat(_circleDistanceKey, settings.CircleDistance);
			EditorPrefs.SetFloat(_impactCircleNormalArrowLengthKey, settings.ImpactCircleNormalArrowLength);
			EditorPrefs.SetFloat(_regularArrowLengthKey, settings.RegularArrowLength);
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
		private static readonly GUIContent _circleResolutionLabel = new GUIContent("Circle Resolution", "How smooth the circles are in the visuals");
		private static readonly GUIContent _hitCircleRadiusLabel = new GUIContent("Impact Circle Radius", "How large the impact circle is");
		private static readonly GUIContent _hitCircleDistanceLabel = new GUIContent("Impact Circle Distance", "How far away from the surface the impact circle is");
		private static readonly GUIContent _hitCircleArrowLengthLabel = new GUIContent("Impact Arrow Length", "How long the arrow arms are");
		private static readonly GUIContent _normalArrowLengthLabel = new GUIContent("Regular Arrow Length", "How long the arrow arms are");

		public static void DrawSettings(VisualPhysicsSettingsHandler.NewCustomSettings settings) {
			EditorGUI.indentLevel++;
			
			EditorGUILayout.HelpBox("Upon changes, the scene needs to repaint or an object has to be moved to actually apply the changes.", MessageType.Info);
			settings.HitColor = EditorGUILayout.ColorField(_hitColorLabel, settings.HitColor);
			settings.NoHitColor = EditorGUILayout.ColorField(_noHitColorLabel, settings.NoHitColor);
			settings.DefaultColor = EditorGUILayout.ColorField(_defaultColorLabel, settings.DefaultColor);
			settings.CircleResolution = (uint)EditorGUILayout.IntSlider(_circleResolutionLabel, (int)settings.CircleResolution, 4, 128);
			settings.CircleRadius = EditorGUILayout.Slider(_hitCircleRadiusLabel, settings.CircleRadius, 0.001f, 0.5f);
			settings.CircleDistance = EditorGUILayout.Slider(_hitCircleDistanceLabel, settings.CircleDistance, 0, 0.5f);
			settings.ImpactCircleNormalArrowLength = EditorGUILayout.Slider(_hitCircleArrowLengthLabel, settings.ImpactCircleNormalArrowLength, 0.0001f, 0.05f);
			settings.RegularArrowLength = EditorGUILayout.Slider(_normalArrowLengthLabel, settings.RegularArrowLength, 0.0001f, 0.5f);

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
						VisualUtils.RecalculateComputations();
					}
				},
				keywords = new HashSet<string>(new [] { "Raycast", "Visualization" })
			};

			return provider;
		}
	}
}
#endif