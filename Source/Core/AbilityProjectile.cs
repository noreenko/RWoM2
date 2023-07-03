using System.Collections.Generic;
using RimWorld;
using RimWorldOfMagic.Core.AbilityUpgrades.Trackers;
using RimWorldOfMagic.ModExtensions.AbilityProjectile;
using Verse;

namespace RimWorldOfMagic.Core;

public class AbilityProjectile : VFECore.Abilities.AbilityProjectile
{
    public new Ability ability;

    public Dictionary<string, ExplosionTracker> explosionTrackers = new();

    protected override void DoImpact(Thing hitThing, Map map)
    {
        base.DoImpact(hitThing, map);
        foreach (DefModExtension extension in def.modExtensions)
        {
            if (extension is IAbilityProjectileModExtension abilityProjectileModExtension)
            {
                abilityProjectileModExtension.PostImpact(this, map, launcher, equipmentDef, hitThing);
            }
        }
    }
}
