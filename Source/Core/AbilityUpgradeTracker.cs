using System.Collections.Generic;

namespace RimWorldOfMagic.Core;

/*
 * Tracks the unlocked Ability Upgrade offsets to apply to the ability. Make sure to call Ability.UpdateUpgradeTracker
 * on AbilityUpgrade changes.
 */
public class AbilityUpgradeTracker
{
    public float basePowerOffset;  // BEFORE multipliers `power += basePowerOffset
    public float powerMultiplier = 1f;  // Multiplies the final power by this

}
