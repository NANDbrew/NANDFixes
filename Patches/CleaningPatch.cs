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
        public static bool Prefix(/*Shipyard __instance, */GameObject ___currentShip, ref bool ___currentOrderIncludesCleaning)
        {
            if (___currentShip.GetComponent<CleanableObject>() == null)
            {
                NotificationUi.instance.ShowNotification("Ship is not cleanable");
                ___currentOrderIncludesCleaning = false;
                //__instance.UpdateOrder();
                return false;
            }
            return true;
        }
    }
}
