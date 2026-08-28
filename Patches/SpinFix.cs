using HarmonyLib;
using UnityEngine;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(BoatPerformanceSwitcher), "Update")]
    internal static class SpinFix
    {
        internal static void Postfix(BoatPerformanceSwitcher __instance, Rigidbody ___body)
        {
            if (!Plugin.spinFix.Value) return;
            if (GameState.currentShipyard && GameState.currentShipyard.GetCurrentBoat() != __instance.gameObject)
            {
                ___body.freezeRotation = true;
            }
            else
            {
                ___body.freezeRotation = false;
            }
        }
    }
}
