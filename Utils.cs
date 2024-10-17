using UnityEngine;
using Home.Shared;
using Server.Shared.State;
using Services;
using SML;

namespace MiscRoleCustomisation;

public static class Utils
{
    public static bool GetRoleInfo(int pos, out Tuple<Role, FactionType> value) => Service.Game.Sim.simulation.knownRolesAndFactions.Data.TryGetValue(pos, out value);

    public static string ToRoleFactionDisplayString(Role role, FactionType faction)
    {
        if (role is Role.STONED or Role.HIDDEN or Role.UNKNOWN) // You forgot to account for Unknown
            faction = FactionType.NONE;

        var tag = faction switch
        {
            FactionType.TOWN => "TOWN",
            FactionType.COVEN => "COVEN",
            FactionType.APOCALYPSE => "APOC",
            FactionType.SERIALKILLER => "SERIALKILLER",
            FactionType.ARSONIST => "ARSO",
            FactionType.WEREWOLF => "WEREWOLF",
            FactionType.SHROUD => "SHROUD",
            FactionType.EXECUTIONER => "EXE",
            FactionType.JESTER => "JEST",
            FactionType.PIRATE => "PIRATE",
            FactionType.DOOMSAYER => "DOOM",
            (FactionType)40 => "INQUIS",
            FactionType.VAMPIRE => "VAMPIRE",
            FactionType.CURSED_SOUL => "CURSEDSOUL",
            (FactionType)33 => "JACKAL",
            (FactionType)34 => "FROG",
            (FactionType)35 => "LION",
            (FactionType)36 => "HAWK",
            (FactionType)38 => "JUDGE",
            (FactionType)39 => "AUDI",
            (FactionType)41 => "STARSPAWN",
            (FactionType)42 => "EGOTIST",
            (FactionType)43 => "PANDORA",
            (FactionType)44 => "COMPLIANCE",
            _ => "NONE",
        };

        if (!IsBTOS2())
            tag += "_VANILLA";

        return Service.Home.LocalizationService.GetLocalizedString($"{tag}_ROLENAME_{(int)role}");
    }

    public static Color GetFactionStartingColor(FactionType faction) => faction switch
    {
        (FactionType)1 => ModSettings.GetColor("Town Start", "det.rolecustomizationmod"),
        (FactionType)2 => ModSettings.GetColor("Coven Start", "det.rolecustomizationmod"),
        (FactionType)7 => ModSettings.GetColor("Apocalypse Start", "det.rolecustomizationmod"),
        (FactionType)8 => ModSettings.GetColor("Executioner Start", "det.rolecustomizationmod"),
        (FactionType)3 => ModSettings.GetColor("Serial Killer Start", "det.rolecustomizationmod"),
        (FactionType)4 => ModSettings.GetColor("Arsonist Start", "det.rolecustomizationmod"),
        (FactionType)5 => ModSettings.GetColor("Werewolf Start", "det.rolecustomizationmod"),
        (FactionType)6 => ModSettings.GetColor("Shroud Start", "det.rolecustomizationmod"),
        (FactionType)9 => ModSettings.GetColor("Jester Start", "det.rolecustomizationmod"),
        (FactionType)40 => ModSettings.GetColor("Inquisitor Start", "det.rolecustomizationmod"),
        (FactionType)10 => ModSettings.GetColor("Pirate Start", "det.rolecustomizationmod"),
        (FactionType)11 => ModSettings.GetColor("Doomsayer Start", "det.rolecustomizationmod"),
        (FactionType)12 => ModSettings.GetColor("Vampire Start", "det.rolecustomizationmod"),
        (FactionType)13 => ModSettings.GetColor("Cursed Soul Start", "det.rolecustomizationmod"),
        (FactionType)33 => ModSettings.GetColor("Jackal/Recruit Start", "det.rolecustomizationmod"),
        (FactionType)38 => ModSettings.GetColor("Judge Start", "det.rolecustomizationmod"),
        (FactionType)39 => ModSettings.GetColor("Auditor Start", "det.rolecustomizationmod"),
        (FactionType)41 => ModSettings.GetColor("Starspawn Start", "det.rolecustomizationmod"),
        (FactionType)42 => ModSettings.GetColor("Egotist Start", "det.rolecustomizationmod"),
        (FactionType)43 => ModSettings.GetColor("Pandora Start", "det.rolecustomizationmod"),
        (FactionType)34 => ModSettings.GetColor("Frogs Start", "det.rolecustomizationmod"),
        (FactionType)35 => ModSettings.GetColor("Lions Start", "det.rolecustomizationmod"),
        (FactionType)36 => ModSettings.GetColor("Hawks Start", "det.rolecustomizationmod"),
        (FactionType)44 => ModSettings.GetColor("Compliance Start", "det.rolecustomizationmod"),
        _ => ModSettings.GetColor("Stoned/Hidden", "det.rolecustomizationmod"),
    };

