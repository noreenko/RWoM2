using System.Collections.Generic;
using RimWorld;
using RimWorldOfMagic.Core.AbilityUpgrades.Trackers;
using RimWorldOfMagic.ModExtensions.AbilityProjectile;
using Verse;

namespace RimWorldOfMagic.Core;

public class AbilityProjectile : VEF.Abilities.AbilityProjectile
{
    private new Ability ability;

    public Ability Ability
    {
        get => ability;
        set
        {
            ability = value;
            base.ability = value;
        }
    }

    private Dictionary<string, ExplosionTracker> explosionTrackers;
    public Dictionary<string, ExplosionTracker> ExplosionTrackers =>
        explosionTrackers ??= new Dictionary<string, ExplosionTracker>();

    protected override void DoImpact(Thing hitThing, Map map)
    {
        Log.Warning($"hitThing = {hitThing}");
        Log.Warning($"map = {map}");
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
