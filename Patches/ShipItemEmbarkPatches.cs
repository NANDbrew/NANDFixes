using Crest;
using HarmonyLib;
//using SailwindModdingHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using NANDFixes.Scripts;

namespace NANDFixes.Patches
{
    internal class ShipItemEmbarkPatches
    {
        [HarmonyPatch(typeof(ShipItem))]
        private static class EmbarkPatches
        {

            [HarmonyPatch("Awake")]
            [HarmonyPrefix]
            public static void Awake(ShipItem __instance)
            {
                __instance.gameObject.AddComponent<EmbarkTracker>();
            }

            [HarmonyPatch("OnTriggerEnter")]
            [HarmonyPrefix]
            public static bool OnTriggerEnter(Collider other, ShipItem __instance, Collider ___currentBoatCollider)
            {
                if (!Plugin.stickyFix.Value) return true;

                if (other.CompareTag("EmbarkCol") && __instance.GetComponent<EmbarkTracker>() is EmbarkTracker tracker)
                {
                    //List<Collider> colliders = __instance.GetComponent<EmbarkTracker>().embarkColliders;
                    if (!tracker.embarkColliders.Contains(other))
                    {
                        tracker.embarkColliders.Add(other);
                    }
                    if (___currentBoatCollider && ___currentBoatCollider != other)
                    {
                        return false;
                    }
                }
                return true;
            }

            [HarmonyPatch("OnTriggerExit")]
            [HarmonyPostfix]
            public static void OnTriggerExit(ShipItem __instance, Collider other, ref Collider ___currentlyStayedEmbarkCol)
            {
                if (!Plugin.stickyFix.Value) return;
                if (other.CompareTag("EmbarkCol") && __instance.GetComponent<EmbarkTracker>() is EmbarkTracker tracker)
                {
                    List<Collider> colliders = tracker.embarkColliders;
                    colliders.Remove(other);

                    if (colliders.Count >= 1)
                    {
                        ___currentlyStayedEmbarkCol = colliders[0];
                    }
                }
            }
            [HarmonyPatch("ExtraFixedUpdate")]
            [HarmonyPostfix]
            public static void ExtraFixedUpdate(ShipItem __instance, Transform ___currentActualBoat, Collider ___currentlyStayedEmbarkCol, int ___frameCounter)
            {
                if (!Plugin.stickyFix.Value) return;
                if (!Plugin.aggressiveSF.Value) return;

                if (___currentActualBoat && !___currentlyStayedEmbarkCol && ___frameCounter > Plugin.threshold && __instance.transform.localPosition.sqrMagnitude > 2500f)
                {
                    AccessTools.Method(__instance.GetType(), "ExitBoat").Invoke(__instance, null);
                    Debug.LogWarning("nandFixes: object exiting boat due to frame count");
                }

            }
            [HarmonyPatch("EnterBoat")]
            [HarmonyPrefix]
            public static bool EnterBoat(ShipItem __instance, Collider ___currentBoatCollider, Collider embarkCol, Transform ___itemRigidbody)
            {
                if (!Plugin.stickyFix.Value) return true;
                if (!__instance.sold) return false;
                if (Plugin.aggressiveSF.Value)
                {
                    if (___itemRigidbody.GetComponent<SimpleFloatingObject>().InWater || (float)Traverse.Create(__instance.itemRigidbodyC).Field("dynamicColTimer").GetValue() <= 0)
                    {
#if DEBUG
                    Debug.Log("nandfixes: item " + __instance.name + " was settled. prevented embark");
#endif
                        return false;
                    }
                }
                if (___currentBoatCollider && ___currentBoatCollider != embarkCol)
                {
                    AccessTools.Method(__instance.GetType(), "ExitBoat").Invoke(__instance, null);
                }
                return true;
            }

            [HarmonyPatch("ExitBoat")]
            [HarmonyPrefix]
            public static void ExitBoat(ShipItem __instance, ItemRigidbody ___itemRigidbodyC, Collider ___currentBoatCollider)
            {
                if (!Plugin.stickyFix.Value) return;
                if (__instance.GetComponent<EmbarkTracker>() is EmbarkTracker tracker)
                {
                    tracker.embarkColliders.Remove(___currentBoatCollider);

                }

            }
            [HarmonyPatch("InsertIntoCargoCarrier")]
            [HarmonyPostfix]
            public static void CarrierPatch(ShipItem __instance)
            {
                if (__instance.GetComponent<EmbarkTracker>() is EmbarkTracker tracker)
                {
                    tracker.embarkColliders.Clear();
                }
            }
        }
    }
}
