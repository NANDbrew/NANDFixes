using HarmonyLib;
using UnityEngine;

namespace NANDFixes.Patches
{
    internal class AnchorFixes
    {
        [HarmonyPatch(typeof(SaveableObject), "UnpurchasedBoatOrMooringRope")]
        public static class AnchorPatch
        {
            private static void Postfix(SaveableObject __instance, bool extraSetting, ref bool __result)
            {
                if (!Plugin.anchorPatches.Value) return;
                if ((bool)__instance.anchor)
                {
                    var boatSaveable = (SaveableObject)AccessTools.Field(typeof(Anchor), "boatSaveable").GetValue(__instance.anchor);
                    if (!boatSaveable.extraSetting) __result = true;
                }

            }
        }
        [HarmonyPatch(typeof(Anchor))]
        public static class AnchorPatch2
        {
            [HarmonyPatch("Awake")]
            [HarmonyPrefix]
            public static void Patch2(Anchor __instance)
            {
                if (!Plugin.anchorPatches.Value) return;
                if (__instance.GetComponent<ConfigurableJoint>() is ConfigurableJoint joint)
                {
                    __instance.transform.parent = joint.connectedBody.transform.GetChild(0);
                }
            }
        }

        [HarmonyPatch(typeof(Anchor))]
        public static class AnchorPatch3
        {

            [HarmonyPatch("ExtraFixedUpdate")]
            [HarmonyPostfix]
            public static void Patch1(Anchor __instance, GoPointer ___held, RopeControllerAnchor ___rope, ConfigurableJoint ___joint)
            {
                if (!Plugin.anchorPatches.Value) return;
                if ((bool)___held)
                {
                    float value = Vector3.Distance(___joint.connectedBody.transform.TransformPoint(___joint.connectedAnchor), __instance.transform.position);
                    float num = Mathf.InverseLerp(0f, ___rope.maxLength, value);
                    if (num > ___rope.currentLength - 0.01f)
                    {
                        ___rope.canPull = false;
                        if (num > ___rope.currentLength)
                        {
                            ___rope.currentLength = num;
                        }
                    }

                }
            }
        }
    }
}
