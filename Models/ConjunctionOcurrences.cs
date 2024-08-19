namespace BooleanSimplifier.Models
{
    internal class ConjunctionOcurrences
    {
        public BooleConjunction conf { get; set; }
        public List<int> indexes { get; }
        public int numberOcurrences
        {
            get
            {
                return indexes.Count;
            }
        }

        public ConjunctionOcurrences(BooleConjunction conf)
        {
            this.indexes = new();
            this.conf = conf;
        }

        public void add(int index)
        {
            indexes.Add(index);
        }
    }
}
