using RimWorld;
using RimWorldOfMagic.Core;
using Verse;

namespace RimWorldOfMagic.UI;

// ReSharper disable once InconsistentNaming
public class ITabMagic: ITab
{
    public ITabMagic()
    {
        this.size = new UnityEngine.Vector2(300f, 200f);
        this.labelKey = "RimWorldOfMagic_MagicTab";
    }

    protected override void FillTab()
    {
        var rect = new UnityEngine.Rect(0, 0, size.x, size.y);
        Widgets.Label(rect, "This is a custom magic tab!");
    }

    public override bool IsVisible => true;
    // SelPawn?.TryGetComp<CompAbilities>() is { IsMagic: true }
}
