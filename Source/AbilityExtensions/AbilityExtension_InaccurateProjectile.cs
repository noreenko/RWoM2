using Verse;

namespace RimWorldOfMagic.AbilityExtensions;

/*
 * Requires the abilityClass to inherit from Ability_InaccurateProjectile or to
 * manually override Cast to handle targeting
 */
public class AbilityExtension_InaccurateProjectile : DefModExtension
{
    public float forcedMissRadius;
}
