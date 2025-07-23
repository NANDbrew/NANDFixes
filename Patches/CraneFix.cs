using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using Crest;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(IslandStreetlightsManager), "Awake")]
    internal static class CraneFix
    {
        internal static void Postfix(IslandStreetlightsManager __instance)
        {
            if (!Plugin.craneFix.Value) return;
            if (__instance.gameObject.GetComponent<IslandSceneryScene>().parentIslandIndex == 1)
            {
                __instance.transform.Find("crane (1)").GetComponent<MeshCollider>().convex = false;
                __instance.transform.Find("crane (2)").GetComponent<MeshCollider>().convex = false;
                __instance.transform.Find("crane (3)").GetComponent<MeshCollider>().convex = false;
            }
        }

    }
}
