using BooleanSimplifier.Constants;
using BooleanSimplifier.Src;

namespace BooleanSimplifier.Models
{
    internal class BooleKarnaugh
    {
        private List<BooleConjunction> _booleConjunctions = new List<BooleConjunction>();  
        public BooleKarnaugh(List<string> vars, BooleTable table) {
            createConjunctions(vars);
            fillValues(table.lines.Where(l => !l.value).ToList());
            removeInvalid(table);
        }

        public string verbose
        {
            get
            {
                string ret = string.Empty;
                _booleConjunctions.SelectIndex().ForEach(x =>
                {
                    string baseStr = x.index > 0 ? CONSTANTS.OUTPUT_OR_OPERATOR : string.Empty;
                    ret += baseStr + x.value.verbose;
                });
                return ret;
            }
        }

        #region constructor functions
        private void createConjunctions(List<string> vars)
        {
            Enumerable.Range(0, vars.Count).ToList().ForEach(stVarIndex =>
            {
                BooleConjunction baseConjunction = new BooleConjunction();
                Enumerable.Range(stVarIndex, vars.Count - stVarIndex).ToList().ForEach(ndVarIndex =>
                {
                    baseConjunction.elements.Add(new(vars[ndVarIndex]));
                    if (!_booleConjunctions.Exists(el => el.equals(baseConjunction)))
                    {
                        var posibilities = Util.getAllPosibilities(new(baseConjunction.getDifferentVars()));
                        _booleConjunctions.AddRange(posibilities);
                    }

                    if (ndVarIndex < vars.Count - 1)
                    {
                        Enumerable.Range(ndVarIndex + 1, vars.Count - 1 - ndVarIndex).ToList().ForEach(rdVarIndex =>
                        {
                            var posibilities = Util.getAllPosibilities(new(baseConjunction.getDifferentVars()) { vars[rdVarIndex] });
                            _booleConjunctions.AddRange(posibilities);
                        });
                    }
                });
            });
        }

        private void fillValues(List<BooleConjunction> falseValues)
        {
            _booleConjunctions.ForEach(l => l.value = true);
            _booleConjunctions.ForEach(line =>
            {
                bool val = falseValues.Any(el => el.minMatch(line));
                line.value = !val;
            });
        }

        private List<ConjunctionOcurrences> fillOcurrences(BooleTable t)
        {
            List<ConjunctionOcurrences> ret = new(_booleConjunctions.Where(bj => bj.value).Select(x => new ConjunctionOcurrences(x)));
            t.lines
                .Where(l => l.value)
                .SelectIndex()
                .ForEach(funcElement =>
                    ret.SelectIndex()
                        .Where(x => funcElement.value.minMatch(x.value.conf))
                        .ToList().ForEach(x =>
                        ret[x.index].add(funcElement.index)
                    )
            );

            return ret;
        }

        private List<int> getRemovedIndex(List<ConjunctionOcurrences> cOcurrences)
        {
            List<int> removedIndexes = new();

            cOcurrences.SelectIndex().OrderBy(x => x.value.numberOcurrences).ToList().ForEach(x =>
            {
                bool val = x.value.indexes.All(ind =>
                    cOcurrences.SelectIndex().Any(y =>
                        !removedIndexes.Contains(y.index) &&
                        y.index != x.index &&
                        y.value.numberOcurrences >= x.value.numberOcurrences &&
                        y.value.indexes.Contains(ind)
                    )
                );
                if (val) removedIndexes.Add(x.index);
            });
            return removedIndexes;
        }

        private void removeInvalid(BooleTable table)
        {
            List<ConjunctionOcurrences> conjunctionOcurrences = fillOcurrences(table);
            List<int> removedIndexes = getRemovedIndex(conjunctionOcurrences);


            _booleConjunctions = new(conjunctionOcurrences
                    .SelectIndex()
                    .Where(x => !removedIndexes.Contains(x.index))
                    .Select(x => x.value.conf).ToList());

        }
        #endregion
    }
}
