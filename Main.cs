using System;
using SML;
using static SML.Mod;
using UnityEngine;

namespace MiscRoleCustomisation;

[SalemMod]
[SalemMenuItem]
public class Main
{
    public void Start()
    {
        Console.WriteLine("Truly a Colour Swapper Moment");
        FixStyles.RefreshStyles();
        Utils.CheckDirectory();
    }

    public static readonly SalemMenuButton FancyMenu = new()
    {
        Label = "Misc Role Customisation XMLS",
        OnClick = OpenLink
    };

    public static void OpenLink()
    {
        Application.OpenURL("https://drive.google.com/drive/u/1/folders/1j8ZeiW59FLQqR5hOprP-o9kcC_zLxq03");
    }
}