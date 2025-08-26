using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using NANDFixes.Scripts;
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
        public const string PLUGIN_VERSION = "1.2.5";

        //--settings--
        //internal static ConfigEntry<bool> hook_shelf;
        internal static ConfigEntry<bool> stickyFix;
        internal static ConfigEntry<bool> aggressiveSF;
        internal static ConfigEntry<bool> bedCamAdjust;
        internal static ConfigEntry<bool> playerEmbark;
        internal static ConfigEntry<bool> velocityFix;
        internal static ConfigEntry<bool> clothFix;
        internal static ConfigEntry<bool> sailBlinkFix;
        internal static ConfigEntry<bool> barrelPatches;
        internal static ConfigEntry<bool> mastColPatch;
        internal static ConfigEntry<bool> buyUIPatch;
        internal static ConfigEntry<bool> albacoreFix;
        internal static ConfigEntry<bool> spinFix;
        internal static ConfigEntry<bool> cleaningFix;
        internal static ConfigEntry<bool> craneFix;
        internal static ConfigEntry<bool> boatColFix;
        internal static ConfigEntry<bool> flyingCarpetFix;
        internal static ConfigEntry<bool> invRotationFix;
        internal static ConfigEntry<bool> mushroomFix;
        internal static ConfigEntry<bool> mapFix;
        internal static int threshold = 1000;

        public static Plugin instance;

        private void Awake()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PLUGIN_ID);
            instance = this;
            AssetTools.LoadAssetBundles();

            stickyFix = Config.Bind("", "StickyFix", true, new ConfigDescription("Fix the infamous \"things getting stuck to other boats\" bug"));
            aggressiveSF = Config.Bind("", "Aggressive StickyFix", false, new ConfigDescription("Should stickyFix prevent items that are stationary or in water from embarking?\nOnly applies when StickyFix is enabled"));

            mapFix = Config.Bind("", "Map Fix", true, new ConfigDescription("fixes rolled maps unrolling themselves on game load"));
            mushroomFix = Config.Bind("", "Mushroom Fix", true, new ConfigDescription("fixes cave mushrooms and field mushrooms falling through the floor"));
            invRotationFix = Config.Bind("", "Inventory rotation adjustments", true, new ConfigDescription("fixes small boxes, compasses and totems facing the wrong way in inventory"));
            flyingCarpetFix = Config.Bind("", "Flying Carpet Fix", true, new ConfigDescription("prevents sleeping in a bed you're holding"));
            boatColFix = Config.Bind("", "Boat Collider Fix", true, new ConfigDescription("makes boats less likely to phase through things"));
            craneFix = Config.Bind("", "Crane Fix", true, new ConfigDescription("fix gold rock city's shipyard crane colliders"));
            spinFix = Config.Bind("", "Spin Fix", true, new ConfigDescription("keep boats from spinning while the player is in a shipyard"));
            cleaningFix = Config.Bind("", "Cleaning Fix", true, new ConfigDescription("workaround for the Jong's shipyard crash"));
            bedCamAdjust = Config.Bind("", "Bed camera adjustment", true, new ConfigDescription("Moves the sleep position in certain beds up a bit to fix the camera clipping through"));
            playerEmbark = Config.Bind("", "Boat-to-boat embark fix", true, new ConfigDescription("Fix for the \"falling through the deck when jumping between boats\" issue"));
            velocityFix = Config.Bind("", "Item velocity fix", true, new ConfigDescription("Fix thrown items bouncing back out of boats or flying the wrong way"));
            clothFix = Config.Bind("", "Sailcloth fix", true, new ConfigDescription("Fix squished/stretched sailcloth"));
            barrelPatches = Config.Bind("", "Barrel patches", true, new ConfigDescription("Fix accidentally drinking from barrels"));
            sailBlinkFix = Config.Bind("", "Sail blinking fix", true, new ConfigDescription("Fix junk and junk square sails white blinky bug"));
            mastColPatch = Config.Bind("", "Mast item fix", true, new ConfigDescription("Fix the bug that makes items attached to masts un-targetable (requires restart)"));
            buyUIPatch = Config.Bind("", "Floating scroll fix", true, new ConfigDescription("Fix floating \"sell item\" menu"));
            albacoreFix = Config.Bind("", "Rotten albacore fix", true, new ConfigDescription("Fix Gold Albacore starting out rotten and being unslicable"));

        }
    }
}
