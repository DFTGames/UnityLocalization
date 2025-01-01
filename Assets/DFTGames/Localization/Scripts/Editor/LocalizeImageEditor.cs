/*
 * DFT Games Studios
 * All rights reserved 2009-Present
 */
using UnityEditor;
using UnityEngine;

namespace DFTGames.Localization
{
    [CustomEditor(typeof(LocalizeImageEditor), true)]
    public class LocalizeImageEditor : BaseCommonInspector
    {
        private void OnEnable()
        {
            headerText = "Localize Image";
        }
    }
}
