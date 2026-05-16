using HarmonyLib;

namespace NANDFixes.Patches
{
/*    [HarmonyPatch(typeof(CrateInventoryUI), "ShowInventory")]
    internal static class CrateUIPatch
    {
        internal static void Prefix(ref Transform ___localPosTracker)
        {
            if (___localPosTracker == null)
            {
                ___localPosTracker = UnityEngine.Object.Instantiate(new GameObject()).transform;
                Debug.Log("nandfixes: created new localPosTracker for CrateInventoryUI");
            }
        }
    }*/
    [HarmonyPatch(typeof(CrateInventoryButton), "PutItemBack")]
    internal static class CrateUIPatch
    {
        public static bool Prefix(CrateInventory crate)
        {
            if (!(bool)crate)
            {
                return false;
            }

            return true;
        }
    }
}
