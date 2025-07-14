using System.Collections.Generic;
using RimWorldOfMagic.Core.AbilityUpgrades;
using RimWorldOfMagic.ModExtensions;
using VEFAbilityUpgradeFramework;
using Verse;

namespace RimWorldOfMagic.Core;

public class AbilityDef : UpgradableAbilityDef
{
    public AbilityDef(): base() {}
    
    // Copy constructor from base class
    public AbilityDef(AbilityDef def, Ability upgradableAbility)
        : base(def, upgradableAbility)
    {
    }
}