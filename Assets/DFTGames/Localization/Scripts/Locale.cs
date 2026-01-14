// SPDX-License-Identifier: Apache-2.0
// Copyright (c) 2012-present Giuseppe “Pino” De Francesco (DFT Games Studios)
//
// Project: Unity Localization System
// File: Locale.cs
// Summary: Static class to manage localization settings
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Attribution: Please retain this header and the NOTICE file when redistributing.
// Citation: If you use this work in a publication, please cite it (see CITATION.cff).

using System.Collections.Generic;
using UnityEngine;

namespace DFTGames.Localization
{
    /// <summary>
    /// Central localization manager that handles language loading, switching, and persistence.
    /// This static class provides access to localized strings and manages the current language state.
    /// </summary>
    public static class Locale
    {
        /// <summary>
        /// PlayerPrefs key used to store the player's language preference.
        /// </summary>
        const string STR_LOCALIZATION_KEY = "locale";
        
        /// <summary>
        /// Resource path prefix for localization files. Language files should be placed at Resources/localization/
        /// </summary>
        const string STR_LOCALIZATION_PREFIX = "localization/";
        
        /// <summary>
        /// Internal storage for the current language name (e.g., "English", "Japanese").
        /// </summary>
        static string currentLanguage;
        
        /// <summary>
        /// Flag indicating whether a language has been explicitly set during this session.
        /// Used to prevent redundant initialization by multiple localization components.
        /// </summary>
        public static bool currentLanguageHasBeenSet = false;
        
        /// <summary>
        /// Dictionary containing all key-value pairs for the currently loaded language.
        /// Keys are localization identifiers, values are the translated strings.
        /// </summary>
        public static Dictionary<string, string> CurrentLanguageStrings = new Dictionary<string, string>();
        
        /// <summary>
        /// TextAsset reference to the currently loaded localization file.
        /// </summary>
        static TextAsset currentLocalizationText;

        /// <summary>
        /// Delegate signature for language change events.
        /// </summary>
        public delegate void OnLanguageChangedDelegate();
        
        /// <summary>
        /// Event fired whenever the language is changed. All localization components subscribe to this
        /// event to automatically update their displayed content when the language switches.
        /// </summary>
        public static event OnLanguageChangedDelegate OnLanguageChanged;



        /// <summary>
        /// Gets or sets the current language. Expects a SystemLanguage enum string (e.g., "English", "Japanese").
        /// When set, this property loads the corresponding language file from Resources/localization/[LanguageName].txt,
        /// parses all key-value pairs, and notifies all subscribers via the OnLanguageChanged event.
        /// 
        /// Fallback behavior:
        /// - If the requested language file is not found, falls back to English
        /// - If English is also not found, logs an error
        /// 
        /// File format: Each line contains a key-value pair separated by '=' or Tab character.
        /// Example:
        ///     welcome=Welcome to the game
        ///     exit=Exit
        /// </summary>
        public static string CurrentLanguage
        {
            get { return currentLanguage; }
            set
            {
                if (value != null && value.Trim() != string.Empty)
                {
                    currentLanguage = value;
                    
                    // Attempt to load the language file from Resources
                    currentLocalizationText = Resources.Load(STR_LOCALIZATION_PREFIX + currentLanguage, typeof(TextAsset)) as TextAsset;
                    
                    // Fallback to English if the requested language file is not found
                    if (currentLocalizationText == null)
                    {
                        Debug.LogWarningFormat("Missing locale '{0}', loading English.", currentLanguage);
                        currentLanguage = SystemLanguage.English.ToString();
                        currentLocalizationText = Resources.Load(STR_LOCALIZATION_PREFIX + currentLanguage, typeof(TextAsset)) as TextAsset;
                    }
                    
                    if (currentLocalizationText != null)
                    {
                        // Split the text file into lines, handling different newline formats (Windows, Unix, Mac)
                        string[] lines = currentLocalizationText.text.Split(new string[] { "\r\n", "\n\r", "\n" }, System.StringSplitOptions.RemoveEmptyEntries);
                        CurrentLanguageStrings.Clear();
                        
                        // Parse each line as a key-value pair
                        for (int i = 0; i < lines.Length; i++)
                        {
                            // Split on Tab or '=' character, limit to 2 parts to allow separators in the value
                            string[] pairs = lines[i].Split(new char[] { '\t', '=' }, 2);
                            if (pairs.Length == 2)
                            {
                                string key = pairs[0].Trim();
                                string localizedValue = pairs[1].Trim();
                                
                                // Add the key-value pair, checking for duplicates
                                if (!CurrentLanguageStrings.ContainsKey(key))
                                {
                                    CurrentLanguageStrings.Add(key, localizedValue);
                                }
                                else
                                {
                                    Debug.LogWarningFormat("Duplicate localization key '{0}' found in '{1}'. Using first occurrence.", key, currentLanguage);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.LogErrorFormat("Locale Language '{0}' not found!", currentLanguage);
                    }
                    
                    // Mark that the language has been initialized and notify all subscribers
                    currentLanguageHasBeenSet = true;
                    OnLanguageChanged?.Invoke();
                }
            }
        }


        /// <summary>
        /// Gets or sets the player's preferred language. 
        /// The getter retrieves the language from PlayerPrefs if available, otherwise returns the system language.
        /// The setter persists the language choice to PlayerPrefs for use across game sessions.
        /// 
        /// This property provides persistent storage of language preference, ensuring the player's
        /// choice is remembered between game sessions.
        /// </summary>
        public static SystemLanguage PlayerLanguage
        {
            get
            {
                return (SystemLanguage)PlayerPrefs.GetInt(STR_LOCALIZATION_KEY, (int)Application.systemLanguage);
            }
            set
            {
                PlayerPrefs.SetInt(STR_LOCALIZATION_KEY, (int)value);
                PlayerPrefs.Save();
            }
        }
    }
}