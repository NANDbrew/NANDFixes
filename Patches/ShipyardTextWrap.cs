using HarmonyLib;

namespace NANDFixes.Patches
{
    internal class ShipyardTextWrap
    {
        [HarmonyPatch(typeof(ShipyardUIOrderText), "AddLine")]
        public static class OrderTextPatch
        {
            public static string wrapString = "->";
            public static void Prefix(ShipyardUIOrderText __instance, ref string line)
            {
                if (line.Length > 45 && line.Contains(wrapString))
                {
                    // add new line for first half
                    __instance.AddLine(line.Substring(0, line.IndexOf(wrapString) + 2));

                    // second half of original is now second line
                    line = "--" + line.Substring(line.IndexOf(wrapString));
                }
            }
        }
        [HarmonyPatch(typeof(ShipyardUI), "ChangePartsOptionText")]
        public static class PartNamePatch
        {
            public static void Prefix(ref string text)
            {
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
