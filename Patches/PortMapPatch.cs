using HarmonyLib;
using UnityEngine;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(Port), "Start")]
    internal static class PortMapPatch
    {
        public static void Postfix(Port __instance, int ___portIndex)
        {
            if (__instance.oceanMapLocation == null && __instance.localMapLocation != null)
            {
                if (___portIndex == 18) // siren song
                {
                    __instance.oceanMapLocation = UnityEngine.Object.Instantiate(__instance.localMapLocation, __instance.localMapLocation.parent);
                    __instance.oceanMapLocation.localPosition = new Vector3(0.0503f, 0.4123f, -0.0001f);
                    __instance.oceanMapLocation.name = "Wm map Siren Song";
                }
                if (___portIndex == 19) // eastwind
                {
                    __instance.oceanMapLocation = UnityEngine.Object.Instantiate(__instance.localMapLocation, __instance.localMapLocation.parent);
                    __instance.oceanMapLocation.localPosition = new Vector3(0.0191f, 0.4262f, -0.0001f);
                    __instance.oceanMapLocation.name = "Wm map Eastwind";
                }
                else if (___portIndex == 26) // aestra abbey
                {
                    __instance.oceanMapLocation = UnityEngine.Object.Instantiate(__instance.localMapLocation, __instance.localMapLocation.parent);
                    __instance.oceanMapLocation.localPosition = new Vector3(0.0315f, 0.3797f, -0.0001f);
                    __instance.oceanMapLocation.name = "Wm map Monastery";
                }
                else if (___portIndex == 27) // fey valley
                {
                    __instance.oceanMapLocation = UnityEngine.Object.Instantiate(__instance.localMapLocation, __instance.localMapLocation.parent);
                    __instance.oceanMapLocation.localPosition = new Vector3(0.0649f, 0.4096f, -0.0001f);
                    __instance.oceanMapLocation.name = "Wm map Valley";
                }
                if (___portIndex == 28) // firefly grotto
                {
                    __instance.oceanMapLocation = UnityEngine.Object.Instantiate(__instance.localMapLocation, __instance.localMapLocation.parent);
                    __instance.oceanMapLocation.localPosition = new Vector3(0.0693f, 0.3961f, -0.0001f);
                    __instance.oceanMapLocation.name = "Wm map Cave";
                }
                else if (___portIndex == 29) // turtle island
                {
                    __instance.oceanMapLocation = UnityEngine.Object.Instantiate(__instance.localMapLocation, __instance.localMapLocation.parent);
                    __instance.oceanMapLocation.localPosition = new Vector3(0.4498f, -0.3815f, -0.0001f);
                    __instance.oceanMapLocation.name = "We map Jungle";
                }
                else if (___portIndex == 30) // dead cove
                {
                    __instance.oceanMapLocation = UnityEngine.Object.Instantiate(__instance.localMapLocation, __instance.localMapLocation.parent);
                    __instance.oceanMapLocation.localPosition = new Vector3(0.3641f, -0.372f, -0.0001f);
                    __instance.oceanMapLocation.name = "We map Swamp";
                }
            }

            if (__instance.oceanMapLocation != null)
            {
                __instance.oceanMapLocation.localPosition = new Vector3(__instance.oceanMapLocation.localPosition.x, __instance.oceanMapLocation.localPosition.y, -0.0002f);
            }
            if (__instance.localMapLocation != null)
            {
                __instance.localMapLocation.localPosition = new Vector3(__instance.localMapLocation.localPosition.x, __instance.localMapLocation.localPosition.y, -0.0002f); 
            }


        }
    }
    [HarmonyPatch(typeof(MissionDetailsUI), "Start")]
    internal static class MissionLinePatch
    {
        public static void Postfix(Transform ___routeLine, Transform ___destinationMarker)
        {
            //___routeLine.GetComponent<Renderer>().sharedMaterial.renderQueue = 3000;
            ___destinationMarker.GetComponent<Renderer>().sharedMaterial.renderQueue = 3000;
        }
    }
}
