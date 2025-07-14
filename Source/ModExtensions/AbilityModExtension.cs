using System.Collections.Generic;
using RimWorldOfMagic.Core;
using RimWorldOfMagic.Core.AbilityUpgrades;
using VEFAbilityUpgradeFramework;
using Verse;

namespace RimWorldOfMagic.ModExtensions;

public class AbilityModExtension : DefModExtension
{
    public string upgradeExtensionKey;
    public List<AbilityUpgradeDef> abilityUpgradeDefs = new();

    public override void ResolveReferences(Def parentDef)
    {
        base.ResolveReferences(parentDef);
        if (string.IsNullOrEmpty(upgradeExtensionKey))
        {
            Log.Error($"AbilityModExtension for {parentDef.defName} has no upgradeExtensionKey set.");
            return;
        }
    }
}