using HarmonyLib;
using Home.Shared;
using Server.Shared.Extensions;
using Server.Shared.State;
using UnityEngine;

namespace MiscRoleCustomisation;

[HarmonyPatch(typeof(ClientRoleExtensions), nameof(ClientRoleExtensions.ToColorizedDisplayString), typeof(Role), typeof(FactionType))]
public static class AddChangedConversionTags
{
    public static void Postfix(ref string __result, ref Role role, ref FactionType factionType)
    {
        if (role.IsResolved() || role is Role.FAMINE or Role.DEATH or Role.PESTILENCE or Role.WAR)
        {
            string text = role.ToDisplayString();

            if (factionType.GetChangedGradient() != null)
                __result = ApplyGradient(text, factionType.GetChangedGradient());
            else
                __result = "<color=" + factionType.GetFactionColor() + ">" + text + "</color>";
        }
    }

    public static string ApplyGradient(string text, Color color1, Color color2)
    {
        Gradient gradient = new();
        gradient.SetKeys([ new(color1, 0f), new(color2, 1f) ], [ new(1f, 0f), new(1f, 1f) ]);
        string text2 = string.Empty;

        for (int i = 0; i < text.Length; i++)
            text2 += $"<color={ToHexString(gradient.Evaluate((float)i / text.Length))}>{text[i]}</color>";

        return text2;
    }

    public static string ApplyThreeColorGradient(string text, Color color1, Color color2, Color color3)
    {
        Gradient gradient = new();
        gradient.SetKeys([ new(color1, 0f), new(color2, 0.5f), new(color3, 1f) ], [ new(1f, 0f), new(1f, 1f) ]);
        string text2 = string.Empty;

        for (int i = 0; i < text.Length; i++)
            text2 += $"<color={ToHexString(gradient.Evaluate((float)i / text.Length))}>{text[i]}</color>";

        return text2;
    }

    public static string ApplyGradient(string text, Gradient gradient)
    {
        string text2 = string.Empty;

        for (int i = 0; i < text.Length; i++)
            text2 += $"<color={ToHexString(gradient.Evaluate((float)i / text.Length))}>{text[i]}</color>";

        return text2;
    }

    public static string ToHexString(Color color)
    {
        Color32 color2 = color;
        return $"#{color2.r:X2}{color2.g:X2}{color2.b:X2}";
    }
}