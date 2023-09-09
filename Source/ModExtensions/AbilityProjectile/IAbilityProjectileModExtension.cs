using RimWorldOfMagic.Core;
using Verse;

namespace RimWorldOfMagic.ModExtensions.AbilityProjectile;

public interface IAbilityProjectileModExtension
{
    // equipmentDef and hitThing can be null
    public void PostImpact(Core.AbilityProjectile abilityProjectile, Map map, Thing launcher, ThingDef equipmentDef, Thing hitThing);
}
