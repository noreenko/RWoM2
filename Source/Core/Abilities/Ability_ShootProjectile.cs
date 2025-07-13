using RimWorld.Planet;
using Verse;
using VEF.Abilities;

namespace RimWorldOfMagic.Core.Abilities;

/*
 * Near Identical in function to VEF.Abilities.Abilities.Ability_ShootProjectile.
 * Unfortunately C# doesn't allow multiple inheritance, so we duplicate the code here instead of patching Ability which
 * would affect other mods using VEF.Abilities
 */
public class Ability_ShootProjectile : Ability
{
    public override void Cast(params GlobalTargetInfo[] targets)
    {
        base.Cast(targets);
        foreach (GlobalTargetInfo target in targets)
        {
            ShootProjectile(target);
        }
    }

    protected virtual Projectile ShootProjectile(GlobalTargetInfo target)
    {
        Projectile projectile = GenSpawn.Spawn(def.GetModExtension<AbilityExtension_Projectile>().projectile, pawn.Position, pawn.Map) as Projectile;
        if (projectile is AbilityProjectile abilityProjectile)
        {
            abilityProjectile.Ability = this;
        }
        if (target.HasThing)
            projectile?.Launch(pawn, pawn.DrawPos, target.Thing, target.Thing, ProjectileHitFlags.IntendedTarget);
        else
            projectile?.Launch(pawn, pawn.DrawPos, target.Cell, target.Cell, ProjectileHitFlags.IntendedTarget);
        return projectile;
    }

    public override void CheckCastEffects(GlobalTargetInfo[] targetInfos, out bool cast, out bool target, out bool hediffApply)
    {
        base.CheckCastEffects(targetInfos, out cast, out _, out _);
        target = false;
        hediffApply = false;
    }
}
