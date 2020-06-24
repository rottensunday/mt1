// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  DESKTOP-CA52FU6
// DateTime: 24.06.2020 20:44:39
// UserName: rotten
// Input file <gen_predef.y - 24.06.2020 20:44:38>

// options: conflicts no-lines diagnose & report gplex conflicts

using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;
using QUT.Gppg;

namespace GardensPoint
{
public enum Tokens {error=2,EOF=3,Print=4,Exit=5,Assign=6,
    Plus=7,Minus=8,Multiplies=9,Divides=10,OpenPar=11,ClosePar=12,
    Endl=13,Eof=14,Error=15,OpenBracket=16,CloseBracket=17,Program=18,
    Ident=19,Type=20,RealNumber=21,IntNumber=22,Semicolon=23,Write=24,
    String=25,Bool=26,If=27,While=28,LogicalOR=29,LogicalAND=30,
    LogicalNEG=31};

// Abstract base class for GPLEX scanners
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public abstract class ScanBase : AbstractScanner<Node,LexLocation> {
  private LexLocation __yylloc = new LexLocation();
  public override LexLocation yylloc { get { return __yylloc; } set { __yylloc = value; } }
  protected virtual bool yywrap() { return true; }
}

// Utility class for encapsulating token information
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public class ScanObj {
  public int token;
  public Node yylval;
  public LexLocation yylloc;
  public ScanObj( int t, Node val, LexLocation loc ) {
    this.token = t; this.yylval = val; this.yylloc = loc;
  }
}

[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public partial class Parser: ShiftReduceParser<Node, LexLocation>
{
#pragma warning disable 649
  private static Dictionary<int, string> aliases;
#pragma warning restore 649
  private static Rule[] rules = new Rule[27];
  private static State[] states = new State[54];
  private static string[] nonTerms = new string[] {
      "program", "$accept", "bracketstmt", "stmt", "expr", "Anon@1", "Anon@2", 
      "Anon@3", "Anon@4", "term", "factor", };

  static Parser() {
    states[0] = new State(new int[]{18,3},new int[]{-1,1});
    states[1] = new State(new int[]{3,2});
    states[2] = new State(-1);
    states[3] = new State(new int[]{16,4});
    states[4] = new State(-3,new int[]{-3,5});
    states[5] = new State(new int[]{17,6,19,8,20,29,27,32,28,38,24,44,16,49},new int[]{-4,7});
    states[6] = new State(-2);
    states[7] = new State(-4);
    states[8] = new State(new int[]{6,9});
    states[9] = new State(new int[]{11,16,22,23,21,24,26,25,19,26},new int[]{-5,10,-10,28,-11,27});
    states[10] = new State(new int[]{23,11,7,12,8,19});
    states[11] = new State(-5);
    states[12] = new State(new int[]{11,16,22,23,21,24,26,25,19,26},new int[]{-10,13,-11,27});
    states[13] = new State(new int[]{9,14,10,21,23,-16,7,-16,8,-16,12,-16});
    states[14] = new State(new int[]{11,16,22,23,21,24,26,25,19,26},new int[]{-11,15});
    states[15] = new State(-19);
    states[16] = new State(new int[]{11,16,22,23,21,24,26,25,19,26},new int[]{-5,17,-10,28,-11,27});
    states[17] = new State(new int[]{12,18,7,12,8,19});
    states[18] = new State(-22);
    states[19] = new State(new int[]{11,16,22,23,21,24,26,25,19,26},new int[]{-10,20,-11,27});
    states[20] = new State(new int[]{9,14,10,21,23,-17,7,-17,8,-17,12,-17});
    states[21] = new State(new int[]{11,16,22,23,21,24,26,25,19,26},new int[]{-11,22});
    states[22] = new State(-20);
    states[23] = new State(-23);
    states[24] = new State(-24);
    states[25] = new State(-25);
    states[26] = new State(-26);
    states[27] = new State(-21);
    states[28] = new State(new int[]{9,14,10,21,23,-18,7,-18,8,-18,12,-18});
    states[29] = new State(new int[]{19,30});
    states[30] = new State(new int[]{23,31});
    states[31] = new State(-6);
    states[32] = new State(new int[]{11,33});
    states[33] = new State(new int[]{11,16,22,23,21,24,26,25,19,26},new int[]{-5,34,-10,28,-11,27});
    states[34] = new State(new int[]{12,35,7,12,8,19});
    states[35] = new State(-7,new int[]{-6,36});
    states[36] = new State(new int[]{19,8,20,29,27,32,28,38,24,44,16,49},new int[]{-4,37});
    states[37] = new State(-8);
    states[38] = new State(new int[]{11,39});
    states[39] = new State(new int[]{11,16,22,23,21,24,26,25,19,26},new int[]{-5,40,-10,28,-11,27});
    states[40] = new State(new int[]{12,41,7,12,8,19});
    states[41] = new State(-9,new int[]{-7,42});
    states[42] = new State(new int[]{19,8,20,29,27,32,28,38,24,44,16,49},new int[]{-4,43});
    states[43] = new State(-10);
    states[44] = new State(new int[]{25,45,11,16,22,23,21,24,26,25,19,26},new int[]{-5,47,-10,28,-11,27});
    states[45] = new State(new int[]{23,46});
    states[46] = new State(-11);
    states[47] = new State(new int[]{23,48,7,12,8,19});
    states[48] = new State(-12);
    states[49] = new State(-13,new int[]{-8,50});
    states[50] = new State(-3,new int[]{-3,51});
    states[51] = new State(new int[]{19,8,20,29,27,32,28,38,24,44,16,49,17,-14},new int[]{-9,52,-4,7});
    states[52] = new State(new int[]{17,53});
    states[53] = new State(-15);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-2, new int[]{-1,3});
    rules[2] = new Rule(-1, new int[]{18,16,-3,17});
    rules[3] = new Rule(-3, new int[]{});
    rules[4] = new Rule(-3, new int[]{-3,-4});
    rules[5] = new Rule(-4, new int[]{19,6,-5,23});
    rules[6] = new Rule(-4, new int[]{20,19,23});
    rules[7] = new Rule(-6, new int[]{});
    rules[8] = new Rule(-4, new int[]{27,11,-5,12,-6,-4});
    rules[9] = new Rule(-7, new int[]{});
    rules[10] = new Rule(-4, new int[]{28,11,-5,12,-7,-4});
    rules[11] = new Rule(-4, new int[]{24,25,23});
    rules[12] = new Rule(-4, new int[]{24,-5,23});
    rules[13] = new Rule(-8, new int[]{});
    rules[14] = new Rule(-9, new int[]{});
    rules[15] = new Rule(-4, new int[]{16,-8,-3,-9,17});
    rules[16] = new Rule(-5, new int[]{-5,7,-10});
    rules[17] = new Rule(-5, new int[]{-5,8,-10});
    rules[18] = new Rule(-5, new int[]{-10});
    rules[19] = new Rule(-10, new int[]{-10,9,-11});
    rules[20] = new Rule(-10, new int[]{-10,10,-11});
    rules[21] = new Rule(-10, new int[]{-11});
    rules[22] = new Rule(-11, new int[]{11,-5,12});
    rules[23] = new Rule(-11, new int[]{22});
    rules[24] = new Rule(-11, new int[]{21});
    rules[25] = new Rule(-11, new int[]{26});
    rules[26] = new Rule(-11, new int[]{19});
  }

  protected override void Initialize() {
    this.InitSpecialTokens((int)Tokens.error, (int)Tokens.EOF);
    this.InitStates(states);
    this.InitRules(rules);
    this.InitNonTerminals(nonTerms);
  }

  protected override void DoAction(int action)
  {
#pragma warning disable 162, 1522
    switch (action)
    {
      case 5: // stmt -> Ident, Assign, expr, Semicolon
{
				MakeAssignNode(ValueStack[ValueStack.Depth-4], ValueStack[ValueStack.Depth-2]);
			}
        break;
      case 6: // stmt -> Type, Ident, Semicolon
{
				Declare(ValueStack[ValueStack.Depth-2], ValueStack[ValueStack.Depth-3]);
			}
        break;
      case 7: // Anon@1 -> /* empty */
{
				MakeIfStmtNode(ValueStack[ValueStack.Depth-2]);
			}
        break;
      case 8: // stmt -> If, OpenPar, expr, ClosePar, Anon@1, stmt
{
				PopBracketStatement();
			}
        break;
      case 9: // Anon@2 -> /* empty */
{
				MakeWhileStmtNode(ValueStack[ValueStack.Depth-2]);
			}
        break;
      case 10: // stmt -> While, OpenPar, expr, ClosePar, Anon@2, stmt
{
				PopBracketStatement();
			}
        break;
      case 11: // stmt -> Write, String, Semicolon
{
				MakeWriteStringNode(ValueStack[ValueStack.Depth-2]);
			}
        break;
      case 12: // stmt -> Write, expr, Semicolon
{
				MakeWriteExpressionNode(ValueStack[ValueStack.Depth-2]);
			}
        break;
      case 13: // Anon@3 -> /* empty */
{
				PushBracketStatement();
			}
        break;
      case 14: // Anon@4 -> /* empty */
{
			
			}
        break;
      case 15: // stmt -> OpenBracket, Anon@3, bracketstmt, Anon@4, CloseBracket
{
				PopBracketStatement();
			}
        break;
      case 16: // expr -> expr, Plus, term
{
				CurrentSemanticValue = new BinaryOperationNode(ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1], Tokens.Plus);
			}
        break;
      case 17: // expr -> expr, Minus, term
{
				CurrentSemanticValue = new BinaryOperationNode(ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1], Tokens.Minus);
			}
        break;
      case 18: // expr -> term
{
				CurrentSemanticValue = ValueStack[ValueStack.Depth-1];
			}
        break;
      case 19: // term -> term, Multiplies, factor
{
				CurrentSemanticValue = new BinaryOperationNode(ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1], Tokens.Multiplies);
			}
        break;
      case 20: // term -> term, Divides, factor
{
				CurrentSemanticValue = new BinaryOperationNode(ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1], Tokens.Divides);
			}
        break;
      case 21: // term -> factor
{
				CurrentSemanticValue = ValueStack[ValueStack.Depth-1];
			}
        break;
      case 22: // factor -> OpenPar, expr, ClosePar
{
				CurrentSemanticValue = ValueStack[ValueStack.Depth-2];
			}
        break;
      case 23: // factor -> IntNumber
{
				CurrentSemanticValue = ValueStack[ValueStack.Depth-1];
			}
        break;
      case 24: // factor -> RealNumber
{
				CurrentSemanticValue = ValueStack[ValueStack.Depth-1];
			}
        break;
      case 25: // factor -> Bool
{
				CurrentSemanticValue = ValueStack[ValueStack.Depth-1];
			}
        break;
      case 26: // factor -> Ident
{
				CurrentSemanticValue = ValueStack[ValueStack.Depth-1];
			}
        break;
    }
#pragma warning restore 162, 1522
  }

  protected override string TerminalToString(int terminal)
  {
    if (aliases != null && aliases.ContainsKey(terminal))
        return aliases[terminal];
    else if (((Tokens)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
        return ((Tokens)terminal).ToString();
    else
        return CharToString((char)terminal);
  }


int lineno=1;

public Parser(Scanner scanner) : base(scanner) { }
}
}
