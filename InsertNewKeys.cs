/*using System.Xml;
using Home.Services;
using HarmonyLib;
using SML;
using UnityEngine;

namespace MiscRoleCustomisation;

[HarmonyPatch(typeof(HomeLocalizationService), nameof(HomeLocalizationService.RebuildStringTables))]
public static class DumpStringTables
{
    [HarmonyPostfix]
    public static void LoadCustomXML(HomeLocalizationService __instance)
    {
        XmlDocument xmlDocument = new();
        xmlDocument.LoadXml(FromResources.LoadString("MiscRoleCustomisation.Resources.ModdedColourSwapper.StringTable.xml"));
        xmlDocument.LoadXml(FromResources.LoadString("MiscRoleCustomisation.Resources.ModdedColourSwapper.Vanilla.StringTable.xml"));
        XMLStringTable.StringTable stringTable = XMLStringTable.Load(xmlDocument);

        foreach (XMLStringTable.TextEntry textEntry in stringTable.entries)
        {
            __instance.stringTable_[textEntry.key] = textEntry.value;

            if (!string.IsNullOrEmpty(textEntry.style))
            {
                if (!__instance.styleTable_.ContainsKey(textEntry.key))
                    __instance.styleTable_.Add(textEntry.key, textEntry.style);
                else
                    Debug.LogWarning($"Duplicate Style Table Key \"{textEntry.key}\"!");
            }
        }
    }
}*/

// patch dosent work, common alch L