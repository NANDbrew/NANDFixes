using HarmonyLib;
using UnityEngine;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(ShipyardUI), "Awake")]
    internal static class MastButtonFix
    {
        internal static void Postfix(GameObject[] ___mastButtons)
        {
            //if (!Plugin.spinFix.Value) return;

            foreach (GameObject button in ___mastButtons) 
            {
                button.GetComponent<LineRenderer>().enabled = false;
            }

        }
    }
}
