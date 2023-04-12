using Verse;

namespace RimWorldOfMagic.ThingDefs.Projectiles;

public class Fireball : Projectile_Explosive
{
    // TODO: Make fireball correctly determine damage from Ability (Should this be done in Ability when spawning projectile?)
    //      And how many explosions should happen. This function needs some serious refactoring. Unsure if we need a new base class instead of Projectile
    // protected override void Impact(Thing hitThing, bool blockedByShield = false)
    // {
    //     base.Impact(hitThing, blockedByShield);
    //     int damageAmount = def.projectile.GetDamageAmount(1);
    //     GenExplosion.DoExplosion(
    //         Position, Map, def.projectile.explosionRadius, DamageDefOf.Bomb, launcher,
    //         Mathf.RoundToInt(Rand.Range(damageAmount/2, damageAmount)),
    //         0, DefOf.RWoM_SoftExplosion, equipmentDef, def,
    //         chanceToStartFire: 0.1f, damageFalloff: true
    //     );
    //
    //     CellRect cellRect = CellRect.CenteredOn(Position, 5);
    //     cellRect.ClipInsideMap(Map);
    //
    // }
}
