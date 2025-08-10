using RimWorld;
using RimWorldOfMagic.Core;
using Verse;

namespace RimWorldOfMagic.UI;

// ReSharper disable once InconsistentNaming
public class ITabMagic: ITab
{
    public ITabMagic()
    {
        UpdateSize();
        labelKey = "RimWorldOfMagic_MagicTab";
        RimWorldOfMagicSettings.onSettingsChanged += UpdateSize;
    }

    protected sealed override void UpdateSize()
    {
        size = new UnityEngine.Vector2(
            RimWorldOfMagicSettings.abilityTabWidth, RimWorldOfMagicSettings.abilityTabHeight
        );
    }

    protected override void FillTab()
    {
        var rect = new UnityEngine.Rect(0, 0, size.x, size.y);
        Widgets.Label(rect, "This is a custom magic tab!");
        
    }

    public override bool IsVisible => SelPawn?.TryGetComp<CompAbilities>() is { IsMagic: true };
    
    // Unsubscribe from settings changes on destructor to avoid memory leaks
    ~ITabMagic()
    {
        RimWorldOfMagicSettings.onSettingsChanged -= UpdateSize;
    }
}
