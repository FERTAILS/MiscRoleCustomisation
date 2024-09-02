using HarmonyLib;
using Home.Shared;
using Server.Shared.Extensions;
using Server.Shared.State;
using SML;

//AS if you yell at me once I hand you this im gonna fucking kill you
//if you want to touch or optimise anything feel free :flushed:

// :P - AS

namespace MiscRoleCustomisation;

[HarmonyPatch(typeof(ClientRoleExtensions), nameof(ClientRoleExtensions.ToColorizedDisplayString), typeof(Role), typeof(FactionType))]
public static class AddTTAndGradients
{
    [HarmonyPostfix]
    public static void Result(ref string __result, ref Role role, ref FactionType factionType)
    {
        string newtext = "";

        if (__result.Contains("<color=#B545FF>(Traitor)"))
            __result = __result.Replace("<color=#B545FF>", "<style=CovenColor>").Replace("</color>", "</style>");

        if (RoleExtensions.IsResolved(role) || role is Role.FAMINE or Role.DEATH or Role.PESTILENCE or Role.WAR)
        {
            string text = "";
            text = ClientRoleExtensions.ToDisplayString(role);
            text = ModSettings.GetBool("Faction-Specific Role Names") ? Utils.ToRoleFactionDisplayString(role, factionType) : ClientRoleExtensions.ToDisplayString(role);

            if (factionType.GetChangedGradient() != null)
                newtext = AddChangedConversionTags.ApplyGradient(text, factionType.GetChangedGradient());
            else
            {
                newtext = string.Concat(
                [
                    "<color=",
                    ClientRoleExtensions.GetFactionColor(factionType),
                    ">",
                    text,
                    "</color>"
                ]);
            }

            if (RoleExtensions.GetFaction(role) != factionType && factionType != FactionType.NONE && ModSettings.GetBool("Faction Name Next to Role"))
            {
                if (factionType is not ((FactionType)33 or (FactionType)44))
                {
                    if (factionType.GetChangedGradient() != null)
                    {
                        newtext += " " + AddChangedConversionTags.ApplyGradient("(" + factionType.ToDisplayString() + ")", factionType.GetChangedGradient().Evaluate(0f),
                            factionType.GetChangedGradient().Evaluate(1f));
                    }
                    else
                        newtext += " " + "<color=" + ClientRoleExtensions.GetFactionColor(factionType) + ">(" + factionType.ToDisplayString() + ")</color>";
                }
                else if (factionType == (FactionType)33)
                {
                    newtext += " " + AddChangedConversionTags.ApplyGradient("(" + ModSettings.GetString("Recruit Label", "det.rolecustomizationmod") + ")",
                        factionType.GetChangedGradient().Evaluate(0f), factionType.GetChangedGradient().Evaluate(1f));
                }
                else if (factionType == (FactionType)44)
                {
                    newtext += " " + AddChangedConversionTags.ApplyThreeColorGradient("(" + factionType.ToDisplayString() + ")", factionType.GetChangedGradient().Evaluate(0f),
                        factionType.GetChangedGradient().Evaluate(0.5f), factionType.GetChangedGradient().Evaluate(1f));
                }

            }

            __result = newtext;
        }
    }
}

[HarmonyPatch(typeof(ClientRoleExtensions), nameof(ClientRoleExtensions.GetFactionColor))]
public static class SwapColor
{
    [HarmonyPostfix]
    public static void Swap(ref string __result, ref FactionType factionType)
    {
        __result = (int)factionType switch
        {
            1 => ModSettings.GetString("Town Start", "det.rolecustomizationmod"),
            2 => ModSettings.GetString("Coven Start", "det.rolecustomizationmod"),
            3 => ModSettings.GetString("Serial Killer Start", "det.rolecustomizationmod"),
            4 => ModSettings.GetString("Arsonist Start", "det.rolecustomizationmod"),
            5 => ModSettings.GetString("Werewolf Start", "det.rolecustomizationmod"),
            6 => ModSettings.GetString("Shroud Start", "det.rolecustomizationmod"),
            7 => ModSettings.GetString("Apocalypse Start", "det.rolecustomizationmod"),
            8 => ModSettings.GetString("Executioner Start", "det.rolecustomizationmod"),
            9 => ModSettings.GetString("Jester Start", "det.rolecustomizationmod"),
            10 => ModSettings.GetString("Pirate Start", "det.rolecustomizationmod"),
            11 => ModSettings.GetString("Doomsayer Start", "det.rolecustomizationmod"),
            12 => ModSettings.GetString("Vampire Start", "det.rolecustomizationmod"),
            13 => ModSettings.GetString("Cursed Soul Start", "det.rolecustomizationmod"),
            33 => ModSettings.GetString("Jackal/Recruit Start", "det.rolecustomizationmod"),
            34 => ModSettings.GetString("Frogs Start", "det.rolecustomizationmod"),
            35 => ModSettings.GetString("Lions Start", "det.rolecustomizationmod"),
            36 => ModSettings.GetString("Hawks Start", "det.rolecustomizationmod"),
            38 => ModSettings.GetString("Judge Start", "det.rolecustomizationmod"),
            39 => ModSettings.GetString("Auditor Start", "det.rolecustomizationmod"),
            40 => ModSettings.GetString("Inquisitor Start", "det.rolecustomizationmod"),
            41 => ModSettings.GetString("Starspawn Start", "det.rolecustomizationmod"),
            42 => ModSettings.GetString("Egotist Start", "det.rolecustomizationmod"),
            43 => ModSettings.GetString("Pandora Start", "det.rolecustomizationmod"),
            44 => ModSettings.GetString("Compliance Start", "det.rolecustomizationmod"),
            _ => ModSettings.GetString("Stoned/Hidden", "det.rolecustomizationmod"),
        };
    }
}