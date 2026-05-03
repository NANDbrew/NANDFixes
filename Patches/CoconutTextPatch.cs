using HarmonyLib;
namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(PrefabsDirectory), "PopulateShipItems")]
    internal static class CoconutTextPatch
    {
        public static void Postfix()
        {
            PrefabsDirectory.instance.shipItems[3].description = "";
        }
    }
}
