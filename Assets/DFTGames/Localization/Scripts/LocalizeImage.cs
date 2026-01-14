// SPDX-License-Identifier: Apache-2.0
// Copyright (c) 2012-present Giuseppe "Pino" De Francesco (DFT Games Studios)
//
// Project: Unity Localization System
// File: LocalizeImage.cs
// Summary: Class managing the localization of Image components
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file unless in compliance with the License.
// You may obtain a copy of the License at
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Attribution: Please retain this header and the NOTICE file when redistributing.
// Citation: If you use this work in a publication, please cite it (see CITATION.cff).

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Localization component for UI Image components. Automatically updates the displayed sprite
/// based on the current language and the specified sprite name.
/// 
/// Usage:
/// 1. Attach this component to a GameObject with an Image component
/// 2. Set the localizationKey field to the sprite filename (without extension)
/// 3. Place language-specific sprites in the appropriate folders
/// 4. The image will automatically update when the language changes
/// 
/// Folder structure for localized sprites:
///     Resources/localization/UI/English/
///     Resources/localization/UI/Italian/
///     Resources/localization/UI/Japanese/
///
/// Important: All language-specific versions of a sprite must have the same filename.
/// Example:
///     Resources/localization/UI/English/logo.png
///     Resources/localization/UI/Italian/logo.png
///     Resources/localization/UI/Japanese/logo.png
/// 
/// The localizationKey should be set to "logo" (without path or extension).
/// </summary>

namespace DFTGames.Localization
{
    [RequireComponent(typeof(Image))]
    public class LocalizeImage : LocalizeBase
    {

        #region Private Fields

        /// <summary>
        /// Resource path prefix for localized UI sprites. 
        /// Sprites should be placed at: Resources/localization/UI/[LanguageName]/
        /// </summary>
        private const string STR_LOCALIZATION_PREFIX = "localization/UI/";

        /// <summary>
        /// Cached reference to the Image component this localization component manages.
        /// </summary>
        private Image image;

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        /// Updates the Image component's sprite with the localized version for the current language.
        /// The method constructs the resource path using the current language and localizationKey,
        /// then loads the appropriate sprite from Resources.
        /// 
        /// Resource path format: Resources/localization/UI/[LanguageName]/[localizationKey]
        /// 
        /// Special handling:
        /// - Safely handles race conditions by checking for null references
        /// - Only updates the sprite if the resource is successfully loaded
        /// - Silently fails if the sprite is not found (no error, image unchanged)
        /// </summary>
        public override void UpdateLocale()
        {
            // Lazy initialization: get the Image component if we don't have it yet
            if (!image)
            {
                image = GetComponent<Image>();
                if (!image) return; // Safety check: catching race condition where component isn't ready
            }
            
            // Construct the full resource path: localization/UI/[Language]/[spriteName]
            Sprite tmp = Resources.Load(STR_LOCALIZATION_PREFIX + Locale.CurrentLanguage + "/" + localizationKey, typeof(Sprite)) as Sprite;
            
            // Only update if we successfully loaded a sprite
            if (tmp != null)
            {
                image.sprite = tmp;
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Initializes the component by caching the Image component reference and calling base initialization.
        /// The base.Start() call handles language initialization and event subscription.
        /// </summary>
        protected override void Start()
        {
            image = GetComponent<Image>();
            base.Start();
        }

        #endregion Private Methods
    }
}