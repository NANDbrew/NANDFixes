using BepInEx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NANDFixes.Scripts
{
    internal class AssetTools
    {
        public static AssetBundle bundle;
        const string assetDir = "NANDFixes";
        const string assetFile = "nandfixes.assets";
        public static void LoadAssetBundles()    //Load the bundle
        {
            string dataPath = Directory.GetParent(Plugin.instance.Info.Location).FullName;
            string firstTry = Path.Combine(dataPath, assetDir, assetFile);
            string secondTry = Path.Combine(dataPath, assetFile);

            bundle = AssetBundle.LoadFromFile(File.Exists(firstTry) ? firstTry : secondTry);
            if (bundle == null)
            {
                Debug.LogError("nandfixes: Bundle not loaded! Did you place it in the correct folder?");
            }
            else { Debug.Log("nandfixes: loaded bundle " + bundle.ToString()); }
        }

    }
}
