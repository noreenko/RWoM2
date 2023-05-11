using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Prepatcher;
using RimWorldOfMagic.AbilityExtensions;
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
    // ===== Ability Upgrades ===============================================================
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

    // ===== END Ability Upgrades ===========================================================

    // AbilityExtensions.RandomDamageMultiplier
    public static float GetRandomPowerForPawn(this Ability target)
    {
        float power = target.GetPowerForPawn();
        var multiplier = target.def.GetModExtension<RandomDamageMultiplier>();
        return multiplier != null ? Rand.Range(power * multiplier.min, power * multiplier.max) : power;
    }
}

// ===== Harmony Patches ==============================================================================================

[HarmonyPatch(typeof(Ability), nameof(Ability.ExposeData))]
public class AbilityExposeData_Patch
{
    private static void Postfix(Ability __instance)
    {
        Scribe_Collections.Look(ref __instance.upgrades(),"RWoM_upgrades", LookMode.Deep);
    }
}


// AbilityExtensions.RandomDamageMultiplier
[HarmonyPatch(typeof(Ability), nameof(Ability.GetDescriptionForPawn))]
public class AbilityGetDescriptionForPawn_Patch
{
    /// Change the GetPowerForPawn part of GetDescriptionForPawn to handle Random ranges
    ///
    /// <param name="instructions"></param>
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var editor = new CodeMatcher(instructions);
        var damageRange = AccessTools.Method(typeof(AbilityGetDescriptionForPawn_Patch), nameof(DamageRange));
        // ---------------------ORIGINAL--------------------
        // float powerForPawn = this.GetPowerForPawn();
        // if ((double) powerForPawn > 0.0)
        //     stringBuilder.AppendLine(string.Format("{0}: {1}", (object) "VFEA.AbilityStatsPower".Translate(), (object) powerForPawn).Colorize(Color.cyan));
        editor.Start().MatchEndForward(
            new CodeMatch(OpCodes.Ldstr, "VFEA.AbilityStatsPower"),
            new CodeMatch(OpCodes.Call),
            new CodeMatch(OpCodes.Box),
            new CodeMatch(OpCodes.Ldloc_S)
        );

        // --------------------MODIFIED-----------------------
        // float powerForPawn = this.GetPowerForPawn();
        // if ((double) powerForPawn > 0.0)
        //     stringBuilder.AppendLine(string.Format("{0}: {1}", (object) "VFEA.AbilityStatsPower".Translate(), (object) DamageRange(powerForPawn, ability)).Colorize(Color.cyan))
        if (!editor.IsInvalid)
        {
            return editor
            .Advance(1)
            .InsertAndAdvance(new CodeInstruction(OpCodes.Ldarg_0))
            .InsertAndAdvance(new CodeInstruction(OpCodes.Call, damageRange))
            .RemoveInstruction()  // Remove Box since strings are objects
            .InstructionEnumeration();
        }
        Log.Error("[RimWorld of Magic] Transpiler could not find target. There may be a mod conflict, or RimWorld/Vanilla Framework Expanded updated?");
        return editor.InstructionEnumeration();
    }

    public static string DamageRange(float powerForPawn, Ability ability)
    {
        var multiplier = ability.def.GetModExtension<RandomDamageMultiplier>();
        if (multiplier == null) return $"{(int)Math.Round(powerForPawn, 0)}";
        return $"{(int)Math.Round(powerForPawn * multiplier.min, 0)} - {(int)Math.Round(powerForPawn * multiplier.max, 0)}";
    }
}
