using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QUT.Gppg;

namespace GardensPoint
{
    #region Utility
    public enum Type
    {
        Int,
        Double,
        Bool,
        NotImportant
    }

    public class Error
    {
        public string ErrorMessage { get; set; }
        public int LineNumber { get; set; }
    }

    public static class Utility
    {
        static int val = 0;
        public static Type CastType(string type)
        {
            switch (type)
            {
                case "int":
                    return Type.Int;
                case "double":
                    return Type.Double;
                case "bool":
                    return Type.Bool;
                default:
                    Compiler.AddError("Compiler error - tried to cast invalid type", Compiler.LineNumber);
                    return Type.NotImportant;
            }
        }

        public static string TypeToCILType(Type type)
        {
            switch (type)
            {
                case Type.Int:
                    return "int32";
                case Type.Double:
                    return "float64";
                case Type.Bool:
                    return "bool";
                default:
                    Compiler.AddError("Compiler error - tried to cast NotImportant or undefined type", Compiler.LineNumber);
                    return "notImportant";
            }
        }

        public static string NewTemp()
        {
            return $"L{val++}";
        }
    }
    
    #endregion


    #region Compiler
    class Compiler
    {
        public static StreamWriter sw;
        public static List<string> source;
        public static Stack<List<Node>> StatementsStack { get; set; } = new Stack<List<Node>>();
        public static ProgramTree MainTree { get; set; } = new ProgramTree();
        public static Scanner Scanner { get; set; }
        public static int LineNumber { get; set; } = 0;
        public static List<Error> CompilerErrors = new List<Error>();
        public static Stack<IfElseStmtNode> IfElseStmtNodes = new Stack<IfElseStmtNode>();


        public static int Main(string[] args)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            string file;
            FileStream source;
            if (args.Length >= 1)
                file = args[0];
            else
            {
                Console.Write("\nsource file:  ");
                file = Console.ReadLine();
            }
            try
            {
                var sr = new StreamReader(file);
                string str = sr.ReadToEnd();
                sr.Close();
                Compiler.source = new List<string>(str.Split(new string[] { "\r\n" }, System.StringSplitOptions.None));
                source = new FileStream(file, FileMode.Open);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
                return 1;
            }
            Scanner = new Scanner(source);
            Parser parser = new Parser(Scanner);
            Console.WriteLine();
            sw = new StreamWriter(file + ".il");
            parser.Parse();
            GenProlog();
            GenVariables();
            GenCulture();
            MainTree.GenCode();
            GenEpilog();
            sw.Close();
            source.Close();
            if (CompilerErrors.Count == 0)
                Console.WriteLine("Compilation successful!\n");
            else
            {
                Console.WriteLine($"\n  {CompilerErrors.Count} error(s) detected!\n");
                int i = 1;
                foreach(var error in CompilerErrors)
                {
                    Console.WriteLine($"{i++}. {error.ErrorMessage} in line {error.LineNumber}!");
                }
                File.Delete(file + ".il");
            }
            return CompilerErrors.Count == 0 ? 0 : 1;
        }

        public static void EmitCode(string instr = null)
        {
            sw.WriteLine(instr);
        }

        public static void EmitCode(string instr, params object[] args)
        {
            sw.WriteLine(instr, args);
        }

        public static void AddError(string errorMsg, int lineNum)
        {
            CompilerErrors.Add(new Error()
            {
                ErrorMessage = errorMsg,
                LineNumber = lineNum
            });
        }


        private static void GenProlog()
        {
            EmitCode("// prolog");
            EmitCode(".assembly extern mscorlib { }");
            EmitCode(".assembly calculator { }");
            EmitCode(".method static void main()");
            EmitCode("{");
            EmitCode(".entrypoint");
            EmitCode(".try");
            EmitCode("{");
            EmitCode();
        }

        private static void GenVariables()
        {
            EmitCode("// variables");
            foreach (var keyval in MainTree.VariablesToInitialize)
            {
                EmitCode($".locals init ( {Utility.TypeToCILType(keyval.Value)} '{keyval.Key}' )");
            }
            EmitCode();
        }

        private static void GenCulture()
        {
            EmitCode("call class [mscorlib]System.Threading.Thread [mscorlib]System.Threading.Thread::get_CurrentThread()");
            EmitCode("call class [mscorlib]System.Globalization.CultureInfo [mscorlib]System.Globalization.CultureInfo::get_InvariantCulture()");
            EmitCode("callvirt instance void [mscorlib]System.Threading.Thread::set_CurrentCulture(class [mscorlib]System.Globalization.CultureInfo)");
        }

