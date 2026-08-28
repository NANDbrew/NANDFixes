using HarmonyLib;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(BuyItemUI), "Update")]
    internal class BuyItemUIPatch
    {
        public static void Postfix(BuyItemUI __instance)
        {
            if (!Plugin.buyUIPatch.Value) return;
            if (__instance.menu.activeInHierarchy && (__instance.activeItem == null || __instance.activeItem.gameObject.layer == 26 || __instance.activeItem.gameObject.layer == 5 || __instance.activeItem.currentActualBoat != null))
            {
                __instance.DeactivateUI();
            }
        }
    }
}
