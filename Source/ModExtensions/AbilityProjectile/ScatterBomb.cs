﻿using RimWorldOfMagic.Core;
using Verse;

namespace RimWorldOfMagic.ModExtensions.AbilityProjectile;

/*
 * DefModExtension for an AbilityProjectile. Triggers explosions randomly within radius after impact.
 */
public class ScatterBomb : AbilityModExtension
{
    public int quantity;  // Base number of explosions
    public IntRange ticksBetweenExplosions = IntRange.zero;  // Waits a random number in range between explosions
    public float power;  // Power of each explosion
    public float radius;

    // All of these are passed to each DoExplosion call.
    public float explosionRadius;
    public DamageDef damageDef;
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
}
