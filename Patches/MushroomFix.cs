using HarmonyLib;
using UnityEngine;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(PrefabsDirectory), "Start")]
    public static class MushroomPatch
    {
        public static void Postfix()
        {
            if (!Plugin.mushroomFix.Value) return;

            FixCollider(PrefabsDirectory.instance.directory[144]);
            FixCollider(PrefabsDirectory.instance.directory[145]);
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
    }
}
