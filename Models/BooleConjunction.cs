namespace BooleanSimplifier.Models
{
    internal class BooleConjunction
    {
        public List<BooleanConjunctionElement> conjunction = new List<BooleanConjunctionElement>();
        public bool value { get; set; } = false;
    }
}
