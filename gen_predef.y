%namespace GardensPoint
%start start
%partial

%YYSTYPE Node

%token Print Exit Assign Plus Minus Multiplies Divides OpenPar ClosePar Endl Error OpenBracket CloseBracket Program Ident Type RealNumber IntNumber Semicolon String Bool
%token RelationEQUALS RelationNOTEQUALS RelationGREATER RelationGREATEREQUALS RelationLESS RelationLESSEQUALS
%token BitOR BitAND BitNEG
%token LogicalOR LogicalAND LogicalNEG
%token If Else While
%token Eof Eol
%token Read Write

%%

start
		: program
		;

program
		: Program OpenBracket bracketstmt 
			{
			
			}
		  CloseBracket
			{
				YYAccept();
			}
		| Program OpenBracket error 
			{
				Compiler.AddError("Syntax error - probably missing closing bracket", Compiler.LineNumber);
				yyerrok();
			}
		| Program error
			{
				Compiler.AddError("Syntax error - probably missing opening bracket", Compiler.LineNumber);
				yyerrok();
			}
		| OpenBracket
			{
				Compiler.AddError("Syntax error - probably missing Program keyword", Compiler.LineNumber);
				yyerrok();
			}
		   bracketstmt CloseBracket
		| error 
			{
				Compiler.AddError("Unknown syntax error or error escape", Compiler.LineNumber);
				YYAbort();
			}
		| error Eof 
			{
				Compiler.AddError("Unknown syntax error or error escape", Compiler.LineNumber);
				YYAbort();
			}
		;

bracketstmt
		: /*empty*/
		| bracketstmt stmt
		| bracketstmt expr_logical Semicolon
			{
				MakeExpressionNode($2);
			}
		| bracketstmt expr_logical error
			{
				Compiler.AddError("Syntax error - missing semicolon in expression", Compiler.LineNumber);
				yyerrok();
			}
		;

