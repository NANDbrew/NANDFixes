using Crest;
using HarmonyLib;
using UnityEngine;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(BoatDamageWater), "Start")]
    internal static class BoatDamagePatch
    {
        public static void Postfix(Renderer ___renderer)
        {
            if (!Plugin.damagePatch.Value) return;
            ___renderer.sharedMaterial.renderQueue = 2002;
        }
    }

    [HarmonyPatch(typeof(WaveSplashZone), "Update")]
    internal static class WaveSplashPatch
    {
        public static bool Prefix()
        {
            if (!Plugin.damagePatch.Value) return true;
            if (GameState.currentlyLoading || GameState.justStarted)
            {
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(BoatHorizon), "UpdateKinematic")]
    internal static class BoatHeightPatch
    {
        private static SampleHeightHelper heightHelper = new SampleHeightHelper();
        internal static bool freshStart = true;
        public static void Prefix(Rigidbody ___rigidbody, BoatPhysicsSwitcher ___physicsSwitcher, ref bool __state)
        {
            if (!Plugin.boatHeightFix.Value) return;
            if (___physicsSwitcher == null) return;
            __state = ___rigidbody.isKinematic;
        }

        public static void Postfix(Rigidbody ___rigidbody, bool __state)
        {
            if (!Plugin.boatHeightFix.Value) return;
            if (__state && !___rigidbody.isKinematic)
            {
                //Debug.Log("nfx: boatHeight is doing");
                heightHelper.Init(___rigidbody.position, 0f, allowMultipleCallsPerFrame: true);
                heightHelper.Sample(out var newHeight);
                newHeight -= ___rigidbody.transform.position.y + (freshStart? Plugin.heightFixMax.Value : Plugin.heightFixMaxPause.Value);
                if (newHeight > 0)
                {
                    ___rigidbody.transform.Translate(0, newHeight, 0);
                    Debug.Log("NANDfixes: moved " + ___rigidbody.name + " " + newHeight + " to match water surface");
                }

            }
        }
    }

    [HarmonyPatch(typeof(StartMenu), "GameToSettings")]
    static class LoadTrigger
    {
        private static void Postfix()
        {
            BoatHeightPatch.freshStart = false;

        }

    }

}
