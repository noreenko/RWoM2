using Verse;

namespace RimWorldOfMagic.AbilityExtensions;


/// Makes it so power from VFECore.Abilities.Ability becomes a random range of (min * power) to (max * power) rounded
public class RandomDamageMultiplier: DefModExtension
{
    public float min = 1f;  // value should be <= 1f
    public float max = 1f;  // value should be >= 1f
}
