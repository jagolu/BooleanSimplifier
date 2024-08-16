using BooleanSimplifier.Constants;
using BooleanSimplifier.Src;

namespace BooleanSimplifier.Models
{
    internal class BooleTree
    {
        public BooleTree(string basestr, Operator op)
        {
            this.Operator = op;
            items = new();
            children = new();
            var parts = Funcs.getGroupsOperator(basestr, op);

            if (parts == null || parts.Count < 1) return;

            parts.ForEach(part =>
            {
                string formatted = part.removeStartingEndingParameters();
                bool hasOutterOR = formatted.hasOutterOperator(Operator.OR);
                bool hasAnyOr = formatted.hasSomeOperator(Operator.OR);
                bool hasAnyOperator = formatted.hasAnyOperator();

                if (!hasAnyOperator) items.Add(part);
                else if (hasAnyOr)
                {
                    BooleTree child = new BooleTree(formatted, hasOutterOR ? Operator.OR : Operator.AND);
                    children.Add(child);
                }
                else if (op.getBooleOp() != Constants.Constants.AND_OPERATOR)
                {
                    items.Add(part);
                }
                else
                {
                    BooleTree child = new BooleTree(formatted, hasOutterOR ? Operator.OR : Operator.AND);
                    children.Add(child);
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
                        string starting = elQuery.index > 0 ? $" {Constants.Constants.OR_OPERATOR.ToString()} " : string.Empty;
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
            if (Operator.getBooleOp() == Constants.Constants.OR_OPERATOR)
            {
                if (children != null)
                {
                    children.ForEach(child =>
                        child.sumarize().ForEach(x => result.Add(x))
                    );
                }

                items.ForEach(i => result.Add(i));
            }
            else if (Operator.getBooleOp() == Constants.Constants.AND_OPERATOR)
            {
                string appended = string.Empty;
                if (items != null) 
                    items.ForEach(x => appended += Constants.Constants.AND_OPERATOR + x);

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
    }
}
