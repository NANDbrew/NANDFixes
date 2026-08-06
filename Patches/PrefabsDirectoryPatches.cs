using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(PrefabsDirectory), "PopulateShipItems")]

    internal class PrefabsDirectoryPatches
    {
        public static void Postfix(PrefabsDirectory __instance)
        {
            // fix coconuts' duplicate look text
            PrefabsDirectory.instance.shipItems[3].description = "";

            if (Plugin.itemPlaceHeightFix.Value)
            {
                __instance.shipItems[70].furniturePlaceHeight = 0.21f;
            }
            if (Plugin.mushroomFix.Value)
            {
                FixCollider(PrefabsDirectory.instance.directory[144]);
                FixCollider(PrefabsDirectory.instance.directory[145]);
            }

            if (Plugin.invRotationFix.Value)
            {
                for (int i = 311; i < 320; i++)
                {
                    FixBoxRot(ref __instance.shipItems[i]);
                }
                FixBoxRot(ref __instance.shipItems[387]);
                FixBoxRot(ref __instance.shipItems[388]);
                FixBoxRot(ref __instance.shipItems[389]);
                FixBoxRot(ref __instance.shipItems[373]);
                ChangeRot(ref __instance.shipItems[80], __instance.shipItems[80].heldRotationOffset, 180, 270);
                ChangeRot(ref __instance.shipItems[81], __instance.shipItems[81].heldRotationOffset, 180, 270);
                ChangeRot(ref __instance.shipItems[82], __instance.shipItems[82].heldRotationOffset, 180, 270);
                ChangeRot(ref __instance.shipItems[86], __instance.shipItems[86].heldRotationOffset, 180, 270);
                ChangeRot(ref __instance.shipItems[99], __instance.shipItems[99].heldRotationOffset, 90, 45);
                ChangeRot(ref __instance.shipItems[79], __instance.shipItems[79].heldRotationOffset, 180, __instance.shipItems[79].inventoryRotationX);
            }
        }

        private static void FixCollider(GameObject item)
        {
            var oldCol = item.GetComponent<SphereCollider>();
            if (oldCol == null)
            {
                return;
            }
            oldCol.enabled = false;
            var col = item.AddComponent<CapsuleCollider>();
            col.isTrigger = true;
            col.height = 0f;
            col.radius = oldCol.radius;
            col.center = oldCol.center;
            Component.Destroy(oldCol);
        }

        private static void ChangeRot(ref ShipItem item, float heldRot, float invRot, float invRotX)
        {
            item.heldRotationOffset = heldRot;
            item.inventoryRotation = invRot;
            item.inventoryRotationX = invRotX;
        }

        private static void FixBoxRot(ref ShipItem item)
        {
            item.heldRotationOffset = -45f;
            item.inventoryRotation = 180f;
            item.inventoryRotationX = 270f;
        }
    }
}
