using System.Collections.Generic;
using Cinematics.Players;
using Game.Characters;
using HarmonyLib;
using Home.Shared;
using Server.Shared.Cinematics;
using Server.Shared.Cinematics.Data;
using Server.Shared.State;
using Services;
using UnityEngine;

namespace MiscRoleCustomisation;

[HarmonyPatch(typeof(FactionWinsCinematicPlayer), nameof(FactionWinsCinematicPlayer.Init))]
public static class PatchDefaultWinScreens
{
    [HarmonyPostfix]
    public static void Postfix(FactionWinsCinematicPlayer __instance, ref ICinematicData cinematicData)
    {
        __instance.elapsedDuration = 0f;
        Debug.Log(string.Format("FactionWinsCinematicPlayer current phase at start = {0}", Pepper.GetGamePhase()));
        __instance.cinematicData = cinematicData as FactionWinsCinematicData;
        float winTimeByFaction = CinematicFactionWinsTimes.GetWinTimeByFaction(__instance.cinematicData.winningFaction);
        __instance.totalDuration = winTimeByFaction;
        __instance.callbackTimers.Clear();
        List<TosCharacter> spawnedCharacters = Service.Game.Cast.GetSpawnedCharacters();

        if (spawnedCharacters == null)
        {
            Debug.LogError("spawnedPlayers is null in GetCrowd()");
            return;
        }

        HashSet<int> positions = [];
        __instance.cinematicData.entries.ForEach(e => positions.Add(e.position));
        spawnedCharacters.ForEach(c =>
        {
            int position = c.position;

            if (positions.Contains(position))
                __instance.winningCharacters.Add(c);
            else
                c.characterSprite.SetColor(Color.clear);
        });
        FactionType winningFaction = __instance.cinematicData.winningFaction;

        if (winningFaction == FactionType.TOWN)
        {
            Service.Home.AudioService.PlayMusic("Audio/Music/TownVictory.wav", false, AudioController.AudioChannel.Cinematic, true);
            __instance.evilProp.SetActive(false);
            __instance.goodProp.SetActive(true);
            __instance.m_Animator.SetInteger("State", 1);
        }
        else
        {
            Service.Home.AudioService.PlayMusic("Audio/Music/CovenVictory.wav", false, AudioController.AudioChannel.Cinematic, true);
            __instance.evilProp.SetActive(true);
            __instance.goodProp.SetActive(false);
            __instance.m_Animator.SetInteger("State", 2);
        }

        string text = string.Format("GUI_WINNERS_ARE_{0}", (int)winningFaction);
        string text2 = __instance.l10n(text);
        string gradientText;

        if (winningFaction.GetChangedGradient() != null)
        {
            Gradient gradient = winningFaction.GetChangedGradient();
            __instance.leftImage.color = Utils.GetFactionStartingColor(winningFaction);
            __instance.rightImage.color = Utils.GetFactionEndingColor(winningFaction);

            if (winningFaction == (FactionType)44)
                gradientText = AddChangedConversionTags.ApplyThreeColorGradient(text2, gradient.Evaluate(0f), gradient.Evaluate(0.5f), gradient.Evaluate(1f));
            else
                gradientText = AddChangedConversionTags.ApplyGradient(text2, gradient.Evaluate(0f), gradient.Evaluate(1f));

            __instance.textAnimatorPlayer.ShowText(gradientText);
        }
        else
        {
            if (ColorUtility.TryParseHtmlString(winningFaction.GetFactionColor(), out Color color))
            {
                __instance.leftImage.color = color;
                __instance.rightImage.color = color;
                __instance.glow.color = color;
            }

            __instance.text.color = color;
            __instance.textAnimatorPlayer.ShowText(text2);
        }

        __instance.SetUpWinners(__instance.winningCharacters);
        return;
    }
}

[HarmonyPatch(typeof(FactionWinsStandardCinematicPlayer), nameof(FactionWinsStandardCinematicPlayer.Init))]
public static class PatchCustomWinScreens
{
    public static void Postfix(FactionWinsStandardCinematicPlayer __instance, ref ICinematicData cinematicData)
    {
        Debug.Log(string.Format("FactionWinsStandardCinematicPlayer current phase at end = {0}", Pepper.GetGamePhase()));
        __instance.elapsedDuration = 0f;
        __instance.cinematicData = cinematicData as FactionWinsCinematicData;
        float num = CinematicFactionWinsTimes.GetWinTimeByFaction(__instance.cinematicData.winningFaction);
        __instance.totalDuration = num;

        if (Pepper.IsResultsPhase())
            num += 0.2f;

        FactionType winningFaction = __instance.cinematicData.winningFaction;

        if (winningFaction == FactionType.TOWN)
            Service.Home.AudioService.PlayMusic("Audio/Music/TownVictory.wav", false, AudioController.AudioChannel.Cinematic, true);
        else if (winningFaction is FactionType.COVEN or FactionType.NONE)
            Service.Home.AudioService.PlayMusic("Audio/Music/CovenVictory.wav", false, AudioController.AudioChannel.Cinematic, true);

        string text2 = __instance.l10n(string.Format("GUI_WINNERS_ARE_{0}", (int)winningFaction));
        string gradientText;

        if (winningFaction.GetChangedGradient() != null)
        {
            Gradient gradient = winningFaction.GetChangedGradient();

            if (winningFaction == (FactionType)44)
                gradientText = AddChangedConversionTags.ApplyThreeColorGradient(text2, gradient.Evaluate(0f), gradient.Evaluate(0.5f), gradient.Evaluate(1f));
            else
                gradientText = AddChangedConversionTags.ApplyGradient(text2, gradient.Evaluate(0f), gradient.Evaluate(1f));

            if (__instance.textAnimatorPlayer.gameObject.activeSelf)
                __instance.textAnimatorPlayer.ShowText(gradientText);
        }
        else if (__instance.textAnimatorPlayer.gameObject.activeSelf)
            __instance.textAnimatorPlayer.ShowText(text2);

        __instance.SetUpWinners();
        return;
    }
}