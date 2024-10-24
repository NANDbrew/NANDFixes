using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(PlayerEmbarkDisembarkTrigger), "Update")]
    internal static class PlayerEmbarkPatch
    {
        public static void Postfix(PlayerEmbarkDisembarkTrigger __instance, ref bool ___exitBoatFlag, Collider ___currentlyStayedTrigger, ref Collider ___currentBoatCollider, ref float ___disembarkHeight, Transform ___playerObserver, bool __runOriginal)
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
