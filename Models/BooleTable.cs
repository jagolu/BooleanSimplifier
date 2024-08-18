using BooleanSimplifier.Constants;
using BooleanSimplifier.Src.Models;

namespace BooleanSimplifier.Models
{
    internal class BooleTable
    {
        List<BooleConjunction> lines = new List<BooleConjunction>();

        public BooleTable(List<string> vars)
        {
            Func<int, int, int> customPow = (index, varsCount) => (int)Math.Pow(2, varsCount - index);
            List<bool> vals = Enumerable.Repeat(false, vars.Count).ToList();
            List<int> actualCounter = Enumerable.Repeat(0, vars.Count).ToList();
            List<int> resetAt = vars.Select((_, i) => customPow(i, vars.Count)).ToList();
            List<int> switchAt = vars.Select((_, i) => customPow(i, vars.Count)/2).ToList();

            (Enumerable.Range(0, (int)Math.Pow(2, vars.Count))).ToList().ForEach(_=>
            {
                List<BooleanConjunctionElement> strVars = new List<BooleanConjunctionElement>();
                vars.Where(x => x != null && x.Length > 0)
                    .Select((value, index) => new { index, value })
                    .ToList().ForEach(item =>
                {
                    if (switchAt[item.index] == actualCounter[item.index])
                    {
                        vals[item.index] = !vals[item.index];
                    }
                    if (resetAt[item.index] == actualCounter[item.index])
                    {
                        actualCounter[item.index] = 0;
                        vals[item.index] = false;
                    }
                    strVars.Add(new(
                        (vals[item.index] ? string.Empty : CONSTANTS.NEGATION_OPERATOR) + item.value
                    ));
                    actualCounter[item.index]++;
                });
                lines.Add(new BooleConjunction() { elements = strVars });
            });
        }

        public void fill(BooleTree boolTree)
        {
            List<BooleConjunction> lines = boolTree.getFunctionAsSummatory();
            this.lines.ForEach(line =>
            {
                bool val = lines.Any(el => line.isEqual(el));
                line.value = val;
            });
        }

        public void show()
        {
            if (lines == null || lines.Count == 0) return;
            string header = string.Empty;
            lines.First().elements.ForEach(el => header += el.name + " ");
            Console.WriteLine(header);
            lines.ForEach(line =>
            {
                List<string> vals = new();
                line.elements.ForEach(cinfo =>
                {
                    vals.Add((cinfo.val ? "1" : "0") + " ");
                });
                string val = string.Empty;
                vals.ForEach(x => val += x);
                Console.WriteLine(val + "   ---> " + (line.value ? "1" : "0"));
            });
        }
    }
}
