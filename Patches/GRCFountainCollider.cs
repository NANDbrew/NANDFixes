using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(IslandStreetlightsManager), "Awake")]
    internal static class GRCFountainCollider
    {
        public static void Postfix(IslandStreetlightsManager __instance)
        {
            if (__instance.gameObject.name != "island 1 A (gold rock) scenery") return;
            Transform clutterContainer = __instance.transform.Find("island 1 A clutter");
            if (clutterContainer != null)
            {
                clutterContainer.Find("pref_Fountain")?.gameObject.AddComponent<MeshCollider>();
                clutterContainer.Find("pref_Brazier_03_Lit (2)").gameObject.AddComponent<MeshCollider>().convex = true;
                clutterContainer.Find("pref_Brazier_03_Lit (3)").gameObject.AddComponent<MeshCollider>().convex = true;
            }
        }
    }
}
