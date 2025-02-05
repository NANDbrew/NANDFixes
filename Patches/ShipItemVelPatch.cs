using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NANDFixes.Patches
{
    internal class ShipItemVelPatch
    {
        [HarmonyPatch(typeof(ItemRigidbody))]
        private static class ItemRigidbodyEmbarkPatches
        {
            [HarmonyPatch("MoveRigidbodyToWalkCol")]
            [HarmonyPostfix]
            public static void EnterPatch(ShipItem ___item, Rigidbody ___rigidbody)
            {
                if (!Plugin.velocityFix.Value) return;
                Vector3 velVector = ___item.currentActualBoat.InverseTransformDirection(___rigidbody.velocity);
                ___rigidbody.velocity = velVector;
            }
            [HarmonyPatch("ExitBoat")]
            [HarmonyPrefix]
            public static void ExitBoatPatch1(ShipItem ___item, ref Transform __state)
            {
                if (!Plugin.velocityFix.Value) return;
                __state = ___item.currentActualBoat;

            }
            [HarmonyPatch("ExitBoat")]
            [HarmonyPostfix]
            public static void ExitBoatPatch1(Rigidbody ___rigidbody, Transform __state)
            {
                if (!Plugin.velocityFix.Value) return;
                if (__state)
                {
                    ___rigidbody.velocity = __state.TransformDirection(___rigidbody.velocity);
                }

            }

        }

        [HarmonyPatch(typeof(GoPointer))]
        private static class GoPointerPatches
        {
            [HarmonyPatch("ThrowItemAfterDelay")]
            [HarmonyPostfix]
            internal static IEnumerator ThrowItemAfterDelayPatch(IEnumerator original, Rigidbody heldRigidbody, float force, GoPointer __instance, float ___throwForce)
            {
                if (Plugin.velocityFix.Value && heldRigidbody.GetComponent<ItemRigidbody>().GetShipItem().currentActualBoat is Transform boat)
                {
                    Debug.Log("enumerator patch");
                    yield return new WaitForFixedUpdate();
                    if (force > 1f)
                    {
                        force = 1f;
                    }
                    Vector3 adjustedThrow = boat.InverseTransformDirection(__instance.transform.forward);
                    heldRigidbody.AddForce(adjustedThrow * ___throwForce * force * heldRigidbody.mass);
                }
                else yield return original;

            }
        }

    }
}
