// SPDX-License-Identifier: Apache-2.0
// Copyright (c) 2012-present Giuseppe "Pino" De Francesco (DFT Games Studios)
//
// Project: Unity Localization System
// File: BaseCommonInspector.cs
// Summary: Base Custom inspector class
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Attribution: Please retain this header and the NOTICE file when redistributing.
// Citation: If you use this work in a publication, please cite it (see CITATION.cff).

using UnityEditor;
using UnityEngine;

namespace DFTGames
{
    /// <summary>
    /// Base class for custom Unity Editor inspectors used across DFT Games components.
    /// Provides a consistent branded header with logo and title for all derived inspectors.
    /// 
    /// Usage:
    /// 1. Derive your custom inspector from this class instead of Editor
    /// 2. In OnEnable(), set the headerText field to customize the title
    /// 3. Optionally override logoPath to use a different logo
    /// 
    /// The inspector automatically displays:
    /// - A styled header bar with dark background
    /// - DFT Games logo on the left
    /// - Custom header text centered
    /// - All default inspector properties below
    /// </summary>
    [CustomEditor(typeof(MonoBehaviour), false)]
    public class BaseCommonInspector : Editor
    {
        /// <summary>
        /// The text to display in the inspector header. Set this in derived classes' OnEnable() method.
        /// Default value: "Header Text"
        /// </summary>
        internal string headerText = "Header Text";
        
        /// <summary>
        /// Asset path to the logo image file. Can be overridden in derived classes to use a custom logo.
        /// Default path: "Assets/DFTGames/Common/Editor/DFTGLogo.png"
        /// </summary>
        internal string logoPath = "Assets/DFTGames/Common/Editor/DFTGLogo.png";
        
        /// <summary>
        /// Cached reference to the loaded logo texture. Loaded once during the first OnInspectorGUI() call.
        /// </summary>
        private Texture2D logo;


        /// <summary>
        /// Renders the custom inspector GUI. First draws the branded header, then the default inspector.
        /// </summary>
        public override void OnInspectorGUI()
        {
            // Lazy load the logo texture if not already loaded
            if (logo == null)
            {
                // Load the logo from the Assets/Editor folder
                logo = AssetDatabase.LoadAssetAtPath<Texture2D>(logoPath);
            }
            
            // Draw the custom header with logo and title
            DrawCustomHeader();
            
            // Draw all the default inspector properties
            DrawDefaultInspector();
        }

        /// <summary>
        /// Draws the custom header bar with logo and title text.
        /// The header features:
        /// - Dark gray background (RGB: 0.10, 0.12, 0.12)
        /// - DFT Games logo positioned on the left (60x37 pixels)
        /// - Centered title text in yellow, bold, 14pt font
        /// - 58 pixels total height with spacing
        /// </summary>
        private void DrawCustomHeader()
        {
            // Configure the text style for the header title
            GUIStyle style = new GUIStyle(GUI.skin.box)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 14,
                fontStyle = FontStyle.Bold,
                normal = { textColor = Color.yellow }
            };

            // Add spacing above the header
            GUILayout.Space(10);
            
            // Reserve space for the header (58 pixels height, full width)
            Rect rect = GUILayoutUtility.GetRect(0, 58, GUILayout.ExpandWidth(true));

            // Draw the dark background
            EditorGUI.DrawRect(rect, new Color(.10f, .12f, .12f, 1f));

            // Draw the logo on the left side if available
            if (logo != null)
            {
                Rect logoRect = new Rect(rect.x + 10, rect.y + 10, 60, 37); // Position and size
                GUI.DrawTexture(logoRect, logo, ScaleMode.ScaleToFit);
            }

            // Draw the centered title text
            Rect textRect = new Rect(rect.x, rect.y, rect.width, rect.height);
            EditorGUI.LabelField(textRect, headerText, style);
            
            // Add spacing below the header
            GUILayout.Space(10);
        }
    }
}
