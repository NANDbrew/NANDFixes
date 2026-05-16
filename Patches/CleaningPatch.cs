using HarmonyLib;
using UnityEngine;

namespace NANDFixes
{
    [HarmonyPatch(typeof(Shipyard), "CleanHull")]
    internal static class CleaningPatch
    {
        public static bool Prefix(GameObject ___currentShip, ref bool ___currentOrderIncludesCleaning)
        {
            if (___currentShip.GetComponent<SaveableObject>().GetCleanable() == null)
            {
                NotificationUi.instance.ShowNotification("Ship is not cleanable");
                ___currentOrderIncludesCleaning = false;
                return false;
            }
            return true;
        }
    }
}
