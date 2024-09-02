using Game.Interface;
using HarmonyLib;
using Home.Shared;
using Server.Shared.Extensions;
using Server.Shared.Info;
using Server.Shared.State;
using Services;
using SML;
using UnityEngine;

namespace MiscRoleCustomisation;

[HarmonyPatch(typeof(RoleCardPanel), nameof(RoleCardPanel.UpdateTitle))]
public static class PatchRoleCard
{
    public static void Postfix(RoleCardPanel __instance)
    {
        GradientRoleColorController component = __instance.GetComponent<GradientRoleColorController>();

        if (component != null)
            Object.Destroy(component);

        __instance.gameObject.AddComponent<GradientRoleColorController>().__instance = __instance.rolecardBG;
        __instance.roleNameText.text = Pepper.GetMyRole().ToChangedDisplayString(Pepper.GetMyFaction(), Service.Game.Sim.simulation.observations.roleCardObservation.Data.modifier);
    }

    public static string ToChangedDisplayString(this Role role, FactionType faction, ROLE_MODIFIER modifier)
    {
        string text = "";
        string roleName = ModSettings.GetBool("Faction-Specific Role Names") ? Utils.ToRoleFactionDisplayString(role, faction) : role.ToDisplayString();

        if (faction.GetChangedGradient() != null)
            text = AddChangedConversionTags.ApplyGradient(roleName, faction.GetChangedGradient());
        else
        {
            text = string.Concat(
            [
                "<color=",
                ClientRoleExtensions.GetFactionColor(faction),
                ">",
                roleName,
                "</color>"
            ]);
        }

        Gradient gradientTT = faction.GetChangedGradient();

        if (modifier == (ROLE_MODIFIER)2 && gradientTT != null)
        {
            text = text + "\n<size=85%>" + AddChangedConversionTags.ApplyGradient($"({ModSettings.GetString("Town Traitor Label", "det.rolecustomizationmod")})", gradientTT.Evaluate(0f), gradientTT.Evaluate(1f)) + "</size>";
        }
        else if (modifier == (ROLE_MODIFIER)10)
        {
            Gradient gradient = ((FactionType)33).GetChangedGradient();
            text = text + "\n<size=85%>" + AddChangedConversionTags.ApplyGradient($"({ModSettings.GetString("Recruit Label", "det.rolecustomizationmod")})", gradient.Evaluate(0f), gradient.Evaluate(1f)) + "</size>";
        }
        else if (RoleExtensions.GetFaction(role) != faction)
        {
            Gradient gradient2 = faction.GetChangedGradient();

            if (gradient2 != null)
            {
                if (faction == (FactionType)44)
                {
                    text = text + "\n<size=85%>" + AddChangedConversionTags.ApplyThreeColorGradient("(" + faction.ToDisplayString() + ")", gradient2.Evaluate(0f), gradient2.Evaluate(0.5f),
                        gradient2.Evaluate(1f)) + "</size>";
                }
                else
                    text = text + "\n<size=85%>" + AddChangedConversionTags.ApplyGradient("(" + faction.ToDisplayString() + ")", gradient2.Evaluate(0f), gradient2.Evaluate(1f)) + "</size>";

                if (modifier == (ROLE_MODIFIER)1)
                {
                    text = text + "\n<size=85%>" + AddChangedConversionTags.ApplyGradient($"({ModSettings.GetString("VIP Label", "det.rolecustomizationmod")})", gradientTT.Evaluate(0f), gradientTT.Evaluate(1f)) + "</size>";
                }

            }
            else
            {
                text = string.Concat(
                [
                    text,
                    "\n<size=85%><color=",
                    ClientRoleExtensions.GetFactionColor(faction),
                    ">(",
                    faction.ToDisplayString(),
                    ")</color></size>"
                ]);
            }
        }

        return text;
    }
}

[HarmonyPatch(typeof(RoleCardPopupPanel), nameof(RoleCardPopupPanel.SetRole))]
public class RoleCardPopupPatches
{
    public static void Postfix(ref Role role, RoleCardPopupPanel __instance) => __instance.roleNameText.text = ClientRoleExtensions.ToColorizedDisplayString(role);
}