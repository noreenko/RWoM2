using RimWorldOfMagic.Core;
using Verse;

namespace RimWorldOfMagic.ModExtensions;

public class AbilityModExtension : DefModExtension
{
    public string upgradeExtensionKey = "";  // If attached to a defModExtension, this must be a unique string. empty string is for base ability
    public AbilityDef abilityDef;
}
