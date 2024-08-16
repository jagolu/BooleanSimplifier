using BooleanSimplifier.Src;

namespace BooleanSimplifier.Models
{
    internal class BooleTable
    {
        List<BooleConjunction> lines = new List<BooleConjunction>();

        public BooleTable(List<string> vars)
        {
            string negative = Constants.Constants.NEGATION_OPERATOR;
            List<bool> vals = new List<bool>();
            List<int> resetAt = new List<int>();
            List<int> switchAt = new List<int>();
            List<int> actualCounter = new List<int>();
            vars.Select((value, index) => new { index, value }).ToList().ForEach(x =>
            {
                int pow = (int)Math.Pow(2, vars.Count - x.index);
                resetAt.Add(pow);
                switchAt.Add(pow / 2);
                actualCounter.Add(0);
                vals.Add(false);
            });


            for (int i = 0; i < Math.Pow(2, vars.Count); i++)
            {
                List<BooleanConjunctionElement> strVars = new List<BooleanConjunctionElement>();
                vars.Where(x => x != null && x.Length > 0).Select((value, index) => new { index, value }).ToList().ForEach(x =>
                {
                    if (switchAt[x.index] == actualCounter[x.index])
                    {
                        vals[x.index] = !vals[x.index];
                    }
                    if (resetAt[x.index] == actualCounter[x.index])
                    {
                        actualCounter[x.index] = 0;
                        vals[x.index] = false;
                    }

                    if (x.value == null || x.value.Length == 0)
                    {

                    }
                    strVars.Add(new((vals[x.index] ? "" : negative) + x.value));
                    actualCounter[x.index]++;
                });
                lines.Add(new BooleConjunction() { elements = strVars });
            }
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
                List<string> vals = new List<string>();
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
