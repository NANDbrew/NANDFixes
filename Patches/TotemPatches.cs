using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(ShipItemTotem))]
    internal static class TotemPatches
    {
        [HarmonyPatch("OnLoad")]
        [HarmonyPostfix]
        public static void LoadPatch(ShipItemTotem __instance)
        {
            if (!Plugin.invRotationFix.Value) return;
            __instance.inventoryRotation = 180f;
        }
    }
}
