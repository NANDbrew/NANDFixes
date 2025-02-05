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
            if (__instance.GetPrefabIndex() >= 311 && __instance.GetPrefabIndex() <= 319)
            {
                ___heldRotationOffset = -45f;
                __instance.inventoryRotation = 180f;
                __instance.inventoryRotationX = 270f;
            }
        }
    }
}
