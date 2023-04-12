using System.Collections.Generic;
using Verse;
using VFECore.Abilities;
using AbilityDef=VFECore.Abilities.AbilityDef;

namespace RimWorldOfMagic;

public class Debug
{
    private const string category = "RimWorld of Magic";

    [DebugAction(
        category,
        actionType = DebugActionType.Action,
        allowedGameStates = AllowedGameStates.PlayingOnMap,
        displayPriority = 1000)
    ]
    private static List<DebugActionNode> AddAbility()
    {
        List<DebugActionNode> debugActionList = new List<DebugActionNode>();
        foreach (AbilityDef abilityDef in DefDatabase<AbilityDef>.AllDefs)
        {
            debugActionList.Add(new DebugActionNode(abilityDef.ToString(), DebugActionType.ToolMapForPawns)
            {
                pawnAction = pawn =>
                {
                    pawn.GetComp<CompAbilities>().GiveAbility(abilityDef);
                    DebugActionsUtility.DustPuffFrom(pawn);
                }
            });
        }

        return debugActionList;
    }
}
