using RimWorldOfMagic.Core;
using Verse;

namespace RimWorldOfMagic.ModExtensions.AbilityProjectile;

public interface IAbilityProjectileModExtension
{
    public void PostImpact(Core.AbilityProjectile abilityProjectile, Map map, Thing launcher, ThingDef equipmentDef, Thing hitThing);
}
