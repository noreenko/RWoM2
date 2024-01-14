using System.Collections.Generic;
using Verse;

namespace RimWorldOfMagic.Core.AbilityUpgrades.Trackers;

/*
 * Class to hold and retrieve trackers without having to worry about creating them on the fly
 */
public class TrackerContainer<TTracker, TDef>
    where TTracker : ITracker<TDef>, new()
    where TDef : AbilityUpgradeDef
{
    private Dictionary<string, TTracker> dictionary = new();

    public TTracker GetTracker(string key)
    {
        try
        {
            return dictionary[key];
        }
        catch (KeyNotFoundException)
        {
            TTracker tracker = new TTracker();
            dictionary[key] = tracker;
            return tracker;
        }
    }
}
