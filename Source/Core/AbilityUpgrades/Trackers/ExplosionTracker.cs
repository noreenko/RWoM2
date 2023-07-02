namespace RimWorldOfMagic.Core.AbilityUpgrades.Trackers;

/*
 * Tracks upgrades to any kind of explosion. These can be the ability if it is
 * an Ability_ShootProjectile, or tracking a DefModExtension like ScatterBomb
 */
public class ExplosionTracker : Tracker<Explosion_AbilityUpgradeDef>
{
    public float radiusOffset;
    public float radiusMultiplier = 1f;
    public float armorPenetrationOffset;
    public float armorPenetrationMultiplier = 1f;
    public float postExplosionSpawnChanceOffset;
    public float postExplosionSpawnChanceMultiplier = 1f;
    public int postExplosionSpawnThingCountOffset;
    public int postExplosionSpawnThingCountMultiplier = 1;
    public float preExplosionSpawnChanceOffset;
    public float preExplosionSpawnChanceMultiplier = 1f;
    public int preExplosionSpawnThingCountOffset;
    public int preExplosionSpawnThingCountMultiplier = 1;
    public float chanceToStartFireOffset;
    public float chanceToStartFireMultiplier = 1f;

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
