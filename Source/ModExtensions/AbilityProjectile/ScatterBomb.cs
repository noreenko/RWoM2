using System;
using System.Collections.Generic;
using RimWorldOfMagic.Core.AbilityUpgrades.Trackers;
using Verse;

// ReSharper disable UnassignedField.Global
// ReSharper disable MemberCanBePrivate.Global

namespace RimWorldOfMagic.ModExtensions.AbilityProjectile;

/*
 * DefModExtension for an AbilityProjectile. Triggers explosions randomly within radius after impact.
 */
public class ScatterBomb : AbilityModExtension, IAbilityProjectileModExtension
{
    public int quantity;  // Base number of explosions
    public float power;  // Power of each explosion
    public float radius;

    // All of these are passed to each DoExplosion call.
    public float explosionRadius;
    public DamageDef damageDef;  // Used for non-damage-amount information about the damage
    public float armorPenetration = -1f;
    public SoundDef explosionSound;
    public ThingDef postExplosionSpawnThingDef;
    public float postExplosionSpawnChance;
    public int postExplosionSpawnThingCount;
    public GasType? postExplosionGasType;
    public bool applyDamageToExplosionCellsNeighbors;
    public ThingDef preExplosionSpawnThingDef;
    public float preExplosionSpawnChance;
    public int preExplosionSpawnThingCount;
    public float chanceToStartFire;
    public bool damageFalloff;

    public void PostImpact(Core.AbilityProjectile abilityProjectile, Map map, Thing launcher, ThingDef equipmentDef, Thing hitThing)
    {
        // Get list of valid cells to spawn explosions on. Exit if none.
        CellRect cellRect = CellRect.CenteredOn(abilityProjectile.Position, (int)Math.Round(radius));
        cellRect.ClipInsideMap(map);
        List<IntVec3> validCells = new List<IntVec3>();
        foreach (IntVec3 cell in cellRect)
        {
            if (cell.IsValid && !cell.Fogged(map)) validCells.Add(cell);
        }
        if (validCells.Count == 0) return;

        // Final stat calculations. Outside of for loop for optimization
        ExplosionTracker tracker = abilityProjectile.Ability.explosionTrackers.GetTracker(upgradeExtensionKey);
        int finalQuantity = tracker.GetQuantity(quantity);
        if (finalQuantity <= 0) return;  // early exit condition

        int finalPower = tracker.GetPower(power, abilityProjectile.Ability);
        float finalExplosionRadius = tracker.GetExplosionRadius(explosionRadius);
        float finalArmorPenetration = tracker.GetArmorPenetration(armorPenetration);
        float finalPostExplosionSpawnChance = tracker.GetPostExplosionSpawnChance(postExplosionSpawnChance);
        int finalPostExplosionSpawnThingCount = tracker.GetPostExplosionSpawnThingCount(postExplosionSpawnThingCount);
        float finalPreExplosionSpawnChance = tracker.GetPreExplosionSpawnChance(preExplosionSpawnChance);
        int finalPreExplosionSpawnThingCount = tracker.GetPreExplosionSpawnThingCount(preExplosionSpawnThingCount);
        float finalChanceToStartFire = tracker.GetChanceToStartFire(chanceToStartFire);

        // Do the explosions!
        for (int i = 0; i < finalQuantity; i++)
        {
            IntVec3 cell = validCells.RandomElement();
            GenExplosion.DoExplosion(
                cell, map, finalExplosionRadius, damageDef, launcher,
                damAmount: finalPower,
                armorPenetration: finalArmorPenetration,
                explosionSound: explosionSound,
                weapon: equipmentDef,
                projectile: abilityProjectile.def,
                postExplosionSpawnThingDef: postExplosionSpawnThingDef,
                postExplosionSpawnChance: finalPostExplosionSpawnChance,
                postExplosionSpawnThingCount: finalPostExplosionSpawnThingCount,
                postExplosionGasType: postExplosionGasType,
                applyDamageToExplosionCellsNeighbors: applyDamageToExplosionCellsNeighbors,
                preExplosionSpawnThingDef: preExplosionSpawnThingDef,
                preExplosionSpawnChance: finalPreExplosionSpawnChance,
                preExplosionSpawnThingCount: finalPreExplosionSpawnThingCount,
                chanceToStartFire: finalChanceToStartFire,
                damageFalloff: damageFalloff
            );
        }
    }
}
