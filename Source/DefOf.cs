using System.Diagnostics.CodeAnalysis;
using Verse;
using VFECore.Abilities;

namespace RimWorldOfMagic;

[RimWorld.DefOf]
[SuppressMessage("ReSharper", "UnassignedField.Global")]
public static class DefOf
{
    public static AbilityDef RWoM_Ability_Fireball;

    public static DamageDef RWoM_Fireball;
    public static DamageDef RWoM_Fireball_I;
    public static DamageDef RWoM_Fireball_II;
    public static DamageDef RWoM_Fireball_III;

    public static SoundDef RWoM_SoftExplosion;
}

[StaticConstructorOnStartup]
public static class DefArrays
{
    public static DamageDef[] RWoM_Fireball = {
        DefOf.RWoM_Fireball,
        DefOf.RWoM_Fireball_I,
        DefOf.RWoM_Fireball_II,
        DefOf.RWoM_Fireball_III
    };
}
