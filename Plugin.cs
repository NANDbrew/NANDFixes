using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Reflection;

namespace NANDFixes
{
    [BepInPlugin(PLUGIN_ID, PLUGIN_NAME, PLUGIN_VERSION)]
    //[BepInDependency("com.app24.sailwindmoddinghelper", "2.0.3")]
    public class Plugin : BaseUnityPlugin
    {
        public const string PLUGIN_ID = "com.nandbrew.nandfixes";
        public const string PLUGIN_NAME = "NAND Fixes";
        public const string PLUGIN_VERSION = "1.0.5";

        //--settings--
        //internal static ConfigEntry<bool> hook_shelf;
        internal static ConfigEntry<bool> stickyFix;
        internal static ConfigEntry<bool> bedCamAdjust;
        internal static ConfigEntry<bool> playerEmbark;
        internal static ConfigEntry<bool> velocityFix;
        //internal static ConfigEntry<bool> clothFix;
        internal static ConfigEntry<bool> barrelPatches;

        private void Awake()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PLUGIN_ID);

            stickyFix = Config.Bind("StickyFix", "StickyFix", true, new ConfigDescription("Fix the infamous things-getting-stuck-to-other-boats bug"));
            bedCamAdjust = Config.Bind("misc.", "Bed camera adjustment", true, new ConfigDescription("Moves the sleep position in certain beds up a bit to fix the camera clipping through"));
            playerEmbark = Config.Bind("Embark", "Boat-to-boat embark fix", true, new ConfigDescription("Fix for the \"falling through the deck when jumping between boats\" issue"));
            velocityFix = Config.Bind("Velocity fix", "Item velocity fix", true, new ConfigDescription("Fix thrown items bouncing back out of boats"));
            //clothFix = Config.Bind("misc.", "Sailcloth fix", true, new ConfigDescription("Fix squished sailcloth"));
            barrelPatches = Config.Bind("misc.", "Barrel patches", true, new ConfigDescription("Fix squished sailcloth"));

        }
    }
}
