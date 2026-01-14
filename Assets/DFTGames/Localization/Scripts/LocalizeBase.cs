// SPDX-License-Identifier: Apache-2.0
// Copyright (c) 2012-present Giuseppe "Pino" De Francesco (DFT Games Studios)
//
// Project: Unity Localization System
// File: LocalizeBase.cs
// Summary: Base class for managing the localization of components
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Attribution: Please retain this header and the NOTICE file when redistributing.
// Citation: If you use this work in a publication, please cite it (see CITATION.cff).

using UnityEngine;

/// <summary>
/// Base class for all localization components. Provides common functionality for managing
/// UI element localization, including initialization, language change handling, and lifecycle management.
/// 
/// Localized text files must follow this structure:
///     Resources/localization/English.txt 
///     Resources/localization/Italian.txt 
///     Resources/localization/Japanese.txt
///
/// File names must match Unity's SystemLanguage enumeration values.
///
/// File format:
///     key=value
///
/// Tab character (\t) is also accepted as a key-value separator. 
/// Use \n notation for newlines within values.
/// </summary>

namespace DFTGames.Localization
{
    public abstract class LocalizeBase : MonoBehaviour
    {
        #region Public Fields

        /// <summary>
        /// The localization key used to look up the translated text or resource name.
        /// This key must exist in the language files for text localization, or match
        /// a sprite filename for image localization.
        /// </summary>
        public string localizationKey;

        #endregion Public Fields

        #region Private Fields

        /// <summary>
        /// Flag to track whether the component has completed its Start() initialization.
        /// Used to prevent race conditions when OnEnable() is called before Start().
        /// </summary>
        private bool isInitialized = false;

        #endregion Private Fields

        #region Public Properties

        #endregion Public Properties

        #region Private Properties


        #endregion Private Properties

        #region Public Methods

        /// <summary>
        /// Updates the localized content of the UI element this component is attached to.
        /// Must be implemented by derived classes to handle specific component types
        /// (Text, TextMeshPro, Image, etc.).
        /// </summary>
        public abstract void UpdateLocale();

        /// <summary>
        /// Called when the component is first initialized. Handles the one-time setup
        /// of the localization system if this is the first localization component to start.
        /// Subscribes to language change events and performs the initial localization update.
        /// </summary>
        protected virtual void Start()
        {
            // The first localization component to start initializes the language system
            // by loading the player's preferred language (from PlayerPrefs or system language)
            if (!Locale.currentLanguageHasBeenSet)
            {
                Locale.currentLanguageHasBeenSet = true;
                SetCurrentLanguage(Locale.PlayerLanguage);
            }
            
            // Mark as initialized to allow OnEnable to safely update content
            isInitialized = true;
            
            // Perform initial localization
            UpdateLocale();
            
            // Subscribe to language change events for dynamic updates
            Locale.OnLanguageChanged += UpdateLocale;
        }

        /// <summary>
        /// Called when the component is destroyed. Unsubscribes from language change events
        /// to prevent memory leaks and null reference exceptions.
        /// </summary>
        private void OnDestroy()
        {
            Locale.OnLanguageChanged -= UpdateLocale;
        }

        /// <summary>
        /// Called when the GameObject is enabled. Updates the localized content if the
        /// component has been initialized. This ensures content is updated when objects
        /// are re-enabled after being disabled.
        /// </summary>
        private void OnEnable()
        {
            // Only update if Start has been called to avoid race condition
            // (OnEnable can be called before Start in Unity's lifecycle)
            if (isInitialized)
            {
                UpdateLocale();
            }
        }

        /// <summary>
        /// Retrieves the localized string for a given key from the currently loaded language.
        /// </summary>
        /// <param name="key">The localization key to look up</param>
        /// <returns>The localized string if the key exists, or an empty string if not found</returns>
        public static string GetLocalizedString(string key)
        {
            if (Locale.CurrentLanguageStrings.ContainsKey(key))
                return Locale.CurrentLanguageStrings[key];
            else
                return string.Empty;
        }

        /// <summary>
        /// Sets the current language programmatically and updates all localization components in the scene.
        /// The language choice is persisted to PlayerPrefs and will be restored in future sessions.
        /// All localization components automatically update via the OnLanguageChanged event.
        /// </summary>
        /// <param name="language">The SystemLanguage to switch to</param>
        public static void SetCurrentLanguage(SystemLanguage language)
        {
            // Update the language in the Locale manager (loads the new language file)
            Locale.CurrentLanguage = language.ToString();
            
            // Persist the choice to PlayerPrefs for future sessions
            Locale.PlayerLanguage = language;
        }



        #endregion Public Methods

    }
}