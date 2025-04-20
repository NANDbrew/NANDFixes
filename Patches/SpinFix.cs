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
    [HarmonyPatch(typeof(BoatPerformanceSwitcher), "Update")]
    internal static class SpinFix
    {
        internal static void Postfix(bool ___performanceModeOn, Rigidbody ___body)
        {
            if (!Plugin.spinFix.Value) return;
            if (GameState.currentShipyard && ___performanceModeOn)
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
