using System.Collections.Generic;

namespace RimWorldOfMagic.Core.AbilityUpgrades.Trackers;

/*
 * Class to hold and retrieve trackers without having to worry about creating them on the fly
 */
public class TrackerContainer<TTracker> where TTracker : new()
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
            return dictionary[key] = new TTracker();
        }
    }
}
