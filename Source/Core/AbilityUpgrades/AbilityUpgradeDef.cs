using System.Collections.Generic;
using Verse;

namespace RimWorldOfMagic.Core.AbilityUpgrades;

public class AbilityUpgradeDef : Def
{
    public string upgradeExtensionKey = "";  // If the upgrade is for a defModExtension, match here. "" applies upgrade directly to ability

    public string upgradeCostType;  // Which upgrade point type to use (Magic or Might)?
    public int costToUnlock;  // How many points to unlock upgrade
    public List<AbilityUpgradeDef> requiredUpgrades;  // Upgrades that are pre-reqs for this one
    public List<AbilityUpgradeDef> additionalUpgrades;  // Additional Upgrades that get unlocked when this one is. Skip cost for these abilities.
}
