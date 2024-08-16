using BooleanSimplifier.Models;

namespace BooleanSimplifier.Src.Models
{
    internal static class BooleConjunctionFunctions
    {
        public static bool isEqual(this BooleConjunction conjunction, BooleConjunction valueToCheck)
        {
            return valueToCheck.elements.All(subVal =>
                conjunction.elements.Any(c =>
                    c.name == subVal.name && c.val == subVal.val
                )
            );
        }
    }
}
