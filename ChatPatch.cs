using HarmonyLib;
using Mentions;
using Server.Shared.State;
using Services;
using UnityEngine;

namespace MiscRoleCustomisation;

[HarmonyPatch(typeof(MentionsProvider), nameof(MentionsProvider.DecodeSpeaker))]
public static class FancyChatExperimentalBTOS2
{
    public static List<int> ExcludedIds = [50, 69, 70, 71];

    public static bool Prefix(MentionsProvider __instance, ref string __result, string encodedText, int position, bool isAlive)
    {
        var text = Service.Home.UserService.Settings.ChatNameColor switch
        {
            1 => "B0B0B0",
            2 => "CC009E",
            _ => "FCCE3B",
        };
        var text2 = encodedText;

        if (!ExcludedIds.Contains(position))
        {
            if (isAlive)
            {
                var flag = Service.Game.Sim.simulation.observations.playerEffects.Any(x => x.Data.effects.Contains((EffectType)100) && x.Data.playerPosition == position);

                if (Utils.GetRoleInfo(position, out var playerInfo))
                {
                    if (playerInfo.Item2.GetChangedGradient() != null)
                    {
                        var gradient = playerInfo.Item2.GetChangedGradient();

                        if (flag)
                            gradient = ((FactionType)33).GetChangedGradient();

                        var text3 = "";

                        if (playerInfo.Item2 == ((FactionType)44))
                        {
                            text3 = AddChangedConversionTags.ApplyThreeColorGradient(Pepper.GetDiscussionPlayerByPosition(position).gameName + ":", gradient.Evaluate(0f),
                                gradient.Evaluate(0.5f), gradient.Evaluate(1f));
                        }
                        else
                            text3 = AddChangedConversionTags.ApplyGradient(Pepper.GetDiscussionPlayerByPosition(position).gameName + ":", gradient.Evaluate(0f), gradient.Evaluate(1f));

                        text2 = text2.Replace(string.Concat(
                        [
                            "<color=#",
                            ColorUtility.ToHtmlStringRGB(Pepper.GetDiscussionPlayerRoleColor(position)),
                            ">",
                            Pepper.GetDiscussionPlayerByPosition(position).gameName,
                            ":"
                        ]), text3);
                    }
                    else if (flag)
                    {
                        var gradient2 = ((FactionType)33).GetChangedGradient();
                        var text4 = AddChangedConversionTags.ApplyGradient(Pepper.GetDiscussionPlayerByPosition(position).gameName + ":", gradient2.Evaluate(0f), gradient2.Evaluate(1f));
                        text2 = text2.Replace(string.Concat(
                        [
                            "<color=#",
                            ColorUtility.ToHtmlStringRGB(Pepper.GetDiscussionPlayerRoleColor(position)),
                            ">",
                            Pepper.GetDiscussionPlayerByPosition(position).gameName,
                            ":"
                        ]), text4);
                    }
                    else
                    {
                        var text5 = ColorUtility.ToHtmlStringRGB(Utils.GetPlayerRoleColor(position));
                        text2 = text2.Replace("<color=#" + text + ">", "<color=#" + text5 + ">");
                    }
                }
                else if (flag)
                {
                    var gradient3 = ((FactionType)33).GetChangedGradient();
                    var text6 = AddChangedConversionTags.ApplyGradient(Pepper.GetDiscussionPlayerByPosition(position).gameName + ":", gradient3.Evaluate(0f), gradient3.Evaluate(1f));
                    text2 = text2.Replace(string.Concat(
                    [
                        "<color=#",
                        text,
                        ">",
                        Pepper.GetDiscussionPlayerByPosition(position).gameName,
                        ":"
                    ]), text6);
                }
            }
        }

        __result = __instance.ProcessSpeakerName(text2, position, isAlive);
        return false;
    }
}