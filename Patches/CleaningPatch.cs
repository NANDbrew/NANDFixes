using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace NANDFixes
{
    [HarmonyPatch(typeof(Shipyard), "CleanHull")]
    internal static class CleaningPatch
    {
        public static bool Prefix(GameObject ___currentShip, ref bool ___currentOrderIncludesCleaning)
        {
            if (___currentShip.GetComponentInChildren<CleanableObject>() == null)
            {
                NotificationUi.instance.ShowNotification("Ship is not cleanable");
                ___currentOrderIncludesCleaning = false;
                return false;
            }
            return true;
        }
    }
}
