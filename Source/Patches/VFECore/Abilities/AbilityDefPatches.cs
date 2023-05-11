using System;
using Prepatcher;
using VFECore.Abilities;

namespace RimWorldOfMagic.Patches.VFECore.Abilities;

// ===== Prepatcher ===================================================================================================

public static class AbilityDefPatches
{
    [PrepatcherField]
    public static extern ref Action upgradePoints(this AbilityDef target);
}

// ===== Harmony Patches ==============================================================================================
// TODO: When loading defs we need to prefix when tags are read into the object and intercept for upgradePoints.
