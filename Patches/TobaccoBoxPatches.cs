using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(ShipItemCrate), "OnPickup")]
    internal class TobaccoBoxPatches
    {
        [HarmonyPostfix]
        public static void Postfix(ShipItemCrate __instance, ref float ___heldRotationOffset)
        {
            if (!Plugin.invRotationFix.Value) return;
            if (__instance.GetPrefabIndex() >= 311 && __instance.GetPrefabIndex() <= 319)
            {
                ___heldRotationOffset = -45f;
                __instance.inventoryRotation = 180f;
                __instance.inventoryRotationX = 270f;
            }
        }
    }
    [HarmonyPatch(typeof(ShipItem), "OnLoad")]
    internal class CoffeeBoxPatches
    {
        [HarmonyPostfix]
        public static void Postfix(ShipItem __instance, ref float ___heldRotationOffset)
        {
            if (!Plugin.invRotationFix.Value) return;
            if (__instance is ShipItemTea)
            {
                if ((__instance.GetPrefabIndex() >= 387 && __instance.GetPrefabIndex() <= 389) || __instance.GetPrefabIndex() == 373)
                {
                    ___heldRotationOffset = -45f;
                    __instance.inventoryRotation = 180f;
                    __instance.inventoryRotationX = 270f;
                }
            }
        }
    }
}
