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
        var gradient = new Gradient();
        var array = new GradientColorKey[2];
        var array2 = new GradientAlphaKey[2];

        if (faction != (FactionType)13)
        {
            switch (faction)
            {
                case (FactionType)1:
                    array[0] = new(ModSettings.GetColor("Town Start", "det.rolecustomizationmod"), 0f);
                    array[1] = new(ModSettings.GetColor("Town End", "det.rolecustomizationmod"), 1f);
                    goto IL_259;

                case (FactionType)2:
                    array[0] = new(ModSettings.GetColor("Coven Start", "det.rolecustomizationmod"), 0f);
                    array[1] = new(ModSettings.GetColor("Coven End", "det.rolecustomizationmod"), 1f);
                    goto IL_259;

                case (FactionType)7:
                    array[0] = new(ModSettings.GetColor("Apocalypse Start", "det.rolecustomizationmod"), 0f);
                    array[1] = new(ModSettings.GetColor("Apocalypse End", "det.rolecustomizationmod"), 1f);
                    goto IL_259;

                case (FactionType)8:
                    array[0] = new(ModSettings.GetColor("Executioner Start", "det.rolecustomizationmod"), 0f);
                    array[1] = new(ModSettings.GetColor("Executioner End", "det.rolecustomizationmod"), 1f);
                    goto IL_259;

                case (FactionType)3:
                    array[0] = new(ModSettings.GetColor("Serial Killer Start", "det.rolecustomizationmod"), 0f);
                    array[1] = new(ModSettings.GetColor("Serial Killer End", "det.rolecustomizationmod"), 1f);
                    goto IL_259;

                case (FactionType)4:
                    array[0] = new(ModSettings.GetColor("Arsonist Start", "det.rolecustomizationmod"), 0f);
                    array[1] = new(ModSettings.GetColor("Arsonist End", "det.rolecustomizationmod"), 1f);
                    goto IL_259;

                case (FactionType)5:
                    array[0] = new(ModSettings.GetColor("Werewolf Start", "det.rolecustomizationmod"), 0f);
                    array[1] = new(ModSettings.GetColor("Werewolf End", "det.rolecustomizationmod"), 1f);
                    goto IL_259;

                case (FactionType)6:
                    array[0] = new(ModSettings.GetColor("Shroud Start", "det.rolecustomizationmod"), 0f);
                    array[1] = new(ModSettings.GetColor("Shroud End", "det.rolecustomizationmod"), 1f);
                    goto IL_259;

                case (FactionType)9:
                    array[0] = new(ModSettings.GetColor("Jester Start", "det.rolecustomizationmod"), 0f);
                    array[1] = new(ModSettings.GetColor("Jester End", "det.rolecustomizationmod"), 1f);
                    goto IL_259;

                case (FactionType)40:
                    array[0] = new(ModSettings.GetColor("Inquisitor Start", "det.rolecustomizationmod"), 0f);
                    array[1] = new(ModSettings.GetColor("Inquisitor End", "det.rolecustomizationmod"), 1f);
                    goto IL_259;

                case (FactionType)10:
                    array[0] = new(ModSettings.GetColor("Pirate Start", "det.rolecustomizationmod"), 0f);
                    array[1] = new(ModSettings.GetColor("Pirate End", "det.rolecustomizationmod"), 1f);
                    goto IL_259;

                case (FactionType)11:
                    array[0] = new(ModSettings.GetColor("Doomsayer Start", "det.rolecustomizationmod"), 0f);
                    array[1] = new(ModSettings.GetColor("Doomsayer End", "det.rolecustomizationmod"), 1f);
                    goto IL_259;

                case (FactionType)12:
                    array[0] = new(ModSettings.GetColor("Vampire Start", "det.rolecustomizationmod"), 0f);
                    array[1] = new(ModSettings.GetColor("Vampire End", "det.rolecustomizationmod"), 1f);
                    goto IL_259;

                case (FactionType)33:
                    array[0] = new(ModSettings.GetColor("Jackal/Recruit Start", "det.rolecustomizationmod"), 0f);
                    array[1] = new(ModSettings.GetColor("Jackal/Recruit End", "det.rolecustomizationmod"), 1f);
                    goto IL_259;

                case (FactionType)38:
                    array[0] = new(ModSettings.GetColor("Judge Start", "det.rolecustomizationmod"), 0f);
                    array[1] = new(ModSettings.GetColor("Judge End", "det.rolecustomizationmod"), 1f);
                    goto IL_259;

                case (FactionType)39:
                    array[0] = new(ModSettings.GetColor("Auditor Start", "det.rolecustomizationmod"), 0f);
                    array[1] = new(ModSettings.GetColor("Auditor End", "det.rolecustomizationmod"), 1f);
                    goto IL_259;

                case (FactionType)41:
                    array[0] = new(ModSettings.GetColor("Starspawn Start", "det.rolecustomizationmod"), 0f);
                    array[1] = new(ModSettings.GetColor("Starspawn End", "det.rolecustomizationmod"), 1f);
                    goto IL_259;

                case (FactionType)42:
                    array[0] = new(ModSettings.GetColor("Egotist Start", "det.rolecustomizationmod"), 0f);
                    array[1] = new(ModSettings.GetColor("Egotist End", "det.rolecustomizationmod"), 1f);
                    goto IL_259;

                case (FactionType)43:
                    array[0] = new(ModSettings.GetColor("Pandora Start", "det.rolecustomizationmod"), 0f);
                    array[1] = new(ModSettings.GetColor("Pandora End", "det.rolecustomizationmod"), 1f);
                    goto IL_259;

                case (FactionType)34:
                    array[0] = new(ModSettings.GetColor("Frogs Start", "det.rolecustomizationmod"), 0f);
                    array[1] = new(ModSettings.GetColor("Frogs End", "det.rolecustomizationmod"), 1f);
                    goto IL_259;

                case (FactionType)35:
                    array[0] = new(ModSettings.GetColor("Lions Start", "det.rolecustomizationmod"), 0f);
                    array[1] = new(ModSettings.GetColor("Lions End", "det.rolecustomizationmod"), 1f);
                    goto IL_259;

                case (FactionType)36:
                    array[0] = new(ModSettings.GetColor("Hawks Start", "det.rolecustomizationmod"), 0f);
                    array[1] = new(ModSettings.GetColor("Hawks End", "det.rolecustomizationmod"), 1f);
                    goto IL_259;

                case (FactionType)44:
                    array =
                    [
                        new(ModSettings.GetColor("Compliance Start", "det.rolecustomizationmod"), 0f),
                        new(ModSettings.GetColor("Compliance Middle", "det.rolecustomizationmod"), 0.5f),
                        new(ModSettings.GetColor("Compliance End", "det.rolecustomizationmod"), 1f)
                    ];
                    goto IL_259;

            }
            return null;
        }

        array[0] = new(ModSettings.GetColor("Cursed Soul Start", "det.rolecustomizationmod"), 0f);
        array[1] = new(ModSettings.GetColor("Cursed Soul End", "det.rolecustomizationmod"), 1f);

        IL_259:
            array2[0] = new(1f, 0f);
            array2[1] = new(1f, 1f);
            gradient.SetKeys(array, array2);

        return gradient;
    }
}

public class GradientRoleColorController : MonoBehaviour
{
    public RoleCardPanelBackground __instance;
    private readonly float duration = 10f;
    private float value = 0f;

    public void Start() => StartCoroutine(ChangeValueOverTime(__instance.currentFaction));

    public void OnDestroy() => StopCoroutine(ChangeValueOverTime(__instance.currentFaction));

    private IEnumerator ChangeValueOverTime(FactionType faction)
    {
        var grad = faction.GetChangedGradient();

        if (grad == null)
        {
            Destroy(this);
            yield break;
        }

        for (;;)
        {
            for (var t = 0f; t < duration; t += Time.deltaTime)
            {
                value = Mathf.Lerp(0f, 1f, t / duration);
                __instance.rolecardBackgroundInstance.SetColor(grad.Evaluate(value));
                yield return new WaitForEndOfFrame();
            }

            for (var t2 = 0f; t2 < duration; t2 += Time.deltaTime)
            {
                value = Mathf.Lerp(1f, 0f, t2 / duration);
                __instance.rolecardBackgroundInstance.SetColor(grad.Evaluate(value));
                yield return new WaitForEndOfFrame();
            }
        }
    }
}