﻿using Crest;
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
                    if (___currentBoatCollider != null)
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


            [HarmonyPatch("EnterBoat")]
            [HarmonyPrefix]
            public static bool EnterBoat(ShipItem __instance, Collider ___currentBoatCollider, Collider embarkCol, Transform ___itemRigidbody)
            {
                if (!Plugin.stickyFix.Value) return true;
                if (!__instance.sold) return false;
                if (Plugin.aggressiveSF.Value && ___itemRigidbody.GetComponent<SimpleFloatingObject>().InWater) 
                {
#if DEBUG
                    Debug.Log("nandfixes: item " + __instance.name + " was in water. prevented embark"); 
#endif
                    return false; 
                }
                if (Plugin.aggressiveSF.Value && (float)Traverse.Create(__instance.itemRigidbodyC).Field("dynamicColTimer").GetValue() <= 0)
                {
#if DEBUG
                    Debug.Log("nandfixes: item " + __instance.name + " was settled. prevented embark");
#endif
                    return false;
                }
                if ((bool)___currentBoatCollider && ___currentBoatCollider != embarkCol)
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
