using System;
using System.Collections.Generic;
using UnityEngine;
using Home.Shared;
using Server.Shared.State;
using Services;
using SML;
using System.IO;
using Game.Services;
using Game.Simulation;
using Server.Shared.Info;

namespace MiscRoleCustomisation;

public static class Utils
{
    private static string directoryPath;

    public static bool IsRoleKnown(int pos)
    {
        Service.Game.Sim.simulation.knownRolesAndFactions.Data.TryGetValue(pos, out Tuple<Role, FactionType> tuple);
        return tuple != null;
    }

    public static void CheckDirectory()
    {
        directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "SalemModLoader", "ModFolders", "ModdedColourSwapper");
        Debug.Log(directoryPath);

        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);
    }

    public static string ToRoleFactionDisplayString(Role role, FactionType faction)
    {
        if (role is Role.STONED or Role.HIDDEN or Role.UNKNOWN) // You forgot to account for Unknown
            faction = FactionType.NONE;

        if (IsBTOS2())
        {
            return faction switch
            {
                FactionType.TOWN => Service.Home.LocalizationService.GetLocalizedString($"TOWN_ROLENAME_{(int)role}"),
                FactionType.COVEN => Service.Home.LocalizationService.GetLocalizedString($"COVEN_ROLENAME_{(int)role}"),
                FactionType.APOCALYPSE => Service.Home.LocalizationService.GetLocalizedString($"APOC_ROLENAME_{(int)role}"),
                FactionType.SERIALKILLER => Service.Home.LocalizationService.GetLocalizedString($"SERIALKILLER_ROLENAME_{(int)role}"),
                FactionType.ARSONIST => Service.Home.LocalizationService.GetLocalizedString($"ARSO_ROLENAME_{(int)role}"),
                FactionType.WEREWOLF => Service.Home.LocalizationService.GetLocalizedString($"WEREWOLF_ROLENAME_{(int)role}"),
                FactionType.SHROUD => Service.Home.LocalizationService.GetLocalizedString($"SHROUD_ROLENAME_{(int)role}"),
                FactionType.EXECUTIONER => Service.Home.LocalizationService.GetLocalizedString($"EXE_ROLENAME_{(int)role}"),
                FactionType.JESTER => Service.Home.LocalizationService.GetLocalizedString($"JEST_ROLENAME_{(int)role}"),
                FactionType.PIRATE => Service.Home.LocalizationService.GetLocalizedString($"PIRATE_ROLENAME_{(int)role}"),
                FactionType.DOOMSAYER => Service.Home.LocalizationService.GetLocalizedString($"DOOM_ROLENAME_{(int)role}"),
                (FactionType)40 => Service.Home.LocalizationService.GetLocalizedString($"INQUIS_ROLENAME_{(int)role}"),
                FactionType.VAMPIRE => Service.Home.LocalizationService.GetLocalizedString($"VAMPIRE_ROLENAME_{(int)role}"),
                FactionType.CURSED_SOUL => Service.Home.LocalizationService.GetLocalizedString($"CURSEDSOUL_ROLENAME_{(int)role}"),
                (FactionType)33 => Service.Home.LocalizationService.GetLocalizedString($"JACKAL_ROLENAME_{(int)role}"),
                (FactionType)34 => Service.Home.LocalizationService.GetLocalizedString($"FROG_ROLENAME_{(int)role}"),
                (FactionType)35 => Service.Home.LocalizationService.GetLocalizedString($"LION_ROLENAME_{(int)role}"),
                (FactionType)36 => Service.Home.LocalizationService.GetLocalizedString($"HAWK_ROLENAME_{(int)role}"),
                (FactionType)38 => Service.Home.LocalizationService.GetLocalizedString($"JUDGE_ROLENAME_{(int)role}"),
                (FactionType)39 => Service.Home.LocalizationService.GetLocalizedString($"AUDI_ROLENAME_{(int)role}"),
                (FactionType)41 => Service.Home.LocalizationService.GetLocalizedString($"STARSPAWN_ROLENAME_{(int)role}"),
                (FactionType)42 => Service.Home.LocalizationService.GetLocalizedString($"EGOTIST_ROLENAME_{(int)role}"),
                (FactionType)43 => Service.Home.LocalizationService.GetLocalizedString($"PANDORA_ROLENAME_{(int)role}"),
                (FactionType)44 => Service.Home.LocalizationService.GetLocalizedString($"COMPLIANCE_ROLENAME_{(int)role}"),
                _ => Service.Home.LocalizationService.GetLocalizedString($"NONE_ROLENAME_{(int)role}"),
            };
        }
        else if (!IsBTOS2())
        {
            return faction switch
            {
                FactionType.TOWN => Service.Home.LocalizationService.GetLocalizedString($"TOWN_VANILLA_ROLENAME_{(int)role}"),
                FactionType.COVEN => Service.Home.LocalizationService.GetLocalizedString($"COVEN_VANILLA_ROLENAME_{(int)role}"),
                FactionType.APOCALYPSE => Service.Home.LocalizationService.GetLocalizedString($"APOC_VANILLA_ROLENAME_{(int)role}"),
                FactionType.SERIALKILLER => Service.Home.LocalizationService.GetLocalizedString($"SERIALKILLER_VANILLA_ROLENAME_{(int)role}"),
                FactionType.ARSONIST => Service.Home.LocalizationService.GetLocalizedString($"ARSO_VANILLA_ROLENAME_{(int)role}"),
                FactionType.WEREWOLF => Service.Home.LocalizationService.GetLocalizedString($"WEREWOLF_VANILLA_ROLENAME_{(int)role}"),
                FactionType.SHROUD => Service.Home.LocalizationService.GetLocalizedString($"SHROUD_VANILLA_ROLENAME_{(int)role}"),
                FactionType.EXECUTIONER => Service.Home.LocalizationService.GetLocalizedString($"EXE_VANILLA_ROLENAME_{(int)role}"),
                FactionType.JESTER => Service.Home.LocalizationService.GetLocalizedString($"JEST_VANILLA_ROLENAME_{(int)role}"),
                FactionType.PIRATE => Service.Home.LocalizationService.GetLocalizedString($"PIRATE_VANILLA_ROLENAME_{(int)role}"),
                FactionType.DOOMSAYER => Service.Home.LocalizationService.GetLocalizedString($"DOOM_VANILLA_ROLENAME_{(int)role}"),
                FactionType.VAMPIRE => Service.Home.LocalizationService.GetLocalizedString($"VAMPIRE_VANILLA_ROLENAME_{(int)role}"),
                FactionType.CURSED_SOUL => Service.Home.LocalizationService.GetLocalizedString($"CURSEDSOUL_VANILLA_ROLENAME_{(int)role}"),
                _ => Service.Home.LocalizationService.GetLocalizedString($"NONE_VANILLA_ROLENAME_{(int)role}"),
            };
        }

        return Service.Home.LocalizationService.GetLocalizedString($"NONE_ROLENAME_{(int)role}");
    }

    public static Color GetFactionStartingColor(FactionType faction) => faction switch
    {
        (FactionType)1 => ModSettings.GetString("Town Start", "det.rolecustomizationmod").ParseColor(),
        (FactionType)2 => ModSettings.GetString("Coven Start", "det.rolecustomizationmod").ParseColor(),
        (FactionType)7 => ModSettings.GetString("Apocalypse Start", "det.rolecustomizationmod").ParseColor(),
        (FactionType)8 => ModSettings.GetString("Executioner Start", "det.rolecustomizationmod").ParseColor(),
        (FactionType)3 => ModSettings.GetString("Serial Killer Start", "det.rolecustomizationmod").ParseColor(),
        (FactionType)4 => ModSettings.GetString("Arsonist Start", "det.rolecustomizationmod").ParseColor(),
        (FactionType)5 => ModSettings.GetString("Werewolf Start", "det.rolecustomizationmod").ParseColor(),
        (FactionType)6 => ModSettings.GetString("Shroud Start", "det.rolecustomizationmod").ParseColor(),
        (FactionType)9 => ModSettings.GetString("Jester Start", "det.rolecustomizationmod").ParseColor(),
        (FactionType)40 => ModSettings.GetString("Inquisitor Start", "det.rolecustomizationmod").ParseColor(),
        (FactionType)10 => ModSettings.GetString("Pirate Start", "det.rolecustomizationmod").ParseColor(),
        (FactionType)11 => ModSettings.GetString("Doomsayer Start", "det.rolecustomizationmod").ParseColor(),
        (FactionType)12 => ModSettings.GetString("Vampire Start", "det.rolecustomizationmod").ParseColor(),
        (FactionType)13 => ModSettings.GetString("Cursed Soul Start", "det.rolecustomizationmod").ParseColor(),
        (FactionType)33 => ModSettings.GetString("Jackal/Recruit Start", "det.rolecustomizationmod").ParseColor(),
        (FactionType)38 => ModSettings.GetString("Judge Start", "det.rolecustomizationmod").ParseColor(),
        (FactionType)39 => ModSettings.GetString("Auditor Start", "det.rolecustomizationmod").ParseColor(),
        (FactionType)41 => ModSettings.GetString("Starspawn Start", "det.rolecustomizationmod").ParseColor(),
        (FactionType)42 => ModSettings.GetString("Egotist Start", "det.rolecustomizationmod").ParseColor(),
        (FactionType)43 => ModSettings.GetString("Pandora Start", "det.rolecustomizationmod").ParseColor(),
        (FactionType)34 => ModSettings.GetString("Frogs Start", "det.rolecustomizationmod").ParseColor(),
        (FactionType)35 => ModSettings.GetString("Lions Start", "det.rolecustomizationmod").ParseColor(),
        (FactionType)36 => ModSettings.GetString("Hawks Start", "det.rolecustomizationmod").ParseColor(),
        (FactionType)44 => ModSettings.GetString("Compliance Start", "det.rolecustomizationmod").ParseColor(),
        _ => ModSettings.GetString("Stoned/Hidden", "det.rolecustomizationmod").ParseColor(),
    };

    public static Color GetFactionEndingColor(FactionType faction) => faction switch
    {
        (FactionType)1 => ModSettings.GetString("Town End", "det.rolecustomizationmod").ParseColor(),
        (FactionType)2 => ModSettings.GetString("Coven End", "det.rolecustomizationmod").ParseColor(),
        (FactionType)7 => ModSettings.GetString("Apocalypse End", "det.rolecustomizationmod").ParseColor(),
        (FactionType)8 => ModSettings.GetString("Executioner End", "det.rolecustomizationmod").ParseColor(),
        (FactionType)3 => ModSettings.GetString("Serial Killer End", "det.rolecustomizationmod").ParseColor(),
        (FactionType)4 => ModSettings.GetString("Arsonist End", "det.rolecustomizationmod").ParseColor(),
        (FactionType)5 => ModSettings.GetString("Werewolf End", "det.rolecustomizationmod").ParseColor(),
        (FactionType)6 => ModSettings.GetString("Shroud End", "det.rolecustomizationmod").ParseColor(),
        (FactionType)9 => ModSettings.GetString("Jester End", "det.rolecustomizationmod").ParseColor(),
        (FactionType)40 => ModSettings.GetString("Inquisitor End", "det.rolecustomizationmod").ParseColor(),
        (FactionType)10 => ModSettings.GetString("Pirate End", "det.rolecustomizationmod").ParseColor(),
        (FactionType)11 => ModSettings.GetString("Doomsayer End", "det.rolecustomizationmod").ParseColor(),
        (FactionType)12 => ModSettings.GetString("Vampire End", "det.rolecustomizationmod").ParseColor(),
        (FactionType)13 => ModSettings.GetString("Cursed Soul End", "det.rolecustomizationmod").ParseColor(),
        (FactionType)33 => ModSettings.GetString("Jackal/Recruit End", "det.rolecustomizationmod").ParseColor(),
        (FactionType)38 => ModSettings.GetString("Judge End", "det.rolecustomizationmod").ParseColor(),
        (FactionType)39 => ModSettings.GetString("Auditor End", "det.rolecustomizationmod").ParseColor(),
        (FactionType)41 => ModSettings.GetString("Starspawn End", "det.rolecustomizationmod").ParseColor(),
        (FactionType)42 => ModSettings.GetString("Egotist End", "det.rolecustomizationmod").ParseColor(),
        (FactionType)43 => ModSettings.GetString("Pandora End", "det.rolecustomizationmod").ParseColor(),
        (FactionType)34 => ModSettings.GetString("Frogs End", "det.rolecustomizationmod").ParseColor(),
        (FactionType)35 => ModSettings.GetString("Lions End", "det.rolecustomizationmod").ParseColor(),
        (FactionType)36 => ModSettings.GetString("Hawks End", "det.rolecustomizationmod").ParseColor(),
        (FactionType)44 => ModSettings.GetString("Compliance End", "det.rolecustomizationmod").ParseColor(),
        _ => ModSettings.GetString("Stoned/Hidden", "det.rolecustomizationmod").ParseColor(),
    };

    public static Color GetPlayerRoleColor(int pos)
    {
        Color color = Color.white;
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

    public static Tuple<Role, FactionType> GetPlayerInfo(int pos)
    {
        Service.Game.Sim.simulation.knownRolesAndFactions.Data.TryGetValue(pos, out Tuple<Role, FactionType> tuple);
        return tuple;
    }

    public static List<int> excludedids = [50, 69, 70, 71];

    public static string ToDisplayString(this FactionType faction) => Service.Home.LocalizationService.GetLocalizedString($"BTOS_FACTIONNAME_{(int)faction}");

    public static bool BTOS2Exists() => ModStates.IsEnabled("curtis.tuba.better.tos2");
 
    public static bool IsBTOS2()
    {
        try
        {
            return IsBTOS2Bypass();
        }
        catch
        {
            return false;
        }
    }

    private static bool IsBTOS2Bypass() => BTOS2Exists() && BetterTOS2.BTOSInfo.IS_MODDED;
}