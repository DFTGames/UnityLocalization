using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DFTGames.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizeTM : LocalizeBase
    {

        #region Private Fields

        private TextMeshProUGUI text;

        #endregion

        #region Public Methods

        /// <summary>
        /// Update the value of the Text we are attached to.
        /// </summary>
        public override void UpdateLocale()
        {
            if (!text) return; // catching race condition
            if (!System.String.IsNullOrEmpty(localizationKey) && Locale.CurrentLanguageStrings.ContainsKey(localizationKey))
                text.SetText(Locale.CurrentLanguageStrings[localizationKey].Replace(@"\n", "" + '\n'));
        }

        #endregion Public Methods


        protected override void Start()
        {
            text = GetComponent<TextMeshProUGUI>();
            base.Start();
        }
    }
}