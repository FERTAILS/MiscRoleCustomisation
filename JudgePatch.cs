using HarmonyLib;
using Mentions;
using SML;

namespace MiscRoleCustomisation;

[HarmonyPatch(typeof(MentionsProvider), nameof(MentionsProvider.ProcessSpeakerName))]
public static class PatchJudge
{
    public static void Postfix(string encodedText, int position, ref string __result)
    {
        if (Utils.IsBTOS2())
        {
            if (position == 70)
            {
                __result = "<link=\"r57\"><sprite=\"BTOSRoleIcons\" name=\"Role57\"><indent=1.1em><b>" + AddChangedConversionTags.ApplyGradient(ModSettings.GetString("Court Label",
                    "det.rolecustomizationmod"), ModSettings.GetColor("Judge Start", "det.rolecustomizationmod"), ModSettings.GetColor("Judge End", "det.rolecustomizationmod")) + ":" +
                    "</b> </link>" + encodedText.Replace("????: </color>", "").Replace("white", "#FFFF00");
            }
            else if (position == 69)
                __result = encodedText.Replace("????:", $"<sprite=\"BTOSRoleIcons\" name=\"Role16\"> {ModSettings.GetColor("Jury Label", "det.rolecustomizationmod")}:");
            else if (position == 71)
            {
                __result = "<link=\"r46\"><sprite=\"BTOSRoleIcons\" name=\"Role46\"><indent=1.1em><b>" + AddChangedConversionTags.ApplyGradient(ModSettings.GetString("Pirate Label",
                    "det.rolecustomizationmod"), ModSettings.GetColor("Pirate Start", "det.rolecustomizationmod"), ModSettings.GetColor("Pirate End",
                    "det.rolecustomizationmod")) + ":</b> </link>" + encodedText.Replace("????: </color>", "").Replace("white", "#ECC23E");
            }
        }
    }
}