stmt 
		: Ident Assign expr_logical Semicolon
			{
				MakeAssignNode($1, $3);
			}

		| Type Ident Semicolon
			{
				Declare($2, $1);
			}
		| Type Ident
			{
				Compiler.AddError("Syntax error - probably missing semicolon", Compiler.LineNumber);
				yyerrok();
			}
		| If OpenPar expr_logical ClosePar 
			{
				StartIf($3);
			}
		  stmt
			{
				EndIf();
			}
		  ifcont
		| If OpenPar error
			{
				Compiler.AddError("Syntax error - probably missing closing parenthesis", Compiler.LineNumber);
				yyerrok();
			}
		| If error
			{
				Compiler.AddError("Syntax error - probably missing opening parenthesis", Compiler.LineNumber);
				yyerrok();
			}
		| While OpenPar expr_logical ClosePar
			{
				MakeWhileStmtNode($3);
			}
		  stmt
			{
				PopBracketStatement();
			}
		| While OpenPar error
			{
				Compiler.AddError("Syntax error - probably missing closing parenthesis", Compiler.LineNumber);
				yyerrok();
			}
		| While error
			{
				Compiler.AddError("Syntax error - probably missing opening parenthesis", Compiler.LineNumber);
				yyerrok();
			}
		| Write String Semicolon
			{
				MakeWriteStringNode($2);
			}
		| Write String error
			{
				Compiler.AddError("Syntax error - probably missing semicolon", Compiler.LineNumber);
				yyerrok();
			}
		| Write expr_logical Semicolon
			{
				MakeWriteExpressionNode($2);
			}
		| Write expr_logical error
			{
				Compiler.AddError("Syntax error - probably missing semicolon", Compiler.LineNumber);
				yyerrok();
			}
		| Read Ident Semicolon
			{
				MakeReadNode($2);
			}
		| Read Ident error
			{
				Compiler.AddError("Syntax error - probably missing semicolon", Compiler.LineNumber);
				yyerrok();
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
		| error
			{
				Compiler.AddError("Syntax error - unknown error", Compiler.LineNumber);
				yyerrok();
			}
		| error Eof
			{
				Compiler.AddError("Syntax error - unexpected end of file", Compiler.LineNumber);
				YYAbort();
			}
		;

ifcont
		: 
			{
				EndIfNoElse();
			}
		| Else
			{
				StartElse();
			}
		  stmt
			{
				EndElse();
			}
		;

expr_logical
		: expr_logical LogicalOR expr_relation
			{
				$$ = new BinaryLogicalOperationNode($1, $3, Tokens.LogicalOR);
			}
		| expr_logical LogicalAND expr_relation
			{
				$$  = new BinaryLogicalOperationNode($1, $3, Tokens.LogicalAND);
			}
		| expr_relation
		;

expr_relation
		: expr_relation RelationEQUALS expr_additive
			{
				$$ = new BinaryRelationOperationNode($1, $3, Tokens.RelationEQUALS);
			}
		| expr_relation RelationNOTEQUALS expr_additive
			{
				$$ = new BinaryRelationOperationNode($1, $3, Tokens.RelationNOTEQUALS);
			}
		| expr_relation RelationGREATER expr_additive
			{
				$$ = new BinaryRelationOperationNode($1, $3, Tokens.RelationGREATER);
			}
		| expr_relation RelationGREATEREQUALS expr_additive
			{
				$$ = new BinaryLogicalOperationNode(new BinaryRelationOperationNode($1, $3, Tokens.RelationGREATER), 
													new BinaryRelationOperationNode($1, $3, Tokens.RelationEQUALS), 
													Tokens.LogicalOR);
			}
		| expr_relation RelationLESS expr_additive
			{
				$$ = new BinaryRelationOperationNode($1, $3, Tokens.RelationLESS);
			}
		| expr_relation RelationLESSEQUALS expr_additive
			{
				$$ = new BinaryLogicalOperationNode(new BinaryRelationOperationNode($1, $3, Tokens.RelationLESS), 
													new BinaryRelationOperationNode($1, $3, Tokens.RelationEQUALS), 
													Tokens.LogicalOR);
			}
		| expr_additive
		;

expr_additive
		: expr_additive Plus expr_multiplicative
			{
				$$ = new BinaryOperationNode($1, $3, Tokens.Plus);
			}
		| expr_additive Minus expr_multiplicative
			{
				$$ = new BinaryOperationNode($1, $3, Tokens.Minus);
			}
		| expr_multiplicative
			{
				$$ = $1;
			}
		;

expr_multiplicative
		: expr_multiplicative Multiplies expr_bit
			{
				$$ = new BinaryOperationNode($1, $3, Tokens.Multiplies);
			}
		| expr_multiplicative Divides expr_bit
			{
				$$ = new BinaryOperationNode($1, $3, Tokens.Divides);
			}
		| expr_bit
			{
				$$ = $1;
			}
		;

expr_bit
		: expr_bit BitOR expr_unary
			{
				$$ = new BinaryBitOperationNode($1, $3, Tokens.BitOR);
			}
		| expr_bit BitAND expr_unary
			{
				$$ = new BinaryBitOperationNode($1, $3, Tokens.BitAND);
			}
		| expr_unary
		;

expr_unary
		: Minus expr_unary
			{
				$$ = new UnaryOperationNode($2, Tokens.Minus);
			}
		| BitNEG expr_unary
			{
				$$ = new UnaryOperationNode($2, Tokens.BitNEG);
			}
		| LogicalNEG expr_unary
			{
				$$ = new UnaryOperationNode($2, Tokens.LogicalNEG);
			}
		| OpenPar Type ClosePar expr_unary
			{
				$$ = new UnaryCastOperationNode($2, $4);
			}
		| factor
		;

factor
		: OpenPar expr_logical ClosePar
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

public Parser(Scanner scanner) : base(scanner) { }