using System;
using System.Linq;
using HarmonyLib;
using Mentions;
using Server.Shared.State;
using Services;
using UnityEngine;

namespace MiscRoleCustomisation;

[HarmonyPatch(typeof(MentionsProvider), nameof(MentionsProvider.DecodeSpeaker))]
public static class FancyChatExperimentalBTOS2
{
    public static bool Prefix(MentionsProvider __instance, ref string __result, string encodedText, int position, bool isAlive)
    {
        string text = Service.Home.UserService.Settings.ChatNameColor == 0 ? "FCCE3B" : (Service.Home.UserService.Settings.ChatNameColor == 1 ? "B0B0B0" :
            (Service.Home.UserService.Settings.ChatNameColor == 2 ? "CC009E" : "FCCE3B"));
        string text2 = encodedText;

        if (!Utils.excludedids.Contains(position))
        {
            if (isAlive)
            {
                bool flag = Service.Game.Sim.simulation.observations.playerEffects.Any(x => x.Data.effects.Contains((EffectType)100) && x.Data.playerPosition == position);

                if (Utils.IsRoleKnown(position))
                {
                    Tuple<Role, FactionType> playerInfo = Utils.GetPlayerInfo(position);

                    if (playerInfo.Item2.GetChangedGradient() != null)
                    {
                        Gradient gradient = playerInfo.Item2.GetChangedGradient();

                        if (flag)
                            gradient = ((FactionType)33).GetChangedGradient();

                        string text3 = "";

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
                    else
                    {
                        if (flag)
                        {
                            Gradient gradient2 = ((FactionType)33).GetChangedGradient();
                            string text4 = AddChangedConversionTags.ApplyGradient(Pepper.GetDiscussionPlayerByPosition(position).gameName + ":", gradient2.Evaluate(0f),
                                gradient2.Evaluate(1f));

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
                            string text5 = ColorUtility.ToHtmlStringRGB(Utils.GetPlayerRoleColor(position));
                            text2 = text2.Replace("<color=#" + text + ">", "<color=#" + text5 + ">");
                        }
                    }
                }
                else
                {
                    if (flag)
                    {
                        Gradient gradient3 = ((FactionType)33).GetChangedGradient();
                        string text6 = AddChangedConversionTags.ApplyGradient(Pepper.GetDiscussionPlayerByPosition(position).gameName + ":", gradient3.Evaluate(0f), gradient3.Evaluate(1f));
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
        }

        __result = __instance.ProcessSpeakerName(text2, position, isAlive);
        return false;
    }
}