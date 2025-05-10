using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using HarmonyLib;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(CrateInventoryUI), "ShowInventory")]
    internal static class CrateUIPatch
    {
        internal static void Prefix(ref Transform ___localPosTracker)
        {
            if (___localPosTracker == null)
            {
                ___localPosTracker = UnityEngine.Object.Instantiate(new GameObject()).transform;
                Debug.Log("nandfixes: created new localPosTracker for CrateInventoryUI");
            }
        }
    }
}
