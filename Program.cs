// See https://aka.ms/new-console-template for more information
using BooleanSimplifier.Constants;
using BooleanSimplifier.Models;
using BooleanSimplifier.Src;

string baseStr = "(a*b*(c+!d)+e)*f+!c+a*!b";
//string baseStr = "!c+c*a*(!b+f)+c*f*e+c*!a*!b*f*!d*e";
BooleTree aaaa = new(baseStr, Operator.OR);
var b = aaaa.getSummarizedList();
var c = aaaa.getDifferentVars();
BooleTable table = new(aaaa.getDifferentVars());
table.fill(aaaa);
table.show();

