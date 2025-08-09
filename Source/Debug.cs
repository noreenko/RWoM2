using System.Collections.Generic;
using System.Linq;
using LudeonTK;
using RimWorldOfMagic.Core;
using Verse;

namespace RimWorldOfMagic;

public class Debug
{
    private const string category = "RimWorld of Magic";

    [DebugAction(
        "Pawns",
        "Give RWoM Ability...",
        actionType = DebugActionType.Action,
        allowedGameStates = AllowedGameStates.PlayingOnMap,
        displayPriority = 1000)
    ]
    private static void GiveRWoMAbility()
    {
        List<DebugMenuOption> list = new List<DebugMenuOption>();
        foreach (AbilityDef def in DefDatabase<AbilityDef>.AllDefs)
        {
            AbilityDef abilityDef = def;

            list.Add(new
                DebugMenuOption(
                    label: abilityDef.LabelCap,
                    mode: DebugMenuOptionMode.Tool,
                    method: () =>
                       {
                           Log.Warning($"abilityDef = {abilityDef}");
                           foreach (Pawn pawn in Find.CurrentMap.thingGrid.ThingsAt(Verse.UI.MouseCell()).OfType<Pawn>())
                           {
                               CompAbilities abilityComp = pawn.TryGetComp<CompAbilities>();
                               if (abilityComp != null)
                               {
                                   abilityComp.GiveAbility(abilityDef);
                                   DebugActionsUtility.DustPuffFrom(pawn);
                               }
                           }
                       }
                )
            );
        }

        Find.WindowStack.Add(new Dialog_DebugOptionListLister(list));
    }
}
