using Prepatcher;
using Verse;

namespace RimWorldOfMagic;

public static class PawnPrepatcher
{
    [PrepatcherField]
    [InjectComponent]
    public static extern CompAbilities RWoMComp(this Pawn target);
}

public class CompAbilities
{

}
