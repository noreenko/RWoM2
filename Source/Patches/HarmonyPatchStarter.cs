using HarmonyLib;
using Verse;

namespace RimWorldOfMagic.Patches;

[StaticConstructorOnStartup]
static class HarmonyPatchStarter
{
    static HarmonyPatchStarter()
    {
        new Harmony("koltonaugust.rwom").PatchAll();
    }
}
