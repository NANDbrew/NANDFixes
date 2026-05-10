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
        public static bool TriggerEnterPatch()
        {
            if (Plugin.playerEmbark.Value && GameState.currentShipyard != null) return false;
            return true;
        }

        [HarmonyPatch("ObserverTriggerEnter")]
        [HarmonyPrefix]
        public static bool ObserverTriggerEnterPatch(Collider other, Collider ___currentSmallCol, PlayerEmbarkerNew __instance)
        {
            if (Plugin.playerEmbark.Value && (GameState.currentShipyard != null || other.GetComponent<Anchor>()))
            {
                return false;
            }
            if (!Plugin.playerEmbarkAggro.Value) return true;
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
        public static void OnTriggerExit(PlayerEmbarkerNew __instance, bool ___embarked, EmbarkBoat ___currentBoat, Collider other)
        {
            if (!Plugin.playerEmbarkAggro.Value) return;

            if (other.CompareTag("EmbarkCol"))
            {
                EmbarkTracker tracker = __instance.GetComponent<EmbarkTracker>();
                tracker.embarkColliders.Remove(other);

                if (tracker.embarkColliders.Count >= 1)
                {
                    __instance.ObserverTriggerEnter(tracker.embarkColliders.Last());

                }

            }
            else if (___embarked && other.CompareTag("EmbarkColPlayer") && other.transform.parent == ___currentBoat.worldBoat)
            {
#if DEBUG
                Debug.Log("NANDfixes: exited big col. triggering disembark");
#endif
                AccessTools.Method(__instance.GetType(), "PlayerDisembark").Invoke(__instance, null);
            }
        }

    }

}
