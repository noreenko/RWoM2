using HarmonyLib;
using Prepatcher;
using Verse;
using VFECore.Abilities;

namespace RimWorldOfMagic.Patches.VFECore.Abilities;

public static class CompAbilitiesPrepatcher
{
    [PrepatcherField]
    [Prepatcher.DefaultValue(1f)]
    public static extern ref float baseManaRate(this CompAbilities target);
}
