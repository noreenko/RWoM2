using System;
using Verse;
using VFECore.Abilities;

namespace RimWorldOfMagic.Core.AbilityUpgrades.Trackers;

/*
 * Tracks upgrades to any kind of explosion. These can be the ability if it is
 * an Ability_ShootProjectile, or tracking a DefModExtension like ScatterBomb
 */
public class ExplosionTracker : ITracker<Explosion_AbilityUpgradeDef>
{
    public int quantityOffset;  // quantity is included to not have to look up multiple trackers. Not always used.
    public float quantityMultiplier = 1f;
    public float powerOffset;
    public float powerMultiplier = 1f;
    public float radiusOffset;
    public float radiusMultiplier = 1f;
    public float explosionRadiusOffset;
    public float explosionRadiusMultiplier = 1f;
    public float armorPenetrationOffset;
    public float armorPenetrationMultiplier = 1f;
    public float postExplosionSpawnChanceOffset;
    public float postExplosionSpawnChanceMultiplier = 1f;
    public int postExplosionSpawnThingCountOffset;
    public float postExplosionSpawnThingCountMultiplier = 1f;
    public float preExplosionSpawnChanceOffset;
    public float preExplosionSpawnChanceMultiplier = 1f;
    public int preExplosionSpawnThingCountOffset;
    public float preExplosionSpawnThingCountMultiplier = 1f;
    public float chanceToStartFireOffset;
    public float chanceToStartFireMultiplier = 1f;

    public int GetQuantity(int quantity) => (int)Math.Round((quantity + quantityOffset) * quantityMultiplier);

    public int GetPower(float power, Ability ability)
    {
        power = ability.CalculateModifiedStatForPawn(
            power + powerOffset,
            ability.def.powerStatFactors,
            ability.def.powerStatOffsets
        );
        power *= powerMultiplier;
        var randomPowerExtension = ability.def.GetModExtension<AbilityExtension_RandomPowerMultiplier>();
        return (int)Math.Round(randomPowerExtension != null ? power * randomPowerExtension.range.RandomInRange : power);
    }
    public float GetExplosionRadius(float explosionRadius) =>
        (explosionRadius + explosionRadiusOffset) * explosionRadiusMultiplier;
    public float GetArmorPenetration(float armorPenetration) =>
        (armorPenetration + armorPenetrationOffset) * armorPenetrationMultiplier;
    public float GetPostExplosionSpawnChance(float chance) =>
        (chance + postExplosionSpawnChanceOffset) * postExplosionSpawnChanceMultiplier;
    public int GetPostExplosionSpawnThingCount(int count) =>
        (int)Math.Round((count + postExplosionSpawnThingCountOffset) * postExplosionSpawnThingCountMultiplier);
    public float GetPreExplosionSpawnChance(float chance) =>
        (chance + preExplosionSpawnChanceOffset) * preExplosionSpawnChanceMultiplier;
    public int GetPreExplosionSpawnThingCount(int count) =>
        (int)Math.Round((count + preExplosionSpawnThingCountOffset) * preExplosionSpawnThingCountMultiplier);
    public float GetChanceToStartFire(float chance) => (chance + chanceToStartFireOffset) * chanceToStartFireMultiplier;

    public virtual void UpdateTracker(Explosion_AbilityUpgradeDef def)
    {
        radiusOffset += def.radiusOffset;
        radiusMultiplier *= def.radiusMultiplier;
        armorPenetrationOffset += def.armorPenetrationOffset;
        armorPenetrationMultiplier *= def.armorPenetrationMultiplier;
        postExplosionSpawnChanceOffset += def.postExplosionSpawnChanceOffset;
        postExplosionSpawnChanceMultiplier *= def.postExplosionSpawnChanceMultiplier;
        postExplosionSpawnThingCountOffset += def.postExplosionSpawnThingCountOffset;
        postExplosionSpawnThingCountMultiplier *= def.postExplosionSpawnThingCountMultiplier;
        preExplosionSpawnChanceOffset += def.preExplosionSpawnChanceOffset;
        preExplosionSpawnChanceMultiplier *= def.preExplosionSpawnChanceMultiplier;
        preExplosionSpawnThingCountOffset += def.preExplosionSpawnThingCountOffset;
        preExplosionSpawnThingCountMultiplier *= def.preExplosionSpawnThingCountMultiplier;
        chanceToStartFireOffset += def.chanceToStartFireOffset;
        chanceToStartFireMultiplier *= def.chanceToStartFireMultiplier;
    }
}
