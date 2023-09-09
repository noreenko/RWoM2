using Verse;

namespace RimWorldOfMagic.Core;

[StaticConstructorOnStartup]
public static class InitializeCompAbilities
{
    static InitializeCompAbilities()
    {
        for (int i = 0; i < DefDatabase<ThingDef>.AllDefsListForReading.Count; i++)
        {
            ThingDef def = DefDatabase<ThingDef>.AllDefsListForReading[i];

            if (def.race is not { Humanlike: true }) continue;

            CompProperties props = new CompProperties { compClass = typeof(CompAbilities) };
            def.comps.Add(props);
            props.ResolveReferences(def);
            props.PostLoadSpecial(def);
        }
    }
}
