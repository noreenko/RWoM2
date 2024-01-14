using Verse;

namespace RimWorldOfMagic.Core.AbilityUpgrades;

// Alters the DoExplosion call of an ability or DefModExtension
public class Explosion_AbilityUpgradeDef : AbilityUpgradeDef
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
}
