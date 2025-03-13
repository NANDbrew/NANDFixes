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
        public const string PLUGIN_VERSION = "1.1.0";

        //--settings--
        //internal static ConfigEntry<bool> hook_shelf;
        internal static ConfigEntry<bool> stickyFix;
        internal static ConfigEntry<bool> bedCamAdjust;
        internal static ConfigEntry<bool> playerEmbark;
        internal static ConfigEntry<bool> velocityFix;
        internal static ConfigEntry<bool> clothFix;
        internal static ConfigEntry<bool> sailBlinkFix;
        internal static ConfigEntry<bool> barrelPatches;
        internal static ConfigEntry<bool> mastColPatch;
        internal static ConfigEntry<bool> buyUIPatch;
        //internal static ConfigEntry<bool> chipLogPatch;

        private void Awake()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PLUGIN_ID);

            stickyFix = Config.Bind("", "StickyFix", true, new ConfigDescription("Fix the infamous \"things getting stuck to other boats\" bug"));
            bedCamAdjust = Config.Bind("", "Bed camera adjustment", true, new ConfigDescription("Moves the sleep position in certain beds up a bit to fix the camera clipping through"));
            playerEmbark = Config.Bind("", "Boat-to-boat embark fix", true, new ConfigDescription("Fix for the \"falling through the deck when jumping between boats\" issue"));
            velocityFix = Config.Bind("", "Item velocity fix", true, new ConfigDescription("Fix thrown items bouncing back out of boats or flying the wrong way"));
            clothFix = Config.Bind("", "Sailcloth fix", true, new ConfigDescription("Fix squished/stretched sailcloth"));
            barrelPatches = Config.Bind("", "Barrel patches", true, new ConfigDescription("Fix accidentally drining from barrels"));
            sailBlinkFix = Config.Bind("", "Sail blinking fix", true, new ConfigDescription("Fix junk and junk square sails white blinky bug"));
            mastColPatch = Config.Bind("", "Mast item fix", true, new ConfigDescription("Fix the bug that makes items attached to masts un-targetable (requires restart)"));
            buyUIPatch = Config.Bind("", "Floating scroll fix", true, new ConfigDescription("Fix floating \"sell item\" menu"));
            //chipLogPatch = Config.Bind("", "chip log fix", true, new ConfigDescription("Fix floating \"sell item\" menu"));

        }
    }
}
