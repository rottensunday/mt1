// prolog
.assembly extern mscorlib { }
.assembly calculator { }
.method static void main()
{
.entrypoint
.try
{

// variables

// main
ldc.i4 0
brfalse L0
ldc.i4 5
call void [mscorlib]System.Console::Write(int32)
L0:
// epilog
leave EndMain
}
catch [mscorlib]System.Exception
{
callvirt instance string [mscorlib]System.Exception::get_Message()
call void [mscorlib]System.Console::WriteLine(string)
leave EndMain
}
EndMain: ret
}

