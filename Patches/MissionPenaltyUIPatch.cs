using HarmonyLib;
using System.Collections.Generic;

namespace NANDFixes.Patches
{
    [HarmonyPatch(typeof(PlayerMissions), "AbandonMission")]
    internal static class MissionPenaltyUIPatch
    {
        public static void Prefix(int missionIndex, ref string __state)
        {
            if (!Plugin.missionPenaltyFix.Value) return;
            string region = regionNames[PlayerMissions.missions[missionIndex].originPort.region] ?? "";
            var num = (PlayerMissions.missions[missionIndex].goodCount - PlayerMissions.missions[missionIndex].GetDeliveredCount()) * 100;
            __state = "Abandoning mission:\n" + PlayerMissions.missions[missionIndex].missionName + "\n-" + num.ToString() + " " + region + " reputation";
        }
        public static void Postfix(string __state)
        {
            if (!Plugin.missionPenaltyFix.Value) return;
            if (__state != null && __state.Length > 0)
            {
                NotificationUi.instance.ShowNotification(__state);
            }
        }
        static readonly Dictionary<PortRegion, string> regionNames = new Dictionary<PortRegion, string>()
        {
            { PortRegion.alankh, "Al\'Ankh" },
            { PortRegion.emerald, "Emerald" },
            { PortRegion.medi, "Aestrin" },
            { PortRegion.none, "" },
        };
    }
}
