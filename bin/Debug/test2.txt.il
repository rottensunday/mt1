// prolog
.assembly extern mscorlib { }
.assembly calculator { }
.method static void main()
{
.entrypoint
.try
{

// variables
.locals init ( int32 x )

// main
ldc.i4 10
ldc.i4 10
add
ldc.i4 10
ldc.i4 10
add
neg
stloc x
ldloc x
call void [mscorlib]System.Console::Write(int32)
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

