using HarmonyLib;
using UnityEngine;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(ItemRigidbody))]
    internal class SaltFix
    {
        [HarmonyPatch("UpdateMass")]
        [HarmonyPrefix]
        public static bool Prefix(Rigidbody ___rigidbody)
        {
            if (Plugin.saltFix.Value && ___rigidbody == null)
            {
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(MissionDetailsUI))]
    internal class WeightFix
    {
        [HarmonyPatch("GetCargoWeight")]
        [HarmonyPostfix]
        public static void Postfix(GameObject goodPrefab, ref float __result)
        {
            if (Plugin.saltFix.Value)
            {
                //ShipItem good = PrefabsDirectory.instance.GetGood(goodPrefab.GetComponent<ShipItem>().GetPrefabIndex());
                //Good component = good.GetComponent<Good>();
                //textGoodName.text = good.name;
                //textWeightSize.text = component.sizeDescription + "\n" + component.GetCargoWeight();
                //__result = component.GetCargoWeight();
                if (goodPrefab.GetComponent<ShipItemTea>() is ShipItemTea tea)
                {
                    __result = tea.mass + tea.amount * 0.1f;
                }
                else if (goodPrefab.GetComponent<ShipItemSalt>() is ShipItemSalt salt)
                {
                    __result = salt.mass + salt.amount * 0.1f;
                }
            }
        }
    }

    [HarmonyPatch(typeof(Good))]
    internal class WeightFix2
    {
        [HarmonyPatch("GetCargoWeight")]
        [HarmonyPostfix]
        public static void Postfix(Good __instance, ref float __result)
        {
            if (Plugin.saltFix.Value)
            {
                if (__instance.GetComponent<ShipItemTea>() is ShipItemTea tea)
                {
                    __result = tea.mass + tea.amount * 0.1f;
                }
                else if (__instance.GetComponent<ShipItemSalt>() is ShipItemSalt salt)
                {
                    __result = salt.mass + salt.amount * 0.1f;
                }
            }
        }
    }


    [HarmonyPatch(typeof(ShipItemSalt), "UpdateLookText")]
    public static class SaltTextFix
    {
        [HarmonyPrefix]
        public static bool UpdateLookText(ShipItemSalt __instance, Good ___good)
        {
            if (!Plugin.saltFix.Value) return true;
            if (!__instance.sold)
            {
                __instance.lookText = "used for preserving food";
            }
            else if ((bool)___good && ___good.GetMissionIndex() > -1)
            {
                Mission mission = PlayerMissions.missions[___good.GetMissionIndex()];
                __instance.lookText = "salt\nto " + mission.destinationPort.GetPortName() + "\ndue: " + mission.GetDueText();
            }
            else if (__instance.amount <= 0f)
            {
                __instance.lookText = "salt\n0";
            }
            else
            {
                __instance.lookText = "salt\n";
                __instance.description = Mathf.RoundToInt(__instance.amount).ToString();
                //__instance.description = System.Math.Round(__instance.amount * 0.1, 2) + " lbs";
            }
            return false;
        }
        
    }
}
