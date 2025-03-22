using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(PrefabsDirectory), "Start")]
    internal static class AlbacorePatch
    {
        internal static void Postfix(GameObject[] ___directory)
        {
            if (!Plugin.albacoreFix.Value) return;
            FoodState[] comps = ___directory[140].gameObject.GetComponents<FoodState>();
            if (comps.Length >= 2)
            {
                comps[0].slicePrefabIndex = comps[1].slicePrefabIndex;
                comps[0].slicesCount = comps[1].slicesCount;
                comps[1].enabled = false;
            }
        }
    }
}
