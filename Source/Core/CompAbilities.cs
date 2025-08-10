using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld.Planet;
using Verse;
using VEF.Abilities;
using VEF.AnimalBehaviours;

namespace RimWorldOfMagic.Core;

/*
 * Note: This is a heavily cut version of CompAbilities from VEF. A lot of the code is copy-pasted from there.
 *
 */
public class CompAbilities : ThingComp, PawnGizmoProvider
{
    private Pawn Pawn => (Pawn) parent;

    private List<Ability> learnedAbilities = new();
    public List<Ability> LearnedAbilities => learnedAbilities;

    private List<Ability> abilitiesToTick = new();

    public Ability  currentlyCasting;
    public GlobalTargetInfo[] currentlyCastingTargets;

    public bool IsMagic { get; set; } = false;

    public override void PostSpawnSetup(bool respawningAfterLoad)
    {
        base.PostSpawnSetup(respawningAfterLoad);
        learnedAbilities ??= new List<Ability>();
    }

    public void GiveAbility(AbilityDef abilityDef)
    {
        if (learnedAbilities.Any(ab => ab.def == abilityDef))
            return;

        Ability ability = (Ability) Activator.CreateInstance(abilityDef.abilityClass);
        ability.def = abilityDef;
        ability.pawn = Pawn;
        ability.holder = Pawn;
        ability.Init();

        learnedAbilities.Add(ability);
        if (ability.def.needsTicking)
        {
            abilitiesToTick.Add(ability);
        }
        learnedAbilities = LearnedAbilities.OrderBy(static ability => ability.def.label).ToList();
        IsMagic = true;  // TODO This should actually calculate
    }

    public bool HasAbility(AbilityDef abilityDef)
    {
        foreach (Ability learnedAbility in learnedAbilities)
            if (learnedAbility.def == abilityDef)
                return true;
        return false;
    }

    public override void CompTick()
    {
        base.CompTick();
        int abilitiesToTickCount = abilitiesToTick.Count;
        for (var i = 0; i < abilitiesToTickCount; i++)
            abilitiesToTick[i].Tick();
    }

    public override string CompInspectStringExtra() => string.Empty;

    public List<GlobalTargetInfo> tmpCurrentlyCastingTargets;
    public override void PostExposeData()
    {
        base.PostExposeData();
        Scribe_Collections.Look(ref learnedAbilities, nameof(learnedAbilities), LookMode.Deep);
        Scribe_References.Look(ref currentlyCasting, nameof(currentlyCasting));

        tmpCurrentlyCastingTargets = currentlyCastingTargets?.ToList() ?? new List<GlobalTargetInfo>();
        Scribe_Collections.Look(ref tmpCurrentlyCastingTargets, nameof(currentlyCastingTargets));
        currentlyCastingTargets = tmpCurrentlyCastingTargets.ToArray();

        if (learnedAbilities == null)
            learnedAbilities = new List<Ability>();
        else switch (Scribe.mode)
        {
            case LoadSaveMode.LoadingVars:
            {
                foreach (Ability ability in learnedAbilities)
                {
                    ability.holder = parent;
                }

                break;
            }
            case LoadSaveMode.PostLoadInit:
            {
                foreach (Ability ability in learnedAbilities)
                {
                    if (ability.pawn == null && parent is Pawn pawn)
                    {
                        ability.pawn = pawn;
                    }
                }

                break;
            }
        }
        if (learnedAbilities.Any())
        {
            abilitiesToTick = learnedAbilities.Where(static x => x.def.needsTicking).ToList();
        }
    }

    public IEnumerable<Gizmo> GetGizmos()
    {
        foreach (Ability ability in learnedAbilities)
            if (ability.ShowGizmoOnPawn())
                yield return ability.GetGizmo();

        var hediffsAbilities = new List<Hediff_Abilities>();
        Pawn.health.hediffSet.GetHediffs(ref hediffsAbilities);
        foreach (Hediff_Abilities hediff in hediffsAbilities)
        {
            foreach (Gizmo gizmo in hediff.DrawGizmos())
                yield return gizmo;
        }
    }
}
