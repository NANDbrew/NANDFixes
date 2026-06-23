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
    internal static class GRCPatches
    {
        public static void Postfix(IslandStreetlightsManager __instance)
        {
            if (__instance.gameObject.GetComponent<IslandSceneryScene>().parentIslandIndex != 1) return;
            try
            {
                Transform clutterContainer = __instance.transform.Find("island 1 A clutter");
                if (clutterContainer != null)
                {
                    clutterContainer.Find("pref_Fountain")?.gameObject.AddComponent<MeshCollider>();
                    clutterContainer.Find("pref_Brazier_03_Lit (2)").gameObject.AddComponent<MeshCollider>().convex = true;
                    clutterContainer.Find("pref_Brazier_03_Lit (3)").gameObject.AddComponent<MeshCollider>().convex = true;
                }
            }
            catch
            {
                Debug.Log("Gold Rock City fountain patch failed");
            }

            try
            {
                if (Plugin.craneFix.Value)
                {
                    if (__instance.gameObject.GetComponent<IslandSceneryScene>().parentIslandIndex == 1)
                    {
                        __instance.transform.Find("crane (1)").GetComponent<MeshCollider>().convex = false;
                        __instance.transform.Find("crane (2)").GetComponent<MeshCollider>().convex = false;
                        __instance.transform.Find("crane (3)").GetComponent<MeshCollider>().convex = false;
                    }
                }
            }
            catch { Debug.Log("Gold Rock City crane patch failed"); }
            
        }
    }
}
