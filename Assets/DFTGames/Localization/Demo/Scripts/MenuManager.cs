// SPDX-License-Identifier: Apache-2.0
// Copyright (c) 2012-present Giuseppe “Pino” De Francesco (DFT Games Studios)
//
// Project: Unity Localization System
// File: MenuManager.cs
// Summary: Example script to manage a simple localised menu
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Attribution: Please retain this header and the NOTICE file when redistributing.
// Citation: If you use this work in a publication, please cite it (see CITATION.cff).

using UnityEngine;
using DFTGames.Localization;

namespace DFTGames.Localization
{
    /// <summary>
    /// Demo/Example class demonstrating how to programmatically change the language at runtime.
    /// 
    /// This script provides simple methods that can be attached to UI buttons to allow players
    /// to switch between languages. When a language is changed:
    /// 1. All localization components in the scene automatically update
    /// 2. The language preference is saved to PlayerPrefs
    /// 3. The choice persists across game sessions
    /// 
    /// Usage Example:
    /// 1. Add this component to a GameObject in your scene
    /// 2. Create UI buttons for each language
    /// 3. Attach the appropriate SetXXX() method to each button's onClick event
    /// 4. When clicked, all localized UI elements will update immediately
    /// 
    /// To add support for additional languages:
    /// 1. Create a new language file (e.g., Resources/localization/Spanish.txt)
    /// 2. Add a new method following the same pattern:
    ///    public void SetSpanish() { LocalizeBase.SetCurrentLanguage(SystemLanguage.Spanish); }
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        #region Public Methods

        /// <summary>
        /// Switches the application language to English.
        /// Can be called from UI button events or other scripts.
        /// All localization components will automatically update to display English text/sprites.
        /// </summary>
        public void SetEnglish()
        {
            LocalizeBase.SetCurrentLanguage(SystemLanguage.English);
        }

        /// <summary>
        /// Switches the application language to Italian.
        /// Can be called from UI button events or other scripts.
        /// All localization components will automatically update to display Italian text/sprites.
        /// </summary>
        public void SetItalian()
        {
            LocalizeBase.SetCurrentLanguage(SystemLanguage.Italian);
        }

        /// <summary>
        /// Switches the application language to Japanese.
        /// Can be called from UI button events or other scripts.
        /// All localization components will automatically update to display Japanese text/sprites.
        /// </summary>
        public void SetJapanese()
        {
            LocalizeBase.SetCurrentLanguage(SystemLanguage.Japanese);
        }

        #endregion Public Methods
    }
}