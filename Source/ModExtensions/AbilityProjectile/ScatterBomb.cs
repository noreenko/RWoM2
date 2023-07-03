using System;
using System.Collections.Generic;
using RimWorld;
using RimWorldOfMagic.Core.AbilityUpgrades.Trackers;
using Verse;
using VFECore.Abilities;
using Ability=RimWorldOfMagic.Core.Ability;

namespace RimWorldOfMagic.ModExtensions.AbilityProjectile;

/*
 * DefModExtension for an AbilityProjectile. Triggers explosions randomly within radius after impact.
 */
public class ScatterBomb : AbilityModExtension, IAbilityProjectileModExtension
{
    public int quantity;  // Base number of explosions
    //public IntRange ticksBetweenExplosions = IntRange.zero;  // Waits a random number in range between explosions
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
        if (abilityProjectile == null) Log.Error("abilityProjectile is null");
        if (launcher == null) Log.Error("launcher is null");
        if (equipmentDef == null) Log.Error("equipmentDef is null");
        if (hitThing == null) Log.Error("hitThing is null");
        IntVec3 position = abilityProjectile.Position;
        Ability ability = abilityProjectile.ability;
        CellRect cellRect = CellRect.CenteredOn(position, (int)Math.Round(radius));


        cellRect.ClipInsideMap(map);
        List<IntVec3> validCells = new List<IntVec3>();
        foreach (IntVec3 cell in cellRect)
        {
            if (cell.IsValid && !cell.Fogged(map)) validCells.Add(cell);
        }

        if (validCells.Count == 0) return;
        ExplosionTracker tracker = ability.explosionTrackers.GetTracker(upgradeExtensionKey);
        if (tracker == null) Log.Error("Tracker is null");
        int finalQuantity = tracker.GetQuantity(quantity);
        if (finalQuantity <= 0) return;

        // Final stat calculations
        float modifiedPowerForPawn = ability.CalculateModifiedStatForPawn(
            power + tracker.powerOffset,
            ability.def.powerStatFactors,
            ability.def.powerStatOffsets
        );
        float nonRandomPower = modifiedPowerForPawn * tracker.powerMultiplier;
        AbilityExtension_RandomPowerMultiplier randomPowerExtension = ability.def.GetModExtension<AbilityExtension_RandomPowerMultiplier>();
        float finalExplosionRadius = tracker.GetExplosionRadius(explosionRadius);
        float finalArmorPenetration = tracker.GetArmorPenetration(armorPenetration);
        float finalPostExplosionSpawnChance = tracker.GetPostExplosionSpawnChance(postExplosionSpawnChance);
        int finalPostExplosionSpawnThingCount = tracker.GetPostExplosionSpawnThingCount(postExplosionSpawnThingCount);
        float finalPreExplosionSpawnChance = tracker.GetPreExplosionSpawnChance(preExplosionSpawnChance);
        int finalPreExplosionSpawnThingCount = tracker.GetPreExplosionSpawnThingCount(preExplosionSpawnThingCount);
        float finalChanceToStartFire = tracker.GetChanceToStartFire(chanceToStartFire);

        for (int i = 0; i < finalQuantity; i++)
        {
            IntVec3 cell = validCells.RandomElement();
            //FleckMaker.Static(center, map, FleckDefOf.ExplosionFlash);
            GenExplosion.DoExplosion(
                cell, map, finalExplosionRadius, damageDef, launcher,
                damAmount: (int)Math.Round(randomPowerExtension != null
                    ? nonRandomPower * randomPowerExtension.range.RandomInRange
                    : nonRandomPower
                ),
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
