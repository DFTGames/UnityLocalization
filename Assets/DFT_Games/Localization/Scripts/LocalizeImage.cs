using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class managing UI art localization. Language specific art shall be saved following this folder structure:
///
///     Resources/localization/UI/English 
///     Resources/localization/UI/Italian 
///     Resources/localization/UI/Japanese
///
/// ... and so on, where the folder name is the string version of the SystemLanguage enumeration.
/// </summary>

namespace DFTGames.Localization
{
    [RequireComponent(typeof(Image))]
    public class LocalizeImage : LocalizeBase
    {

        #region Private Fields

        private const string STR_LOCALIZATION_PREFIX = "localization/UI/";

        private Image image;

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        /// Update the Sprite of the Image we are attached to. It has a 100ms delay to allow Start operations.
        /// </summary>
        public override void UpdateLocale()
        {
            if (!image) return; // catching race condition
            Sprite tmp = Resources.Load(STR_LOCALIZATION_PREFIX + Locale.PlayerLanguage.ToString() + "/" + localizationKey, typeof(Sprite)) as Sprite;
            if (tmp != null)
            {
                image.sprite = tmp;
            }
        }

        #endregion Public Methods

        #region Private Methods


        protected override void Start()
        {
            image = GetComponent<Image>();
            base.Start();
        }

        #endregion Private Methods
    }
}