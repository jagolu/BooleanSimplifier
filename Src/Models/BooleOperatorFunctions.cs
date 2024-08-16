﻿using BooleanSimplifier.Constants;

namespace BooleanSimplifier.Src.Models
{
    internal static class BooleOperatorFunctions
    {
        public static char getBooleOp(this Operator op)
        {
            switch (op)
            {
                case Operator.AND:
                    return CONSTANTS.AND_OPERATOR;
                case Operator.OR:
                    return CONSTANTS.OR_OPERATOR;
                default:
                    return CONSTANTS.AND_OPERATOR;
            }
        }
    }
}
