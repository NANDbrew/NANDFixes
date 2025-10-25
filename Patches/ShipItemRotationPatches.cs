using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(PrefabsDirectory), "PopulateShipItems")]
    internal static class InvRotPatch
    {
        public static void Postfix(PrefabsDirectory __instance)
        {
            if (!Plugin.invRotationFix.Value) return;
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
            ChangeRot(ref __instance.shipItems[79], 180, __instance.shipItems[79].inventoryRotation, __instance.shipItems[79].inventoryRotationX);

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
