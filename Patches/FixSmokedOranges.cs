using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
//using SailwindModdingHelper;

namespace NANDFixes.Patches
{
    internal class FixSmokedOranges
    {
        [HarmonyPatch(typeof(ShipItemCrate), "OnLoad")]
        private static class OrangeCratePatch
        {
            [HarmonyPrefix]
            public static void Prefix(ShipItemCrate __instance)
            {
                if (__instance.GetPrefabIndex() == 213)
                {
                    __instance.smokedFood = false;
                    //__instance.name = "Bruh";
                    //ModLogger.Log(Main.mod,"We did a thing!");
                }
                //ModLogger.Log(Main.mod, "Did we do a thing?");
            }

        }

    }
}
