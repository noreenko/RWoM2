using System.Collections.Generic;
using HarmonyLib;
using Prepatcher;
using Verse;
using VFECore.Abilities;

namespace RimWorldOfMagic.Patches.VFECore.Abilities;

/*
 * An AbilityUpgrade is a collection of attributes on an ability that define how an ability
 * can be upgraded. The actual implementation of the upgrade would be found in the ability, projectile, etc.
 */
public class AbilityUpgrade
{
    public string label;  // When left empty, it assumes there is no skill
    public int value;  // What level the AbilitySkill is actually on (how many times upgraded)
    public int costToUpgrade;  // How many points to upgrade value
    public int maxValue;  // How high can value go

    private AbilityUpgrade(string label, int costToUpgrade = 1, int maxValue = 3)
    {
        this.label = label;
        this.costToUpgrade = costToUpgrade;
        this.maxValue = maxValue;
    }

    // Used to verify there is no ability skill with label
    public static AbilityUpgrade Null = new AbilityUpgrade(string.Empty);

    // TODO: Implement
    public bool canUpgrade(CompAbilities compAbilities)
    {
        return false;
    }

    // TODO: Implement
    public void upgrade(CompAbilities compAbilities)
    {

    }
}

// ===== Prepatcher ===================================================================================================

public static class AbilityPatches
{
    [PrepatcherField]
    public static extern ref List<AbilityUpgrade> upgrades(this Ability target);
    public static List<AbilityUpgrade> Upgrades(this Ability target) =>
        target.upgrades() ??= new List<AbilityUpgrade>();

    private static AbilityUpgrade GetUpgradeOrNullUpgrade(this Ability target, string label)
    {
        for (int i = 0; i < target.Upgrades().Count; i++)
        {
            if (target.Upgrades()[i].label == label) return target.Upgrades()[i];
        }
        return AbilityUpgrade.Null;
    }

    // These are commonly accessed AbilitySkills, so I've added a  quicker way of accessing them
    [PrepatcherField]
    private static extern ref AbilityUpgrade power(this Ability target);
    public static AbilityUpgrade Power(this Ability target) => target.power() ??= target.GetUpgradeOrNullUpgrade("Power");

    [PrepatcherField]
    private static extern ref AbilityUpgrade efficiency(this Ability target);
    public static AbilityUpgrade Efficiency(this Ability target) => target.efficiency() ??= target.GetUpgradeOrNullUpgrade("Efficiency");

    [PrepatcherField]
    private static extern ref AbilityUpgrade versatility(this Ability target);
    public static AbilityUpgrade Versatility(this Ability target) => target.versatility() ??= target.GetUpgradeOrNullUpgrade("Versatility");

    [PrepatcherField]
    private static extern ref AbilityUpgrade level(this Ability target);
    public static AbilityUpgrade Level(this Ability target) => target.level() ??= target.GetUpgradeOrNullUpgrade("Level");

}

// ===== Harmony Patches ==============================================================================================

[HarmonyPatch(typeof(Ability), "postExposeData", null)]
public class AbilityPostExposeData_Patch
{
    private static void Postfix(Ability __instance)
    {
        Scribe_Collections.Look(ref __instance.upgrades(),"RWoM_upgrades", LookMode.Deep);
    }
}
