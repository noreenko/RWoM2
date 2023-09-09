using Verse;

namespace RimWorldOfMagic.Core.AbilityUpgrades.Trackers;

public interface ITracker<in TDef> where TDef : Def
{
    public void UpdateTracker(TDef def);
}
