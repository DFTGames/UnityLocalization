/*
 * DFT Games Studios
 * All rights reserved 2009-Present
 */
using UnityEditor;
using UnityEngine;

namespace DFTGames.Localization
{
    [CustomEditor(typeof(LocalizeTMProEditor), true)]
    public class LocalizeTMProEditor : BaseCommonInspector
    {
        private void OnEnable()
        {
            headerText = "Localize TextMeshPro";
        }
    }
}
