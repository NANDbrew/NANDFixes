using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SailwindModdingHelper;

namespace NANDFixes.Patches
{
    internal class BottleFillPatches
    {
        [HarmonyPatch(typeof(ShipItemBottle))]
        private static class BottlePatch
        {
            [HarmonyPatch("OnItemClick")]
            [HarmonyPrefix]
            public static bool ItemClickPatch(ShipItemBottle __instance, PickupableItem heldItem)
            {
                if (__instance.health <= 0) { return false; }
                if (heldItem is ShipItemBottle bottle) 
                {
                    if (bottle.health >= (float)Traverse.Create(bottle).Field("capacity").GetValue()) return false;
                }
                return true;
            }
        }
    }
}
