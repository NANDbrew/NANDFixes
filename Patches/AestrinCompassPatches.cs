using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(ShipItemCompass), "OnLoad")]
    internal static class AestrinCompassPatches
    {
        public static void Postfix(ShipItemCompass __instance)
        {
            if (__instance.GetPrefabIndex() == 82)
            {
                __instance.inventoryRotation = 180f;
                __instance.inventoryRotationX = 270f;
            }
        }
    }
}

