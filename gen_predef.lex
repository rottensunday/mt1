%using QUT.Gppg;
%namespace GardensPoint

IntNumber   [0-9]+
RealNumber  [0-9]+\.[0-9]+
Bool		"true"|"false"
Ident       [a-zA-z][a-zA-Z0-9]*
PrintErr    "print"("@"|"$"|[a-z0-9])[a-z0-9]*
Type		"int"|"double"|"bool"
String		\".*\"

%%

"print"       { return (int)Tokens.Print; }
"exit"        { return (int)Tokens.Exit; }
"program"	  { return (int)Tokens.Program; }
"write"		  { return (int)Tokens.Write; }
"if"		  { return (int)Tokens.If; }
"while"		  { return (int)Tokens.While; }
{Type}		  { yylval = new TypeNode(yytext); return (int)Tokens.Type; }
{IntNumber}   { yylval = new IntNumberNode(yytext); return (int)Tokens.IntNumber; }
{RealNumber}  { yylval = new RealNumberNode(yytext); return (int)Tokens.RealNumber; }
{Bool}		  { yylval = new BoolNode(yytext); return (int)Tokens.Bool; }
{Ident}       { yylval = new IdentifierNode(yytext); return (int)Tokens.Ident; }
{String}	  { yylval = new StringNode(yytext); return (int)Tokens.String; }
"!"			  { return (int)Tokens.LogicalNEG; }
"||"		  { return (int)Tokens.LogicalOR; }
"&&"		  { return (int)Tokens.LogicalAND; }
"~"			  { return (int)Tokens.BitNEG; }
"|"			  { return (int)Tokens.BitOR; }
"&"			  { return (int)Tokens.BitAND; }
"=="		  { return (int)Tokens.RelationEQUALS; }
"!="		  { return (int)Tokens.RelationNOTEQUALS; }
">"			  { return (int)Tokens.RelationGREATER; }
">="		  { return (int)Tokens.RelationGREATEREQUALS; }
"<"			  { return (int)Tokens.RelationLESS; }
"<="		  { return (int)Tokens.RelationLESSEQUALS; }
";"			  { return (int)Tokens.Semicolon; }
"="           { return (int)Tokens.Assign; }
"+"           { return (int)Tokens.Plus; }
"-"           { return (int)Tokens.Minus; }
"*"           { return (int)Tokens.Multiplies; }
"/"           { return (int)Tokens.Divides; }
"("           { return (int)Tokens.OpenPar; }
")"           { return (int)Tokens.ClosePar; }
"{"			  { return (int)Tokens.OpenBracket; }
"}"			  { return (int)Tokens.CloseBracket; }
"\r"          { }
<<EOF>>       { return (int)Tokens.Eof; }
" "           { }
"\t"          { }
{PrintErr}    { return (int)Tokens.Error; }
.             { return (int)Tokens.Error; }