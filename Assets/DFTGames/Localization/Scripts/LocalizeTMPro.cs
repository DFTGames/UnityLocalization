// SPDX-License-Identifier: Apache-2.0
// Copyright (c) 2012-present Giuseppe “Pino” De Francesco (DFT Games Studios)
//
// Project: Unity Localization System
// File: LocalizeTMPro.cs
// Summary: TMPro localization component
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Attribution: Please retain this header and the NOTICE file when redistributing.
// Citation: If you use this work in a publication, please cite it (see CITATION.cff).

using UnityEngine;
using TMPro;

/// <summary>
/// Localization component for TextMeshPro UGUI components. Automatically updates the text
/// content based on the current language and the specified localization key.
/// 
/// This component is specifically designed for TextMeshProUGUI and uses the TextMeshPro
/// SetText() method for optimal performance and proper text rendering.
/// 
/// Usage:
/// 1. Attach this component to a GameObject with a TextMeshProUGUI component
/// 2. Set the localizationKey field to match a key in your language files
/// 3. The text will automatically update when the language changes
/// 
/// Language files structure:
///     Resources/localization/English.txt 
///     Resources/localization/Italian.txt 
///     Resources/localization/Japanese.txt
///
/// File format example:
///     welcome=Welcome to the game
///     exit=Exit
///
/// Tab character (\t) can also be used as a separator instead of '='.
/// Use \n in the value to create multi-line text.
/// 
/// Note: For optimal TextMeshPro font atlas generation, use the mergeAllText.ps1 utility
/// to create a combined character set from all your language files.
/// </summary>

namespace DFTGames.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizeTMPro : LocalizeBase
    {

        #region Private Fields

        /// <summary>
        /// Cached reference to the TextMeshProUGUI component this localization component manages.
        /// </summary>
        private TextMeshProUGUI text;

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the TextMeshProUGUI component with the localized string corresponding to the localizationKey.
        /// Uses TextMeshPro's SetText() method for optimal performance and proper text mesh updates.
        /// 
        /// Differences from standard Text component:
        /// - Uses SetText() instead of the text property for better performance
        /// - Properly handles TextMeshPro's internal mesh generation
        /// 
        /// Special handling:
        /// - Converts literal "\n" sequences in the localized string to actual newline characters
        /// - Silently returns if the TextMeshProUGUI component is not yet available (handles race conditions)
        /// - Only updates if the localizationKey is not empty and exists in the current language
        /// </summary>
        public override void UpdateLocale()
        {
            // Lazy initialization: get the TextMeshProUGUI component if we don't have it yet
            if (!text)
            {
                text = GetComponent<TextMeshProUGUI>();
                if (!text) return; // Safety check: catching race condition where component isn't ready
            }
            
            // Update the text using TextMeshPro's SetText method if we have a valid key
            if (!System.String.IsNullOrEmpty(localizationKey) && Locale.CurrentLanguageStrings.ContainsKey(localizationKey))
                text.SetText(Locale.CurrentLanguageStrings[localizationKey].Replace(@"\n", "" + '\n'));
        }

        #endregion Public Methods

        /// <summary>
        /// Initializes the component by caching the TextMeshProUGUI component reference and calling base initialization.
        /// The base.Start() call handles language initialization and event subscription.
        /// </summary>
        protected override void Start()
        {
            text = GetComponent<TextMeshProUGUI>();
            base.Start();
        }
    }
}