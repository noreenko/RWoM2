using System.Collections.Generic;
using System.Globalization;
using RimWorldOfMagic.UI;
using UnityEngine;
using Verse;

namespace RimWorldOfMagic;

[StaticConstructorOnStartup]
public class RimWorldOfMagic : Mod
{
    public static RimWorldOfMagicSettings settings;

    public RimWorldOfMagic(ModContentPack content) : base(content)
    {
        settings = GetSettings<RimWorldOfMagicSettings>();
    }

    public override string SettingsCategory()
    {
        return "RimWorld of Magic";
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        Listing_Standard listing = new();
        listing.Begin(inRect);
        // ---------- Ability Tab Settings ----------
        // Header for Section
        Rect labelRect = listing.GetRect(Text.LineHeight);
        Widgets.Label(labelRect, "RimWorldOfMagic_Settings_AbilityTabSettings".Translate());
        TooltipHandler.TipRegion(labelRect, "RimWorldOfMagic_Settings_AbilityTabSettingsTooltip".Translate());
        
        // Ability Tab Width Setting
        Rect abilityTabWidthLabelRect = listing.GetRect(Text.LineHeight * 2);
        ITabUtils.DrawFloatSetting(
            abilityTabWidthLabelRect,
            ref RimWorldOfMagicSettings.abilityTabWidth,
            "RimWorldOfMagic_Settings_AbilityTabWidth",
            RimWorldOfMagicSettings.AbilityTabWidthMin,
            RimWorldOfMagicSettings.AbilityTabWidthMax,
           "abilityTabWidthBuffer",
            "F0"
        );
        // Ability Tab Height Setting
        Rect abilityTabHeightLabelRect = listing.GetRect(Text.LineHeight * 2);
        ITabUtils.DrawFloatSetting(
            abilityTabHeightLabelRect,
            ref RimWorldOfMagicSettings.abilityTabHeight,
            "RimWorldOfMagic_Settings_AbilityTabHeight",
            RimWorldOfMagicSettings.AbilityTabHeightMin,
            RimWorldOfMagicSettings.AbilityTabHeightMax,
            "abilityTabHeightBuffer",
            "F0"
        );
        
        listing.End();
    }
    
    public override void WriteSettings()
    {
        base.WriteSettings();
        // Notify that settings have changed
        if (RimWorldOfMagicSettings.settingsChanged)
        {
            RimWorldOfMagicSettings.NotifySettingsChanged();
        }
    }
}

public class RimWorldOfMagicSettings : ModSettings
{
    // Event for when settings change
    public static System.Action onSettingsChanged;
    public static bool settingsChanged;
    
    public static float abilityTabWidth = 300f;
    public const float AbilityTabWidthMin = 100f;
    public const float AbilityTabWidthMax = 1000f;
    public static float abilityTabHeight = 200f;
    public const float AbilityTabHeightMin = 100f;
    public const float AbilityTabHeightMax = 1000f;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref  abilityTabWidth, "RimWorldOfMagic_abilityTabWidth");
        Scribe_Values.Look(ref abilityTabHeight, "RimWorldOfMagic_abilityTabHeight");
    }
    
    // Call this whenever a setting changes. We will call the listeners when the settings window is closed.
    public static void NotifySettingsChanged()
    {
        onSettingsChanged?.Invoke();
        settingsChanged = false;
    }
}
