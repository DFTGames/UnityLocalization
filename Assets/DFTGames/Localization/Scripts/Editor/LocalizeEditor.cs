/*
 * DFT Games Studios
 * All rights reserved 2009-Present
 */
using UnityEditor;
using UnityEngine;

namespace DFTGames.Localization
{
    [CustomEditor(typeof(BaseCommonInspector), true)]
    public class LocalizeEditor : BaseCommonInspector
    {
        private void OnEnable()
        {
            headerText = "Localize Text";
        }
    }
}
