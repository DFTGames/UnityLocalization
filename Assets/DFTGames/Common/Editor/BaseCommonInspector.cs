/*
 * DFT Games Studios
 * All rights reserved 2009-Present
 */
using UnityEditor;
using UnityEngine;

namespace DFTGames
{
    [CustomEditor(typeof(MonoBehaviour), false)]
    public class BaseCommonInspector : Editor
    {
        internal string headerText = "Header Text";
        internal string logoPath = "Assets/DFTGames/Common/Editor/DFTGLogo.png";
        private Texture2D logo;


        public override void OnInspectorGUI()
        {
            if (logo == null)
            {
                // Load the logo from the Assets/Editor folder
                logo = AssetDatabase.LoadAssetAtPath<Texture2D>(logoPath);
            }
            DrawCustomHeader();
            DrawDefaultInspector();
        }

        private void DrawCustomHeader()
        {
            // Add a bar with the header text
            GUIStyle style = new GUIStyle(GUI.skin.box)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 14,
                fontStyle = FontStyle.Bold,
                normal = { textColor = Color.yellow }
            };

            // Set the height for the header
            GUILayout.Space(10);
            Rect rect = GUILayoutUtility.GetRect(0, 58, GUILayout.ExpandWidth(true));

            // Draw the background
            EditorGUI.DrawRect(rect, new Color(.10f, .12f, .12f, 1f));

            // Draw the logo
            if (logo != null)
            {
                Rect logoRect = new Rect(rect.x + 10, rect.y + 10, 60, 37); // Adjust size and position
                GUI.DrawTexture(logoRect, logo, ScaleMode.ScaleToFit);
            }

            // Draw the title
            Rect textRect = new Rect(rect.x, rect.y, rect.width, rect.height);
            EditorGUI.LabelField(textRect, headerText, style);
            GUILayout.Space(10);

            // Draw the default inspector below
        }
    }
}
