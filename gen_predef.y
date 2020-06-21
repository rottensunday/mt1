%namespace GardensPoint
%start program
%partial

%YYSTYPE Node

%token Print Exit Assign Plus Minus Multiplies Divides OpenPar ClosePar Endl Eof Error OpenBracket CloseBracket Program Ident Type RealNumber IntNumber Semicolon Write String Bool If While

%%

program
		: Program OpenBracket bracketstmt CloseBracket
		;

bracketstmt
		: /*empty*/
		| bracketstmt stmt
		;

stmt 
		: Ident Assign expr Semicolon
			{
				MakeAssignNode($1, $3);
			}
		| Type Ident Semicolon
			{
				Declare($2, $1);
			}
		| If OpenPar expr ClosePar 
			{
				MakeIfStmtNode($3);
			}
		  stmt
			{
				PopBracketStatement();
			}
		| While OpenPar expr ClosePar
			{
				MakeWhileStmtNode($3);
			}
		  stmt
			{
				PopBracketStatement();
			}
		| Write String Semicolon
			{
				MakeWriteStringNode($2);
			}
		| Write expr Semicolon
			{
				MakeWriteExpressionNode($2);
			}
		| OpenBracket 
			{
				PushBracketStatement();
			}
		  bracketstmt
			{
			
			}
		  CloseBracket
			{
				PopBracketStatement();
			}
		;

expr
		: expr Plus term
			{
				$$ = new BinaryOperationNode($1, $3, Tokens.Plus);
			}
		| expr Minus term
			{
				$$ = new BinaryOperationNode($1, $3, Tokens.Minus);
			}
		| term
			{
				$$ = $1;
			}
		;

term
		: term Multiplies factor
			{
				$$ = new BinaryOperationNode($1, $3, Tokens.Multiplies);
			}
		| term Divides factor
			{
				$$ = new BinaryOperationNode($1, $3, Tokens.Divides);
			}
		| factor
			{
				$$ = $1;
			}
		;

factor
		: OpenPar expr ClosePar
			{
				$$ = $2;
			}
		| IntNumber
			{
				$$ = $1;
			}
		| RealNumber
			{
				$$ = $1;
			}
		| Bool
			{
				$$ = $1;
			}
		| Ident
			{
				$$ = $1;
			}
		;

%%

int lineno=1;

public Parser(Scanner scanner) : base(scanner) { }