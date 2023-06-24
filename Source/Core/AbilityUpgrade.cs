using Verse;
using VFECore.Abilities;

namespace RimWorldOfMagic.Core;

/*
 * An AbilityUpgrade is a collection of attributes on an ability that define how an ability
 * can be upgraded. The actual implementation of the upgrade would be found in the ability, projectile, etc.
*/
public class AbilityUpgrade : IExposable
{
    public Def def;
    public Ability ability;
    public bool unlocked;

    // TODO: Implement
    public virtual bool canUpgrade(CompAbilities compAbilities)
    {
        return false;
    }

    // TODO: Implement
    public virtual void upgrade(CompAbilities compAbilities)
    {
        // Find point system and subtract points
        // if that succeeded:
        unlocked = true;
    }

    public virtual void ExposeData()
    {
        Scribe_Defs.Look(ref def, "def");
        Scribe_References.Look(ref ability, "ability");
        Scribe_Values.Look(ref unlocked, "unlocked");
    }
}
