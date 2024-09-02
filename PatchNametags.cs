using BetterTOS2;
using Game.Characters;
using HarmonyLib;
using Home.Shared;
using Server.Shared.Info;
using Server.Shared.State;
using Services;
using SML;
using UnityEngine;

namespace MiscRoleCustomisation;

// gotta find a way to fix jackal gradients breaking for recs

[HarmonyPatch(typeof(TosCharacterNametag), nameof(TosCharacterNametag.ColouredName))]
public static class TosCharacterNametagPatch
{
    public static void Postfix(TosCharacterNametag __instance, ref string theName, ref FactionType factionType, ref Role role, ref string __result)
    {
        string roleName = ModSettings.GetBool("Faction-Specific Role Names") ? Utils.ToRoleFactionDisplayString(role, factionType) : role.ToDisplayString();

        if (factionType.GetChangedGradient() != null && role is not (Role.STONED or Role.HIDDEN))
        {
            Gradient gradient = factionType.GetChangedGradient();
            string gradientName = "";
            string gradientRole = "";

            if (factionType == (FactionType)44 && factionType != (FactionType)33)
            {
                gradientName = AddChangedConversionTags.ApplyThreeColorGradient(theName, gradient.Evaluate(0f), gradient.Evaluate(0.5f), gradient.Evaluate(1f));
                gradientRole = AddChangedConversionTags.ApplyThreeColorGradient("(" + roleName + ")", gradient.Evaluate(0f), gradient.Evaluate(0.5f), gradient.Evaluate(1f));
            }
            else if (factionType == (FactionType)33 && role != RolePlus.JACKAL)
            {
                Gradient jackalGradient = FactionTypePlus.JACKAL.GetChangedGradient();
                gradientName = AddChangedConversionTags.ApplyGradient(theName, jackalGradient.Evaluate(0f), jackalGradient.Evaluate(1f));
                gradientRole = AddChangedConversionTags.ApplyGradient("(" + roleName + ")", gradient.Evaluate(0f), jackalGradient.Evaluate(1f));
            }
            else
            {
                gradientName = AddChangedConversionTags.ApplyGradient(theName, gradient.Evaluate(0f), gradient.Evaluate(1f));
                gradientRole = AddChangedConversionTags.ApplyGradient("(" + roleName + ")", gradient.Evaluate(0f), gradient.Evaluate(1f));
            }

            if (Utils.IsBTOS2())
                __result = $"<size=36><sprite=\"BTOSRoleIcons\" name=\"Role{(int)role}\"></size>\n<size=24>{gradientName}</size>\n<size=18>{gradientRole}</size>";
            else
                __result = $"<size=36><sprite=\"RoleIcons\" name=\"Role{(int)role}\"></size>\n<size=24>{gradientName}</size>\n<size=18>{gradientRole}</size>";
        }
    }
}