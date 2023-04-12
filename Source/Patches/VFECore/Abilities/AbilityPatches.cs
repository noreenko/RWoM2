using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Prepatcher;
using Verse;
using VFECore.Abilities;

namespace RimWorldOfMagic.Patches.VFECore.Abilities;

/*
 * An AbilitySkill is a collection of attributes on an ability that define how an ability
 * can be upgraded. The actual implementation of the skill would be found in the ability, projectile, etc.
 */
public class AbilitySkill
{
    public string label;  // When left empty, it assumes there is no skill
    public int value;  // What level the AbilitySkill is actually on (how many times upgraded)
    public int costToUpgrade;  // How many points to upgrade value
    public int maxValue;  // How high can value go

    public AbilitySkill(string label, int costToUpgrade = 1, int maxValue = 3)
    {
        this.label = label;
        this.costToUpgrade = costToUpgrade;
        this.maxValue = maxValue;
    }

    // Used to verify there is no ability skill with label
    public static AbilitySkill Null = new AbilitySkill(string.Empty);

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
    [Prepatcher.DefaultValue(typeof(List<AbilitySkill>))]
    public static extern ref List<AbilitySkill> skills(this Ability target);

    private static AbilitySkill GetSkillOrNullSkill(this Ability target, string label)
    {
        for (int i = 0; i < target.skills().Count; i++)
        {
            if (target.skills()[i].label == label) return target.skills()[i];
        }
        return AbilitySkill.Null;
    }

    // These are commonly accessed AbilitySkills, so I've added a  quicker way of accessing them
    [PrepatcherField]
    private static extern ref AbilitySkill power(this Ability target);
    public static AbilitySkill Power(this Ability target) => target.power() ??= target.GetSkillOrNullSkill("Power");

    [PrepatcherField]
    private static extern ref AbilitySkill efficiency(this Ability target);
    public static AbilitySkill Efficiency(this Ability target) => target.efficiency() ??= target.GetSkillOrNullSkill("Efficiency");

    [PrepatcherField]
    private static extern ref AbilitySkill versatility(this Ability target);
    public static AbilitySkill Versatility(this Ability target) => target.versatility() ??= target.GetSkillOrNullSkill("Versatility");
}

// ===== Harmony Patches ==============================================================================================

[HarmonyPatch(typeof(Ability), "postExposeData", null)]
public class AbilityPostExposeData_Patch
{
    private static void Postfix(Ability __instance)
    {
        Scribe_Collections.Look(ref __instance.skills(),"RWoM_skills", LookMode.Deep);
    }
}
