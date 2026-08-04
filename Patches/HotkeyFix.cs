using HarmonyLib;
using UnityEngine;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(InventoryHotkeys), "Update")]
    internal static class HotkeyPatches
    {
        internal static bool Prefix()
        {
            //if (!Plugin.spinFix.Value) return true;
            if (GameState.wasInSettingsMenu)
            {
                return false;
            }
            return true;
        }
    }
}
