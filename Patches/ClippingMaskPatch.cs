using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crest;
using HarmonyLib;
using UnityEngine;
using NANDFixes.Scripts;

namespace NANDFixes
{
    [HarmonyPatch(typeof(SaveableObject), "Awake")]
    internal static class ClippingMaskPatch
    {
        public static void Postfix(SaveableObject __instance)
        {
            if (__instance.sceneIndex != 20) return;

            RegisterClipSurfaceInput[] parts = __instance.GetComponentsInChildren<RegisterClipSurfaceInput>();
            foreach (RegisterClipSurfaceInput part in parts)
            {
                if (part.name == "interior_trigger")
                {
                    if (AssetTools.bundle == null) return;
                    var newMask = UnityEngine.Object.Instantiate(part, part.transform.parent);
                    UnityEngine.Object.Destroy(newMask.GetComponent<InteriorEffectsTrigger>());
                    UnityEngine.Object.Destroy(newMask.GetComponent<Collider>());
                    newMask.name = "newMask";
                    newMask.transform.localScale = new Vector3(1.14f, 1.14f, 1.68f);
                    newMask.transform.localPosition += Vector3.up * 0.68f;

                    var prefab = AssetTools.bundle.LoadAsset<GameObject>("Assets/san_mask_1.obj");
                    newMask.GetComponent<MeshFilter>().sharedMesh = prefab.GetComponentInChildren<MeshFilter>().sharedMesh;
                }
                part.enabled = false;
            }
        }
    }
}
