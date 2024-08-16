using BooleanSimplifier.Constants;

namespace BooleanSimplifier.Models
{
    internal class BooleConjunction
    {
        public List<BooleanConjunctionElement> elements = new List<BooleanConjunctionElement>();
        public bool value { get; set; } = false;

        public string verbose
        {
            get
            {
                string ret = string.Empty;
                elements.Select((value, index) => new { value, index }).ToList()
                        .ForEach(element =>
                        {
                            string starting = element.index > 0 ? CONSTANTS.AND_OPERATOR.ToString() : string.Empty;
                            ret += starting + element.value.conjunctionName;
                        });
                return ret;
            }
        }
    }
}
