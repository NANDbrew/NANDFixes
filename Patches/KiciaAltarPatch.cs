using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(KiciaAltar), "Update")]
    internal class KiciaAltarPatch
    {
        static bool hasRun;

        [HarmonyPostfix]
        public static void UpdatePatch(ParticleSystem ___sacParticles)
        {
            if (!hasRun)
            {
                ___sacParticles.GetComponent<AudioSource>().enabled = false;
                hasRun = true;
            }
        }

    }
}
