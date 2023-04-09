using RimWorld.Planet;
using RimWorldOfMagic.AbilityExtensions;
using Verse;
using VFECore.Abilities;

namespace RimWorldOfMagic.Abilities.Abstract;

public class Ability_InaccurateProjectile : Ability_ShootProjectile
{
    public override void Cast(params GlobalTargetInfo[] targets)
    {
        float radius = def.GetModExtension<AbilityExtension_InaccurateProjectile>().forcedMissRadius;
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i] = new GlobalTargetInfo(
                GenRadial.RadialCellsAround(targets[0].Cell, radius, true).RandomElement(),
                targets[i].Map
            );
        }
        base.Cast(targets);
    }
}
