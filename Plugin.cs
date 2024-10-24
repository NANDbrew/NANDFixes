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
        public const string PLUGIN_VERSION = "1.0.4";

        //--settings--
        //internal static ConfigEntry<bool> hook_shelf;
        internal static ConfigEntry<bool> stickyFix;
        internal static ConfigEntry<bool> bedCamAdjust;
        internal static ConfigEntry<bool> playerEmbark;

        private void Awake()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PLUGIN_ID);

            //hook_shelf = Config.Bind("StickyFix", "Hook & Shelf fixer (obsolete)", false, new ConfigDescription("Keep lantern hooks and shelves stuck to boat. If they're causing issues, turn this off (does nothing if StickyFix is off)"));
            stickyFix = Config.Bind("StickyFix", "StickyFix", true, new ConfigDescription("Fix the infamous things-getting-stuck-to-other-boats bug"));
            bedCamAdjust = Config.Bind("misc.", "Bed camera adjustment", true, new ConfigDescription("Moves the sleep position in certain beds up a bit to fix the camera clipping through"));
            playerEmbark = Config.Bind("Embark", "Boat-to-boat embark fix", true, new ConfigDescription("Fix for the \"falling through the deck when jumping between boats\" issue"));

        }
    }
}
