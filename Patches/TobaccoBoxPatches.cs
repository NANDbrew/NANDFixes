using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NANDFixes.Patches
{
    internal class TobaccoBoxPatches
    {
        [HarmonyPatch(typeof(ShipItemCrate))]
        private static class BarrelDrinkPatches
        {
            [HarmonyPatch("Awake")]
            [HarmonyPostfix]
            public static void ExtraLateUpdatePatch(ShipItemCrate __instance, ref float __heldRotationOffset)
            {
                if (__instance.GetPrefabIndex() >= 311 && __instance.GetPrefabIndex() <= 319)
                {
                    __heldRotationOffset = 45f;
                    __instance.inventoryRotation = 180f;
                    __instance.inventoryRotationX = 270f;
                }
            }
        }
    }
}
