using Prepatcher;
using VFECore.Abilities;

namespace RimWorldOfMagic.Patches.VFECore.Abilities;

public static class CompAbilitiesPrepatcher
{
    [PrepatcherField]
    [DefaultValue(1f)]
    public static extern ref float baseManaRate(this CompAbilities target);
}