        private static void GenEpilog()
        {
            EmitCode("// epilog");
            EmitCode("leave EndMain");
            EmitCode("}");
            EmitCode("catch [mscorlib]System.Exception");
            EmitCode("{");
            EmitCode("callvirt instance string [mscorlib]System.Exception::get_Message()");
            EmitCode("call void [mscorlib]System.Console::WriteLine(string)");
            EmitCode("leave EndMain");
            EmitCode("}");
            EmitCode("EndMain: ret");
            EmitCode("}");
            EmitCode();
        }
        
    }
    #endregion

    #region Parser
    public partial class Parser
    {
     
        public void MakeExpressionNode(Node expr)
        {
            Compiler.StatementsStack.First().Add(new ExpressionNode(expr));
        }
        
        public void Declare(Node identifier, Node type)
        {
            IdentifierNode temp = (IdentifierNode)identifier;
            Compiler.MainTree.VariablesToInitialize[temp.Id] = type.Type;
            Compiler.StatementsStack.First().Add(new DeclareStatementNode(((IdentifierNode)identifier).Id, type.Type, identifier.LineNumber));
        }

        public void MakeAssignNode(Node identifierNode, Node expressionNode)
        {
            Compiler.StatementsStack.First().Add(new AssignStatementNode(identifierNode, expressionNode));
        }

        public void MakeWriteExpressionNode(Node expr)
        {
            Compiler.StatementsStack.First().Add(new WriteExpressionNode(expr));
        }

        public void MakeWriteStringNode(Node strnode)
        {
            Compiler.StatementsStack.First().Add(new WriteStringNode(strnode));
        }
        
        public void MakeReadNode(Node identifier)
        {
            Compiler.StatementsStack.First().Add(new ReadNode(identifier));
        }

        public void PushBracketStatement()
        {
            BracketStatementNode temp = new BracketStatementNode();
            Compiler.StatementsStack.First().Add(temp);
            Compiler.StatementsStack.Push(temp.Statements);
        }

        public void PopBracketStatement()
        {
            Compiler.StatementsStack.Pop();
        }

        public void StartIf(Node expr)
        {
            IfElseStmtNode temp = new IfElseStmtNode(expr);
            Compiler.IfElseStmtNodes.Push(temp);
            Compiler.StatementsStack.First().Add(temp);
            Compiler.StatementsStack.Push(temp.IfStatements);
        }

        public void EndIf()
        {
            PopBracketStatement();
        }

        public void EndIfNoElse()
        {
            Compiler.IfElseStmtNodes.Pop();
        }

        public void StartElse()
        {
            Compiler.IfElseStmtNodes.First().IsElseNode = true;
            Compiler.StatementsStack.Push(Compiler.IfElseStmtNodes.First().ElseStatements);
        }

        public void EndElse()
        {
            PopBracketStatement();
            Compiler.IfElseStmtNodes.Pop();
        }

        public void MakeWhileStmtNode(Node expr)
        {
            WhileStmtNode temp = new WhileStmtNode(expr);
            Compiler.StatementsStack.First().Add(temp);
            Compiler.StatementsStack.Push(temp.Statements);
        }
    }
    #endregion
    


    #region Node
    public abstract class Node
    {
        public abstract Type Type { get; set; }
        public abstract void GenCode();
        public abstract int LineNumber { get; set; }
    }

    public class ProgramTree : Node
    {
        public List<Node> Statements { get; set; } = new List<Node>();
        public override Type Type { get; set; }
        public override int LineNumber { get; set; }

        public Dictionary<string, Type> Variables = new Dictionary<string, Type>(); // this dictionary is created while processing tree
        public Dictionary<string, Type> VariablesToInitialize = new Dictionary<string, Type>(); // this dictionary is created before processing the tree, so we can initialize all the variables at the start of program

        public override void GenCode()
        {
            Compiler.EmitCode("// main");
            foreach(var node in Statements)
            {
                node.GenCode();
            }
        }
        public ProgramTree()
        {
            LineNumber = Compiler.LineNumber;
            Compiler.StatementsStack.Push(Statements);
        }
    }

    // this is only necessary for expressions which are not real statements
    public class ExpressionNode : Node
    {
        private Node _expr;
        public override Type Type { get; set; }
        public override int LineNumber { get; set; }

        public override void GenCode()
        {
            _expr.GenCode();
        }

        public ExpressionNode(Node expr)
        {
            _expr = expr;
        }
    }

    // this is "dummy" node made only so we can't use variables before declaration
    public class DeclareStatementNode : Node
    {
        private string _id;
        private Type _type;
        public override Type Type { get; set; }
        public override int LineNumber { get; set; }

        public override void GenCode()
        {
            if (Compiler.MainTree.Variables.ContainsKey(_id))
            {
                Compiler.AddError("Semantic error - identifier already declared", LineNumber);
            }
            Compiler.MainTree.Variables[_id] = _type;
        }

        public DeclareStatementNode(string id, Type type, int lineNum)
        {
            _id = id;
            _type = type;
            LineNumber = lineNum;
        }
    }

    public class AssignStatementNode : Node
    {
        private IdentifierNode _identifierNode;
        private Node _expressionNode;
        public override Type Type { get; set; }
        public override int LineNumber { get; set; }

        public override void GenCode()
        {
            if (!Compiler.MainTree.Variables.ContainsKey(_identifierNode.Id))
            {
                Compiler.AddError("Semantic error - identifier not declared", LineNumber);
            }
            else
            {
                if (_expressionNode.Type == Type.Int && _identifierNode.Type == Type.Double)
                {
                    _expressionNode.GenCode();
                    Compiler.EmitCode("conv.r8");
                    Compiler.EmitCode($"stloc '{_identifierNode.Id}'");
                }
                else if (_expressionNode.Type == _identifierNode.Type)
                {
                    _expressionNode.GenCode();
                    Compiler.EmitCode($"stloc '{_identifierNode.Id}'");
                }
                else
                {
                    Compiler.AddError("Semantic error - assignement arguments should be of the same type, or expression has to be Int and identifier Double", LineNumber);
                }
            }

        }

        public AssignStatementNode(Node identifierNode, Node expressionNode)
        {
            LineNumber = Compiler.LineNumber;
            _identifierNode = (IdentifierNode)identifierNode;
            _expressionNode = expressionNode;
        }

    }

    public class WriteExpressionNode : Node
    {
        private Node _expression;
        public override Type Type { get; set; }
        public override int LineNumber { get; set; }

        public override void GenCode()
        {
            if(_expression.Type != Type.Double)
            {
                _expression.GenCode();
                Compiler.EmitCode($"call void [mscorlib]System.Console::Write({Utility.TypeToCILType(_expression.Type)})");
            }
            else
            {
                Compiler.EmitCode("call class [mscorlib]System.Globalization.CultureInfo[mscorlib] System.Globalization.CultureInfo::get_InvariantCulture()");
                Compiler.EmitCode("ldstr \"{0:0.000000}\"");
                _expression.GenCode();
                Compiler.EmitCode("box [mscorlib]System.Double");
                Compiler.EmitCode("call string[mscorlib] System.String::Format(class [mscorlib] System.IFormatProvider, string, object)");
                Compiler.EmitCode("call void [mscorlib]System.Console::Write(string)");
            }
        }
        public WriteExpressionNode(Node expr)
        {
            LineNumber = Compiler.LineNumber;
            _expression = expr;
        }
    }

    public class WriteStringNode : Node
    {
        private Node _strNode;
        public override Type Type { get; set; }
        public override int LineNumber { get; set; }

        public override void GenCode()
        {
            _strNode.GenCode();
            Compiler.EmitCode("call void [mscorlib]System.Console::Write(string)");
        }
        public WriteStringNode(Node strnode)
        {
            LineNumber = Compiler.LineNumber;
            _strNode = strnode;
        }
    }

    public class ReadNode : Node
    {
        private string _id;
        public override Type Type { get; set; }
        public override int LineNumber { get; set; }

        public override void GenCode()
        {
            if (!Compiler.MainTree.Variables.ContainsKey(_id))
            {
                Compiler.AddError("Semantic error - identifier not declared", LineNumber);
            }
            else
            {
                Type type = Compiler.MainTree.Variables[_id];
                Compiler.EmitCode("call string [mscorlib]System.Console::ReadLine()");
                switch (type)
                {
                    case Type.Int:
                        Compiler.EmitCode($"call {Utility.TypeToCILType(type)} [mscorlib]System.Int32::Parse(string)");
                        break;
                    case Type.Double:
                        Compiler.EmitCode($"call {Utility.TypeToCILType(type)} [mscorlib]System.Double::Parse(string)");
                        break;
                    case Type.Bool:
                        Compiler.EmitCode($"call {Utility.TypeToCILType(type)} [mscorlib]System.Boolean::Parse(string)");
                        break;
                }
                
                Compiler.EmitCode($"stloc '{_id}'");
            }
        }

        public ReadNode(Node identifier)
        {
            LineNumber = Compiler.LineNumber;
            _id = ((IdentifierNode)identifier).Id;
        }
    }

    public class BracketStatementNode : Node
    {
        public override Type Type { get; set; }
        public List<Node> Statements { get; set; } = new List<Node>();
        public override int LineNumber { get; set; }

        public override void GenCode()
        {
            foreach (var node in Statements)
            {
                node.GenCode();
            }
        }

        public BracketStatementNode()
        {
            LineNumber = Compiler.LineNumber;
        }
    }

    public class IfElseStmtNode : Node
    {
        private Node _expression;
        public List<Node> IfStatements { get; set; } = new List<Node>();
        public List<Node> ElseStatements { get; set; } = new List<Node>();
        public bool IsElseNode = false;
        public override Type Type { get; set; }
        public override int LineNumber { get; set; }

        public override void GenCode()
        {
            if (_expression.Type == Type.Bool)
            {
                if (IsElseNode)
                {
                    string labStartElse = Utility.NewTemp();
                    string labEndElse = Utility.NewTemp();
                    _expression.GenCode();
                    Compiler.EmitCode($"brfalse {labStartElse}");
                    foreach (var node in IfStatements)
                    {
                        node.GenCode();
                    }
                    Compiler.EmitCode($"br {labEndElse}");
                    Compiler.EmitCode($"{labStartElse}:");
                    foreach (var node in ElseStatements)
                    {
                        node.GenCode();
                    }
                    Compiler.EmitCode($"{labEndElse}:");
                }
                else
                {
                    string labEnd = Utility.NewTemp();
                    _expression.GenCode();
                    Compiler.EmitCode($"brfalse {labEnd}");
                    foreach (var node in IfStatements)
                    {
                        node.GenCode();
                    }
                    Compiler.EmitCode($"{labEnd}:");
                }
            }
            else
            {
                Compiler.AddError("Semantic error - condition should be of bool type", LineNumber);
            }
        }

        public IfElseStmtNode(Node expression)
        {
            LineNumber = Compiler.LineNumber;
            _expression = expression;
        }
    }

    public class WhileStmtNode : Node
    {
        private Node _expression;
        public List<Node> Statements { get; set; } = new List<Node>();
        public override Type Type { get; set; }
        public override int LineNumber { get; set; }

        public override void GenCode()
        {
            if(_expression.Type == Type.Bool)
            {
                string labEnd = Utility.NewTemp();
                string labStart = Utility.NewTemp();
                Compiler.EmitCode($"{labStart}:");
                _expression.GenCode();
                Compiler.EmitCode($"brfalse {labEnd}");
                foreach (var node in Statements)
                {
                    node.GenCode();
                }
                Compiler.EmitCode($"br {labStart}");
                Compiler.EmitCode($"{labEnd}:");
            }
            else
            {
                Compiler.AddError("Semantic error - condition should be of bool type", LineNumber);
            }
        }

        public WhileStmtNode(Node expr)
        {
            LineNumber = Compiler.LineNumber;
            _expression = expr;
        }
    }

    public class BinaryOperationNode : Node
    {
        private Node _left;
        private Node _right;
        private Tokens _operation;
        public override Type Type { get; set; }
        public override int LineNumber { get; set; }

        private void CastLeftIfNecessary(Node left, Node right)
        {
            if (left.Type == Type.Int && right.Type == Type.Double)
            {
                Compiler.EmitCode("conv.r8");
            }
        }

        private void CastRightIfNecessary(Node left, Node right)
        {
            CastLeftIfNecessary(right, left);
        }

        private bool AreComparable(Node left, Node right)
        {
            return (left.Type != Type.Bool && right.Type != Type.Bool);
        }

        private Type DetermineOperationType(Node left, Node right)
        {
            if (left.Type == Type.Double || right.Type == Type.Double) return Type.Double;
            else return Type.Int;
        }

        public override void GenCode()
        {
            if(AreComparable(_left, _right))
            {
                _left.GenCode();
                CastLeftIfNecessary(_left, _right);
                _right.GenCode();
                CastRightIfNecessary(_left, _right);
                switch (_operation)
                {
                    case Tokens.Plus:
                        Compiler.EmitCode("add");
                        break;
                    case Tokens.Minus:
                        Compiler.EmitCode("sub");
                        break;
                    case Tokens.Multiplies:
                        Compiler.EmitCode("mul");
                        break;
                    case Tokens.Divides:
                        Compiler.EmitCode("div");
                        break;
                    default:
                        Compiler.AddError("Compiler error - unknown binary operation type", LineNumber);
                        break;
                }
            }
            else
            {
                Compiler.AddError("Semantic error - bad types in binary operation - both should be numerical", LineNumber);
            }
            Type = DetermineOperationType(_left, _right);
        }

        public BinaryOperationNode(Node left, Node right, Tokens operation)
        {
            LineNumber = Compiler.LineNumber;
            _left = left;
            _right = right;
            _operation = operation;
            Type = DetermineOperationType(_left, _right);
        }
    }

    public class BinaryLogicalOperationNode : Node
    {
        private Node _left;
        private Node _right;
        private Tokens _operation;
        public override Type Type { get; set; } = Type.Bool;
        public override int LineNumber { get; set; }

        public override void GenCode()
        {
            if(_left.Type == Type.Bool && _right.Type == Type.Bool)
            {
                string labEnd = Utility.NewTemp();
                _left.GenCode();
                Compiler.EmitCode("dup");
                if (_operation == Tokens.LogicalOR)
                {
                    Compiler.EmitCode($"brtrue {labEnd}");
                }
                else if (_operation == Tokens.LogicalAND)
                {
                    Compiler.EmitCode($"brfalse {labEnd}");
                }
                _right.GenCode();
                if (_operation == Tokens.LogicalOR)
                {
                    Compiler.EmitCode("or");
                }
                else if (_operation == Tokens.LogicalAND)
                {
                    Compiler.EmitCode("and");
                }
                Compiler.EmitCode($"{labEnd}:");
            }
            else
            {
                Compiler.AddError("Semantic error - bad types in logical operation - both should be bool", LineNumber);
            }

        }

        public BinaryLogicalOperationNode(Node left, Node right, Tokens operation)
        {
            LineNumber = Compiler.LineNumber;
            _left = left;
            _right = right;
            _operation = operation;
        }
    }

    public class BinaryRelationOperationNode : Node
    {
        private Node _left;
        private Node _right;
        private Tokens _operation;
        public override Type Type { get; set; } = Type.Bool;
        public override int LineNumber { get; set; }

        private void CastLeftIfNecessary(Node left, Node right)
        {
            if (left.Type == Type.Int && right.Type == Type.Double)
            {
                Compiler.EmitCode("conv.r8");
            }
        }

        private void CastRightIfNecessary(Node left, Node right)
        {
            CastLeftIfNecessary(right, left);
        }

        private bool AreComparableWithBool(Node left, Node right)
        {
            return (left.Type != Type.Bool && right.Type != Type.Bool) || (left.Type == Type.Bool && right.Type == Type.Bool);
        }

        private bool AreComparableWithoutBool(Node left, Node right)
        {
            return (left.Type != Type.Bool && right.Type != Type.Bool);
        }

        public override void GenCode()
        {
            switch (_operation)
            {
                case Tokens.RelationEQUALS:
                    if(AreComparableWithBool(_left, _right))
                    {
                        _left.GenCode();
                        CastLeftIfNecessary(_left, _right);
                        _right.GenCode();
                        CastRightIfNecessary(_left, _right);
                        Compiler.EmitCode("ceq");
                    }
                    else
                    {
                        Compiler.AddError("Semantic error - bad types in equals relation (one is bool, other is numerical)", LineNumber);
                    }
                    break;
                case Tokens.RelationNOTEQUALS:
                    if (AreComparableWithBool(_left, _right))
                    {
                        _left.GenCode();
                        CastLeftIfNecessary(_left, _right);
                        _right.GenCode();
                        CastRightIfNecessary(_left, _right);
                        Compiler.EmitCode("ceq");
                        Compiler.EmitCode("ldc.i4 1");
                        Compiler.EmitCode("xor");
                    }
                    else
                    {
                        Compiler.AddError("Semantic error - bad types in not equals relation (one is bool, other is numerical)", LineNumber);
                    }
                    break;
                case Tokens.RelationGREATER:
                    if (AreComparableWithoutBool(_left, _right))
                    {
                        _left.GenCode();
                        CastLeftIfNecessary(_left, _right);
                        _right.GenCode();
                        CastRightIfNecessary(_left, _right);
                        Compiler.EmitCode("cgt");
                    }
                    else
                    {
                        Compiler.AddError("Semantic error - bad types in greater relation (atleast one is bool)", LineNumber);
                    }
                    break;
                case Tokens.RelationLESS:
                    if (AreComparableWithoutBool(_left, _right))
                    {
                        _left.GenCode();
                        CastLeftIfNecessary(_left, _right);
                        _right.GenCode();
                        CastRightIfNecessary(_left, _right);
                        Compiler.EmitCode("clt");
                    }
                    else
                    {
                        Compiler.AddError("Semantic error - bad types in less relation (atleast one is bool)", LineNumber);
                    }
                    break;
                default:
                    Compiler.AddError("Compiler error - unknown relation operation type", LineNumber);
                    break;
            }
        }

        public BinaryRelationOperationNode(Node left, Node right, Tokens operation)
        {
            LineNumber = Compiler.LineNumber;
            _left = left;
            _right = right;
            _operation = operation;
        }
    }

    public class BinaryBitOperationNode : Node
    {
        private Node _left;
        private Node _right;
        private Tokens _operation;
        public override Type Type { get; set; } = Type.Int;
        public override int LineNumber { get; set; }

        public override void GenCode()
        {
            _left.GenCode();
            _right.GenCode();
            if(_left.Type == Type.Int && _right.Type == Type.Int)
            {
                if (_operation == Tokens.BitOR)
                {
                    Compiler.EmitCode("or");
                }
                else if (_operation == Tokens.BitAND)
                {
                    Compiler.EmitCode("and");
                }
                else
                {
                    Compiler.AddError("Compiler error - unknown binary operation type", LineNumber);
                }
            }
            else
            {
                Compiler.AddError("Semantic error - binary bit operations should have integers as arguments", LineNumber);
            }

        }

        public BinaryBitOperationNode(Node left, Node right, Tokens operation)
        {
            LineNumber = Compiler.LineNumber;
            _left = left;
            _right = right;
            _operation = operation;
        }
    }

    public class UnaryOperationNode : Node
    {
        private Node _node;
        private Tokens _operation;
        public override Type Type { get; set; }
        public override int LineNumber { get; set; }

        public override void GenCode()
        {
            _node.GenCode();
            switch (_operation)
            {
                case Tokens.Minus:
                    _node.GenCode();
                    if(_node.Type == Type.Int || _node.Type == Type.Double)
                    {
                        Compiler.EmitCode("neg");
                    }
                    else
                    {
                        Compiler.AddError("Semantic error - cannot negate value other than int or double", LineNumber);
                    }
                    break;
                case Tokens.BitNEG:
                    _node.GenCode();
                    if (_node.Type == Type.Int)
                    {
                        Compiler.EmitCode("not");
                    }
                    else
                    {
                        Compiler.AddError("Semantic error - cannot bitwise negate value other than int", LineNumber);
                    }
                    break;
                case Tokens.LogicalNEG:
                    if (_node.Type == Type.Bool)
                    {
                        Compiler.EmitCode("ldc.i4 1");
                        Compiler.EmitCode("xor");
                    }
                    else
                    {
                        Compiler.AddError("Semantic error - cannot logically negate value other than bool", LineNumber);
                    }
                    break;
                default:
                    Compiler.AddError("Compiler error - unknown unary operation type", LineNumber);
                    break;
            }
            Type = _node.Type;
        }

        public UnaryOperationNode(Node node, Tokens operation)
        {
            LineNumber = Compiler.LineNumber;
            _node = node;
            _operation = operation;
            Type = _node.Type;
        }
    }

    public class UnaryCastOperationNode : Node
    {
        private Node _castType;
        private Node _expr;
        public override Type Type { get; set; }
        public override int LineNumber { get; set; }

        public override void GenCode()
        {
            _expr.GenCode();
            if(_castType.Type == Type.Int)
            {
                switch (_expr.Type)
                {
                    case Type.Int:
                        return;
                    case Type.Bool:
                        return;
                    case Type.Double:
                        Compiler.EmitCode("conv.i4");
                        return;
                    case Type.NotImportant:
                        Compiler.AddError("Compiler error - can't cast object of unknown type", LineNumber);
                        return;
                }
                Type = Type.Int;
            }
            else if (_castType.Type == Type.Double)
            {
                switch (_expr.Type)
                {
                    case Type.Int:
                        Compiler.EmitCode("conv.r8");
                        return;
                    case Type.Bool:
                        Compiler.EmitCode("conv.r8");
                        return;
                    case Type.Double:
                        return;
                    case Type.NotImportant:
                        Compiler.AddError("Compiler error - can't cast object of unknown type", LineNumber);
                        return;
                }
                Type = Type.Double;
            }
            else
            {
                Compiler.AddError("Semantic error - can't cast to bool/unknown type", LineNumber);
            }
        }

        public UnaryCastOperationNode(Node type, Node expr)
        {
            LineNumber = Compiler.LineNumber;
            _castType = type;
            _expr = expr;
            Type = _castType.Type;
        }
    }

    public class IntNumberNode : Node
    {
        private int _val;
        public override Type Type { get; set; } = Type.Int;
        public override int LineNumber { get; set; }

        public override void GenCode()
        {
            Compiler.EmitCode($"ldc.i4 {_val}");
        }
        public IntNumberNode(string v)
        {
            LineNumber = Compiler.LineNumber;
            bool parseSucceeded = int.TryParse(v, out _val);
            if (!parseSucceeded)
            {
                Compiler.AddError("Compiler error - couldn't parse int value", LineNumber);
                _val = 0;
            }
        }

    }

    public class RealNumberNode : Node
    {
        private double _val;
        public override Type Type { get; set; } = Type.Double;
        public override int LineNumber { get; set; }

        public override void GenCode()
        {
            Compiler.EmitCode($"ldc.r8 {_val}");
        }
        public RealNumberNode(string v)
        {
            LineNumber = Compiler.LineNumber;
            bool parseSucceeded = double.TryParse(v, NumberStyles.Any, CultureInfo.InvariantCulture, out _val);
            if (!parseSucceeded)
            {
                Compiler.AddError("Compiler error - couldn't parse double value", LineNumber);
                _val = 0;
            }
        }
    }

    public class IdentifierNode : Node
    {
        public string Id { get; set; }
        public override Type Type { get => DetermineType(); set => Type = value; }
        public override int LineNumber { get; set; }

        public override void GenCode()
        {
            if (!Compiler.MainTree.Variables.ContainsKey(Id))
            {
                Compiler.AddError("Semantic error - identifier not declared", LineNumber);
            }
            else
            {
                Compiler.EmitCode($"ldloc '{Id}'");
            }
        }
        private Type DetermineType()
        {
            Type retVal = Type.NotImportant;
            try
            {
                retVal = Compiler.MainTree.Variables[Id];
            }
            catch (Exception e)
            {
                // identifier will be checked in GenCode or other nodes anyway
            }
            return retVal;
        }
        public IdentifierNode(string s)
        {
            Id = s;
            LineNumber = Compiler.LineNumber;
        }
    }

    public class BoolNode : Node
    {
        private bool _val;
        public override Type Type { get; set; } = Type.Bool;
        public override int LineNumber { get; set; }

        public override void GenCode()
        {
            Compiler.EmitCode($"ldc.i4 {(_val ? 1 : 0)}");
        }
        public BoolNode(string s)
        {
            LineNumber = Compiler.LineNumber;
            bool parseSucceeded = bool.TryParse(s, out _val);
            if (!parseSucceeded)
            {
                Compiler.AddError("Compiler error - couldn't parse boolean value", LineNumber);
                _val = false;
            }
        }
    }

    public class TypeNode : Node
    {
        public override Type Type { get; set; }
        public override int LineNumber { get; set; }

        public override void GenCode()
        {
            Compiler.AddError("Compiler error - GenCode invoked by TypeNode", LineNumber); // we should never generate code for type node
        }
        public TypeNode(string v)
        {
            Type = Utility.CastType(v);
            LineNumber = Compiler.LineNumber;
        }
    }

    public class StringNode : Node
    {
        private string _val;
        public override Type Type { get; set; } = Type.NotImportant;
        public override int LineNumber { get; set; }

        public override void GenCode()
        {
            Compiler.EmitCode($"ldstr {_val}");
        }
        public StringNode(string val)
        {
            _val = val;
            LineNumber = Compiler.LineNumber;
        }
    }
    #endregion
}
