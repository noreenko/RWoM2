using System.Collections.Generic;
using RimWorldOfMagic.Core.AbilityUpgrades.Trackers;

namespace RimWorldOfMagic.Core;

public class AbilityProjectile : VFECore.Abilities.AbilityProjectile
{
    public new Ability ability;

    public Dictionary<string, ExplosionTracker> explosionTrackers = new();
}
