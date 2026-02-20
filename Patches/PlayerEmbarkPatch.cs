//using HarmonyLib;
//using NANDFixes.Scripts;
//using System.Linq;
//using UnityEngine;

//namespace NANDFixes.Patches
//{
//    [HarmonyPatch(typeof(PlayerEmbarkDisembarkTrigger))]
//    internal static class PlayerEmbarkPatch
//    {
//        //static float debounce = 0.5f;
//        [HarmonyPatch("Awake")]
//        [HarmonyPrefix]
//        public static void Awake(PlayerEmbarkDisembarkTrigger __instance)
//        {
//            __instance.gameObject.AddComponent<EmbarkTracker>();
//        }

//        [HarmonyPatch("OnTriggerEnter")]
//        [HarmonyPrefix]
//        public static bool OnTriggerEnter(Collider other, PlayerEmbarkDisembarkTrigger __instance)
//        {
//            if (!Plugin.playerEmbark.Value) return true;
//            //if (!GameState.playing || GameState.justStarted /*|| PlayerEmbarkDisembarkTrigger.timeSinceEmbark < debounce*/) return true;

//            if (other.CompareTag("EmbarkCol"))
//            {
//                EmbarkTracker tracker = __instance.GetComponent<EmbarkTracker>();
//                if (!tracker.embarkColliders.Contains(other))
//                {
//                    tracker.embarkColliders.Add(other);
//                }
//                if (tracker.embarkColliders.Count > 1)
//                {
//                    return false;
//                }
//            }
//            return true;
//        }

//        [HarmonyPatch("OnTriggerExit")]
//        [HarmonyPostfix]
//        public static void OnTriggerExit(PlayerEmbarkDisembarkTrigger __instance, Collider other)
//        {
//            if (!Plugin.playerEmbark.Value) return;
//            //if (!GameState.playing || GameState.justStarted) return;

//            if (other.CompareTag("EmbarkCol"))
//            {
//                EmbarkTracker tracker = __instance.GetComponent<EmbarkTracker>();
//                tracker.embarkColliders.Remove(other);

//                if (tracker.embarkColliders.Count >= 1)
//                {
//                    AccessTools.Method(__instance.GetType(), "OnTriggerEnter").Invoke(__instance, new object[] { tracker.embarkColliders.Last() });

//                    /*___currentlyStayedTrigger = tracker.embarkColliders.Last();
//                    ___currentlyStayedEmbarkCol = ___currentlyStayedTrigger.GetComponent<BoatEmbarkCollider>();
//                    ___currentlyStayedEmbarkCol.ToggleBoatCapsuleCol(newState: false);
//                    ___exitBoatFlag = false;*/

//                }
//            }
//        }
//        [HarmonyPatch("LateUpdate")]
//        [HarmonyPostfix]
//        public static void UpdatePatch(PlayerEmbarkDisembarkTrigger __instance, ref bool ___exitBoatFlag, Collider ___currentlyStayedTrigger, ref Collider ___currentBoatCollider, ref float ___disembarkHeight, Transform ___playerObserver, bool __runOriginal)
//        {
//            if (!Plugin.playerEmbark.Value || !__runOriginal) return;
//            //if (!GameState.playing || GameState.justStarted) return;
//            //PlayerEmbarkDisembarkTrigger.timeSinceEmbark += Time.deltaTime;
//            if (PlayerEmbarkDisembarkTrigger.embarked && !GameState.sleeping && !GameState.justWokeUp && !GameState.currentShipyard)
//            {
//                if (___currentlyStayedTrigger != null && ___currentlyStayedTrigger != ___currentBoatCollider && !(bool)AccessTools.Method(__instance.GetType(), "IsGroundedOnBoat").Invoke(__instance, null))
//                {
//                    ___exitBoatFlag = true;
//                    ___disembarkHeight = ___playerObserver.position.y;
//                }
//            }
//        }
//    }
//}
