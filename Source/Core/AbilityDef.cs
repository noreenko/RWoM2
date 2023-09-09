using System.Collections.Generic;
using RimWorldOfMagic.Core.AbilityUpgrades;
using RimWorldOfMagic.ModExtensions;
using Verse;
using VFECore.Abilities;

namespace RimWorldOfMagic.Core;

public class AbilityDef : VFECore.Abilities.AbilityDef
{
    public List<AbilityUpgradeDef> abilityUpgradeDefs;

    public override void ResolveReferences()
    {
        base.ResolveReferences();
        abilityUpgradeDefs ??= new List<AbilityUpgradeDef>();

        if (modExtensions == null) return;
        foreach (DefModExtension modExtension in modExtensions)
        {
            if (modExtension is AbilityModExtension abilityModExtension)
                abilityModExtension.abilityDef = this;
        }
    }
}
