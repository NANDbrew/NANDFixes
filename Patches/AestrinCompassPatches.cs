using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(ShipItemCompass))]
    internal static class AestrinCompassPatches
    {
        [HarmonyPatch("Awake")]
        [HarmonyPostfix]
        public static void AwakePatch(ShipItemCompass __instance)
        {
            if (__instance.GetPrefabIndex() == 82)
            {
                __instance.inventoryRotation = 180f;
                __instance.inventoryRotationX = 270f;
            }
        }
    }
}

