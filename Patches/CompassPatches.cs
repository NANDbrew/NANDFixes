using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(ShipItemCompass))]
    internal static class CompassPatches
    {
        [HarmonyPatch("OnLoad")]
        [HarmonyPostfix]
        public static void LoadPatch(ShipItemCompass __instance)
        {
            if (!Plugin.invRotationFix.Value) return;
            if (__instance.GetPrefabIndex() == 82 || __instance.GetPrefabIndex() == 80)
            {
                __instance.inventoryRotation = 180f;
                __instance.inventoryRotationX = 270f;
            }
        }
    }
    [HarmonyPatch(typeof(ShipItem))]
    internal static class HookPatches
    {
        [HarmonyPatch("OnLoad")]
        [HarmonyPostfix]
        public static void LoadPatch(ShipItem __instance)
        {
            if (!Plugin.invRotationFix.Value) return;
            if (__instance is ShipItemFishingHook)
            {
                __instance.inventoryRotation = 90f;
                __instance.inventoryRotationX = 45f;
            }
        }
    }
}
