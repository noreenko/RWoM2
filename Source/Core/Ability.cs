using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld.Planet;
using RimWorldOfMagic.Core.AbilityUpgrades;
using RimWorldOfMagic.Core.AbilityUpgrades.Trackers;
using Verse;
using VFECore.Abilities;

namespace RimWorldOfMagic.Core;

public class Ability : VFECore.Abilities.Ability
{
    public new AbilityDef def;
    public List<AbilityUpgrade> upgrades;

    // Trackers
    public readonly TrackerContainer<ExplosionTracker, Explosion_AbilityUpgradeDef> explosionTrackers = new();

    public override void Init()
    {
        base.def = def;  // We use new to refer to our def, but base Ability still needs def set to VFECore.AbilityDef
        base.Init();
        upgrades ??= new List<AbilityUpgrade>();
        foreach (AbilityUpgradeDef abilityUpgradeDef in def.abilityUpgradeDefs)
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
