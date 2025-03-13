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
            Vector3 pos = __instance.transform.GetChild(0).localPosition;
            if (__instance.name == "hammock_001")
            {
                pos.z += 0.3f;
            }
            if (__instance.name == "bed")
            {
                pos.z += 0.2f;
            }
            __instance.transform.GetChild(0).localPosition = pos;
        }
    }
}
