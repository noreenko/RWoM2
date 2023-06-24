using System.Collections.Generic;
using Verse;

namespace RimWorldOfMagic.Core;

public abstract class AbilityUpgradeDefBase : Def
{
    public string upgradeCostType;  // Which upgrade point type to use (Magic or Might)?
    public int costToUnlock;  // How many points to unlock upgrade
    public List<AbilityUpgradeDefBase> requiredUpgrades;  // Upgrades that are pre-reqs for this one
    public List<AbilityUpgradeDefBase> additionalUpgrades;  // Additional Upgrades that get unlocked when this one is
}
