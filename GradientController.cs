using System.Collections;
using Game.Interface;
using Server.Shared.State;
using SML;
using UnityEngine;

namespace MiscRoleCustomisation;

public static class GetChangedGradients
{
    public static Gradient GetChangedGradient(this FactionType faction)
    {
        Gradient gradient = new();
        GradientColorKey[] array = new GradientColorKey[2];
        GradientAlphaKey[] array2 = new GradientAlphaKey[2];

        if (faction != (FactionType)13)
        {
            switch (faction)
            {
                case (FactionType)1:
                    array[0] = new(ModSettings.GetString("Town Start", "det.rolecustomizationmod").ParseColor(), 0f);
                    array[1] = new(ModSettings.GetString("Town End", "det.rolecustomizationmod").ParseColor(), 1f);
                    goto IL_259;

                case (FactionType)2:
                    array[0] = new(ModSettings.GetString("Coven Start", "det.rolecustomizationmod").ParseColor(), 0f);
                    array[1] = new(ModSettings.GetString("Coven End", "det.rolecustomizationmod").ParseColor(), 1f);
                    goto IL_259;

                case (FactionType)7:
                    array[0] = new(ModSettings.GetString("Apocalypse Start", "det.rolecustomizationmod").ParseColor(), 0f);
                    array[1] = new(ModSettings.GetString("Apocalypse End", "det.rolecustomizationmod").ParseColor(), 1f);
                    goto IL_259;

                case (FactionType)8:
                    array[0] = new(ModSettings.GetString("Executioner Start", "det.rolecustomizationmod").ParseColor(), 0f);
                    array[1] = new(ModSettings.GetString("Executioner End", "det.rolecustomizationmod").ParseColor(), 1f);
                    goto IL_259;

                case (FactionType)3:
                    array[0] = new(ModSettings.GetString("Serial Killer Start", "det.rolecustomizationmod").ParseColor(), 0f);
                    array[1] = new(ModSettings.GetString("Serial Killer End", "det.rolecustomizationmod").ParseColor(), 1f);
                    goto IL_259;

                case (FactionType)4:
                    array[0] = new(ModSettings.GetString("Arsonist Start", "det.rolecustomizationmod").ParseColor(), 0f);
                    array[1] = new(ModSettings.GetString("Arsonist End", "det.rolecustomizationmod").ParseColor(), 1f);
                    goto IL_259;

                case (FactionType)5:
                    array[0] = new(ModSettings.GetString("Werewolf Start", "det.rolecustomizationmod").ParseColor(), 0f);
                    array[1] = new(ModSettings.GetString("Werewolf End", "det.rolecustomizationmod").ParseColor(), 1f);
                    goto IL_259;

                case (FactionType)6:
                    array[0] = new(ModSettings.GetString("Shroud Start", "det.rolecustomizationmod").ParseColor(), 0f);
                    array[1] = new(ModSettings.GetString("Shroud End", "det.rolecustomizationmod").ParseColor(), 1f);
                    goto IL_259;

                case (FactionType)9:
                    array[0] = new(ModSettings.GetString("Jester Start", "det.rolecustomizationmod").ParseColor(), 0f);
                    array[1] = new(ModSettings.GetString("Jester End", "det.rolecustomizationmod").ParseColor(), 1f);
                    goto IL_259;

                case (FactionType)40:
                    array[0] = new(ModSettings.GetString("Inquisitor Start", "det.rolecustomizationmod").ParseColor(), 0f);
                    array[1] = new(ModSettings.GetString("Inquisitor End", "det.rolecustomizationmod").ParseColor(), 1f);
                    goto IL_259;

                case (FactionType)10:
                    array[0] = new(ModSettings.GetString("Pirate Start", "det.rolecustomizationmod").ParseColor(), 0f);
                    array[1] = new(ModSettings.GetString("Pirate End", "det.rolecustomizationmod").ParseColor(), 1f);
                    goto IL_259;

                case (FactionType)11:
                    array[0] = new(ModSettings.GetString("Doomsayer Start", "det.rolecustomizationmod").ParseColor(), 0f);
                    array[1] = new(ModSettings.GetString("Doomsayer End", "det.rolecustomizationmod").ParseColor(), 1f);
                    goto IL_259;

                case (FactionType)12:
                    array[0] = new(ModSettings.GetString("Vampire Start", "det.rolecustomizationmod").ParseColor(), 0f);
                    array[1] = new(ModSettings.GetString("Vampire End", "det.rolecustomizationmod").ParseColor(), 1f);
                    goto IL_259;

                case (FactionType)33:
                    array[0] = new(ModSettings.GetString("Jackal/Recruit Start", "det.rolecustomizationmod").ParseColor(), 0f);
                    array[1] = new(ModSettings.GetString("Jackal/Recruit End", "det.rolecustomizationmod").ParseColor(), 1f);
                    goto IL_259;

                case (FactionType)38:
                    array[0] = new(ModSettings.GetString("Judge Start", "det.rolecustomizationmod").ParseColor(), 0f);
                    array[1] = new(ModSettings.GetString("Judge End", "det.rolecustomizationmod").ParseColor(), 1f);
                    goto IL_259;

                case (FactionType)39:
                    array[0] = new(ModSettings.GetString("Auditor Start", "det.rolecustomizationmod").ParseColor(), 0f);
                    array[1] = new(ModSettings.GetString("Auditor End", "det.rolecustomizationmod").ParseColor(), 1f);
                    goto IL_259;

                case (FactionType)41:
                    array[0] = new(ModSettings.GetString("Starspawn Start", "det.rolecustomizationmod").ParseColor(), 0f);
                    array[1] = new(ModSettings.GetString("Starspawn End", "det.rolecustomizationmod").ParseColor(), 1f);
                    goto IL_259;

                case (FactionType)42:
                    array[0] = new(ModSettings.GetString("Egotist Start", "det.rolecustomizationmod").ParseColor(), 0f);
                    array[1] = new(ModSettings.GetString("Egotist End", "det.rolecustomizationmod").ParseColor(), 1f);
                    goto IL_259;

                case (FactionType)43:
                    array[0] = new(ModSettings.GetString("Pandora Start", "det.rolecustomizationmod").ParseColor(), 0f);
                    array[1] = new(ModSettings.GetString("Pandora End", "det.rolecustomizationmod").ParseColor(), 1f);
                    goto IL_259;

                case (FactionType)34:
                    array[0] = new(ModSettings.GetString("Frogs Start", "det.rolecustomizationmod").ParseColor(), 0f);
                    array[1] = new(ModSettings.GetString("Frogs End", "det.rolecustomizationmod").ParseColor(), 1f);
                    goto IL_259;

                case (FactionType)35:
                    array[0] = new(ModSettings.GetString("Lions Start", "det.rolecustomizationmod").ParseColor(), 0f);
                    array[1] = new(ModSettings.GetString("Lions End", "det.rolecustomizationmod").ParseColor(), 1f);
                    goto IL_259;

                case (FactionType)36:
                    array[0] = new(ModSettings.GetString("Hawks Start", "det.rolecustomizationmod").ParseColor(), 0f);
                    array[1] = new(ModSettings.GetString("Hawks End", "det.rolecustomizationmod").ParseColor(), 1f);
                    goto IL_259;

                case (FactionType)44:
                    array =
                    [
                        new(ModSettings.GetString("Compliance Start", "det.rolecustomizationmod").ParseColor(), 0f),
                        new(ModSettings.GetString("Compliance Middle", "det.rolecustomizationmod").ParseColor(), 0.5f),
                        new(ModSettings.GetString("Compliance End", "det.rolecustomizationmod").ParseColor(), 1f)
                    ];
                    goto IL_259;

            }
            return null;
        }

        array[0] = new(ModSettings.GetString("Cursed Soul Start", "det.rolecustomizationmod").ParseColor(), 0f);
        array[1] = new(ModSettings.GetString("Cursed Soul End", "det.rolecustomizationmod").ParseColor(), 1f);

        IL_259:
            array2[0] = new(1f, 0f);
            array2[1] = new(1f, 1f);
            gradient.SetKeys(array, array2);

        return gradient;
    }
}

public class GradientRoleColorController : MonoBehaviour
{
    private void Start() => StartCoroutine(ChangeValueOverTime(__instance.currentFaction));

    private void OnDestroy() => StopCoroutine(ChangeValueOverTime(__instance.currentFaction));

    private IEnumerator ChangeValueOverTime(FactionType faction)
    {
        Gradient grad = faction.GetChangedGradient();

        if (grad == null)
        {
            Destroy(this);
            yield break;
        }

        for (;;)
        {
            for (float t = 0f; t < duration; t += Time.deltaTime)
            {
                value = Mathf.Lerp(0f, 1f, t / duration);
                __instance.rolecardBackgroundInstance.SetColor(grad.Evaluate(value));
                yield return null;
            }

            for (float t2 = 0f; t2 < duration; t2 += Time.deltaTime)
            {
                value = Mathf.Lerp(1f, 0f, t2 / duration);
                __instance.rolecardBackgroundInstance.SetColor(grad.Evaluate(value));
                yield return null;
            }
        }
    }

    public RoleCardPanelBackground __instance;

    private readonly float duration = 10f;

    private float value = 0f;
}