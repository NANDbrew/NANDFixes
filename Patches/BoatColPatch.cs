using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(BoatEmbarkCollider), "ToggleBoatCapsuleCol")]
    internal static class BoatColPatch
    {
        public static bool Prefix(BoatEmbarkCollider __instance, CapsuleCollider ___capsuleCol)
        {
            Physics.IgnoreCollision(___capsuleCol, Refs.charController, Plugin.boatColFix.Value);
            //Collider hullCol = __instance.GetTopmostBoatParent().GetComponentInChildren<HullPlayerCollider>().GetComponent<Collider>();
            //Physics.IgnoreCollision(hullCol, Refs.charController, Plugin.boatColFix.Value);
            //hullCol.enabled = Plugin.boatColFix.Value;

            return !Plugin.boatColFix.Value;
        }
    }
}
