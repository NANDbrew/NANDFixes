/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(ReefEffectAnimUniversal), "Unfurl")]
    internal static class SailClothPatch
    {
        public static void Postfix(ReefEffectAnimUniversal __instance)
        {
            if (!Plugin.clothFix.Value) return;
            if (__instance.CompareTag("Boat")) return;
            __instance.RefreshCloth();
            __instance.tag = "Boat";
        }
    }
}
*/