using System.Collections.Generic;
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
}

public class RimWorldOfMagicSettings : ModSettings
{
    public static float xpMultiplier = 1f;
    public static float needMultiplier = 1f;

    public static Dictionary<string, bool> classToggle = new();

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref xpMultiplier, "xpMultiplier", 1f);
        Scribe_Values.Look(ref needMultiplier, "needMultiplier", 1f);

        Scribe_Collections.Look(ref classToggle, "classToggle");
    }
}
