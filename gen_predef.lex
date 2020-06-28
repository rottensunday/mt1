%using QUT.Gppg;
%namespace GardensPoint

IntNumber   0|[1-9][0-9]*
RealNumber  (0|[1-9][0-9]*)\.(0|[1-9][0-9]*)
BadNumber	(0[0-9][0-9]*)|((0[0-9][0-9]*)\.(0[0-9][0-9]*))|((0[0-9][0-9]*)\.([0-9]*))|(([0-9]*)\.(0[0-9][0-9]*))
Bool		"true"|"false"
Ident       [a-zA-z][a-zA-Z0-9]*
Type		"int"|"double"|"bool"
String		\"(([^\n\r\"])|(\\\"))*([^\\])\"
BadString	\".*\"
Comment		"//".*"\r"$
%%

"print"       { Compiler.LineNumber = tokLin; return (int)Tokens.Print; }
"exit"        { Compiler.LineNumber = tokLin; return (int)Tokens.Exit; }
"program"	  { Compiler.LineNumber = tokLin; return (int)Tokens.Program; }
"write"		  { Compiler.LineNumber = tokLin; return (int)Tokens.Write; }
"read"		  { Compiler.LineNumber = tokLin; return (int)Tokens.Read; }
"if"		  { Compiler.LineNumber = tokLin; return (int)Tokens.If; }
"else"		  { Compiler.LineNumber = tokLin; return (int)Tokens.Else; }
"while"		  { Compiler.LineNumber = tokLin; return (int)Tokens.While; }
{Type}		  { Compiler.LineNumber = tokLin; yylval = new TypeNode(yytext); return (int)Tokens.Type; }
{IntNumber}   { Compiler.LineNumber = tokLin; yylval = new IntNumberNode(yytext); return (int)Tokens.IntNumber; }
{RealNumber}  { Compiler.LineNumber = tokLin; yylval = new RealNumberNode(yytext); return (int)Tokens.RealNumber; }
{Bool}		  { Compiler.LineNumber = tokLin; yylval = new BoolNode(yytext); return (int)Tokens.Bool; }
{Ident}       { Compiler.LineNumber = tokLin; yylval = new IdentifierNode(yytext); return (int)Tokens.Ident; }
{String}	  { Compiler.LineNumber = tokLin; yylval = new StringNode(yytext); return (int)Tokens.String; }
{BadString}	  { Compiler.AddError("Lexical error - bad string format", tokLin); }
{BadNumber}   { Compiler.AddError("Lexical error - bad number format", tokLin); }
"!"			  { Compiler.LineNumber = tokLin; return (int)Tokens.LogicalNEG; }
"||"		  { Compiler.LineNumber = tokLin; return (int)Tokens.LogicalOR; }
"&&"		  { Compiler.LineNumber = tokLin; return (int)Tokens.LogicalAND; }
"~"			  { Compiler.LineNumber = tokLin; return (int)Tokens.BitNEG; }
"|"			  { Compiler.LineNumber = tokLin; return (int)Tokens.BitOR; }
"&"			  { Compiler.LineNumber = tokLin; return (int)Tokens.BitAND; }
"=="		  { Compiler.LineNumber = tokLin; return (int)Tokens.RelationEQUALS; }
"!="		  { Compiler.LineNumber = tokLin; return (int)Tokens.RelationNOTEQUALS; }
">"			  { Compiler.LineNumber = tokLin; return (int)Tokens.RelationGREATER; }
">="		  { Compiler.LineNumber = tokLin; return (int)Tokens.RelationGREATEREQUALS; }
"<"			  { Compiler.LineNumber = tokLin; return (int)Tokens.RelationLESS; }
"<="		  { Compiler.LineNumber = tokLin; return (int)Tokens.RelationLESSEQUALS; }
";"			  { Compiler.LineNumber = tokLin; return (int)Tokens.Semicolon; }
"="           { Compiler.LineNumber = tokLin; return (int)Tokens.Assign; }
"+"           { Compiler.LineNumber = tokLin; return (int)Tokens.Plus; }
"-"           { Compiler.LineNumber = tokLin; return (int)Tokens.Minus; }
"*"           { Compiler.LineNumber = tokLin; return (int)Tokens.Multiplies; }
"/"           { Compiler.LineNumber = tokLin; return (int)Tokens.Divides; }
"("           { Compiler.LineNumber = tokLin; return (int)Tokens.OpenPar; }
")"           { Compiler.LineNumber = tokLin; return (int)Tokens.ClosePar; }
"{"			  { Compiler.LineNumber = tokLin; return (int)Tokens.OpenBracket; }
"}"			  { Compiler.LineNumber = tokLin; return (int)Tokens.CloseBracket; }
"\r"          { Compiler.LineNumber = tokLin; }
<<EOF>>       { Compiler.LineNumber = tokLin; return (int)Tokens.Eof; }
" "           { Compiler.LineNumber = tokLin; }
"\t"          { Compiler.LineNumber = tokLin; }
{Comment}	  { Compiler.LineNumber = tokLin; }