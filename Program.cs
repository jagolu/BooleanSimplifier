// See https://aka.ms/new-console-template for more information
using BooleanSimplifier.Constants;
using BooleanSimplifier.Models;
using BooleanSimplifier.Src.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Escribe la funcion: ");
            string val = Console.ReadLine();
            if (String.IsNullOrEmpty(val))
            {
                Console.WriteLine("Funcion no valida");
                continue;
            }
            val = val.Trim()
                .Replace(" ", "")
                .Replace(CONSTANTS.OUTPUT_OR_OPERATOR, CONSTANTS.OR_OPERATOR.ToString())
                .Replace(CONSTANTS.OUTPUT_AND_OPERATOR, CONSTANTS.AND_OPERATOR.ToString());


            if (String.IsNullOrEmpty(val))
            {
                Console.WriteLine("Funcion no valida");
                continue;
            }

            BooleTree boolTree = new(val, Operator.OR);
            BooleTable boolTable = new(boolTree);
            BooleKarnaugh boolKarnaugh = new(boolTree.getDistinctVars(), boolTable);

            Console.WriteLine("<------------------------->");
            boolTable.show();
            Console.WriteLine("<------------------------->");
            Console.WriteLine(boolTree.verboseFunctionAsSummatory);
            Console.WriteLine("<------------------------->");
            Console.WriteLine(boolKarnaugh.verbose);
            Console.WriteLine("<------------------------->");
        }
    }
}