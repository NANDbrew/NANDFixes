using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(ShipyardSailColChecker), "Awake")]
    internal class SailColPatch
    {
        public static void Postfix(Sail ___sail, ref Vector3 ___sailModelOffset)
        {
            if (colfixSails.Contains(___sail.prefabIndex))
            {
                ___sailModelOffset = new Vector3(0f, 0f, 1.5f);
            }
        }

        private static readonly int[] colfixSails = new int[] { 17, 18, 107 }; // junk gaffs
    }

    [HarmonyPatch(typeof(ReefEffectAnimUniversal), "Start")]
    internal static class SailBlinkFix
    {
        public static void Postfix(ReefEffectAnimUniversal __instance)
        {
            if (!Plugin.sailBlinkFix.Value) return;
            __instance.furledSail.material.shader = Shader.Find("Standard");
        }
    }

    [HarmonyPatch(typeof(ReefEffectAnimUniversal))]
    internal static class SailClothPatch
    {
        [HarmonyPatch("Unfurl")]
        [HarmonyPostfix]
        public static void Postfix(ReefEffectAnimUniversal __instance)
        {
            if (!Plugin.clothFix.Value) return;
            //if (__instance.CompareTag("Boat")) return;
            __instance.RefreshCloth();
            //__instance.tag = "Boat";
        }

        [HarmonyPatch("Furl")]
        [HarmonyPrefix]
        public static bool Prefix(ReefEffectAnimUniversal __instance, ref Material ___unfurledMaterial, Sail ___sail, ref bool ___isFurled)
        {
            if (!Plugin.clothFix.Value) return true;
            __instance.furledSail.enabled = true;
            if (__instance.debugToggleCloth)
            {
                ___unfurledMaterial = ((Component)(object)___sail.cloth).GetComponent<Renderer>().material;
                ((Component)(object)___sail.cloth).GetComponent<Renderer>().sharedMaterial = Refs.emptyMaterial;
            }

            ___isFurled = true;
            return false;
            /*if (!Plugin.clothFix.Value) return;
            if (__instance.CompareTag("Boat")) return;
            __instance.RefreshCloth();
            __instance.tag = "Boat";*/
        }
    }
}
