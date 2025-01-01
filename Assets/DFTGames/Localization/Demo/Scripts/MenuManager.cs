/*
 * DFT Games Studios
 * All rights reserved 2009-Present
 */
using UnityEngine;
using DFTGames.Localization;

namespace DFTGames.Localization
{
    public class MenuManager : MonoBehaviour
    {
        #region Public Methods

        public void SetEnglish()
        {
            Localize.SetCurrentLanguage(SystemLanguage.English);
            LocalizeImage.SetCurrentLanguage(SystemLanguage.English);
        }

        public void SetItalian()
        {
            Localize.SetCurrentLanguage(SystemLanguage.Italian);
            LocalizeImage.SetCurrentLanguage(SystemLanguage.Italian);
        }

        public void SetJapanese()
        {
            Localize.SetCurrentLanguage(SystemLanguage.Japanese);
            LocalizeImage.SetCurrentLanguage(SystemLanguage.Japanese);
        }

        #endregion Public Methods
    }
}