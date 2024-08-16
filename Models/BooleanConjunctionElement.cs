using BooleanSimplifier.Constants;

namespace BooleanSimplifier.Models
{
    internal class BooleanConjunctionElement
    {
        public string name { get; } = string.Empty;
        public Boolean val { get; } = true;
        public BooleanConjunctionElement(string myVar)
        {
            var negOp = CONSTANTS.NEGATION_OPERATOR;
            if (myVar == null) throw new ArgumentNullException(nameof(myVar));
            if (myVar.Length == 0) throw new ArgumentException("Length 0 at " + myVar);
            if (myVar.Contains(negOp) && myVar.IndexOf(negOp) != 0)
                throw new ArgumentException($"${negOp} in incorrect position");

            this.val = !myVar.Contains(negOp);
            name = myVar.Contains(negOp) ? myVar.Remove(0, 1) : myVar;
        }

        public string conjunctionName { 
            get {
                string negativeOperator = val ? string.Empty : CONSTANTS.NEGATION_CONJUNTION_OPERATOR;
                return $"{negativeOperator}{name}"; 
            } 
        }
    }
}
