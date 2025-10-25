using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(GPButtonBed))]
    internal static class BedPatches
    {
        [HarmonyPatch("Awake")]
        [HarmonyPostfix]
        public static void Postfix(GPButtonBed __instance) 
        {
            if (!Plugin.bedCamAdjust.Value) return;
            __instance.transform.GetChild(0).Translate(new Vector3(0f, 0.3f, 0f), Space.Self);
        }
    }
    [HarmonyPatch(typeof(PrefabsDirectory), "PopulateShipItems")]
    internal static class BedPatche
    {
        public static void Postfix(PrefabsDirectory __instance)
        {
            if (!Plugin.bedCamAdjust.Value) return;
            for (int i = 60; i < 64; i++)
            {
                var sleepPos = __instance.shipItems[i].transform.GetChild(0);
                Transform child = null;
                if (sleepPos.childCount > 0)
                {
                    child = sleepPos.GetChild(0);
                    child.parent = sleepPos.parent;
                }
                
                sleepPos.Translate(new Vector3(0f, 0.2f, 0.0f), Space.Self);

                if (child != null) child.parent = sleepPos;
            }

        }
    }
    [HarmonyPatch(typeof(ShipItemBed), "OnAltActivate")]
    internal static class BedPathes2
    {
        public static bool Prefix(ShipItemBed __instance)
        {
            if (!Plugin.flyingCarpetFix.Value) return true;
            if (__instance.sold && __instance.held)
            {
                return false;
            }
            return true;
        }
    }
}
