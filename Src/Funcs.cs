using BooleanSimplifier.Constants;
using BooleanSimplifier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanSimplifier.Src
{
    internal static class Funcs
    {
        public static string removeStartingEndingParameters(this string str)
        {
            if (str == null || str == String.Empty) return string.Empty;
            if (str.Length < 3) return str;
            int left = str.Count(x => x == Constants.Constants.LEFT_PARENTESIS);
            int right = str.Count(x => x == Constants.Constants.RIGHT_PARENTESIS);

            if (left != right) return str;
            else if (str[0] == Constants.Constants.LEFT_PARENTESIS && str[str.Length - 1] == Constants.Constants.RIGHT_PARENTESIS)
            {
                return str.Substring(1, str.Length - 2);
            }
            else return str;
        }

        public static List<string> getConjuntoAndOperator(this string part)
        {
            int lastIndex = part.Length - 1;
            if (part == null || part == string.Empty) return new();
            else if (part.Length == 1) return new() { part };
            else return new() { part.Substring(0, lastIndex - 1), part.Substring(lastIndex - 1, lastIndex) };
        }

        public static char getOp(this Operator op)
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

        public static bool hasOutterOperator(this string str, Operator op)
        {
            if (str == null || str == String.Empty) return false;
            if (str.Length < 3) return false;
            int left = str.Count(x => x == Constants.Constants.LEFT_PARENTESIS);
            int right = str.Count(x => x == Constants.Constants.RIGHT_PARENTESIS);
            int countParentesis = 0;
            bool ret = false;

            if (left != right) return false;


            foreach (char c in str)
            {
                if (c == Constants.Constants.LEFT_PARENTESIS)
                {
                    countParentesis++;
                }
                else if (c == Constants.Constants.RIGHT_PARENTESIS)
                {
                    countParentesis--;
                }
                else if (c == op.getOp() && countParentesis == 0)
                {
                    ret = true;
                    break;
                }
            }

            return ret;
        }

        public static bool hasSomeOperator(this string str, Operator op)
        {
            if (str == null || str == String.Empty) return false;
            return str.Contains(op.getOp());
        }

        public static bool hasAnyOperator(this string str)
        {
            return str.hasSomeOperator(Operator.OR) || str.hasSomeOperator(Operator.AND);
        }

        public static List<string> getGroupsOperator(string str, Operator op, List<string>? retValue = null)
        {
            if (retValue == null) retValue = new List<string>();
            if (str == null || str.Length == 0) return retValue;
            str = str.removeStartingEndingParameters();

            string conjunto = string.Empty;
            int countParentesis = 0;
            String nextFunction = String.Empty;

            foreach (char c in str)
            {
                if (c == Constants.Constants.LEFT_PARENTESIS)
                {
                    conjunto += c;
                    countParentesis++;
                }
                else if (c == Constants.Constants.RIGHT_PARENTESIS)
                {
                    conjunto += c;
                    countParentesis--;
                }
                else if (c == op.getOp() && countParentesis == 0)
                {
                    nextFunction = c.ToString();
                    break;
                }
                else conjunto += c;
            }

            if (conjunto.Length > 0)
            {
                retValue.Add(conjunto);
                str = str.Remove(str.IndexOf(conjunto), conjunto.Length);
            }
            if (nextFunction != String.Empty)
            {
                str = str.Remove(str.IndexOf(nextFunction), 1);
            }

            return getGroupsOperator(str, op, retValue);
        }

        public static List<List<BooleanConjunctionElement>> getSummarizedList(this BooleTree query)
        {
            List<List<BooleanConjunctionElement>> list = new List<List<BooleanConjunctionElement>>();
            query.sumarize().ForEach(elQuery =>
            {
                List<BooleanConjunctionElement> singleElement = new List<BooleanConjunctionElement>();
                elQuery.Split("*").ToList().Where(el => el.Length > 0).ToList().ForEach(singleEl =>
                {
                    singleElement.Add(new(singleEl.ToString()));
                });
                list.Add(singleElement);
            });
            return list;
        }

        public static List<string> getDifferentVars(this BooleTree query)
        {
            List<string> vars = new List<string>();
            query.getSummarizedList().ForEach(elQuery =>
            {
                elQuery.ForEach(el =>
                {
                    vars.Add(el.name);
                });
            });

            return vars.Distinct().ToList();
        }

        public static bool hasTrueValue(List<BooleanConjunctionElement> cellInfo, List<BooleanConjunctionElement> val)
        {
            bool hasTrue = true;
            val.ForEach(subVal =>
            {
                hasTrue = hasTrue && cellInfo.Any(c =>
                {
                    return c.name == subVal.name && c.val == subVal.val;
                });
            });

            return hasTrue;
        }
    }
}
