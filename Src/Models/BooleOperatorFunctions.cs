using BooleanSimplifier.Constants;

namespace BooleanSimplifier.Src.Models
{
    internal static class BooleOperatorFunctions
    {
        public static char getBooleOp(this Operator op)
        {
            switch (op)
            {
                case Operator.AND:
                    return Constants.Constants.AND_OPERATOR;
                case Operator.OR:
                    return Constants.Constants.OR_OPERATOR;
                default:
                    return Constants.Constants.AND_OPERATOR;
            }
        }
    }
}
