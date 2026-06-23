using HarmonyLib;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(MapTableCamera), "Awake")]
    public static class MapTablePatch
    {
        private static void Postfix(MapTableCamera __instance)
        {
            __instance.quill.gameObject.SetActive(value: false);
            __instance.ruler.gameObject.SetActive(value: false);
            __instance.prot.gameObject.SetActive(value: false);

        }
    }
}
