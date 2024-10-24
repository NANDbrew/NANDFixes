using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(GPButtonPurchaseBoat), "Awake")]
    internal class DhowPurchasePatch
    {
        [HarmonyPostfix]
        public static void Postfix(GPButtonPurchaseBoat __instance)
        {
            if (__instance.boat.name.StartsWith("BOAT dhow small"))
            {
                if (__instance.GetComponent<BoxCollider>().enabled) { Debug.Log("purchase collider disabled"); }
                __instance.GetComponent<BoxCollider>().enabled = true;
                __instance.transform.parent.parent.localPosition = new Vector3(-0.0820f, 2.961f, 2.01f);
                //Debug.Log("did second thing!");

            }

        }
    }
/*    [HarmonyPatch(typeof(PurchasableBoat), "RegisterUI")]
    internal class DhowPurchasePatch2
    {
        [HarmonyPostfix]
        public static void Postfix(GameObject ui, SaveableObject ___saveable)
        {
            Debug.Log("did first thing!");

            if (___saveable.sceneIndex == 10)
            {
                Debug.Log("did second thing!");
                ui.transform.localPosition = new Vector3(-0.0820f, 3, 2);
            }
        }
    }*/
}
