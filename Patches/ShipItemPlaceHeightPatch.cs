using HarmonyLib;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(PrefabsDirectory), "PopulateShipItems")]
    internal static class ShipItemPlaceHeightPatch
    {
        public static void Postfix(PrefabsDirectory __instance)
        {
            if (!Plugin.itemPlaceHeightFix.Value) return;
            __instance.shipItems[70].furniturePlaceHeight = 0.21f;

        }
    }

}
