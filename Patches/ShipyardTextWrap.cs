using HarmonyLib;

namespace NANDFixes.Patches
{
    internal class ShipyardTextWrap
    {
        [HarmonyPatch(typeof(ShipyardUIOrderText), "AddLine")]
        public static class OrderTextPatch
        {
            public static string arrow = "->";
            public static string wrapString2 = "(";
            public static void Prefix(ShipyardUIOrderText __instance, ref string line)
            {
                if (!Plugin.shipyardTextWrap.Value) return;
                if (line.Length > Plugin.orderWrapThreshold)
                {
                    if (line.Contains(arrow))
                    {
                        // add new line for first half
                        __instance.AddLine(line.Substring(0, line.IndexOf(arrow) + 2));

                        // second half of original is now second line
                        line = "--" + line.Substring(line.IndexOf(arrow));
                    }
                    else if (line.Contains(wrapString2))
                    {
                        // add new line for first half
                        __instance.AddLine(line.Substring(0, line.LastIndexOf(wrapString2)));

                        // second half of original is now second line
                        line = "      " + line.Substring(line.LastIndexOf(wrapString2));
                    }
                }
            }
        }
        [HarmonyPatch(typeof(ShipyardUI), "ChangePartsOptionText")]
        public static class PartNamePatch
        {
            public static void Prefix(ref string text)
            {
                if (!Plugin.shipyardTextWrap.Value) return;

                if (text.Length > 18 && text[0] != '(')
                {
                    // start lower than max to avoid single-letter second line
                    for (int i = 16; i > 1; i--)
                    {
                        if (text[i] == ' ')
                        {
                            text = text.Insert(i, "\n");
                            break;
                        }
                    }

                }
            }

        }
    }
}
