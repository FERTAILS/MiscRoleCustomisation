using Game;
using HarmonyLib;
using Home.HomeScene;
using SML;
using TMPro;

namespace MiscRoleCustomisation;

[HarmonyPatch(typeof(HomeSceneController), nameof(HomeSceneController.HandleClickPlay))]
public static class FixStyles
{
    [HarmonyPostfix]
    public static void RefreshStyles()
    {
        TMP_StyleSheet defaultStyleSheet = TMP_Settings.defaultStyleSheet;
        defaultStyleSheet.styles.Find(style => style.name == "TownColor").m_OpeningDefinition = "<color=" + ModSettings.GetString("Town Start", "det.rolecustomizationmod") + ">";
        defaultStyleSheet.styles.Find(style => style.name == "CovenColor").m_OpeningDefinition = "<color=" + ModSettings.GetString("Coven Start", "det.rolecustomizationmod") + ">";
        defaultStyleSheet.styles.Find(style => style.name == "ApocalypseColor").m_OpeningDefinition = "<color=" + ModSettings.GetString("Apocalypse Start", "det.rolecustomizationmod") + ">";
        defaultStyleSheet.styles.Find(style => style.name == "SerialKillerColor").m_OpeningDefinition = "<color=" + ModSettings.GetString("Serial Killer Start", "det.rolecustomizationmod") + ">";
        defaultStyleSheet.styles.Find(style => style.name == "ArsonistColor").m_OpeningDefinition = "<color=" + ModSettings.GetString("Arsonist Start", "det.rolecustomizationmod") + ">";
        defaultStyleSheet.styles.Find(style => style.name == "WerewolfColor").m_OpeningDefinition = "<color=" + ModSettings.GetString("Werewolf Start", "det.rolecustomizationmod") + ">";
        defaultStyleSheet.styles.Find(style => style.name == "ShroudColor").m_OpeningDefinition = "<color=" + ModSettings.GetString("Shroud Start", "det.rolecustomizationmod") + ">";
        defaultStyleSheet.styles.Find(style => style.name == "ExecutionerColor").m_OpeningDefinition = "<color=" + ModSettings.GetString("Executioner Start", "det.rolecustomizationmod") + ">";
        defaultStyleSheet.styles.Find(style => style.name == "JesterColor").m_OpeningDefinition = "<color=" + ModSettings.GetString("Jester Start", "det.rolecustomizationmod") + ">";
        defaultStyleSheet.styles.Find(style => style.name == "PirateColor").m_OpeningDefinition = "<color=" + ModSettings.GetString("Pirate Start", "det.rolecustomizationmod") + ">";
        defaultStyleSheet.styles.Find(style => style.name == "DoomsayerColor").m_OpeningDefinition = "<color=" + ModSettings.GetString("Doomsayer Start", "det.rolecustomizationmod") + ">";
        defaultStyleSheet.styles.Find(style => style.name == "VampireColor").m_OpeningDefinition = "<color=" + ModSettings.GetString("Vampire Start", "det.rolecustomizationmod") + ">";
        defaultStyleSheet.styles.Find(style => style.name == "CursedSoulColor").m_OpeningDefinition = "<color=" + ModSettings.GetString("Cursed Soul Start", "det.rolecustomizationmod") + ">";
        defaultStyleSheet.styles.Find(style => style.name == "NeutralColor").m_OpeningDefinition = "<color=" + ModSettings.GetString("Neutral", "det.rolecustomizationmod") + ">";
        defaultStyleSheet.RefreshStyles();
    }
}

[HarmonyPatch(typeof(GameSceneController), nameof(GameSceneController.Start))]
public static class RefreshGame
{
    public static void Postfix() => FixStyles.RefreshStyles();
}