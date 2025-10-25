using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(RecoveryPort), "Start")]
    public static class RecoveryPortFix
    {
        public static List<Transform> referenceList = new List<Transform>();
        public static void Postfix(RecoveryPort __instance)
        {
            if (!Plugin.recoveryFix.Value) return;
            if (__instance.mooringFront == null || __instance.mooringFront.parent != __instance.parentPort.transform.parent)
            {
                var mooring = FindClosestMooring(__instance.boatPos.TransformPoint(Vector3.forward * 5), __instance.parentPort.transform.parent.GetComponentsInChildren<GPButtonDockMooring>());
                if (mooring != null)
                {
                    __instance.mooringFront = mooring;
                    Debug.Log("RecoveryMooringPatch: fixed front mooring!");
                }
                else
                {
                    Debug.LogWarning("RecoveryMooringPatch: couldn't find replacement mooring!");
                }
            }

            if (__instance.mooringBack == null || __instance.mooringBack.parent != __instance.parentPort.transform.parent)
            {
                var mooring = FindClosestMooring(__instance.boatPos.TransformPoint(Vector3.forward * -5), __instance.parentPort.transform.parent.GetComponentsInChildren<GPButtonDockMooring>());
                if (mooring != null)
                {
                    __instance.mooringBack = mooring;
                    Debug.Log("RecoveryMooringPatch: Fixed Back mooring!");
                }
                else
                {
                    Debug.LogWarning("RecoveryMooringPatch: couldn't find replacement mooring!");
                }
            }
        }

        private static Transform FindClosestMooring(Vector3 pos, GPButtonDockMooring[] moorings)
        {
            float shortestDist = float.MaxValue;
            Transform closest = null;
            for (int i = 0; i < moorings.Length; i++)
            {
                float dist = Vector3.Distance(moorings[i].transform.position, pos);
                if (dist < shortestDist)
                {
                    shortestDist = dist;
                    closest = moorings[i].transform;
                }
            }
            Debug.Log("closest mooring: " + closest.name + " @ " + shortestDist.ToString());
            referenceList.Add(closest);
            return closest;
        }
    }
}
