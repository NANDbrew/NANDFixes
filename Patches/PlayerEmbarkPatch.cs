using HarmonyLib;
using NANDFixes.Scripts;
using UnityEngine;
using System.Linq;


namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(PlayerEmbarkerNew))]
    internal static class PlayerEmbarkPatch
    {
        [HarmonyPatch("Awake")]
        [HarmonyPostfix]
        public static void AwakePatch(PlayerEmbarkerNew __instance)
        {
            __instance.gameObject.AddComponent<EmbarkTracker>();
        }

        [HarmonyPatch("OnTriggerEnter")]
        [HarmonyPrefix]
        public static bool TriggerEnterPatch(Collider other)
        {
            if (!Plugin.playerEmbark2.Value) return true;
            if (GameState.currentShipyard != null) return false;
            return true;
        }

        [HarmonyPatch("ObserverTriggerEnter")]
        [HarmonyPrefix]
        public static bool ObserverTriggerEnterPatch(Collider other, Collider ___currentSmallCol, PlayerEmbarkerNew __instance)
        {
            if (!Plugin.playerEmbark.Value) return true;
            if (GameState.currentShipyard != null || other.GetComponent<Anchor>()) return false;

            if (!Plugin.playerEmbark2.Value) return true;
            if (other.CompareTag("EmbarkCol"))
            {
                EmbarkTracker tracker = __instance.GetComponent<EmbarkTracker>();
                if (!tracker.embarkColliders.Contains(other))
                {
                    tracker.embarkColliders.Add(other);
                }
                if (tracker.embarkColliders.Count > 1)
                {
                    return false;
                }
            }

            if (___currentSmallCol != null)
            {

#if DEBUG
                Debug.Log("NANDFixes: prevented disembark due to still in smallCol");
#endif
                return false;

            }
            return true;
        }

        [HarmonyPatch("ObserverTriggerExit")]
        [HarmonyPostfix]
        public static void OnTriggerExit(PlayerEmbarkerNew __instance, Collider other)
        {
            if (!Plugin.playerEmbark2.Value) return;
            //if (!GameState.playing || GameState.justStarted) return;

            if (other.CompareTag("EmbarkCol"))
            {
                EmbarkTracker tracker = __instance.GetComponent<EmbarkTracker>();
                tracker.embarkColliders.Remove(other);

                if (tracker.embarkColliders.Count >= 1)
                {
                    __instance.ObserverTriggerEnter(tracker.embarkColliders.Last());

                    /*___currentlyStayedTrigger = tracker.embarkColliders.Last();
                    ___currentlyStayedEmbarkCol = ___currentlyStayedTrigger.GetComponent<BoatEmbarkCollider>();
                    ___currentlyStayedEmbarkCol.ToggleBoatCapsuleCol(newState: false);
                    ___exitBoatFlag = false;*/

                }
            }
        }

    }
}
