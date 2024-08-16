namespace BooleanSimplifier.Models
{
    internal class BooleanConjunctionElement
    {
        public string name { get; } = string.Empty;
        public Boolean val { get; } = true;
        public BooleanConjunctionElement(string myVar)
        {
            var negOp = Constants.Constants.NEGATION_OPERATOR;
            if (myVar == null) throw new ArgumentNullException(nameof(myVar));
            if (myVar.Length == 0) throw new ArgumentException("Length 0 at " + myVar);
            if (myVar.Contains(negOp) && myVar.IndexOf(negOp) != 0)
                throw new ArgumentException($"${negOp} in incorrect position");

            val = !myVar.Contains(negOp);
            name = !myVar.Contains(negOp) ? myVar.Remove(0, 1) : myVar;
        }

        public string conjunctionName { 
            get {
                string negativeOperator = val ? string.Empty : Constants.Constants.NEGATION_CONJUNTION_OPERATOR.ToString();
                return $"${negativeOperator}${name}"; 
            } 
        }
    }
}
