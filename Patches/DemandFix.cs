using HarmonyLib;
using System;
using UnityEngine;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(IslandEconomy))]
    internal static class DemandFix
    {
        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        public static void Patch1(ref int[] ___baseDemand)
        {
            if (!Plugin.demandFix.Value) return;
            if (___baseDemand.Length < 65)
            {
                Array.Resize(ref ___baseDemand, 65);
            }
            Debug.Log("NANDFixes: resized demand array");
        }

    }
}
