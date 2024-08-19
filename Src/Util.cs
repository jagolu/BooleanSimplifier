using BooleanSimplifier.Constants;
using BooleanSimplifier.Models;

namespace BooleanSimplifier.Src
{
    internal static class Util
    {
        public static List<BooleConjunction> getAllPosibilities(List<string> vars)
        {
            List<BooleConjunction> lines = new List<BooleConjunction>();
            Func<int, int, int> customPow = (index, varsCount) => (int)Math.Pow(2, varsCount - index);
            List<bool> vals = Enumerable.Repeat(false, vars.Count).ToList();
            List<int> actualCounter = Enumerable.Repeat(0, vars.Count).ToList();
            List<int> resetAt = vars.Select((_, i) => customPow(i, vars.Count)).ToList();
            List<int> switchAt = vars.Select((_, i) => customPow(i, vars.Count) / 2).ToList();

            (Enumerable.Range(0, (int)Math.Pow(2, vars.Count))).ToList().ForEach(_ =>
            {
                List<BooleanConjunctionElement> strVars = new List<BooleanConjunctionElement>();
                vars.Where(x => x != null && x.Length > 0)
                    .SelectIndex()
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
                lines.Add(new BooleConjunction(strVars));
            });
            return lines;
        }

        public static List<(T value, int index)> SelectIndex<T>(this IEnumerable<T> myList)
        {
            return myList.Select((v, i) => {
                (T value, int index) retValue = new() { value = v, index = i };
                return retValue;
            }).ToList();
        }
    }
}