    public static Color GetFactionEndingColor(FactionType faction) => faction switch
    {
        (FactionType)1 => ModSettings.GetColor("Town End", "det.rolecustomizationmod"),
        (FactionType)2 => ModSettings.GetColor("Coven End", "det.rolecustomizationmod"),
        (FactionType)7 => ModSettings.GetColor("Apocalypse End", "det.rolecustomizationmod"),
        (FactionType)8 => ModSettings.GetColor("Executioner End", "det.rolecustomizationmod"),
        (FactionType)3 => ModSettings.GetColor("Serial Killer End", "det.rolecustomizationmod"),
        (FactionType)4 => ModSettings.GetColor("Arsonist End", "det.rolecustomizationmod"),
        (FactionType)5 => ModSettings.GetColor("Werewolf End", "det.rolecustomizationmod"),
        (FactionType)6 => ModSettings.GetColor("Shroud End", "det.rolecustomizationmod"),
        (FactionType)9 => ModSettings.GetColor("Jester End", "det.rolecustomizationmod"),
        (FactionType)40 => ModSettings.GetColor("Inquisitor End", "det.rolecustomizationmod"),
        (FactionType)10 => ModSettings.GetColor("Pirate End", "det.rolecustomizationmod"),
        (FactionType)11 => ModSettings.GetColor("Doomsayer End", "det.rolecustomizationmod"),
        (FactionType)12 => ModSettings.GetColor("Vampire End", "det.rolecustomizationmod"),
        (FactionType)13 => ModSettings.GetColor("Cursed Soul End", "det.rolecustomizationmod"),
        (FactionType)33 => ModSettings.GetColor("Jackal/Recruit End", "det.rolecustomizationmod"),
        (FactionType)38 => ModSettings.GetColor("Judge End", "det.rolecustomizationmod"),
        (FactionType)39 => ModSettings.GetColor("Auditor End", "det.rolecustomizationmod"),
        (FactionType)41 => ModSettings.GetColor("Starspawn End", "det.rolecustomizationmod"),
        (FactionType)42 => ModSettings.GetColor("Egotist End", "det.rolecustomizationmod"),
        (FactionType)43 => ModSettings.GetColor("Pandora End", "det.rolecustomizationmod"),
        (FactionType)34 => ModSettings.GetColor("Frogs End", "det.rolecustomizationmod"),
        (FactionType)35 => ModSettings.GetColor("Lions End", "det.rolecustomizationmod"),
        (FactionType)36 => ModSettings.GetColor("Hawks End", "det.rolecustomizationmod"),
        (FactionType)44 => ModSettings.GetColor("Compliance End", "det.rolecustomizationmod"),
        _ => ModSettings.GetColor("Stoned/Hidden", "det.rolecustomizationmod"),
    };

    public static Color GetPlayerRoleColor(int pos)
    {
        var color = Color.white;
        Service.Game.Sim.simulation.knownRolesAndFactions.Data.TryGetValue(pos, out Tuple<Role, FactionType> tuple);
        Color color2;

        if (tuple == null)
            color2 = color;
        else
        {
            if (tuple.Item1 is (Role)254 or (Role)241)
                color2 = color;
            else
            {
                color = ClientRoleExtensions.GetFactionColor(tuple.Item2).ParseColor();
                color2 = color;
            }
        }

        return color2;
    }

    public static string ToDisplayString(this FactionType faction) => Service.Home.LocalizationService.GetLocalizedString($"BTOS_FACTIONNAME_{(int)faction}");

    public static bool BTOS2Exists() => ModStates.IsEnabled("curtis.tuba.better.tos2");

    public static bool IsBTOS2()
    {
        try
        {
            return IsBTOS2Bypass() || FindCasualQueue();
        }
        catch
        {
            return false;
        }
    }

    private static bool IsBTOS2Bypass() => BTOS2Exists() && BetterTOS2.BTOSInfo.IS_MODDED;

    public static bool FindCasualQueue()
    {
        try
        {
            return FindCasualQueueBypass();
        }
        catch
        {
            return false;
        }
    }

    private static bool FindCasualQueueBypass() => BTOS2Exists() && BetterTOS2.BTOSInfo.CasualModeController;
}