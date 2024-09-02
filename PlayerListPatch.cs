using BetterTOS2;
using Game.Interface;
using HarmonyLib;
using Home.Shared;
using Server.Shared.Extensions;
using Server.Shared.State;
using SML;
using UnityEngine;

namespace MiscRoleCustomisation;

[HarmonyPatch(typeof(TosAbilityPanelListItem), nameof(TosAbilityPanelListItem.SetKnownRole))]
public class PlayerListPatch
{
    public static bool Prefix(ref Role role, ref FactionType faction, TosAbilityPanelListItem __instance)
    {
        __instance.playerRole = role;
        string roleName = ModSettings.GetBool("Faction-Specific Role Names") ? Utils.ToRoleFactionDisplayString(role, faction) : role.ToDisplayString();
        string factionName = faction.ToDisplayString();

        
        if (role is not (0 or (Role)byte.MaxValue))
        {
            Gradient gradient = faction.GetChangedGradient();

            if (gradient != null && role is not ((Role)254 or (Role)241))
            {
                if (faction == (FactionType)44 && faction != (FactionType)33)
                {
                    __instance.playerRoleText.text = AddChangedConversionTags.ApplyThreeColorGradient("(" + roleName + ")", gradient.Evaluate(0f), gradient.Evaluate(0.5f),
                        gradient.Evaluate(1f));
                }
                else if (faction == (FactionType)33 && role != BetterTOS2.RolePlus.JACKAL)
                {
                    Gradient jackalGradient = FactionTypePlus.JACKAL.GetChangedGradient();

                    __instance.playerRoleText.text = AddChangedConversionTags.ApplyGradient("(" + roleName + ")", jackalGradient.Evaluate(0f), jackalGradient.Evaluate(1f));
                }
                else
                    __instance.playerRoleText.text = AddChangedConversionTags.ApplyGradient("(" + roleName + ")", gradient.Evaluate(0f), gradient.Evaluate(1f));

            }
            else if (role is not ((Role)254 or (Role)241))
            {
                __instance.playerRoleText.text = string.Concat(
                [
                    "<color=",
                    ClientRoleExtensions.GetFactionColor(faction),
                    ">(",
                    roleName,
                    ")</color>"
                ]);
            }
            else
            {
                __instance.playerRoleText.text = string.Concat(
                [
                    "<color=",
                    ClientRoleExtensions.GetFactionColor(RoleExtensions.GetFaction(role)),
                    ">(",
                    roleName,
                    ")</color>"
                ]);
            }

            __instance.playerRoleText.gameObject.SetActive(true);
        }

        return false;
    }
}