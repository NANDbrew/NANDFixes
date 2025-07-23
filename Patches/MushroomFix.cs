using HarmonyLib;
using UnityEngine;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(ShipItemFood), "OnLoad")]
    public static class MushroomPatch
    {
        public static void Postfix(ShipItemFood __instance)
        {
            if (!Plugin.mushroomFix.Value) return;
            int index = __instance.GetPrefabIndex();
            if (index == 144 || index == 145)
            {
                var oldCol = __instance.gameObject.GetComponent<SphereCollider>();
                if (oldCol == null)
                {
                    return;
                }
                oldCol.enabled = false;
                var col = __instance.gameObject.AddComponent<CapsuleCollider>();
                col.isTrigger = true;
                col.height = 0f;
                col.radius = oldCol.radius;
                col.center = oldCol.center;
                Component.Destroy(oldCol);
            }
        }
    }
}
