using static SML.Mod;
using SML;
using UnityEngine;
using Witchcraft.Modules;

namespace MiscRoleCustomisation;

[SalemMod]
[SalemMenuItem]
[DynamicSettings]
[WitchcraftMod(typeof(Main), "MiscRoleCustomisation")]
public class Main
{
    public static WitchcraftModAttribute Instance { get; private set; }

    public void Start()
    {
        Instance = ModSingleton<Main>.Instance;

        Instance.Message("Truly a Colour Swapper Moment");
        FixStyles.RefreshStyles();
    }

    [UponAssetsLoaded]
    public static void AssetsLoaded() => XmlsButton.Icon = Instance.Assets.GetSprite("Xmls");

    public static readonly SalemMenuButton XmlsButton = new()
    {
        Label = "Misc Role Customisation XMLS",
        OnClick = OpenLink
    };

    public static void OpenLink() => Application.OpenURL("https://drive.google.com/drive/u/1/folders/1j8ZeiW59FLQqR5hOprP-o9kcC_zLxq03");

    public ModSettings.CheckboxSetting FactionSpecificNames => new()
    {
        Name = "Faction-Specific Role Names",
        Description = "Uses a special string table you can edit that changes Role Names depending on what faction they belong to.<color=#FF0000> WARNING: This setting will break role names if you do not have the text editor mod. You must add all strings from this mod's xmls to your xml. To download these xmls, there will be a button under Mod Actions that will send you to a Google Drive containing the strings needed.</color>",
        Available = ModStates.IsEnabled("curtis.text.editor")
    };
}