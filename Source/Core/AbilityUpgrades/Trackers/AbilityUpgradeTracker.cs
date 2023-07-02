namespace RimWorldOfMagic.Core.AbilityUpgrades.Trackers;

/*
 * Tracks the unlocked Ability Upgrade offsets to apply to the ability. Make sure to call Ability.UpdateTracker
 * on AbilityUpgrade changes.
 *
 * Offsets happen before any multiplication, multipliers happen last
 */
public class AbilityUpgradeTracker {
    public float rangeOffset;
    public float rangeMultiplier = 1f;
    public float minRangeOffset;
    public float minRangeMultiplier = 1f;

}
