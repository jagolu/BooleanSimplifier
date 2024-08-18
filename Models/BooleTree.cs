using BooleanSimplifier.Constants;
using BooleanSimplifier.Src.Models;

namespace BooleanSimplifier.Models
{
    internal class BooleTree
    {
        public BooleTree(string basestr, Operator op)
        {
            Func<string, Operator, bool> hasSomeOperator = (str, op) =>
                str != null && str != string.Empty && str.Contains(op.getBooleOp());
            Func<string, bool> hasAnyOperator = (str) => hasSomeOperator(str, Operator.OR);

            this.Operator = op;
            items = new();
            children = new();
            var parts = getGroupsOperator(basestr, op);

            if (parts == null || parts.Count < 1) return;

            parts.ForEach(part =>
            {
                string formatted = removeStartingEndingParameters(part);
                Operator childOperator = hasOutterOperator(formatted, Operator.OR) ? Operator.OR : Operator.AND;
                bool hasAnyOr = hasSomeOperator(formatted, Operator.OR);
                bool hasAnyOp = hasAnyOperator(formatted);

                if (!hasAnyOp) items.Add(part);
                else if (hasAnyOr || op.IsEqualToBooleOp(CONSTANTS.AND_OPERATOR))
                {
                    BooleTree child = new BooleTree(formatted, childOperator);
                    children.Add(child);
                }
                else if (!op.IsEqualToBooleOp(CONSTANTS.AND_OPERATOR))
                {
                    items.Add(part);
                }
            });
        }
        private Operator Operator { get; set; }
        private List<BooleTree> children { get; set; }
        private List<string> items { get; set; }

        public string verboseFunctionAsSummatory
        {
            get
            {
                string ret = string.Empty;
                this.getFunctionAsSummatory().Select((value, index) => new { value, index })
                    .ToList().ForEach(elQuery =>
                    {
                        string starting = elQuery.index > 0 ? $" {CONSTANTS.OR_OPERATOR} " : string.Empty;
                        ret += starting + elQuery.value.verbose;
                    });

                return ret;
            }
        }

        public List<string> sumarize()
        {
            List<string> result = new();

            if (
                (children == null || children.Count == 0) && 
                (items == null || items.Count == 0)
            ) return result;
            if (Operator.IsEqualToBooleOp(CONSTANTS.OR_OPERATOR))
            {
                if (children != null)
                {
                    children.ForEach(child =>
                        child.sumarize().ForEach(x => result.Add(x))
                    );
                }

                items.ForEach(i => result.Add(i));
            }
            else if (Operator.IsEqualToBooleOp(CONSTANTS.AND_OPERATOR))
            {
                string appended = string.Empty;
                if (items != null) 
                    items.ForEach(x => appended += CONSTANTS.AND_OPERATOR + x);

                if (children != null && children.Count > 0)
                {
                    children.ForEach(c => 
                        c.sumarize().ForEach(x => result.Add(x + appended))
                    );
                }
                else if (children == null || children.Count == 0)
                {
                    result.Add(appended);
                }
            }

            return result;
        }

        #region private constructor
        private string removeStartingEndingParameters(string str)
        {
            if (str == null || str == String.Empty) return string.Empty;
            if (str.Length < 3) return str;
            int left = str.Count(x => x == CONSTANTS.LEFT_PARENTESIS);
            int right = str.Count(x => x == CONSTANTS.RIGHT_PARENTESIS);

            if (left != right) return str;
            else if (str[0] == CONSTANTS.LEFT_PARENTESIS && str[str.Length - 1] == CONSTANTS.RIGHT_PARENTESIS)
            {
                return str.Substring(1, str.Length - 2);
            }
            else return str;
        }

        private List<string> getGroupsOperator(string str, Operator op, List<string>? retValue = null)
        {
            if (retValue == null) retValue = new List<string>();
            if (str == null || str.Length == 0) return retValue;
            str = removeStartingEndingParameters(str);

            string conjunto = string.Empty;
            int countParentesis = 0;
            string nextFunction = string.Empty;

            foreach (char c in str)
            {
                if (c == CONSTANTS.LEFT_PARENTESIS)
                {
                    conjunto += c;
                    countParentesis++;
                }
                else if (c == CONSTANTS.RIGHT_PARENTESIS)
                {
                    conjunto += c;
                    countParentesis--;
                }
                else if (op.IsEqualToBooleOp(c) && countParentesis == 0)
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

        private bool hasOutterOperator(string str, Operator op)
        {
            if (str == null || str == String.Empty) return false;
            if (str.Length < 3) return false;
            int left = str.Count(x => x == CONSTANTS.LEFT_PARENTESIS);
            int right = str.Count(x => x == CONSTANTS.RIGHT_PARENTESIS);
            int countParentesis = 0;
            bool ret = false;

            if (left != right) return false;


            foreach (char c in str)
            {
                if (c == CONSTANTS.LEFT_PARENTESIS)
                {
                    countParentesis++;
                }
                else if (c == CONSTANTS.RIGHT_PARENTESIS)
                {
                    countParentesis--;
                }
                else if (op.IsEqualToBooleOp(c) && countParentesis == 0)
                {
                    ret = true;
                    break;
                }
            }

            return ret;
        }
        #endregion
    }
}
