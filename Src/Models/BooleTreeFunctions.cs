using BooleanSimplifier.Models;
using System.Runtime.CompilerServices;

namespace BooleanSimplifier.Src.Models
{
    internal static class BooleTreeFunctions
    {
        public static List<string> getDistinctVars(this BooleTree query)
        {
            List<string> vars = new List<string>();
            query.getFunctionAsSummatory().ForEach(elQuery =>
            {
                elQuery.elements.ForEach(el =>
                {
                    vars.Add(el.name);
                });
            });

            return vars.Distinct().ToList();
        }

        public static List<BooleConjunction> getFunctionAsSummatory(this BooleTree query)
        {
            List<BooleConjunction> list = new List<BooleConjunction>();
            query.sumarize().ForEach(elQuery =>
            {
                BooleConjunction singleElement = new BooleConjunction();
                elQuery.Split(Constants.Constants.AND_OPERATOR).ToList()
                    .Where(el => el.Length > 0).ToList()
                    .ForEach(singleEl =>
                    {
                        singleElement.elements.Add(new(singleEl));
                    });
                list.Add(singleElement);
            });
            return list;
        }
    }
}
