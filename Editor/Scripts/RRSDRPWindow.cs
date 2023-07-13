#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ERP
{
    public class ERPWindow : EditorWindow
    {
        private static ERPWindow _window;

        [MenuItem("Window/RRS Rich Presence")]
        private static void Init()
        {
            _window = (ERPWindow)GetWindow(typeof(ERPWindow), false, "RRS Rich Presence");
            _window.Show();
        }
        private void OnGUI()
        {
            if (ERP.discord == null && !ERP.Failed)
                ERP.DelayStart();

            if (ERP.Failed | ERP.Errored)
            {
                GUILayout.Label($"ERP Failed to start", EditorStyles.boldLabel);
                if (GUILayout.Button("Retry"))
                {
                    ERP.Errored = false;
                    ERP.Failed = false;
                    ERP.Init();
                }
                return;
            }
            GUILayout.Label("Rec Room Studio Discord Rich Presence", EditorStyles.boldLabel);

            GUILayout.Label(ERP.projectName);
            GUILayout.Label(ERP.sceneName);
            GUILayout.Label(string.Empty);
            GUILayout.Label($"Room Name Visible: {ERP.showSceneName}");
            GUILayout.Label($"Project Name Visible: {ERP.showProjectName}");
            GUILayout.Label($"Reset Time On Scene Change: {ERP.resetOnSceneChange}");

            if (ToggleButton("Hide Room", "Show Room", ref ERP.showSceneName))
            {
                ERP.UpdateActivity();
                ERPSettings.SaveSettings();
            }
            if (ToggleButton("Hide Project", "Show Project", ref ERP.showProjectName))
            {
                ERP.UpdateActivity();
                ERPSettings.SaveSettings();
            }
            if (ToggleButton("Don't Reset Time On Scene Change", "Reset Time On Scene Change", ref ERP.resetOnSceneChange))
            {
                ERP.UpdateActivity();
                ERPSettings.SaveSettings();
            }
            
        }

        private bool ToggleButton(string trueText, string falseText, ref bool value)
        {
            if (value && GUILayout.Button(trueText))
            {
                value = false;
                return true;
            }
            else if (!value && GUILayout.Button(falseText))
            {
                value = true;
                return true;
            }
            return false;
        }
    }
}
#endif