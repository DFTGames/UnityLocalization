// SPDX-License-Identifier: Apache-2.0
// Copyright (c) 2012-present Giuseppe "Pino" De Francesco (DFT Games Studios)
//
// Project: Unity Localization System
// File: Localize.cs
// Summary: Class managing the localization of Text components
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Attribution: Please retain this header and the NOTICE file when redistributing.
// Citation: If you use this work in a publication, please cite it (see CITATION.cff).

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Localization component for Unity's UGUI Text components. Automatically updates the text
/// content based on the current language and the specified localization key.
/// 
/// Usage:
/// 1. Attach this component to a GameObject with a Text component
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
/// </summary>

namespace DFTGames.Localization
{
    [RequireComponent(typeof(Text))]
    public class Localize : LocalizeBase
    {

        #region Private Fields

        /// <summary>
        /// Cached reference to the Text component this localization component manages.
        /// </summary>
        private Text text;

        #endregion



        #region Public Methods

        /// <summary>
        /// Updates the Text component with the localized string corresponding to the localizationKey.
        /// The method safely handles race conditions by checking for null references before updating.
        /// 
        /// Special handling:
        /// - Converts literal "\n" sequences in the localized string to actual newline characters
        /// - Silently returns if the Text component is not yet available (handles race conditions)
        /// - Only updates if the localizationKey is not empty and exists in the current language
        /// </summary>
        public override void UpdateLocale()
        {
            // Lazy initialization: get the Text component if we don't have it yet
            if (!text)
            {
                text = GetComponent<Text>();
                if (!text) return; // Safety check: catching race condition where component isn't ready
            }
            
            // Update the text if we have a valid key and it exists in the current language dictionary
            if (!System.String.IsNullOrEmpty(localizationKey) && Locale.CurrentLanguageStrings.ContainsKey(localizationKey))
                text.text = Locale.CurrentLanguageStrings[localizationKey].Replace(@"\n", "" + '\n');
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Initializes the component by caching the Text component reference and calling base initialization.
        /// The base.Start() call handles language initialization and event subscription.
        /// </summary>
        protected override void Start()
        {
            text = GetComponent<Text>();
            base.Start();
        }

        #endregion Private Methods
    }
}