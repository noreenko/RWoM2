using VEFAbilityUpgradeFramework;
using Verse;

namespace RimWorldOfMagic.Core.AbilityUpgrades;

/*
 * An AbilityUpgrade is a collection of attributes on an ability that define how an ability
 * can be upgraded. The actual implementation of the upgrade would be found in the ability, projectile, etc.
*/
public class AbilityUpgrade : IExposable
{
    public AbilityUpgradeDef def;
    public Ability ability;
    public bool unlocked;

    // TODO: Implement
    public virtual bool canUpgrade(CompAbilities compAbilities)
    {
        return true;
    }

    public virtual void Upgrade()
    {
        // TODO: Find point system and subtract points
        // if that succeeded:
        Unlock();
        foreach (AbilityUpgradeDef additionalDef in def.additionalUpgradeDefsToUnlock)
        {
            foreach (AbilityUpgrade upgrade in ability.upgrades)
            {
                if (upgrade.def.defName != additionalDef.defName) continue;
                upgrade.Unlock();
                break;
            }
        }
    }

    protected virtual void Unlock()
    {
        unlocked = true;
        ability.UpdateTracker(this);
    }

    public virtual void ExposeData()
    {
        Scribe_Defs.Look(ref def, "def");
        Scribe_References.Look(ref ability, "ability");
        Scribe_Values.Look(ref unlocked, "unlocked");

        if (Scribe.mode == LoadSaveMode.PostLoadInit)
        {
            if (unlocked) ability.UpdateTracker(this);
        }
    }
}
