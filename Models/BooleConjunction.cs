using BooleanSimplifier.Constants;
using BooleanSimplifier.Src;

namespace BooleanSimplifier.Models
{
    internal class BooleConjunction
    {
        public List<BooleanConjunctionElement> elements = new List<BooleanConjunctionElement>();
        public bool value { get; set; } = false;

        public BooleConjunction() { }
        public BooleConjunction(List<BooleanConjunctionElement> els, bool val = false) {
            elements = els;
            value = false;
        }

        public string verbose
        {
            get
            {
                string ret = string.Empty;
                elements.SelectIndex().ForEach(element =>
                {
                    string starting = element.index > 0 ? CONSTANTS.AND_OPERATOR.ToString() : string.Empty;
                    ret += starting + element.value.conjunctionName;
                });
                return ret;
            }
        }

        public List<string> getDifferentVars ()
        {
            List<string> ret = new List<string>();
            elements.ForEach(el => ret.Add(el.name));
            return ret.Distinct().OrderBy(x => x).ToList();
        }

        public bool equals(BooleConjunction rhs)
        {
            if (elements.Count != rhs.elements.Count) return false;
            return elements.All(el => rhs.elements.Any(el2 => el2.name == el.name && el2.val == el.val));
        }

        public bool minMatch(BooleConjunction conj)
        {
            return conj.elements.All(el =>
            {
                return elements.Any(el2 => el2.name == el.name && el2.val == el.val);
            });
        }
    }
}
