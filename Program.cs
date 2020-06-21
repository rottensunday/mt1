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
        Bool
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
                    throw new Exception("Zły typ");
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
                    throw new Exception("Zły typ");
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
        public static int errors = 0;
        public static Stack<List<Node>> StatementsStack { get; set; } = new Stack<List<Node>>();
        public static ProgramTree MainTree { get; set; } = new ProgramTree();


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
            Scanner scanner = new Scanner(source);
            Parser parser = new Parser(scanner);
            Console.WriteLine();
            sw = new StreamWriter(file + ".il");
            parser.Parse();
            GenProlog();
            GenVariables();
            MainTree.GenCode();
            GenEpilog();
            sw.Close();
            source.Close();
            if (errors == 0)
                Console.WriteLine("  compilation successful\n");
            else
            {
                Console.WriteLine($"\n  {errors} errors detected\n");
                File.Delete(file + ".il");
            }
            return errors == 0 ? 0 : 2;
        }

        public static void EmitCode(string instr = null)
        {
            sw.WriteLine(instr);
        }

        public static void EmitCode(string instr, params object[] args)
        {
            sw.WriteLine(instr, args);
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
            foreach (var keyval in MainTree.Variables)
            {
                EmitCode($".locals init ( {Utility.TypeToCILType(keyval.Value)} {keyval.Key} )");
            }
            EmitCode();
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
        public void PrintHelp()
        {
            Compiler.EmitCode("ldstr \"\\nHELPIK\\n\"");
            Compiler.EmitCode("call void [mscorlib]System.Console::WriteLine(string)");
            Compiler.EmitCode("");
        }

        public void Reset()
        {
            Compiler.EmitCode("ldstr \"\\nRESECIK\\n\"");
            Compiler.EmitCode("call void [mscorlib]System.Console::WriteLine(string)");
            Compiler.EmitCode("");
        }
        
        public void Declare(Node identifier, Node type)
        {
            IdentifierNode identifierCasted = (IdentifierNode) identifier;
            TypeNode typeCasted = (TypeNode) type;
            if (Compiler.MainTree.Variables.ContainsKey(identifierCasted.id)) YYError();
            Compiler.MainTree.Variables[identifierCasted.id] = Utility.CastType(typeCasted.val);
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

        public void MakeIfStmtNode(Node expr)
        {
            IfStmtNode temp = new IfStmtNode(expr);
            Compiler.StatementsStack.First().Add(temp);
            Compiler.StatementsStack.Push(temp.Statements);
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
    }

    public class ProgramTree : Node
    {
        public List<Node> Statements { get; set; } = new List<Node>();
        public override Type Type { get; set; }

        public Dictionary<string, Type> Variables = new Dictionary<string, Type>();

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
            Compiler.StatementsStack.Push(Statements);
        }
    }

    public class AssignStatementNode : Node
    {
        public override Type Type { get; set; }
        public IdentifierNode IdentifierNode { get; set; }
        public Node ExpressionNode { get; set; }

        public override void GenCode()
        {
            ExpressionNode.GenCode();
            Compiler.EmitCode($"stloc {IdentifierNode.id}");

        }

        public AssignStatementNode(Node identifierNode, Node expressionNode)
        {
            IdentifierNode = (IdentifierNode)identifierNode;
            ExpressionNode = expressionNode;
        }

    }

    public class WriteExpressionNode : Node
    {
        public override Type Type { get; set; }
        public Node Expression { get; set; }

        public override void GenCode()
        {
            Expression.GenCode();
            Compiler.EmitCode($"call void [mscorlib]System.Console::Write({Utility.TypeToCILType(Expression.Type)})");
            //Compiler.EmitCode($"call void [mscorlib]System.Console::Write(string.Format(System.Globalization.CultureInfo.InvariantCulture, \"{{0:0.000000}}\"))");
        }
        public WriteExpressionNode(Node expr) => Expression = expr;
    }

    public class WriteStringNode : Node
    {
        public override Type Type { get; set; }
        public StringNode StrNode { get; set; }

        public override void GenCode()
        {
            StrNode.GenCode();
            Compiler.EmitCode("call void [mscorlib]System.Console::Write(string)");
        }
        public WriteStringNode(Node strnode) => StrNode = (StringNode)strnode;
    }

    public class BracketStatementNode : Node
    {
        public override Type Type { get; set; }
        public List<Node> Statements { get; set; } = new List<Node>();

        public override void GenCode()
        {
            foreach (var node in Statements)
            {
                node.GenCode();
            }
        }
    }

    public class IfStmtNode : Node
    {
        public override Type Type { get; set; }
        public List<Node> Statements { get; set; } = new List<Node>();
        public Node Expression { get; set; }
        public override void GenCode()
        {
            string labEnd = Utility.NewTemp();
            Expression.GenCode();
            Compiler.EmitCode($"brfalse {labEnd}");
            foreach (var node in Statements)
            {
                node.GenCode();
            }
            Compiler.EmitCode($"{labEnd}:");

        }
        public IfStmtNode(Node expr)
        {
            Expression = expr;
        }
    }

    public class WhileStmtNode : Node
    {
        public override Type Type { get; set; }
        public List<Node> Statements { get; set; } = new List<Node>();
        public Node Expression { get; set; }

        public override void GenCode()
        {
            string labEnd = Utility.NewTemp();
            string labStart = Utility.NewTemp();
            Compiler.EmitCode($"{labStart}:");
            Expression.GenCode();
            Compiler.EmitCode($"brfalse {labEnd}");
            foreach (var node in Statements)
            {
                node.GenCode();
            }
            Compiler.EmitCode($"br {labStart}");
            Compiler.EmitCode($"{labEnd}:");
        }

        public WhileStmtNode(Node expr)
        {
            Expression = expr;
        }
    }

    public class BinaryOperationNode : Node
    {
        public override Type Type { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Tokens Operation { get; set; }

        public override void GenCode()
        {
            Left.GenCode();
            Right.GenCode();
            switch (Operation)
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
                    Console.WriteLine($"  line:  internal gencode error");
                    ++Compiler.errors;
                    break;
            }

        }

        public BinaryOperationNode(Node left, Node right, Tokens operation)
        {
            Left = left;
            Right = right;
            Operation = operation;
        }
    }

    public class IntNumberNode : Node
    {
        public override Type Type { get; set; } = Type.Int;
        public override void GenCode()
        {
            Compiler.EmitCode($"ldc.i4 {val}");
        }
        public int val;
        public IntNumberNode(string v) { val = int.Parse(v); }

    }

    public class RealNumberNode : Node
    {
        public override Type Type { get; set; } = Type.Double;
        public override void GenCode()
        {
            Compiler.EmitCode($"ldc.r8 {val}");
        }
        public double val;
        public RealNumberNode(string v) { val = double.Parse(v, NumberStyles.Any, CultureInfo.InvariantCulture); }
    }

    public class IdentifierNode : Node
    {
        public override Type Type { get => DetermineType(); set => Type = value; }
        public override void GenCode()
        {
            Compiler.EmitCode($"ldloc {id}");
        }
        public Type DetermineType()
        {
            return Compiler.MainTree.Variables[id];
        }
        public string id;
        public IdentifierNode(string s) { id = s; }
    }

    public class BoolNode : Node
    {
        public override Type Type { get; set; } = Type.Bool;

        public override void GenCode()
        {
            Compiler.EmitCode($"ldc.i4 {(Val ? 1 : 0)}");
        }
        public bool Val { get; set; }
        public BoolNode(string s) => Val = bool.Parse(s);
    }

    public class TypeNode : Node
    {
        public override Type Type { get; set; }
        public override void GenCode()
        {
            
        }
        public string val;
        public TypeNode(string v) { val = v; }
    }

    public class StringNode : Node
    {
        public override Type Type { get; set; }
        public string Val { get; set; }

        public override void GenCode()
        {
            Compiler.EmitCode($"ldstr {Val}");
        }
        public StringNode(string val) => Val = val;
    }
    #endregion
}
