using HarmonyLib;
using UnityEngine;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(BoatDamageWater), "Start")]
    internal static class BoatDamagePatch
    {
        public static void Postfix(Renderer ___renderer)
        {
            if (!Plugin.damagePatch.Value) return;
            ___renderer.sharedMaterial.renderQueue = 2002;
        }
    }
}
