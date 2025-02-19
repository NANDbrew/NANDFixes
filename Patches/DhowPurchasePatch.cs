using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(PurchasableBoat), "Awake")]
    internal class DhowPurchasePatch2
    {
        [HarmonyPostfix]
        public static void Postfix(PurchasableBoat __instance)
        {
            if (Plugin.mastColPatch.Value) return;
            if (__instance.name.StartsWith("BOAT dhow small"))
            {
                /*if (__instance.GetComponent<BoxCollider>().enabled) { Debug.Log("purchase collider disabled"); }
                __instance.GetComponent<BoxCollider>().enabled = true;
                __instance.transform.parent.parent.localPosition = new Vector3(-0.0820f, 2.961f, 2.01f);*/
                Debug.Log("trying to fix dhow");

                BoatRefs refs = __instance.GetComponent<BoatRefs>();
                refs.masts[6].GetComponent<CapsuleCollider>().radius = 0.2f;
                refs.masts[7].GetComponent<CapsuleCollider>().radius = 0.2f;
                //Debug.Log(refs ? "Refs!!" : "not refs!");
            }
        }
    }

    [HarmonyPatch(typeof(GPButtonPurchaseBoat), "Awake")]
    internal class DhowPurchasePatch3
    {
        [HarmonyPostfix]
        public static void Postfix(GPButtonPurchaseBoat __instance)
        {
            BoxCollider collider = __instance.GetComponent<BoxCollider>();
            collider.size = new Vector3(collider.size.x, collider.size.y, 0.25f);
        }
    }
}
