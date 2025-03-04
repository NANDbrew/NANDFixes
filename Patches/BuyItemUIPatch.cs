using HarmonyLib;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(BuyItemUI), "Update")]
    internal class BuyItemUIPatch
    {
        public static void Postfix(BuyItemUI __instance)
        {
            if (!Plugin.buyUIPatch.Value) return;
            if (__instance.menu.activeInHierarchy && (!__instance.activeItem || __instance.activeItem && !__instance.activeItem.held))
            {
                __instance.DeactivateUI();
            }
        }
    }
}
