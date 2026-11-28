using HarmonyLib;
using System.Collections;
using UnityEngine;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(PickupableBoatMooringRope))]
    internal static class MooringPatches
    {
        [HarmonyPatch("OnDrop")]
        [HarmonyPrefix]
        public static void DropPatch(PickupableBoatMooringRope __instance, float ___maxLength, Vector3 ___initialPos, Quaternion ___initialRot, bool ___throwing, bool ___justUnmoored)
        {
            if (!Plugin.mooringPatch.Value) return;
            if ((bool)__instance.IsMoored() || ___throwing) return;
            if (___justUnmoored && ((float)AccessTools.Method(__instance.GetType(), "GetCurrentDistanceSquared").Invoke(__instance, null) > ___maxLength))
            {
                __instance.transform.localRotation = ___initialRot;
                __instance.StartCoroutine(ThrowRopeSequenceLocal(__instance, ___initialPos));
                if (__instance.GetComponent<Rigidbody>() is Rigidbody rb)
                {
                    rb.isKinematic = true;
                }
            }
        }

        private static IEnumerator ThrowRopeSequenceLocal(PickupableBoatMooringRope rope, Vector3 to)
        {
            AccessTools.Field(rope.GetType(), "throwing").SetValue(rope, true);
            rope.transform.GetComponent<Collider>().enabled = false;
            float t = 0f;
            while (t <= 0.8f)
            {
                rope.transform.localPosition = Vector3.Lerp(rope.transform.localPosition, to, t / 0.8f);
                t += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            rope.transform.GetComponent<Collider>().enabled = true;
            AccessTools.Field(rope.GetType(), "throwing").SetValue(rope, false);
        }
    }
}
