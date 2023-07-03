using System.Collections.Generic;
using RimWorldOfMagic.Core.AbilityUpgrades;
using RimWorldOfMagic.Core.AbilityUpgrades.Trackers;
using Verse;

namespace RimWorldOfMagic.Core;

public class Ability : VFECore.Abilities.Ability
{
    public List<AbilityUpgrade> upgrades = new();

    // Trackers
    public TrackerContainer<ExplosionTracker> explosionTrackers = new();


    public override void Init()
    {
        upgrades ??= new List<AbilityUpgrade>();
        foreach (AbilityUpgradeDef abilityUpgradeDef in ((AbilityDef)def).abilityUpgradeDefs)
        {
            upgrades.Add(new AbilityUpgrade { def = abilityUpgradeDef, ability = this });
        }
    }

    // Given an abilityUpgrade that has just unlocked, apply the stats to the associated tracker
    public virtual void UpdateTracker(AbilityUpgrade abilityUpgrade)
    {
        if (abilityUpgrade.def is Explosion_AbilityUpgradeDef explosionDef)
        {
            explosionTrackers.GetTracker(explosionDef.upgradeExtensionKey).UpdateTracker(explosionDef);
        }
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Collections.Look(ref upgrades, "upgrades");
    }
}
