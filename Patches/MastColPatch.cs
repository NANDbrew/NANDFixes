using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace NANDFixes.Patches
{ 
    [HarmonyPatch(typeof(Mast), "Awake")]
    internal class MastColPatch
    {
        [HarmonyPostfix]
        public static void Postfix(Mast __instance)
        {
            if (!Plugin.mastColPatch.Value) return;
            __instance.gameObject.layer = 2;

        }
    }
}
