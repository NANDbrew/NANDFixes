using HarmonyLib;
using NANDFixes.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(PlayerEmbarkDisembarkTrigger))]
    internal static class PlayerEmbarkPatch
    {
        [HarmonyPatch("Awake")]
        [HarmonyPrefix]
        public static void Awake(PlayerEmbarkDisembarkTrigger __instance)
        {
            __instance.gameObject.AddComponent<Scripts.EmbarkTracker>();
        }

        [HarmonyPatch("OnTriggerEnter")]
        [HarmonyPrefix]
        public static bool OnTriggerEnter(Collider other, PlayerEmbarkDisembarkTrigger __instance, ref BoatEmbarkCollider ___currentlyStayedEmbarkCol, ref bool ___exitBoatFlag, ref Collider ___currentlyStayedTrigger)
        {
            if (!Plugin.playerEmbark.Value) return true;

            if (other.CompareTag("EmbarkCol"))
            {
                List<Collider> colliders = __instance.GetComponent<Scripts.EmbarkTracker>().embarkColliders;
                if (!colliders.Contains(other))
                {
                    colliders.Add(other);
                }
                if (colliders.Count > 1)
                {
                    return false;
                }
            }
            return true;
        }

        [HarmonyPatch("OnTriggerExit")]
        [HarmonyPostfix]
        public static void OnTriggerExit(PlayerEmbarkDisembarkTrigger __instance, Collider other, ref BoatEmbarkCollider ___currentlyStayedEmbarkCol, ref bool ___exitBoatFlag, ref Collider ___currentlyStayedTrigger)
        {
            if (!Plugin.playerEmbark.Value) return;

            List<Collider> colliders = __instance.GetComponent<Scripts.EmbarkTracker>().embarkColliders;
            colliders.Remove(other);

            if (colliders.Count >= 1)
            {
                ___currentlyStayedTrigger = colliders.Last();
                ___currentlyStayedEmbarkCol = ___currentlyStayedTrigger.GetComponent<BoatEmbarkCollider>();
                ___currentlyStayedEmbarkCol.ToggleBoatCapsuleCol(newState: false);
                ___exitBoatFlag = false;

            }
        }
        [HarmonyPatch("FixedUpdate")]
        [HarmonyPostfix]
        public static void UpdatePatch(PlayerEmbarkDisembarkTrigger __instance, ref bool ___exitBoatFlag, Collider ___currentlyStayedTrigger, ref Collider ___currentBoatCollider, ref float ___disembarkHeight, Transform ___playerObserver, bool __runOriginal)
        {
            if (!Plugin.playerEmbark.Value || !__runOriginal) return;
            if (PlayerEmbarkDisembarkTrigger.embarked && !GameState.sleeping && !GameState.justWokeUp && !GameState.currentShipyard)
            {
                if (___currentlyStayedTrigger != null && ___currentlyStayedTrigger != ___currentBoatCollider && !(bool)AccessTools.Method(__instance.GetType(), "IsGroundedOnBoat").Invoke(__instance, null))
                {
                    ___exitBoatFlag = true;
                    ___disembarkHeight = ___playerObserver.position.y;
                }
            }
        }
    }
}
