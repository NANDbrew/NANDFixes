using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NANDFixes.Patches
{
    internal class BarrelDrinkingPatches
    {
        [HarmonyPatch(typeof(ShipItemBottle))]
        private static class BarrelDrinkPatches
        {
            [HarmonyPatch("ExtraLateUpdate")]
            [HarmonyPostfix]
            public static void ExtraLateUpdatePatch(ref bool ___big, bool ___wasDrinking, bool ___drinking, float ___capacity)
            {
                if (Plugin.barrelPatches.Value) return;
                if (!___drinking && !___wasDrinking)
                {
                    if (___capacity > 10f)
                    {
                        ___big = true;
                    }
                }
            }

            [HarmonyPatch("OnAltHeld")]
            [HarmonyPrefix]
            public static bool OnAltHeldPatch(Good ___goodC)
            {
                if (Plugin.barrelPatches.Value) return true;
                if (___goodC && ___goodC.GetMissionIndex() != -1) return false;
                return true;

            }

            [HarmonyPatch("TryDrinkBottle")]
            [HarmonyPrefix]
            public static bool TryDrinkPatch(bool ___big)
            {
                if (Plugin.barrelPatches.Value) return true;
                if (___big && !GameInput.GetKey(InputName.Activate))
                {
                    Debug.Log("NANDFixes: prevented drinking");
                    return false;
                }
                return true;

            }
        }
    }
}
