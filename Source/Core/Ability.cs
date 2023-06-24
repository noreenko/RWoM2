using System.Collections.Generic;
using System.Linq;
using Verse;

namespace RimWorldOfMagic.Core;

public class Ability : VFECore.Abilities.Ability
{
    private List<AbilityUpgrade> upgrades = new();
    public override void Init()
    {
        upgrades ??= new List<AbilityUpgrade>();
        foreach (AbilityUpgradeDefBase abilityUpgradeDef in ((AbilityDef)def).abilityUpgradeDefs)
        {
            upgrades.Add(new AbilityUpgrade { def = abilityUpgradeDef, ability = this });
        }
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Collections.Look(ref upgrades, "upgrades");
    }
}
