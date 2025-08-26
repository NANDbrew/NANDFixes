using HarmonyLib;
using UnityEngine;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(ShipItemFoldable), "OnLoad")]
    internal static class MapPatch
    {
        public static void Prefix(ShipItemFoldable __instance, ref float __state)
        {
            if (!Plugin.mapFix.Value) return;
            __state = __instance.amount;

        }
        public static void Postfix(ShipItemFoldable __instance, float __state)
        {
            if (!Plugin.mapFix.Value) return;
            __instance.amount = __state;
        }
    }
}
