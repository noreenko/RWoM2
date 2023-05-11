using System;
using System.Collections.Generic;
using RimWorld;
using RimWorldOfMagic.Patches.VFECore.Abilities;
using UnityEngine;
using Verse;
using VFECore.Abilities;
using Random=UnityEngine.Random;

namespace RimWorldOfMagic.ThingDefs.Projectiles;

public class Fireball : AbilityProjectile
{
    protected override void Impact(Thing hitThing, bool blockedByShield = false)
    {
        CellRect cellRect = CellRect.CenteredOn(Position, 4);
        Map map = Map;

        base.Impact(hitThing, blockedByShield: blockedByShield);

        cellRect.ClipInsideMap(map);
        List<IntVec3> validCells = new List<IntVec3>();
        foreach (IntVec3 cell in cellRect)
        {
            if (cell.IsValid && !cell.Fogged(map)) validCells.Add(cell);
        }

        if (validCells.Count == 0) return;
        int level = ability.Level().value;
        DamageDef damageDef = DefArrays.RWoM_Fireball[level];
        for (int i = 0; i < level * 3; i++)
        {
            FireExplosion(validCells.RandomElement(), map, damageDef, launcher, equipmentDef);
        }
    }

    private void FireExplosion(IntVec3 center, Map map, DamageDef dmgDef, Thing instigator, ThingDef source)
    {
        FleckMaker.Static(center, map, FleckDefOf.ExplosionFlash);
        int damage = (int)Math.Round((float)Random.Range(6, def.projectile.GetDamageAmount(1) / 2));
        Explosion explosion = (Explosion)GenSpawn.Spawn(ThingDefOf.Explosion, center, map);
        explosion.Position = center;
        explosion.radius = 2.2f;
        explosion.damType = dmgDef;
        explosion.instigator = instigator;
        explosion.damAmount = damage;
        explosion.weapon = source;
        explosion.damageFalloff = true;
        explosion.chanceToStartFire = 0.05f;
        explosion.StartExplosion(null, null);
    }
}
