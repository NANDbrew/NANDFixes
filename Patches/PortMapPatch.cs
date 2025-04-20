using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(Port), "Start")]
    internal static class PortMapPatch
    {
        public static void Postfix(Port __instance, int ___portIndex)
        {
            if (__instance.oceanMapLocation != null || __instance.localMapLocation == null) return;
            if (___portIndex == 28)
            {
                __instance.oceanMapLocation = UnityEngine.Object.Instantiate(__instance.localMapLocation);
                __instance.oceanMapLocation.localPosition = new Vector3(0.0693f, 0.3961f, -0.0001f);
                __instance.oceanMapLocation.name = "Wm map Cave";
            }
            else if (___portIndex == 27)
            {
                __instance.oceanMapLocation = UnityEngine.Object.Instantiate(__instance.localMapLocation);
                __instance.oceanMapLocation.localPosition = new Vector3(0.0649f, 0.4096f, -0.0001f);
                __instance.oceanMapLocation.name = "Wm map Valley";
            }
            else if (___portIndex == 26)
            {
                __instance.oceanMapLocation = UnityEngine.Object.Instantiate(__instance.localMapLocation);
                __instance.oceanMapLocation.localPosition = new Vector3(0.0315f, 0.3797f, -0.0001f);
                __instance.oceanMapLocation.name = "Wm map Abbey";
            }
        }
    }
}
