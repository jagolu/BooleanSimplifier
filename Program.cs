// See https://aka.ms/new-console-template for more information
using BooleanSimplifier.Constants;
using BooleanSimplifier.Models;
using BooleanSimplifier.Src.Models;

//List<string> strings = new List<string>() { "a", "b", "c", "d", "e", "f"};
//KarnaughTable t = new(strings);

//string baseStr = "(a*b*(c+!d)+e)*f+!c+a*!b";
string baseStr = "!c+c*a*(!b+f)+c*f*e+c*!a*!b*f*!d*e";
//string baseStr = "!c+ca(!b+f)+cfe+c!a!bf!de";
//string baseStr = "!a*!b*c+b*c+a*b*!c";
BooleTree bTree = new(baseStr, Operator.OR);
BooleTable table = new(bTree);
table.fill(bTree);
BooleKarnaugh tKarnaugh = new(bTree.getDistinctVars(), table);
table.show();
Console.WriteLine(bTree.verboseFunctionAsSummatory);
Console.WriteLine("<------------------------->");
Console.WriteLine(tKarnaugh.verbose);

