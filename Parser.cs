// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  DESKTOP-CA52FU6
// DateTime: 28.06.2020 02:33:29
// UserName: rotten
// Input file <gen_predef.y - 27.06.2020 21:38:28>

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
    Endl=13,Error=14,OpenBracket=15,CloseBracket=16,Program=17,Ident=18,
    Type=19,RealNumber=20,IntNumber=21,Semicolon=22,String=23,Bool=24,
    RelationEQUALS=25,RelationNOTEQUALS=26,RelationGREATER=27,RelationGREATEREQUALS=28,RelationLESS=29,RelationLESSEQUALS=30,
    BitOR=31,BitAND=32,BitNEG=33,LogicalOR=34,LogicalAND=35,LogicalNEG=36,
    If=37,Else=38,While=39,Eof=40,Eol=41,Read=42,
    Write=43};

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
  private static Rule[] rules = new Rule[70];
  private static State[] states = new State[118];
  private static string[] nonTerms = new string[] {
      "start", "$accept", "program", "bracketstmt", "Anon@1", "Anon@2", "stmt", 
      "expr_logical", "Anon@3", "Anon@4", "ifcont", "Anon@5", "Anon@6", "Anon@7", 
      "Anon@8", "expr_relation", "expr_additive", "expr_multiplicative", "expr_bit", 
      "expr_unary", "factor", };

  static Parser() {
    states[0] = new State(new int[]{17,4,15,112,2,116},new int[]{-1,1,-3,3});
    states[1] = new State(new int[]{3,2});
    states[2] = new State(-1);
    states[3] = new State(-2);
    states[4] = new State(new int[]{15,5,2,111});
    states[5] = new State(new int[]{2,110,18,-11,19,-11,37,-11,39,-11,43,-11,42,-11,15,-11,8,-11,33,-11,36,-11,11,-11,21,-11,20,-11,24,-11,16,-11},new int[]{-4,6});
    states[6] = new State(new int[]{18,63,19,67,37,70,39,82,43,88,42,95,15,99,2,104,8,23,33,25,36,27,11,29,21,34,20,35,24,36,16,-3},new int[]{-5,7,-7,9,-8,10,-16,62,-17,61,-18,52,-19,51,-20,50,-21,33});
    states[7] = new State(new int[]{16,8});
    states[8] = new State(-4);
    states[9] = new State(-12);
    states[10] = new State(new int[]{22,11,2,12,34,13,35,40});
    states[11] = new State(-13);
    states[12] = new State(-14);
    states[13] = new State(new int[]{8,23,33,25,36,27,11,29,21,34,20,35,24,36,18,37},new int[]{-16,14,-17,61,-18,52,-19,51,-20,50,-21,33});
    states[14] = new State(new int[]{25,15,26,42,27,53,28,55,29,57,30,59,22,-41,2,-41,34,-41,35,-41,12,-41});
    states[15] = new State(new int[]{8,23,33,25,36,27,11,29,21,34,20,35,24,36,18,37},new int[]{-17,16,-18,52,-19,51,-20,50,-21,33});
    states[16] = new State(new int[]{7,17,8,44,25,-44,26,-44,27,-44,28,-44,29,-44,30,-44,22,-44,2,-44,34,-44,35,-44,12,-44});
    states[17] = new State(new int[]{8,23,33,25,36,27,11,29,21,34,20,35,24,36,18,37},new int[]{-18,18,-19,51,-20,50,-21,33});
    states[18] = new State(new int[]{9,19,10,46,7,-51,8,-51,25,-51,26,-51,27,-51,28,-51,29,-51,30,-51,22,-51,2,-51,34,-51,35,-51,12,-51});
    states[19] = new State(new int[]{8,23,33,25,36,27,11,29,21,34,20,35,24,36,18,37},new int[]{-19,20,-20,50,-21,33});
    states[20] = new State(new int[]{31,21,32,48,9,-54,10,-54,7,-54,8,-54,25,-54,26,-54,27,-54,28,-54,29,-54,30,-54,22,-54,2,-54,34,-54,35,-54,12,-54});
    states[21] = new State(new int[]{8,23,33,25,36,27,11,29,21,34,20,35,24,36,18,37},new int[]{-20,22,-21,33});
    states[22] = new State(-57);
    states[23] = new State(new int[]{8,23,33,25,36,27,11,29,21,34,20,35,24,36,18,37},new int[]{-20,24,-21,33});
    states[24] = new State(-60);
    states[25] = new State(new int[]{8,23,33,25,36,27,11,29,21,34,20,35,24,36,18,37},new int[]{-20,26,-21,33});
    states[26] = new State(-61);
    states[27] = new State(new int[]{8,23,33,25,36,27,11,29,21,34,20,35,24,36,18,37},new int[]{-20,28,-21,33});
    states[28] = new State(-62);
    states[29] = new State(new int[]{19,30,8,23,33,25,36,27,11,29,21,34,20,35,24,36,18,37},new int[]{-8,38,-16,62,-17,61,-18,52,-19,51,-20,50,-21,33});
    states[30] = new State(new int[]{12,31});
    states[31] = new State(new int[]{8,23,33,25,36,27,11,29,21,34,20,35,24,36,18,37},new int[]{-20,32,-21,33});
    states[32] = new State(-63);
    states[33] = new State(-64);
    states[34] = new State(-66);
    states[35] = new State(-67);
    states[36] = new State(-68);
    states[37] = new State(-69);
    states[38] = new State(new int[]{12,39,34,13,35,40});
    states[39] = new State(-65);
    states[40] = new State(new int[]{8,23,33,25,36,27,11,29,21,34,20,35,24,36,18,37},new int[]{-16,41,-17,61,-18,52,-19,51,-20,50,-21,33});
    states[41] = new State(new int[]{25,15,26,42,27,53,28,55,29,57,30,59,22,-42,2,-42,34,-42,35,-42,12,-42});
    states[42] = new State(new int[]{8,23,33,25,36,27,11,29,21,34,20,35,24,36,18,37},new int[]{-17,43,-18,52,-19,51,-20,50,-21,33});
    states[43] = new State(new int[]{7,17,8,44,25,-45,26,-45,27,-45,28,-45,29,-45,30,-45,22,-45,2,-45,34,-45,35,-45,12,-45});
    states[44] = new State(new int[]{8,23,33,25,36,27,11,29,21,34,20,35,24,36,18,37},new int[]{-18,45,-19,51,-20,50,-21,33});
    states[45] = new State(new int[]{9,19,10,46,7,-52,8,-52,25,-52,26,-52,27,-52,28,-52,29,-52,30,-52,22,-52,2,-52,34,-52,35,-52,12,-52});
    states[46] = new State(new int[]{8,23,33,25,36,27,11,29,21,34,20,35,24,36,18,37},new int[]{-19,47,-20,50,-21,33});
    states[47] = new State(new int[]{31,21,32,48,9,-55,10,-55,7,-55,8,-55,25,-55,26,-55,27,-55,28,-55,29,-55,30,-55,22,-55,2,-55,34,-55,35,-55,12,-55});
    states[48] = new State(new int[]{8,23,33,25,36,27,11,29,21,34,20,35,24,36,18,37},new int[]{-20,49,-21,33});
    states[49] = new State(-58);
    states[50] = new State(-59);
    states[51] = new State(new int[]{31,21,32,48,9,-56,10,-56,7,-56,8,-56,25,-56,26,-56,27,-56,28,-56,29,-56,30,-56,22,-56,2,-56,34,-56,35,-56,12,-56});
    states[52] = new State(new int[]{9,19,10,46,7,-53,8,-53,25,-53,26,-53,27,-53,28,-53,29,-53,30,-53,22,-53,2,-53,34,-53,35,-53,12,-53});
    states[53] = new State(new int[]{8,23,33,25,36,27,11,29,21,34,20,35,24,36,18,37},new int[]{-17,54,-18,52,-19,51,-20,50,-21,33});
    states[54] = new State(new int[]{7,17,8,44,25,-46,26,-46,27,-46,28,-46,29,-46,30,-46,22,-46,2,-46,34,-46,35,-46,12,-46});
    states[55] = new State(new int[]{8,23,33,25,36,27,11,29,21,34,20,35,24,36,18,37},new int[]{-17,56,-18,52,-19,51,-20,50,-21,33});
    states[56] = new State(new int[]{7,17,8,44,25,-47,26,-47,27,-47,28,-47,29,-47,30,-47,22,-47,2,-47,34,-47,35,-47,12,-47});
    states[57] = new State(new int[]{8,23,33,25,36,27,11,29,21,34,20,35,24,36,18,37},new int[]{-17,58,-18,52,-19,51,-20,50,-21,33});
    states[58] = new State(new int[]{7,17,8,44,25,-48,26,-48,27,-48,28,-48,29,-48,30,-48,22,-48,2,-48,34,-48,35,-48,12,-48});
    states[59] = new State(new int[]{8,23,33,25,36,27,11,29,21,34,20,35,24,36,18,37},new int[]{-17,60,-18,52,-19,51,-20,50,-21,33});
    states[60] = new State(new int[]{7,17,8,44,25,-49,26,-49,27,-49,28,-49,29,-49,30,-49,22,-49,2,-49,34,-49,35,-49,12,-49});
    states[61] = new State(new int[]{7,17,8,44,25,-50,26,-50,27,-50,28,-50,29,-50,30,-50,22,-50,2,-50,34,-50,35,-50,12,-50});
    states[62] = new State(new int[]{25,15,26,42,27,53,28,55,29,57,30,59,22,-43,2,-43,34,-43,35,-43,12,-43});
    states[63] = new State(new int[]{6,64,31,-69,32,-69,9,-69,10,-69,7,-69,8,-69,25,-69,26,-69,27,-69,28,-69,29,-69,30,-69,22,-69,2,-69,34,-69,35,-69});
    states[64] = new State(new int[]{8,23,33,25,36,27,11,29,21,34,20,35,24,36,18,37},new int[]{-8,65,-16,62,-17,61,-18,52,-19,51,-20,50,-21,33});
    states[65] = new State(new int[]{22,66,34,13,35,40});
    states[66] = new State(-15);
    states[67] = new State(new int[]{18,68});
    states[68] = new State(new int[]{22,69,18,-17,19,-17,37,-17,39,-17,43,-17,42,-17,15,-17,2,-17,8,-17,33,-17,36,-17,11,-17,21,-17,20,-17,24,-17,16,-17,38,-17});
    states[69] = new State(-16);
    states[70] = new State(new int[]{11,71,2,109});
    states[71] = new State(new int[]{2,108,8,23,33,25,36,27,11,29,21,34,20,35,24,36,18,37},new int[]{-8,72,-16,62,-17,61,-18,52,-19,51,-20,50,-21,33});
    states[72] = new State(new int[]{12,73,34,13,35,40});
    states[73] = new State(-18,new int[]{-9,74});
    states[74] = new State(new int[]{18,81,19,67,37,70,39,82,43,88,42,95,15,99,2,104},new int[]{-7,75});
    states[75] = new State(-19,new int[]{-10,76});
    states[76] = new State(new int[]{38,78,18,-38,19,-38,37,-38,39,-38,43,-38,42,-38,15,-38,2,-38,8,-38,33,-38,36,-38,11,-38,21,-38,20,-38,24,-38,16,-38},new int[]{-11,77});
    states[77] = new State(-20);
    states[78] = new State(-39,new int[]{-15,79});
    states[79] = new State(new int[]{18,81,19,67,37,70,39,82,43,88,42,95,15,99,2,104},new int[]{-7,80});
    states[80] = new State(-40);
    states[81] = new State(new int[]{6,64});
    states[82] = new State(new int[]{11,83,2,107});
    states[83] = new State(new int[]{2,106,8,23,33,25,36,27,11,29,21,34,20,35,24,36,18,37},new int[]{-8,84,-16,62,-17,61,-18,52,-19,51,-20,50,-21,33});
    states[84] = new State(new int[]{12,85,34,13,35,40});
    states[85] = new State(-23,new int[]{-12,86});
    states[86] = new State(new int[]{18,81,19,67,37,70,39,82,43,88,42,95,15,99,2,104},new int[]{-7,87});
    states[87] = new State(-24);
    states[88] = new State(new int[]{23,89,8,23,33,25,36,27,11,29,21,34,20,35,24,36,18,37},new int[]{-8,92,-16,62,-17,61,-18,52,-19,51,-20,50,-21,33});
    states[89] = new State(new int[]{22,90,2,91});
    states[90] = new State(-27);
    states[91] = new State(-28);
    states[92] = new State(new int[]{22,93,2,94,34,13,35,40});
    states[93] = new State(-29);
    states[94] = new State(-30);
    states[95] = new State(new int[]{18,96});
    states[96] = new State(new int[]{22,97,2,98});
    states[97] = new State(-31);
    states[98] = new State(-32);
    states[99] = new State(-33,new int[]{-13,100});
    states[100] = new State(-11,new int[]{-4,101});
    states[101] = new State(new int[]{18,63,19,67,37,70,39,82,43,88,42,95,15,99,2,104,8,23,33,25,36,27,11,29,21,34,20,35,24,36,16,-34},new int[]{-14,102,-7,9,-8,10,-16,62,-17,61,-18,52,-19,51,-20,50,-21,33});
    states[102] = new State(new int[]{16,103});
    states[103] = new State(-35);
    states[104] = new State(new int[]{40,105,18,-36,19,-36,37,-36,39,-36,43,-36,42,-36,15,-36,2,-36,8,-36,33,-36,36,-36,11,-36,21,-36,20,-36,24,-36,16,-36,38,-36});
    states[105] = new State(-37);
    states[106] = new State(-25);
    states[107] = new State(-26);
    states[108] = new State(-21);
    states[109] = new State(-22);
    states[110] = new State(-5);
    states[111] = new State(-6);
    states[112] = new State(-7,new int[]{-6,113});
    states[113] = new State(-11,new int[]{-4,114});
    states[114] = new State(new int[]{16,115,18,63,19,67,37,70,39,82,43,88,42,95,15,99,2,104,8,23,33,25,36,27,11,29,21,34,20,35,24,36},new int[]{-7,9,-8,10,-16,62,-17,61,-18,52,-19,51,-20,50,-21,33});
    states[115] = new State(-8);
    states[116] = new State(new int[]{40,117,3,-9});
    states[117] = new State(-10);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-2, new int[]{-1,3});
    rules[2] = new Rule(-1, new int[]{-3});
    rules[3] = new Rule(-5, new int[]{});
    rules[4] = new Rule(-3, new int[]{17,15,-4,-5,16});
    rules[5] = new Rule(-3, new int[]{17,15,2});
    rules[6] = new Rule(-3, new int[]{17,2});
    rules[7] = new Rule(-6, new int[]{});
    rules[8] = new Rule(-3, new int[]{15,-6,-4,16});
    rules[9] = new Rule(-3, new int[]{2});
    rules[10] = new Rule(-3, new int[]{2,40});
    rules[11] = new Rule(-4, new int[]{});
    rules[12] = new Rule(-4, new int[]{-4,-7});
    rules[13] = new Rule(-4, new int[]{-4,-8,22});
    rules[14] = new Rule(-4, new int[]{-4,-8,2});
    rules[15] = new Rule(-7, new int[]{18,6,-8,22});
    rules[16] = new Rule(-7, new int[]{19,18,22});
    rules[17] = new Rule(-7, new int[]{19,18});
    rules[18] = new Rule(-9, new int[]{});
    rules[19] = new Rule(-10, new int[]{});
    rules[20] = new Rule(-7, new int[]{37,11,-8,12,-9,-7,-10,-11});
    rules[21] = new Rule(-7, new int[]{37,11,2});
    rules[22] = new Rule(-7, new int[]{37,2});
    rules[23] = new Rule(-12, new int[]{});
    rules[24] = new Rule(-7, new int[]{39,11,-8,12,-12,-7});
    rules[25] = new Rule(-7, new int[]{39,11,2});
    rules[26] = new Rule(-7, new int[]{39,2});
    rules[27] = new Rule(-7, new int[]{43,23,22});
    rules[28] = new Rule(-7, new int[]{43,23,2});
    rules[29] = new Rule(-7, new int[]{43,-8,22});
    rules[30] = new Rule(-7, new int[]{43,-8,2});
    rules[31] = new Rule(-7, new int[]{42,18,22});
    rules[32] = new Rule(-7, new int[]{42,18,2});
    rules[33] = new Rule(-13, new int[]{});
    rules[34] = new Rule(-14, new int[]{});
    rules[35] = new Rule(-7, new int[]{15,-13,-4,-14,16});
    rules[36] = new Rule(-7, new int[]{2});
    rules[37] = new Rule(-7, new int[]{2,40});
    rules[38] = new Rule(-11, new int[]{});
    rules[39] = new Rule(-15, new int[]{});
    rules[40] = new Rule(-11, new int[]{38,-15,-7});
    rules[41] = new Rule(-8, new int[]{-8,34,-16});
    rules[42] = new Rule(-8, new int[]{-8,35,-16});
    rules[43] = new Rule(-8, new int[]{-16});
    rules[44] = new Rule(-16, new int[]{-16,25,-17});
    rules[45] = new Rule(-16, new int[]{-16,26,-17});
    rules[46] = new Rule(-16, new int[]{-16,27,-17});
    rules[47] = new Rule(-16, new int[]{-16,28,-17});
    rules[48] = new Rule(-16, new int[]{-16,29,-17});
    rules[49] = new Rule(-16, new int[]{-16,30,-17});
    rules[50] = new Rule(-16, new int[]{-17});
    rules[51] = new Rule(-17, new int[]{-17,7,-18});
    rules[52] = new Rule(-17, new int[]{-17,8,-18});
    rules[53] = new Rule(-17, new int[]{-18});
    rules[54] = new Rule(-18, new int[]{-18,9,-19});
    rules[55] = new Rule(-18, new int[]{-18,10,-19});
    rules[56] = new Rule(-18, new int[]{-19});
    rules[57] = new Rule(-19, new int[]{-19,31,-20});
    rules[58] = new Rule(-19, new int[]{-19,32,-20});
    rules[59] = new Rule(-19, new int[]{-20});
    rules[60] = new Rule(-20, new int[]{8,-20});
    rules[61] = new Rule(-20, new int[]{33,-20});
    rules[62] = new Rule(-20, new int[]{36,-20});
    rules[63] = new Rule(-20, new int[]{11,19,12,-20});
    rules[64] = new Rule(-20, new int[]{-21});
    rules[65] = new Rule(-21, new int[]{11,-8,12});
    rules[66] = new Rule(-21, new int[]{21});
    rules[67] = new Rule(-21, new int[]{20});
    rules[68] = new Rule(-21, new int[]{24});
    rules[69] = new Rule(-21, new int[]{18});
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
      case 3: // Anon@1 -> /* empty */
{
			
			}
        break;
      case 4: // program -> Program, OpenBracket, bracketstmt, Anon@1, CloseBracket
{
				YYAccept();
			}
        break;
      case 5: // program -> Program, OpenBracket, error
{
				Compiler.AddError("Syntax error - probably missing closing bracket", Compiler.LineNumber);
				yyerrok();
			}
        break;
      case 6: // program -> Program, error
{
				Compiler.AddError("Syntax error - probably missing opening bracket", Compiler.LineNumber);
				yyerrok();
			}
        break;
      case 7: // Anon@2 -> /* empty */
{
				Compiler.AddError("Syntax error - probably missing Program keyword", Compiler.LineNumber);
				yyerrok();
			}
        break;
      case 9: // program -> error
{
				Compiler.AddError("Unknown syntax error or error escape", Compiler.LineNumber);
				YYAbort();
			}
        break;
      case 10: // program -> error, Eof
{
				Compiler.AddError("Unknown syntax error or error escape", Compiler.LineNumber);
				YYAbort();
			}
        break;
      case 13: // bracketstmt -> bracketstmt, expr_logical, Semicolon
{
				MakeExpressionNode(ValueStack[ValueStack.Depth-2]);
			}
        break;
      case 14: // bracketstmt -> bracketstmt, expr_logical, error
{
				Compiler.AddError("Syntax error - missing semicolon in expression", Compiler.LineNumber);
				yyerrok();
			}
        break;
      case 15: // stmt -> Ident, Assign, expr_logical, Semicolon
{
				MakeAssignNode(ValueStack[ValueStack.Depth-4], ValueStack[ValueStack.Depth-2]);
			}
        break;
      case 16: // stmt -> Type, Ident, Semicolon
{
				Declare(ValueStack[ValueStack.Depth-2], ValueStack[ValueStack.Depth-3]);
			}
        break;
      case 17: // stmt -> Type, Ident
{
				Compiler.AddError("Syntax error - probably missing semicolon", Compiler.LineNumber);
				yyerrok();
			}
        break;
      case 18: // Anon@3 -> /* empty */
{
				StartIf(ValueStack[ValueStack.Depth-2]);
			}
        break;
      case 19: // Anon@4 -> /* empty */
{
				EndIf();
			}
        break;
      case 21: // stmt -> If, OpenPar, error
{
				Compiler.AddError("Syntax error - probably missing closing parenthesis", Compiler.LineNumber);
				yyerrok();
			}
        break;
      case 22: // stmt -> If, error
{
				Compiler.AddError("Syntax error - probably missing opening parenthesis", Compiler.LineNumber);
				yyerrok();
			}
        break;
      case 23: // Anon@5 -> /* empty */
{
				MakeWhileStmtNode(ValueStack[ValueStack.Depth-2]);
			}
        break;
      case 24: // stmt -> While, OpenPar, expr_logical, ClosePar, Anon@5, stmt
{
				PopBracketStatement();
			}
        break;
      case 25: // stmt -> While, OpenPar, error
{
				Compiler.AddError("Syntax error - probably missing closing parenthesis", Compiler.LineNumber);
				yyerrok();
			}
        break;
      case 26: // stmt -> While, error
{
				Compiler.AddError("Syntax error - probably missing opening parenthesis", Compiler.LineNumber);
				yyerrok();
			}
        break;
      case 27: // stmt -> Write, String, Semicolon
{
				MakeWriteStringNode(ValueStack[ValueStack.Depth-2]);
			}
        break;
      case 28: // stmt -> Write, String, error
{
				Compiler.AddError("Syntax error - probably missing semicolon", Compiler.LineNumber);
				yyerrok();
			}
        break;
      case 29: // stmt -> Write, expr_logical, Semicolon
{
				MakeWriteExpressionNode(ValueStack[ValueStack.Depth-2]);
			}
        break;
      case 30: // stmt -> Write, expr_logical, error
{
				Compiler.AddError("Syntax error - probably missing semicolon", Compiler.LineNumber);
				yyerrok();
			}
        break;
      case 31: // stmt -> Read, Ident, Semicolon
{
				MakeReadNode(ValueStack[ValueStack.Depth-2]);
			}
        break;
      case 32: // stmt -> Read, Ident, error
{
				Compiler.AddError("Syntax error - probably missing semicolon", Compiler.LineNumber);
				yyerrok();
			}
        break;
      case 33: // Anon@6 -> /* empty */
{
				PushBracketStatement();
			}
        break;
      case 34: // Anon@7 -> /* empty */
{
			
			}
        break;
      case 35: // stmt -> OpenBracket, Anon@6, bracketstmt, Anon@7, CloseBracket
{
				PopBracketStatement();
			}
        break;
      case 36: // stmt -> error
{
				Compiler.AddError("Syntax error - unknown error", Compiler.LineNumber);
				yyerrok();
			}
        break;
      case 37: // stmt -> error, Eof
{
				Compiler.AddError("Syntax error - unexpected end of file", Compiler.LineNumber);
				YYAbort();
			}
        break;
      case 38: // ifcont -> /* empty */
{
				EndIfNoElse();
			}
        break;
      case 39: // Anon@8 -> /* empty */
{
				StartElse();
			}
        break;
      case 40: // ifcont -> Else, Anon@8, stmt
{
				EndElse();
			}
        break;
      case 41: // expr_logical -> expr_logical, LogicalOR, expr_relation
{
				CurrentSemanticValue = new BinaryLogicalOperationNode(ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1], Tokens.LogicalOR);
			}
        break;
      case 42: // expr_logical -> expr_logical, LogicalAND, expr_relation
{
				CurrentSemanticValue  = new BinaryLogicalOperationNode(ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1], Tokens.LogicalAND);
			}
        break;
      case 44: // expr_relation -> expr_relation, RelationEQUALS, expr_additive
{
				CurrentSemanticValue = new BinaryRelationOperationNode(ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1], Tokens.RelationEQUALS);
			}
        break;
      case 45: // expr_relation -> expr_relation, RelationNOTEQUALS, expr_additive
{
				CurrentSemanticValue = new BinaryRelationOperationNode(ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1], Tokens.RelationNOTEQUALS);
			}
        break;
      case 46: // expr_relation -> expr_relation, RelationGREATER, expr_additive
{
				CurrentSemanticValue = new BinaryRelationOperationNode(ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1], Tokens.RelationGREATER);
			}
        break;
      case 47: // expr_relation -> expr_relation, RelationGREATEREQUALS, expr_additive
{
				CurrentSemanticValue = new BinaryLogicalOperationNode(new BinaryRelationOperationNode(ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1], Tokens.RelationGREATER), 
													new BinaryRelationOperationNode(ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1], Tokens.RelationEQUALS), 
													Tokens.LogicalOR);
			}
        break;
      case 48: // expr_relation -> expr_relation, RelationLESS, expr_additive
{
				CurrentSemanticValue = new BinaryRelationOperationNode(ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1], Tokens.RelationLESS);
			}
        break;
      case 49: // expr_relation -> expr_relation, RelationLESSEQUALS, expr_additive
{
				CurrentSemanticValue = new BinaryLogicalOperationNode(new BinaryRelationOperationNode(ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1], Tokens.RelationLESS), 
													new BinaryRelationOperationNode(ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1], Tokens.RelationEQUALS), 
													Tokens.LogicalOR);
			}
        break;
      case 51: // expr_additive -> expr_additive, Plus, expr_multiplicative
{
				CurrentSemanticValue = new BinaryOperationNode(ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1], Tokens.Plus);
			}
        break;
      case 52: // expr_additive -> expr_additive, Minus, expr_multiplicative
{
				CurrentSemanticValue = new BinaryOperationNode(ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1], Tokens.Minus);
			}
        break;
      case 53: // expr_additive -> expr_multiplicative
{
				CurrentSemanticValue = ValueStack[ValueStack.Depth-1];
			}
        break;
      case 54: // expr_multiplicative -> expr_multiplicative, Multiplies, expr_bit
{
				CurrentSemanticValue = new BinaryOperationNode(ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1], Tokens.Multiplies);
			}
        break;
      case 55: // expr_multiplicative -> expr_multiplicative, Divides, expr_bit
{
				CurrentSemanticValue = new BinaryOperationNode(ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1], Tokens.Divides);
			}
        break;
      case 56: // expr_multiplicative -> expr_bit
{
				CurrentSemanticValue = ValueStack[ValueStack.Depth-1];
			}
        break;
      case 57: // expr_bit -> expr_bit, BitOR, expr_unary
{
				CurrentSemanticValue = new BinaryBitOperationNode(ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1], Tokens.BitOR);
			}
        break;
      case 58: // expr_bit -> expr_bit, BitAND, expr_unary
{
				CurrentSemanticValue = new BinaryBitOperationNode(ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1], Tokens.BitAND);
			}
        break;
      case 60: // expr_unary -> Minus, expr_unary
{
				CurrentSemanticValue = new UnaryOperationNode(ValueStack[ValueStack.Depth-1], Tokens.Minus);
			}
        break;
      case 61: // expr_unary -> BitNEG, expr_unary
{
				CurrentSemanticValue = new UnaryOperationNode(ValueStack[ValueStack.Depth-1], Tokens.BitNEG);
			}
        break;
      case 62: // expr_unary -> LogicalNEG, expr_unary
{
				CurrentSemanticValue = new UnaryOperationNode(ValueStack[ValueStack.Depth-1], Tokens.LogicalNEG);
			}
        break;
      case 63: // expr_unary -> OpenPar, Type, ClosePar, expr_unary
{
				CurrentSemanticValue = new UnaryCastOperationNode(ValueStack[ValueStack.Depth-3], ValueStack[ValueStack.Depth-1]);
			}
        break;
      case 65: // factor -> OpenPar, expr_logical, ClosePar
{
				CurrentSemanticValue = ValueStack[ValueStack.Depth-2];
			}
        break;
      case 66: // factor -> IntNumber
{
				CurrentSemanticValue = ValueStack[ValueStack.Depth-1];
			}
        break;
      case 67: // factor -> RealNumber
{
				CurrentSemanticValue = ValueStack[ValueStack.Depth-1];
			}
        break;
      case 68: // factor -> Bool
{
				CurrentSemanticValue = ValueStack[ValueStack.Depth-1];
			}
        break;
      case 69: // factor -> Ident
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


public Parser(Scanner scanner) : base(scanner) { }
}
}
