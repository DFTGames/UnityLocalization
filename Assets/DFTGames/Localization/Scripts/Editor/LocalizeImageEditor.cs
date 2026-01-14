// SPDX-License-Identifier: Apache-2.0
// Copyright (c) 2012-present Giuseppe “Pino” De Francesco (DFT Games Studios)
//
// Project: Unity Localization System
// File: LocalizeImageEditor.cs
// Summary: Custom inspector for LocalizeImage component
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

namespace DFTGames.Localization
{
    /// <summary>
    /// Custom Unity Editor inspector for the LocalizeImage component.
    /// Provides a branded header and enhanced editor interface for managing
    /// image/sprite localization settings in the Unity Inspector.
    /// 
    /// This editor extends BaseCommonInspector to provide consistent branding
    /// across all DFT Games localization components.
    /// </summary>
    [CustomEditor(typeof(LocalizeImage), true)]
    public class LocalizeImageEditor : BaseCommonInspector
    {
        /// <summary>
        /// Initializes the custom inspector by setting the header text that will be
        /// displayed at the top of the inspector window.
        /// </summary>
        private void OnEnable()
        {
            headerText = "Localize Image";
        }
    }
}
