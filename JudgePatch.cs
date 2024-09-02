using HarmonyLib;
using Mentions;
using BetterTOS2;
using SML;
using Server.Shared.State;
using Services;
using Server.Shared.State.Chat;
using System.Data;
using Game.Simulation;
using System.Net;

namespace MiscRoleCustomisation;

[HarmonyPatch(typeof(MentionsProvider), nameof(MentionsProvider.ProcessSpeakerName))]
public static class PatchJudge
{
    public static void Postfix(string encodedText, int position, bool isAlive, ref string __result)
    {
        if (Utils.IsBTOS2())
        {
            if (position == 70)
            {
                __result = "<link=\"r57\"><sprite=\"BTOSRoleIcons\" name=\"Role57\"><indent=1.1em><b>" + AddNewConversionTags.ApplyGradient(ModSettings.GetString("Court Label",
                    "det.rolecustomizationmod"), ModSettings.GetString("Judge Start", "det.rolecustomizationmod").ParseColor(), ModSettings.GetString("Judge End", "det.rolecustomizationmod")
                    .ParseColor()) + ":" + "</b> </link>" + encodedText.Replace("????: </color>", "").Replace("white", "#FFFF00");
            }
            else if (position == 69)
                __result = encodedText.Replace("????:", $"<sprite=\"BTOSRoleIcons\" name=\"Role16\"> {ModSettings.GetString("Jury Label", "det.rolecustomizationmod")}:");
            else if (position == 71)
            {
                __result = "<link=\"r46\"><sprite=\"BTOSRoleIcons\" name=\"Role46\"><indent=1.1em><b>" + AddNewConversionTags.ApplyGradient(ModSettings.GetString("Pirate Label",
                    "det.rolecustomizationmod"), ModSettings.GetString("Pirate Start", "det.rolecustomizationmod").ParseColor(), ModSettings.GetString("Pirate End",
                    "det.rolecustomizationmod").ParseColor()) + ":</b> </link>" + encodedText.Replace("????: </color>", "").Replace("white", "#ECC23E");
            }
        }
    }
}