using BooleanSimplifier.Src;
using BooleanSimplifier.Src.Models;

namespace BooleanSimplifier.Models
{
    internal class BooleTable
    {
        public List<BooleConjunction> lines { get; set; }

        public BooleTable(BooleTree boolTree)
        {
            lines = Util.getAllPosibilities(boolTree.getDistinctVars());
            fill(boolTree);
        }

        private void fill(BooleTree boolTree)
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
